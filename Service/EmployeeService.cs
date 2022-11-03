using AutoMapper;
using Core.DTO;
using Core.Entity;
using Infrastructure.Tools;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IEmployeeService
    {
        Task<ResponseHelper<EmployeeDto>> Get(Guid id);
        Task<ResponseHelper<List<EmployeeDto>>> Get(EmployeeFilterDto? employeeFilter);
        Task<ResponseHelper<EmployeeDto>> Create(EmployeeCreateDto employee);
        Task<ResponseHelper> Masive(List<EmployeeCreateDto> employeesDto);
        Task<ResponseHelper> Update(Guid id, EmployeeCreateDto employee);
        Task<bool> Delete(Guid id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseHelper<EmployeeDto>> Get(Guid id)
        {
            ResponseHelper<EmployeeDto> response = new ResponseHelper<EmployeeDto>();
            try
            {
                EmployeeDto employee = _mapper.Map<EmployeeDto>(await _unitOfWork.Employee.Get(id));
                response.IsSuccess = true;
                if (employee == null)
                    return response;

                response.Result = employee;
                response.IsSuccess = true;

                return response;
            }
            catch (Exception e)
            {
                response.Message = "Unknown error";
                Console.WriteLine(e.ToString());

                return response;
            }
        }

        public async Task<ResponseHelper<List<EmployeeDto>>> Get(EmployeeFilterDto? employeeFilter)
        {
            ResponseHelper<List<EmployeeDto>> response = new ResponseHelper<List<EmployeeDto>>();
            try
            {
                var data = await _unitOfWork.Employee.GetAll(employeeFilter);

                var employees = _mapper.Map<List<EmployeeDto>>(data);

                response.IsSuccess = true;
                response.Result = employees;

                return response;
            }
            catch (Exception e)
            {
                response = new ResponseHelper<List<EmployeeDto>>();
                response.Message = "Unknown error";
                Console.WriteLine(e.ToString());

                return response;
            }
        }

        public async Task<ResponseHelper<EmployeeDto>> Create(EmployeeCreateDto employeeDto)
        {
            ResponseHelper<EmployeeDto> response = new ResponseHelper<EmployeeDto>();
            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);

                var employeeResult = await _unitOfWork.Employee.Add(employee);
                await _unitOfWork.Save();

                response.Result = _mapper.Map<EmployeeDto>(employeeResult);
                response.IsSuccess = true;

                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                response.Message = "Unknown error";

                return response;
            }
        }

        public async Task<ResponseHelper> Masive(List<EmployeeCreateDto> employeesDto)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var employees = _mapper.Map<List<Employee>>(employeesDto);

                _unitOfWork.Employee.AddRange(employees);
                await _unitOfWork.Save();

                response.IsSuccess = true;
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                response.Message = "Unknown error";
                return response;
            }
        }


        public async Task<ResponseHelper> Update(Guid id, EmployeeCreateDto employeeDto)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var oldEmployee = await _unitOfWork.Employee.Get(id);
                if (oldEmployee == null)
                    return response;

                var employee = _mapper.Map<EmployeeCreateDto, Employee>(employeeDto, oldEmployee);
                _unitOfWork.Employee.Update(employee);

                await _unitOfWork.Save();
                response.IsSuccess = true;

                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                response.Message = "Unknown error";

                return response;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var employee = await _unitOfWork.Employee.Get(id);
                if (employee == null)
                    return false;
                var result = await _unitOfWork.Employee.Delete(id);
                await _unitOfWork.Save();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return false;
            }
        }
    }
}
