using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace Common.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly IHotspotResultService _hotspotResultService;

        public BackgroundJobService(IHotspotResultService hotspotResultService)
        {
            _hotspotResultService = hotspotResultService;
        }
        public void Continuations()
        {
            throw new NotImplementedException();
        }

        public void Enqueue()
        {

            throw new NotImplementedException();
        }

        public void Recurring()
        {
            _hotspotResultService.CrawlHotspotResult().Wait();
        }

        public void Schedule()
        {
            throw new NotImplementedException();
        }
    }
}
