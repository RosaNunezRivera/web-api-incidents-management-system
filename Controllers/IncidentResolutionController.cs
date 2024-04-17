using AutoMapper;
using BLL;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_IncidentsManagementSystem.Controllers
{
    [Route("api/incidentresolutions")]
    [ApiController]
    public class IncidentResolutionController : ControllerBase
    {
        //Declaring a private read-only variable named _IncidenteResolutionService 
        private readonly IIncidentResolutionService _incidentResolutionService;

        //Define mapper
        private readonly IMapper _mapper;


        //Define constructor
        public IncidentResolutionController(IIncidentResolutionService incidentResolutionService, IMapper mapper)
        {
            _incidentResolutionService = incidentResolutionService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetIncidenteResolutions()
        {
            //Creating a list 
            var listIncidenteResolutions = await _incidentResolutionService.GetIncidentResolutionService();

            //Converting a list in format of DTO 
            List<IncidentResolutionDTO> incidenteResolutionDto = _mapper.Map<List<IncidentResolutionDTO>>(listIncidenteResolutions);

            return Ok(incidenteResolutionDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetIncidenteResolutionById(int id)
        {
            //Getting the IncidenteResolution by id
            IncidentResolution incidenteResolution = await _incidentResolutionService.GetIncidentResolutionByIdService(id);

            if (incidenteResolution == null)
            {
                return BadRequest("IncidenteResolution not found");
            }

            //Convert IncidenteResolution object in Dto
            IncidentResolutionDTO incidenteResolutionDto = _mapper.Map<IncidentResolutionDTO>(incidenteResolution);
            return Ok(incidenteResolutionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddIncidenteResolution(IncidentResolutionDTO incidentResolutionDto)
        {
            var result = await _incidentResolutionService.AddIncidentResolutionService(incidentResolutionDto);

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
        /// MEthod to tupdate a IncidenteResolution
        /// </summary>
        /// <param name="IncidenteResolutionDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateIncidenteResolution(IncidentResolutionDTO incidentResolutionDto)
        {
            var result = await _incidentResolutionService.AddIncidentResolutionService(incidentResolutionDto);

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
        /// Method to delete a IncidenteResolution
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteIncidenteResolution(int id)
        {
            var result = await _incidentResolutionService.DeleteIncidentResolutionService(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
