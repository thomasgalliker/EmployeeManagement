using AutoMapper;

using Employee.Mapping.Abstraction;
using Employee.Service.Contracts.DataContracts;

namespace Employee.Mapping
{
    internal class EmployeeMappingProfile : Profile,
                                            IMapper<Model.Employee, EmployeeDto>, 
                                            IMapper<EmployeeDto, Model.Employee>
    {
        public EmployeeMappingProfile()
        {
            Mapper.CreateMap<Model.Employee, EmployeeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

            Mapper.CreateMap<EmployeeDto, Model.Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.Id))
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());
        }

        public EmployeeDto Map(Model.Employee src)
        {
            return Mapper.Map<EmployeeDto>(src);
        }

        public Model.Employee Map(EmployeeDto src)
        {
            return Mapper.Map<Model.Employee>(src);
        }
    }
}