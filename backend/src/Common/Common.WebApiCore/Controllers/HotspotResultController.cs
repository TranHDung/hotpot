/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.Services.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Utils;

namespace Common.WebApiCore.Controllers
{
    [Route("users")]
    public class HotspotResultController : BaseApiController
    {
        private readonly IHotspotResultRepository _hotspotResultRepos;

        public HotspotResultController(IHotspotResultRepository hotspotResultRepos)
        {
            _hotspotResultRepos = hotspotResultRepos;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            var entities = _hotspotResultRepos.GetAll().ToList();
            var dtos = entities.MapTo<List<HotspotResultDTO>>();
            return Ok(dtos);
        }
    }
}