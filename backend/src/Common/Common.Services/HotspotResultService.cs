using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class HotspotResultService: IHotspotResultService
    {
        private readonly IHotspotResultRepository _hotspotResultRepos;
        public HotspotResultService(IHotspotResultRepository hotspotResultRepos)
        {
            _hotspotResultRepos = hotspotResultRepos;
        }
        public async Task CrawlHotspotResult()
        {
            var url = "https://www.calottery.com/draw-games/hot-spot";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            // need cache
            var _drawNumber = await _hotspotResultRepos.GetNewestDrawNumberAsync();
            var drawNumber = Int32.Parse(doc.QuerySelector(".current-drawNumber").InnerText);
            //need refactor
            if (_drawNumber != drawNumber)
            {
                var date = doc.QuerySelectorAll(".htspt__cards--next-draw-date strong")[0].InnerText;
                var time = doc.QuerySelectorAll(".htspt__cards--next-draw-date strong")[1].InnerText.ToUpper().Replace(".", "");
                var result = new HotspotResult
                {
                    DrawNumber = drawNumber,
                    DrawDate = DateTime.ParseExact(date + " " + time, "MMM d, yyyy h:mm tt", CultureInfo.InvariantCulture),
                    BlueNumbers = doc.QuerySelectorAll(".list-inline.htspt__cards--winning-numbers .list-inline-item").Select(e => e.InnerText).ToList(),
                };

                await _hotspotResultRepos.AddAsync(result);
            }
        }
    }
}
