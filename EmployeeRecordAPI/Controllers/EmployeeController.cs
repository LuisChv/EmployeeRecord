using Core.DTO;
using Infrastructure.Tools;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace EmployeeRecordAPI.Controllers;

[Route("api/employee/v{version:apiVersion}")]
[ApiController]
[ApiVersion("1.0")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IConfiguration _configuration;

    public EmployeeController(IEmployeeService employeeService, IConfiguration configuration)
    {
        _employeeService = employeeService;
        _configuration = configuration;
    }

    [HttpGet("{id}", Name = "GetEmployeeById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(Guid id)
    {
        ResponseHelper<EmployeeDto> response = await _employeeService.Get(id);

        if (response.IsSuccess)
            return response.Result != null ? Ok(response.Result) : NotFound();

        return BadRequest(response.Message);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] EmployeeFilterDto? employeeFilter)
    {
        ResponseHelper<List<EmployeeDto>> response = await _employeeService.Get(employeeFilter);

        return response.IsSuccess ? Ok(response.Result) : BadRequest(response.Message);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(EmployeeCreateDto employee)
    {

        ResponseHelper<EmployeeDto> response = await _employeeService.Create(employee);

        return response.IsSuccess ?
            CreatedAtAction(nameof(Get), new { id = response.Result?.EmployeeId }, response.Result) :
            BadRequest(response.Message);

    }

    [HttpPost("masive")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Masive(List<EmployeeCreateDto> employees)
    {
        ResponseHelper response = await _employeeService.Masive(employees);

        return response.IsSuccess ? NoContent() : BadRequest(response.Message);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(Guid id, EmployeeCreateDto employeeDto)
    {
        ResponseHelper<EmployeeDto> getResponse = await _employeeService.Get(id);
        if (getResponse.Result == null)
            return NotFound();

        ResponseHelper updateResponse = await _employeeService.Update(id, employeeDto);

        return updateResponse.IsSuccess ? NoContent() : BadRequest(updateResponse.Message);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        ResponseHelper<EmployeeDto> getResponse = await _employeeService.Get(id);
        if (getResponse.Result == null)
            return NotFound();

        ResponseHelper deleteResponse = new ResponseHelper();
        deleteResponse.IsSuccess = await _employeeService.Delete(id);

        return deleteResponse.IsSuccess ? NoContent() : BadRequest(deleteResponse.Message);
    }
}
