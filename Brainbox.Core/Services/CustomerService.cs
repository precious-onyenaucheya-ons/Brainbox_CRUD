using Brainbox.Core.DTO;
using Brainbox.Core.Interfaces;
using Brainbox.Domain.enums;
using Brainbox.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Brainbox.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly UserManager<Customer> _userManager;
        public CustomerService(UserManager<Customer> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseDTO<bool>> Register(RegisterDTO model)
        {
            var response = new ResponseDTO<bool>();

            var checkEmail = await _userManager.FindByEmailAsync(model.Email);

            if (checkEmail == null)
            {

                Customer user = new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = model.Email,
                    FullName = model.FullName,
                    UserName = model.Email
                };


                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    var res = await _userManager.AddToRoleAsync(user, UserRoles.Customer.ToString());
                    if (res.Succeeded)
                    {
                        response.Data = true;
                        response.Message = "User Creation Successfull";
                        response.StatusCode = (int)HttpStatusCode.OK;

                        return response;
                    }
                }
                response.Message = "User Creation Unsuccessfull";
                response.StatusCode = (int)HttpStatusCode.BadRequest;

                return response;

            }
            response.Message = $"The Email {checkEmail.Email} already exist";
            response.StatusCode = (int)HttpStatusCode.Forbidden;

            return response;
        }

        public async Task<ResponseDTO<LoginResponseDTO>> Login(LoginDTO model)
        {
            var response = new ResponseDTO<LoginResponseDTO>();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                response.Message = "Invalid User";
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Data = null;
                return response;
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                response.Message = "Invalid Password";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Data = null;
                return response;
            }
            var roles = await _userManager.GetRolesAsync(user);


            LoginResponseDTO result = new LoginResponseDTO()
            {
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName
            };

            response.Message = "Login Successfull";
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = result;
            return response;



        }
    }
}
