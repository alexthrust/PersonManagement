using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonManagement.Services.Abstraction;
using PersonManagement.Services.Models;
using PersonManagement.Services.Models.Common;

namespace PersonManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET api/person/records
        [HttpGet("records")]
        public async Task<IActionResult> GetPersonRecords([FromQuery] CustomStoreRequestModel requestModel)
        {
            var result = await _personService.GetPersonRecords(requestModel);
            if (result.IsSuccess)
                return new OkObjectResult(result.Data);

            return StatusCode(500, result.ErrorMessage);
        }

        // GET api/person/1
        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPersonInfo(int personId)
        {
            var result = await _personService.GetPerson(personId);
            if (result.IsSuccess)
                return new OkObjectResult(result.Data);

            return StatusCode(500, result.ErrorMessage);
        }

        // POST api/person
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonModel person)
        {
            var result = await _personService.CreatePerson(person);
            if (result.IsSuccess)
                return Ok();

            return StatusCode(500, result.ErrorMessage);
        }

        // PUT api/person/1
        [HttpPut("{personId}")]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonModel person, int personId)
        {
            var result = await _personService.UpdatePerson(personId, person);
            if (result.IsSuccess)
                return Ok();

            return StatusCode(500, result.ErrorMessage);
        }

        // DELETE api/person/1
        [HttpDelete("{personId}")]
        public async Task<IActionResult> DeletePerson(int personId)
        {
            var result = await _personService.DeletePerson(personId);
            if (result.IsSuccess)
                return Ok();

            return StatusCode(500, result.ErrorMessage);
        }
    }
}