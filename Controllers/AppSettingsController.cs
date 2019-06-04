using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Name
{
  [Route("api/[controller]")]
  [ApiController]
  public class AppSettings : ControllerBase
  {
    private readonly IConfiguration config;

    public AppSettings(IConfiguration config)
    {
      this.config = config;
    }

    // public ActionResult<List<SchoolSetting>> Get()
    // {
    //   var schoolSettings = new List<SchoolSetting>();
    //   this.config.GetSection("SchoolSettings").Bind(schoolSettings);
    //   return Ok(schoolSettings);
    // }
  }
}

public class School
{
  public string Name { get; set; }
  public string Color { get; set; }
}