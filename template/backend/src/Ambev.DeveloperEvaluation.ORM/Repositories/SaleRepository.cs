using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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
        /// Retrieves a sale by its sale number
        /// </summary>
        /// <param name="saleNumber">The sale number</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale if found, null otherwise</returns>
        public async Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
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
        /// Retrieves sales by customer name
        /// </summary>
        /// <param name="customer">The customer name</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of sales for the customer</returns>
        public async Task<List<Sale>> GetByCustomerAsync(string customer, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .Where(s => s.Customer.ToLower() == customer.ToLower())
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
            var existingSale = await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == sale.Id, cancellationToken);

            if (existingSale == null)
                return null;

            // Update sale scalar properties
            _context.Entry(existingSale).CurrentValues.SetValues(sale);

            // Remove items no longer present
            foreach (var existingItem in existingSale.Items.ToList())
            {
                if (!sale.Items.Any(i => i.Id == existingItem.Id))
                    _context.Entry(existingItem).State = EntityState.Deleted;
            }

            foreach (var item in sale.Items)
            {
                var existingItem = existingSale.Items.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(item);
                }
                else
                {
                    existingSale.AddItem(item);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return existingSale;
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