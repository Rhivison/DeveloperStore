using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Ambev.DeveloperEvaluation.ORM.Repositories
{   
    /// <summary>
    /// Implementation of ISaleRepository using Entity Framework Core
    /// </summary>
    public class SaleRepository: ISaleRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of SaleRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Sale in the database
        /// </summary>
        /// <param name="sale">The sale to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale</returns>
        public async Task<Sale> AddAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        /// <summary>
        /// Retrieves a sale by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale if found, null otherwise</returns>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves all sales from the database
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of sales in database</returns>
        public async Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Updates a sale in the database
        /// </summary>
        /// <param name="sale">The sale to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sale</returns>
        /// <summary>
        /// Updates a sale and its items in the database
        /// </summary>
        /// <param name="sale">The sale to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sale or null if not found</returns>
        public async Task<Sale?> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {   
            const int maxRetries = 3;
            
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    var existingSale = await _context.Sales
                        .Include(s => s.Items)
                        .FirstOrDefaultAsync(s => s.Id == sale.Id, cancellationToken);
                        
                    if (existingSale == null)
                        return null;

                    existingSale.SaleDate = sale.SaleDate;
                    existingSale.UserId = sale.UserId;
                    existingSale.Customer = sale.Customer;
                    existingSale.Branch = sale.Branch;
                    existingSale.Cancelled = sale.Cancelled;
                    existingSale.TotalAmount = sale.TotalAmount;


                    var updatedItemIds = sale.Items.Select(i => i.Id).ToHashSet();
                    
                    var itemsToCancel = existingSale.Items.Where(i => !updatedItemIds.Contains(i.Id) && !i.Cancelled).ToList();
                    Console.WriteLine($"=== ITEMS TO CANCEL: {itemsToCancel.Count} ===");
                    foreach (var itemToCancel in itemsToCancel)
                    {
                        itemToCancel.Cancelled = true;
                    }

                    var newItems = new List<SaleItem>();
                    var itemsToUpdate = new List<SaleItem>();

                    foreach (var item in sale.Items)
                    {
                        var existingItem = existingSale.Items.FirstOrDefault(i => i.Id == item.Id);
                        
                        
                        if (existingItem != null)
                        {
                            var currentItemXmin = (uint)_context.Entry(existingItem).Property("xmin").CurrentValue;
                            
                            if (item.xmin > 0 && item.xmin != currentItemXmin)
                            {
                                var entry = _context.Entry(existingItem);
                                    throw new DbUpdateConcurrencyException(
                                        "Item was modified by another process");
                            }
                            
                            existingItem.Quantity = item.Quantity;
                            existingItem.ProductName = item.ProductName;
                            existingItem.UnitPrice = item.UnitPrice;
                            existingItem.Discount = item.Discount;
                            existingItem.TotalAmount = item.TotalAmount;
                            existingItem.Cancelled = item.Cancelled;
                            
                            itemsToUpdate.Add(existingItem);
                        }
                        else
                        {
                            
                            var newItem = new SaleItem(
                                item.ProductId,
                                item.ProductName,
                                item.Quantity,
                                item.UnitPrice,
                                item.Discount,
                                item.TotalAmount)
                            {
                                SaleId = existingSale.Id,
                                Cancelled = item.Cancelled
                            };
                            
                            newItems.Add(newItem);
                        }
                    }

                    if (newItems.Any())
                    {
                        await _context.SaleItems.AddRangeAsync(newItems, cancellationToken);
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                    
                    return existingSale;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    
                    if (attempt == maxRetries)
                    {
                        throw new InvalidOperationException($"The sale was modified by another process multiple times. Please refresh and try again. Original error: {ex.Message}", ex);
                    }
                    
                    _context.ChangeTracker.Clear();
                    
                    var delay = TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt - 1));
                    await Task.Delay(delay, cancellationToken);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            
            throw new InvalidOperationException("Unexpected end of retry loop");
        }
        
        /// <summary>
        /// Deletes a sale and its related items from the database
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the sale was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await _context.Sales
                .Include(s => s.Items)  // para garantir que itens estejam carregados, se for remover manualmente
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (sale == null)
                return false;

            // Se o relacionamento estiver configurado com cascade delete, só isso basta:
            _context.Sales.Remove(sale);

            // Caso contrário, remova os itens manualmente antes:
            // _context.SaleItems.RemoveRange(sale.Items);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    
    }
}