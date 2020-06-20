using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using lindsey.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace lindsey.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentsController : ControllerBase
  {
    private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly string _path = "";
    private readonly IConfiguration config;

    public StudentsController(IConfiguration config)
    {
      _path = config["DataFilePath"];
      this.config = config;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Student>> Get(string schoolName)
    {
      try
      {
        return System.IO.File.ReadAllLines(_path).Skip(1).Select(l => Student.FromCsv(l)).Where(s => s.School == schoolName).OrderBy(s => s.LastName).ToList();
      }
      catch (Exception ex)
      {
        _logger.Error(ex);
        return StatusCode(500);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Student> Get(int id)
    {
      return System.IO.File.ReadAllLines(_path).Skip(1).Select(l => Student.FromCsv(l)).FirstOrDefault(s => s.ClientId == id);
    }

    [HttpPatch("{id}")]
    public ActionResult Patch(int id, [FromBody] Student patchedStudent)
    {
      try
      {
        var rand = new Random();
        var file = System.IO.File.ReadAllLines(_path).ToList();
        var header = file.First();
        var students = file.Skip(1).Select(l => Student.FromCsv(l)).ToList();
        var studentLine = students.FirstOrDefault(s => s.ClientId == id);
        studentLine.PrimaryEmail = patchedStudent.PrimaryEmail.Trim();
        studentLine.SecondaryEmail = patchedStudent.SecondaryEmail.Trim();
        studentLine.ParentFirstName = patchedStudent.ParentFirstName;
        studentLine.ParentLastName = patchedStudent.ParentLastName;
        if (string.IsNullOrWhiteSpace(studentLine.Password))
        {
          // get unique password
          string pwd = patchedStudent.LastName + rand.Next(0, 10) + rand.Next(0, 10) + rand.Next(0, 10) + rand.Next(0, 10);
          while (students.FirstOrDefault(s => s.Password == pwd) != null)
          {
            pwd = patchedStudent.LastName + rand.Next(0, 10) + rand.Next(0, 10) + rand.Next(0, 10) + rand.Next(0, 10);
          }
          studentLine.Password = pwd.ToLower();
        }
        studentLine.Permission = patchedStudent.Permission;
        studentLine.Phone1 = patchedStudent.Phone1;
        studentLine.Phone2 = patchedStudent.Phone2;
        studentLine.CheckInDate = DateTime.Now.ToString();
        file = new List<string>();
        file.Add(header);
        file.AddRange(students.Select(s => Student.ToCsv(s)));
        System.IO.File.WriteAllLines(_path, file.ToArray());
        return Ok();
      }
      catch (Exception ex)
      {
        _logger.Error(ex);
        return StatusCode(500);
      }
    }

    // GET api/values/5
    [HttpGet("schools")]
    public ActionResult<IEnumerable<School>> Get()
    {
      try
      {
        var lines = System.IO.File.ReadAllLines(_path).Skip(1);
        var students = lines.Select(l => Student.FromCsv(l)).ToList();
        var schools = students.Select(s => s.School).Distinct().OrderBy(a => a).Select(s => new School { Name = s }).ToList();
        var schoolSettings = new List<School>();
        config.GetSection("SchoolSettings").Bind(schoolSettings);
        schools.ForEach(s =>
        {
          var setting = schoolSettings.FirstOrDefault(set => set.Name.ToLower() == s.Name.ToLower());
          if (setting != null)
          {
            s.Color = setting.Color;
          }
        });
        return schools.OrderBy(s =>
        {
          int index = schoolSettings.FindIndex(set => set.Name.ToLower() == s.Name.ToLower());
          if (index > -1)
          {
            return index;
          }
          else
          {
            return 100;
          }
        }).ToList();
      }
      catch (Exception ex)
      {
        _logger.Error(ex);
        return StatusCode(500);
      }
    }
  }
}
