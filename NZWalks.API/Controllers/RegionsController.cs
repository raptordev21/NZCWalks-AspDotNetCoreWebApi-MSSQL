using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        // GET ALL REGIONS
        // GET: https://localhost:port/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get data from database - Domain models
            var regionsDomain = dbContext.Regions.ToList();

            // Map domain models to DTOs (Data Transfer Objects)
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });
            }

            // Retrun DTOs
            return Ok(regionsDto);
        }

        // GET SINGLE REGIONS
        // GET: https://localhost:port/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            //var region = dbContext.Regions.Find(id);

            // Get Region Domain Model from database
            var regionDomain = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            /*
                Find(id) only takes primary key 
                FirstOrDefault(r => r.Id == id) can compare with any value
            */

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map Region Domain Model to Region DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return Ok(regionDto);
        }
    }
}
