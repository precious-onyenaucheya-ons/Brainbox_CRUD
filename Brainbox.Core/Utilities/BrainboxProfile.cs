using AutoMapper;
using Brainbox.Core.DTO;
using Brainbox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainbox.Core.Utilities
{
    public class BrainboxProfile :Profile
    {
        public BrainboxProfile()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<RegisterDTO, Customer>()
                .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email.ToLower())); 
        }
    }
}
