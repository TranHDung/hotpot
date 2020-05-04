using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure
{
    public interface IHotspotResultService
    {
        Task CrawlHotspotResult();  
    }
}
