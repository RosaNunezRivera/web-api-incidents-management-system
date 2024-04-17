using AutoMapper;
using BLL;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_IncidentsManagementSystem.Controllers
{
    [Route("api/incidents")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        //Declaring a private read-only variable named _IncidentService 
        private readonly IIncidentService _incidentService;

        //Define mapper
        private readonly IMapper _mapper;


        //Define constructor
        public IncidentController(IIncidentService incidentService, IMapper mapper)
        {
            _incidentService = incidentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetIncidents()
        {
            //Creating a list 
            var listIncidents = await _incidentService.GetIncidentService();

            //Converting a list in format of DTO 
            List<IncidentDTO> incidentDto = _mapper.Map<List<IncidentDTO>>(listIncidents);

            return Ok(incidentDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetIncidentById(int id)
        {
            //Getting the Incident by id
            var incident = await _incidentService.GetIncidentByIdService(id);

            if (incident == null)
            {
                return BadRequest("Incident not found");
            }

            //Convert Incident object in Dto
            IncidentDTO incidentDto = _mapper.Map<IncidentDTO>(incident);
            return Ok(incidentDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddIncident(IncidentDTO incidentDto)
        {
            var result = await _incidentService.AddIncidentService(incidentDto);

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
        /// MEthod to tupdate a Incident
        /// </summary>
        /// <param name="incidentDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateIncident(IncidentDTO incidentDto)
        {
            var result = await _incidentService.UpdateIncidentService(incidentDto);

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
        /// Method to delete a Incident
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteIncident(int id)
        {
            var result = await _incidentService.DeleteIncidentService(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
