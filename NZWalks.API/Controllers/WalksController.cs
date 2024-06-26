using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        // POST CREATE NEW WALK
        // POST: https://localhost:port/api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = walkDto.Id }, walkDto);
        }

        // GET WALKS
        // GET: https://localhost:port/api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // GET SINGLE WALK
        // GET: https://localhost:port/api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomianModel = await walkRepository.GetByIdAsync(id);
            if (walkDomianModel == null)
            {
                return NotFound();   
            }
            return Ok(mapper.Map<WalkDto>(walkDomianModel));
        }
    }
}
