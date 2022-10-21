using Brainbox.Core.DTO;
using Brainbox.Domain.Models;

namespace Brainbox.Core.Interfaces
{
    public interface IProductService
    {
        Task<ResponseDTO<bool>> AddProductAsync(ProductDTO product);
        Task<ResponseDTO<bool>> DeleteProductAsync(int id);
        Task<ResponseDTO<ProductDTO>> GetProductByIdAsync(int id);
        ResponseDTO<PaginationResult<IEnumerable<ProductDTO>>> GetProductsByPaginationAsync(int pageSize, int pageNumber);
        Task<ResponseDTO<bool>> UpdateProductAsync(int id, ProductDTO product);
    }
}