using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static PartsTraderApi.Library;


namespace PartsTraderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartsController : ControllerBase
    {


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PartsController> _logger;

        public PartsController(ILogger<PartsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Compatible/{partNumber}")]
        public async Task<ActionResult<IEnumerable<Part>>> GetCompatibleParts(string partNumber)
        {
            try
            {
                var exceptions = new List<Exception>();
                Dictionary<string, Part> excludedParts = new Dictionary<string, Part>();
                using (StreamReader r = new StreamReader("Data/Exclusions.json"))
                {
                    string json = r.ReadToEnd();
                    excludedParts = JsonConvert.DeserializeObject<List<Part>>(json).ToDictionary(p => p.partNumber, p => p);
                }

                if (!PartNumberIsValid(partNumber)) exceptions.Add(new ArgumentException("The Part Number is an invalid format.It should use the following format: PartId(4xnumbers) +\"-\"+PartCode(4+ aplhanumeric) e.g 1234-a1b2c3d4"));
                if (excludedParts.ContainsKey(partNumber)) exceptions.Add(new ArgumentException("The Part Number is excluded"));

                if (exceptions.Any()) throw new AggregateException("Part Number is not valid", exceptions);

                var parts = new List<Part>();

                return parts;
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }


        }
    }
}
