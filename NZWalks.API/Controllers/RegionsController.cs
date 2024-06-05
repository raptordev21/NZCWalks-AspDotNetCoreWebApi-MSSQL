using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository) 
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        // GET ALL REGIONS
        // GET: https://localhost:port/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - Domain models
            // var regionsDomain = dbContext.Regions.ToList(); // working synchronous
            // var regionsDomain = await dbContext.Regions.ToListAsync();
            var regionsDomain = await regionRepository.GetAllAsync();

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

        // GET SINGLE REGION
        // GET: https://localhost:port/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            //var region = dbContext.Regions.Find(id);

            // Get Region Domain Model from database
            // var regionDomain = dbContext.Regions.FirstOrDefault(r => r.Id == id); // working synchronous
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            /*
                Find(id) only takes primary key 
                FirstOrDefault(r => r.Id == id) can compare with any value
            */

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map Region Domain Model to Region DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        // POST CREATE NEW REGION
        // POST: https://localhost:port/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            // Use Domain Model to create Region
            // dbContext.Regions.Add(regionDomainModel); // sync
            // dbContext.SaveChanges(); // sync
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            // Map Domain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }

        // PUT: UPDATE SINGLE REGION
        // PUT: https://localhost:port/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Check if region exists
            // var regionDomainModel = dbContext.Regions.FirstOrDefault(r => r.Id == id); // sync
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            // dbContext.SaveChanges(); // sync
            await dbContext.SaveChangesAsync();

            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        // DELETE: DELETE SINGLE REGION
        // DELETE: https://localhost:port/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (regionDomainModel == null)
            {
                var notFoundRes = new { success = false, error = "No Record Found", data = "" };
                return NotFound(notFoundRes);
            }

            // Delete region
            dbContext.Regions.Remove(regionDomainModel); // no async version available for remove
            await dbContext.SaveChangesAsync();

            // return deleted region back
            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            var okRes = new { success = true, error = "", data = regionDto };
            return Ok(okRes);
        }
    }
}
