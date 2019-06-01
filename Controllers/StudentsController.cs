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
        private const string Path = @"c:\users\aaron\documents\code\schoolcheckin\MOCK_DATA.csv";
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get(string schoolName)
        {
            return System.IO.File.ReadAllLines(Path).Skip(1).Select(l => Student.FromCsv(l)).Where(s => s.School == schoolName).OrderBy(s => s.LastName).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            return System.IO.File.ReadAllLines(Path).Skip(1).Select(l => Student.FromCsv(l)).FirstOrDefault(s => s.ClientId == id);
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody]Student patchedStudent)
        {
            var rand = new Random(patchedStudent.LastName.GetHashCode());
            var file = System.IO.File.ReadAllLines(Path).ToList();
            var header = file.First();
            var students = file.Skip(1).Select(l => Student.FromCsv(l)).ToList();
            var studentLine = students.FirstOrDefault(s => s.ClientId == id);
            studentLine.Emails = patchedStudent.Emails;
            studentLine.ParentFirstName = patchedStudent.ParentFirstName;
            studentLine.ParentLastName = patchedStudent.ParentLastName;
            studentLine.Password = patchedStudent.LastName + rand.Next(0, 10) + rand.Next(0, 10) + rand.Next(0, 10) + rand.Next(0, 10);
            studentLine.Permission = patchedStudent.Permission;
            studentLine.Phone1 = patchedStudent.Phone1;
            studentLine.Phone2 = patchedStudent.Phone2;
            studentLine.CheckInDate = DateTime.Now.ToString();
            file = new List<string>();
            file.Add(header);
            file.AddRange(students.Select(s => Student.ToCsv(s)));
            System.IO.File.WriteAllLines(Path, file.ToArray());
            return Ok();
        }

        // GET api/values/5
        [HttpGet("schools")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var students = System.IO.File.ReadAllLines(Path).Skip(1).Select(l => Student.FromCsv(l)).ToList();
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
