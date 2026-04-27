using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Application.Contracts.Pagination;

namespace Rental_Project_2026.Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginationResponse<T>> ToPagedListAsync<T>(
            this IQueryable<T> query,
            PaginationRequest request,
            CancellationToken cancellationToken = default)
        {
            int totalCount = await query.CountAsync(cancellationToken);

            List<T> items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return PaginationResponse<T>.Create(items, totalCount, request);
        }
    }
}
