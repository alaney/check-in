using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using lindsey.Models;

namespace lindsey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get(string schoolName)
        {
            return System.IO.File.ReadAllLines(@"c:\code\lindsey\MOCK_DATA.csv").Skip(1).Select(l => Student.FromCsv(l)).Where(s => s.School == schoolName).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            return System.IO.File.ReadAllLines(@"c:\code\lindsey\MOCK_DATA.csv").Skip(1).Select(l => Student.FromCsv(l)).FirstOrDefault(s => s.ClientId == id);
        }

        // GET api/values/5
        [HttpGet("schools")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var students = System.IO.File.ReadAllLines(@"c:\code\lindsey\MOCK_DATA.csv").Skip(1).Select(l => Student.FromCsv(l)).ToList();
            return students.Select(s => s.School).Distinct().OrderBy(a => a).ToList();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
