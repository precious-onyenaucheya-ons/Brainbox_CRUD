using AutoMapper;
using Brainbox.Core.DTO;
using Brainbox.Core.Interfaces;
using Brainbox.Core.Utilities;
using Brainbox.Domain.Models;
using Brainbox.Infrastructure;
using System.Net;

namespace Brainbox.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repo;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public ResponseDTO<PaginationResult<IEnumerable<ProductDTO>>> GetProductsByPaginationAsync(int pageSize, int pageNumber)
        {
            var responseDto = new ResponseDTO<PaginationResult<IEnumerable<ProductDTO>>>();
            try
            {
                var exec =  _repo.GetAll();
                var response = Paginator.PaginationAsync<Product, ProductDTO>(exec, pageSize, pageNumber, _mapper);
                responseDto.Data = response;
                responseDto.StatusCode = response.PageItems != null ? (int)HttpStatusCode.Accepted : (int)HttpStatusCode.NoContent;
                responseDto.Status = true;
                responseDto.Message = response.PageItems != null ? "Resquest is Successfull" : "the database is Empty";
                return responseDto;
            }
            catch (Exception Ex)
            {
                responseDto.Data = null;
                responseDto.StatusCode = (int)HttpStatusCode.BadRequest;
                responseDto.Status = false;
                responseDto.Message = "Resquest was unSuccessfull";
                responseDto.Error = new List<ErrorItem>();
                responseDto.Error.Add(new ErrorItem() { InnerException = Ex.Message });
                return responseDto;
            }
        }

        public async Task<ResponseDTO<ProductDTO>> GetProductByIdAsync(int id)
        {
            var responseDto = new ResponseDTO<ProductDTO>();
            try
            {                
                var response = await _repo.GetByIdAsync(id);
                var productDto = _mapper.Map<ProductDTO>(response);
                responseDto.Data = productDto;
                responseDto.StatusCode = response != null ? (int)HttpStatusCode.Accepted : (int)HttpStatusCode.NoContent;
                responseDto.Status = response != null ? true : false;
                responseDto.Message = response != null ? "Resquest is Successfull" : "Instance doesn't exist in the Entity";
                return responseDto;
            }
            catch (Exception Ex)
            {
                //throw Ex.Message;
                responseDto.Data = null;
                responseDto.StatusCode = (int)HttpStatusCode.BadRequest;
                responseDto.Status = false;
                responseDto.Message = "Resquest was unSuccessfull";
                responseDto.Error = new List<ErrorItem>();
                responseDto.Error.Add(new ErrorItem() { InnerException = Ex.Message });
                return responseDto;
            }
        }
        
        public async Task<ResponseDTO<bool>> AddProductAsync(ProductDTO product)
        {
            var response = new ResponseDTO<bool>();
            try
            {
                var productEntity = _mapper.Map<Product>(product);
                var allProducts = await _repo.GetAllAsync();
                if (!allProducts.Any(x => x.Name.Equals(productEntity.Name)))
                {
                    await _repo.AddAsync(productEntity);
                    response.Status = true;
                    response.StatusCode = (int)HttpStatusCode.Created;
                    response.Data = true;
                    response.Message = "Product was added successfully";
                    return response;
                }
                else
                {
                    response.Status = false;
                    response.StatusCode = (int)HttpStatusCode.NotModified;
                    response.Data = false;
                    response.Message = "Product already exists";
                    return response;                    
                }
                
            }
            catch (Exception Ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Data = false;
                response.Message = "Product was not added successfully";
                response.Error = new List<ErrorItem>();
                response.Error.Add(new ErrorItem() { InnerException = Ex.Message });
                return response;
            }
        }

        public async Task<ResponseDTO<bool>> UpdateProductAsync(int id, ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var response = new ResponseDTO<bool>();
            try
            {
                await _repo.UpdateAsync(id, product);
                response.Status = true;
                response.StatusCode = (int)HttpStatusCode.Accepted;
                response.Data = true;
                response.Message = "Product was update successfully";
                return response;
            }
            catch (Exception Ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Data = false;
                response.Message = "Product Update failed, Try again later.";
                response.Error = new List<ErrorItem>();
                response.Error.Add(new ErrorItem() { InnerException = Ex.Message });
                return response;
            }
        }

        public async Task<ResponseDTO<bool>> DeleteProductAsync(int id)
        {
            var response = new ResponseDTO<bool>();
            try
            {
                await _repo.DeleteAsync(id);
                response.Status = true;
                response.StatusCode = (int)HttpStatusCode.Accepted;
                response.Data = true;
                response.Message = "Product was deleted successfully";
                return response;
            }
            catch (Exception Ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Data = false;
                response.Message = "Product could not delete, Try again later.";
                response.Error = new List<ErrorItem>();
                response.Error.Add(new ErrorItem() { InnerException = Ex.Message });
                return response;
            }
        }

    }
}
