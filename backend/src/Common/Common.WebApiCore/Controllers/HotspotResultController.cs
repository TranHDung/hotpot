using Common.DTO;
using Common.Services.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Common.Entities;
using System;

namespace Common.WebApiCore.Controllers
{
    [Route("hotspotResult")]
    public class HotspotResultController : BaseApiController
    {
        private readonly IHotspotResultRepository _hotspotResultRepos;

        public HotspotResultController(IHotspotResultRepository hotspotResultRepos)
        {
            _hotspotResultRepos = hotspotResultRepos;
        }

        [HttpPost]
        [Route("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> Filter(FilterHotspotResult filter)
        {
            var allQuery = _hotspotResultRepos.GetByFilter(filter);
            var totalCount = await allQuery.CountAsync();
            allQuery = _hotspotResultRepos.Sort(allQuery, filter.Sorting);
            allQuery = _hotspotResultRepos.Paging(allQuery, filter.Paging);
           var result = allQuery.ToList();
           
            var dtos = result?.MapTo<List<HotspotResultDTO>>();
            return Ok(new ResultFilter<HotspotResultDTO> { Data = dtos, TotalCount = totalCount });
        }

        [HttpPost]
        [Route("add")]
        [AllowAnonymous]
        public async Task<IActionResult> Add(HotspotResultDTO dto)
        {
            if(dto.DrawNumber < 0)
            {
                return BadRequest();
            }

            if (dto.DrawDate == default)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(dto.YellowNumber))
            {
                return BadRequest();
            }

            if (dto.BlueNumbers == null && dto.BlueNumbers.Count < 20)
            {
                if (dto.BlueNumbers.GroupBy(x => x).Any(g => g.Count() > 1))
                { 
                    return BadRequest();
                }
            }

            var entity = dto.MapTo<HotspotResult>();

            await _hotspotResultRepos.AddAsync(entity);
            return Ok();
        }

        [HttpPost]
        [Route("update")]
        [AllowAnonymous]
        public IActionResult Update(HotspotResultDTO dto)
        {
            if (dto.DrawNumber < 0)
            {
                return BadRequest();
            }

            if (dto.DrawDate == default)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(dto.YellowNumber))
            {
                return BadRequest();
            }

            if (dto.BlueNumbers == null && dto.BlueNumbers.Count < 20)
            {
                if (dto.BlueNumbers.GroupBy(x => x).Any(g => g.Count() > 1))
                {
                    return BadRequest();
                }
            }

            var entity = dto.MapTo<HotspotResult>();

            _hotspotResultRepos.Update(entity);
            return Ok();
        }

        [HttpGet]
        [Route("remove/{id:int}")]
        [AllowAnonymous]
        public IActionResult Update(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            _hotspotResultRepos.Remove(id);
            return Ok();
        }
    }
}