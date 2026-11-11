using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTshirt.Application.Abstractions
{
    public interface ITshirtService
    {
        Task<TshirtResponse> CreateAsync(
         TshirtRequest request,
         CancellationToken cancellationToken = default);

        Task<List<TshirtResponse>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<TshirtResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<TshirtResponse> UpdateAsync(
            Guid id,
            TshirtRequest request,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default);
    }
}
