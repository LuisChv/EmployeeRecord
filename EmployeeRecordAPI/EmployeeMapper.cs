using AutoMapper;
using Core.DTO;
using Core.Entity;

namespace EmployeeRecordAPI
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            /* ---------------------------------- Employee ---------------------------------- */
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<EmployeeCreateDto, Employee>();
        }
    }
}
