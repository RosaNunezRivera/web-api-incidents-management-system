using AutoMapper;
using BLL;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_IncidentsManagementSystem.Controllers
{
    [Route("api/groupprocesses")]
    [ApiController]
    public class GroupprocessController : ControllerBase
    {
        //Declaring a private read-only variable named _postService 
        private readonly IGroupProcessService _groupProcessService;

        //Define mapper
        private readonly IMapper _mapper;
        

        //Define constructor
        public GroupprocessController(IGroupProcessService groupProcessService, IMapper mapper) 
        {
            _groupProcessService = groupProcessService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupsProcess() 
        {
            //Creating a list 
            var listGroupProcess = await _groupProcessService.GetGroupProcessService();

            //Converting a list in format of DTO 
            List<GroupProcessDTO> groupProcessDto = _mapper.Map<List<GroupProcessDTO>>(listGroupProcess);
           
            return Ok(groupProcessDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGroupProcessById(int id)
        {
            //Getting the groupProcess by id
            GroupProcess groupProcess = await _groupProcessService.GetGroupProcessByIdService(id);

            if (groupProcess == null)
            {
                return BadRequest("GroupProcess not found");
            }

            //Convert GroupProcess object in Dto
            GroupProcessDTO groupProcessDto = _mapper.Map<GroupProcessDTO>(groupProcess);
            return Ok(groupProcessDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddGroupProcess(GroupProcessDTO groupProcessDto) 
        {
            var result = await _groupProcessService.AddGroupProcessService(groupProcessDto);

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
        /// MEthod to tupdate a groupprocess
        /// </summary>
        /// <param name="groupProcessDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateGroupProcess(GroupProcessDTO groupProcessDto)
        { 
            var result = await _groupProcessService.UpdateGroupProcessService(groupProcessDto);

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
        /// Method to delete a groupprocess
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteGroupProcess(int id) 
        {
            var result = await _groupProcessService.DeleteGroupProcessService(id);

            if (result == null)
            {
                return NotFound();  
            }
            return Ok(result);
        }
    }
}
