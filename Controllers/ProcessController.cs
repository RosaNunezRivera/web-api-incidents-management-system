using AutoMapper;
using BLL;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_IncidentsManagementSystem.Controllers
{
    [Route("api/processes")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        //Declaring a private read-only variable named _processService type IprocessService
        private readonly IProcessService _processService;

        //Define private variable to mapper 
        private readonly IMapper _mapper;

        //Creating process validator 
        private readonly AbstractValidator<Process> _processValidator;

        //Define a constructor for the ProcessService class  
        public ProcessController(IProcessService processService, IMapper mapper, IValidator<Process> processValidator)
        {
            _processService = processService;
            _mapper = mapper;
            _processValidator = (AbstractValidator<Process>)processValidator;
        }

        /// <summary>
        /// Get all process already registered
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProcesses()
        {
            //Creating a list 
            var listProcesses = await _processService.GetProcessesService();

            //Converting a list in format of DTO 
            List<ProcessDTO> processesDto = _mapper.Map<List<ProcessDTO>>(listProcesses);

            return Ok(processesDto);
        }

        /// <summary>
        /// Method to Get Process by processId using (DTO) Data Transfer Object 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProcessById(int id)
        {
            //Storage the list of process by Post ID
            var listProcesses = await _processService.GetProcessByIdService(id);

            //Converting listprocess in format of DTO 
            List<ProcessDTO> processes = _mapper.Map<List<ProcessDTO>>(listProcesses);

            return Ok(processes);
        }

        /// <summary>
        /// Method to add new process using DTO Data Transf Object 
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProcess(ProcessDTO processDto)
        {

            Process process = _mapper.Map<Process>(processDto);

            var validationResult = _processValidator.Validate(process);

            if (validationResult.IsValid)
            {
                await _processService.AddProcessService(processDto);
                return Ok("New Process has been added successfully");
            }

            //Returning error message 
            return BadRequest(validationResult.Errors);

        }

        /// <summary>
        /// Method to update a process using DTO container of data
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProcess(ProcessDTO process)
        {
            //Getting the result from service 
            var result = await _processService.UpdateProcessService(process);

            return Ok(result);
        }

        /// <summary>
        /// Method to detele process using Process ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProcess(int id)
        {
            //Getting the result from service 
            var result = await _processService.DeleteProcessService(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
