using AutoMapper;
using BLL;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_IncidentsManagementSystem.Controllers
{
    [Route("api/rootcauses")]
    [ApiController]
    public class RootCauseController : ControllerBase
    {
        //Declaring a private read-only variable named _rootCauseService 
        private readonly IRootCauseService _rootCauseService;

        //Define mapper
        private readonly IMapper _mapper;

        //Define constructor
        public RootCauseController(IRootCauseService rootCauseService, IMapper mapper)
        {
            _rootCauseService = rootCauseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRootCauses()
        {
            //Creating a list 
            var listRootCauses = await _rootCauseService.GetRootCauseService();

            //Converting a list in format of DTO 
            List<RootCauseDTO> RootCauseDto = _mapper.Map<List<RootCauseDTO>>(listRootCauses);

            return Ok(RootCauseDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRootCauseById(int id)
        {
            //Getting the RootCause by id
            RootCause RootCause = await _rootCauseService.GetRootCauseByIdService(id);

            if (RootCause == null)
            {
                return NotFound();
            }

            //Convert RootCause object in Dto

            RootCauseDTO RootCauseDto = _mapper.Map<RootCauseDTO>(RootCause);
            return Ok(RootCauseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddRootCause(RootCauseDTO RootCauseDto)
        {
            var result = await _rootCauseService.AddRootCauseService(RootCauseDto);
            return Ok(result);
        }

        /// <summary>
        /// MEthod to tupdate a RootCause
        /// </summary>
        /// <param name="RootCauseDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRootCause(RootCauseDTO RootCauseDto)
        {
            var result = await _rootCauseService.UpdateRootCauseService(RootCauseDto);
            return Ok(result);
        }

        /// <summary>
        /// Method to delete a RootCause
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRootCause(int id)
        {
            var result = await _rootCauseService.DeleteRootCauseService(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
