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
    [Route("group")]
    public class GroupController : BaseApiController
    {
        private readonly IGroupRepository _groupRepos;

        public GroupController(IGroupRepository groupRepos)
        {
            _groupRepos = groupRepos;
        }

        [HttpPost]
        [Route("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> Filter(FilterGroup filter)
        {
            var allQuery = _groupRepos.GetByFilter(filter);
            var totalCount = await allQuery.CountAsync();
            allQuery = _groupRepos.Sort(allQuery, filter.Sorting);
            allQuery = _groupRepos.Paging(allQuery, filter.Paging);
           var result = allQuery.ToList();
           
            var dtos = result?.MapTo<List<GroupDTO>>();
            return Ok(new ResultFilter<GroupDTO> { Data = dtos, TotalCount = totalCount });
        }

        [HttpPost]
        [Route("add")]
        [AllowAnonymous]
        public async Task<IActionResult> Add(GroupDTO dto)
        {
            if(string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest();
            }

            var entity = dto.MapTo<Group>();

            var success = await _groupRepos.AddAsync(entity);
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        [AllowAnonymous]
        public IActionResult Update(GroupDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest();
            }

            var entity = dto.MapTo<Group>();

            var success = _groupRepos.Update(entity);
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

            var success = _groupRepos.Remove(id);
            if (!success)
                return BadRequest();

            return Ok();
        }
    }
}