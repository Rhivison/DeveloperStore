using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        /// <summary>
        /// Persists a new sale in the repository
        /// </summary>
        Task<Sale> AddAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its unique identifier
        /// </summary>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all sales
        /// </summary>
        Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its sale number
        /// </summary>
        Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all sales from a specific customer
        /// </summary>
        Task<List<Sale>> GetByCustomerAsync(string customer, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing sale (e.g. cancel item or edit)
        /// </summary>
        Task<Sale?> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a sale from the repository
        /// </summary>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}