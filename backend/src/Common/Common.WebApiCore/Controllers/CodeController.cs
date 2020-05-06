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
    [Route("code")]
    public class CodeController : BaseApiController
    {
        private readonly ICodeRepository _CodeRepos;

        public CodeController(ICodeRepository CodeRepos)
        {
            _CodeRepos = CodeRepos;
        }

        [HttpPost]
        [Route("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> Filter(FilterCode filter)
        {
            if (filter.StartNotAppeareCount.Value < filter.EndNotAppeareCount.Value)
            {
                return BadRequest();
            }

            var allQuery = _CodeRepos.GetByFilter(filter);
            var totalCount = await allQuery.CountAsync();
            allQuery = _CodeRepos.Sort(allQuery, filter.Sorting);
            allQuery = _CodeRepos.Paging(allQuery, filter.Paging);
            var result = allQuery.ToList();
           
            var dtos = result?.MapTo<List<CodeDTO>>();
            return Ok(new ResultFilter<CodeDTO> { Data = dtos, TotalCount = totalCount });
        }

        [HttpPost]
        [Route("add")]
        [AllowAnonymous]
        public async Task<IActionResult> Add(AddOrUpdateCodeDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                var code = await _CodeRepos.FirstOrDefaultAsync(c => c.Name.Contains(dto.Name));
                if (code != null)
                {
                    return BadRequest();
                }
            }

            if (dto.Numbers == null && dto.Numbers.Count > 20)
            {
                if (dto.Numbers.GroupBy(x => x).Any(g => g.Count() > 1))
                { 
                    return BadRequest();
                }
            }

            var entity = dto.MapTo<Code>();

            var success = await _CodeRepos.AddAsync(entity);

            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(AddOrUpdateCodeDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                var code = await _CodeRepos.FirstOrDefaultAsync(c => c.Name.Contains(dto.Name));
                if (code != null)
                {
                    return BadRequest();
                }
            }

            if (dto.Numbers == null && dto.Numbers.Count > 20)
            {
                if (dto.Numbers.GroupBy(x => x).Any(g => g.Count() > 1))
                {
                    return BadRequest();
                }
            }

            var entity = dto.MapTo<Code>();

            var success = _CodeRepos.Update(entity);

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

            var success = _CodeRepos.Remove(id);
            if (!success)
                return BadRequest();

            return Ok();
        }
    }
}