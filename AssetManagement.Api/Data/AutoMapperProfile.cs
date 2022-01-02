using AssetManagement.Api.Data.Entities;
using AssetManagement.Api.Models;
using AutoMapper;

namespace AssetManagement.Api.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<AssetDto, Asset>().ReverseMap();
            CreateMap<AssignmentRequestDto, AssetAssignment>().ReverseMap();
            CreateMap<AssignmentResponseDto, AssetAssignment>().ReverseMap();
        }
    }
}

   