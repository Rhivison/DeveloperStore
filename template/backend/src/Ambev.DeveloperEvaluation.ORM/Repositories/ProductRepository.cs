using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{   
    /// <summary>
    /// Implementation of IProductRepository using Entity Framework Core
    /// </summary>
    public class ProductRepository: IProductRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of UserRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Product in the database
        /// </summary>
        /// <param name="product">The user to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created user</returns>
        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
             _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        /// <summary>
        /// Retrieves a product by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The user if found, null otherwise</returns>
        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
        }
        
        /// <summary>
        /// Retrieves all products on Database
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of products in database</returns>
        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Deletes a product from the database
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the product was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await GetByIdAsync(id, cancellationToken);
            if (product == null)
            return false;

            _context.Products.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}