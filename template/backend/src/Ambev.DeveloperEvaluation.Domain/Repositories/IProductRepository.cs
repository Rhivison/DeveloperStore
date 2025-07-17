using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Creates a new product in the repository
        /// </summary>
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a product by their unique identifier
        /// </summary>
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all products (optionally paginated and ordered)
        /// </summary>
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves products by category (with pagination and ordering)
        /// </summary>
        Task<List<Product>> GetByCategoryAsync(
            string category,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all unique categories
        /// </summary>
        Task<List<string>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing product
        /// </summary>
        Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a product from the repository
        /// </summary>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}