using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PartsTraderApi.Models;
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
        private IConfiguration _configuration;

        private readonly PartsDbContext _context;

        public PartsController(IConfiguration configuration, PartsDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        [HttpGet("{partNumber}")]
        public async Task<ActionResult<IEnumerable<Part>>> GetCompatibleParts(string partNumber)
        {
            partNumber = partNumber.ToLower();
            try
            {
                var exceptions = new List<Exception>();
                Dictionary<string, Part> excludedParts = new Dictionary<string, Part>();

                //Validate Part Number
                if (!PartNumberIsValid(partNumber)) exceptions.Add(new ArgumentException("The Part Number is an invalid format.It should use the following format: PartId(4xnumbers) +\"-\"+PartCode(4+ aplhanumeric) e.g 1234-a1b2c3d4"));

                //Check Exclusions
                using (StreamReader r = new StreamReader("Data/Exclusions.json"))
                {
                    string json = r.ReadToEnd();
                    excludedParts = JsonConvert.DeserializeObject<List<Part>>(json).ToDictionary(p => p.partNumber.ToLower(), p => p);
                }

                if (excludedParts.ContainsKey(partNumber)) exceptions.Add(new ArgumentException("The Part Number \"" + partNumber + "\" is excluded."));

                //Return Errors
                if (exceptions.Any()) throw new AggregateException("Part Number \"" + partNumber + "\" is not valid.", exceptions);


                //Get compatible Parts
                var currentPart = await _context.Parts
                                                .Where(p => p.partNumber == partNumber)
                                                .FirstOrDefaultAsync();

                if (currentPart == null) throw new ArgumentException("Part Number \"" + partNumber + "\" cannot be found.");

                var compatibleParts = await _context.Parts
                                                    .Where(p => p.description == currentPart.description)
                                                    .ToListAsync();



                return compatibleParts;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
