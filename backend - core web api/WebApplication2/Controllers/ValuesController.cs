using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IStudentService _studentService;
        public ValuesController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        // GET api/values
        [Authorize]
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            //return new List<Student>();
            return _studentService.GetAll().ToList();
        }


        [HttpPost]
        [Route("testpost")]
        public IActionResult testpost(string name)
        {
            return Ok(new { data = $"Welcome {name}" });
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "added";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        
    }
}
