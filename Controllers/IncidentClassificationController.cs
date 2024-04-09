using AutoMapper;
using BLL;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_IncidentsManagementSystem.Controllers
{
    [Route("api/incidentClassifications")]
    [ApiController]
    public class IncidentClassificationController : ControllerBase
    {
        //Declaring a private read-only variable named _IncidentClassificationService 
        private readonly IIncidentClassificationsService _incidentClassificationService;

        //Define mapper
        private readonly IMapper _mapper;

        //Define constructor
        public IncidentClassificationController(IIncidentClassificationsService incidentClassificationService, IMapper mapper)
        {
            _incidentClassificationService = incidentClassificationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetIncidentClassifications()
        {
            //Creating a list 
            var listIncidentClassifications = await _incidentClassificationService.GetIncidentClassificationService();

            //Converting a list in format of DTO 
            List<IncidentClassificationDTO> IncidentClassificationDto = _mapper.Map<List<IncidentClassificationDTO>>(listIncidentClassifications);

            return Ok(IncidentClassificationDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetIncidentClassificationById(int id)
        {
            //Getting the IncidentClassification by id
            IncidentClassification incidentClassification = await _incidentClassificationService.GetIncidentClassificationByIdService(id);

            if (incidentClassification == null)
            {
                return NotFound();
            }

            //Convert IncidentClassification object in Dto

            IncidentClassificationDTO incidentClassificationDto = _mapper.Map<IncidentClassificationDTO>(incidentClassification);
            return Ok(incidentClassificationDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddIncidentClassification(IncidentClassificationDTO incidentClassificationDto)
        {
            var result = await _incidentClassificationService.AddIncidentClassificationService(incidentClassificationDto);
            return Ok(result);
        }

        /// <summary>
        /// MEthod to tupdate a IncidentClassification
        /// </summary>
        /// <param name="incidentClassificationDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateIncidentClassification(IncidentClassificationDTO incidentClassificationDto)
        {
            var result = await _incidentClassificationService.UpdateIncidentClassificationService(incidentClassificationDto);
            return Ok(result);
        }

        /// <summary>
        /// Method to delete a IncidentClassification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteIncidentClassification(int id)
        {
            var result = await _incidentClassificationService.DeleteIncidentClassificationService(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
