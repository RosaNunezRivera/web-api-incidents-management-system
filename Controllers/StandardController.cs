using AutoMapper;
using BLL;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_IncidentsManagementSystem.Controllers
{
    [Route("api/standards")]
    [ApiController]
    public class StandardController : ControllerBase
    {
        //Declaring a private read-only variable named _StandardService 
        private readonly IStandardService _standardService;

        //Define mapper
        private readonly IMapper _mapper;

        //Define constructor
        public StandardController(IStandardService standardService, IMapper mapper)
        {
            _standardService = standardService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStandards()
        {
            //Creating a list 
            var listStandards = await _standardService.GetStandardService();

            //Converting a list in format of DTO 
            List<StandardDTO> StandardDtos = _mapper.Map<List<StandardDTO>>(listStandards);

            return Ok(StandardDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStandardById(int id)
        {
            //Getting the Standard by id
            var standard = await _standardService.GetStandardByIdService(id);

            if (standard == null)
            {
                return BadRequest("Standard not found");
            }

            //Convert Standard object in Dto
            StandardDTO standardDto = _mapper.Map<StandardDTO>(standard);
            return Ok(standardDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddStandard(StandardDTO standardDto)
        {
            var result = await _standardService.AddStandardService(standardDto);

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
        /// MEthod to tupdate a Standard
        /// </summary>
        /// <param name="standardDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateStandard(StandardDTO standardDto)
        {
           var result = await _standardService.UpdateStandardService(standardDto);

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
        /// Method to delete a Standard
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStandard(int id)
        {
            var result = await _standardService.DeleteStandardService(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
