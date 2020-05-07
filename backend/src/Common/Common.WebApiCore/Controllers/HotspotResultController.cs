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
            
            { 
                if (filter.StartSession < filter.EndSession)
                {
                    return BadRequest();
                }
            }

            if (filter.StartDrawDate != null && filter.EndDrawDate != null) 
            { 
                if (filter.StartDrawDate.Value < filter.EndDrawDate.Value)
                {
                    return BadRequest();
                }
            }

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

            var success = await _hotspotResultRepos.AddAsync(entity);

            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        [AllowAnonymous]
        public IActionResult Update(HotspotResult dto)
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

            var success = _hotspotResultRepos.Update(entity);

            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpGet]
        [Route("remove/{id:int}")]
        [AllowAnonymous]
        public IActionResult Remove(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var success = _hotspotResultRepos.Remove(id);
            if (!success)
                return BadRequest();

            return Ok();
        }
    }
}