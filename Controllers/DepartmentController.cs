using AutoMapper;
using BLL;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_IncidentsManagementSystem.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        //Declaring a private read-only variable named _postService 
        private readonly IDepartmentService _departmentService;

        //Define mapper
        private readonly IMapper _mapper;

        //Define constructor
        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            //Creating a list 
            var listDepartment = await _departmentService.GetDepartmentsService();

            //Converting a list in format of DTO 
            List<DepartmentDTO> departmentDto = _mapper.Map<List<DepartmentDTO>>(listDepartment);

            return Ok(departmentDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            //Getting the Department by id
            var department = await _departmentService.GetDepartmentByIdService(id);

            if (department == null)
            {
                return BadRequest("Department not found");
            }

            //Convert Department object in Dto
            DepartmentDTO departmentDto = _mapper.Map<DepartmentDTO>(department);
            return Ok(departmentDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentDTO departmentDto)
        {
            var result = await _departmentService.AddDepartmentService(departmentDto);

            if (result.StartsWith("Validation failed:"))
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        /// <summary>
        /// MEthod to tupdate a Department
        /// </summary>
        /// <param name="departmentDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(DepartmentDTO departmentDto)
        {
            var result = await _departmentService.UpdateDepartmentService(departmentDto);

            if (result.StartsWith("Validation failed:"))
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        /// <summary>
        /// Method to delete a Department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _departmentService.DeleteDepartmentService(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
