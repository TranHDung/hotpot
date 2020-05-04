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

            var date = doc.QuerySelectorAll(".htspt__cards--next-draw-date strong")[0].InnerText;
            var time = doc.QuerySelectorAll(".htspt__cards--next-draw-date strong")[1].InnerText.ToUpper().Replace(".", "");
            var result = new HotspotResult
            {
                DrawNumber = Int32.Parse(doc.QuerySelector(".current-drawNumber").InnerText),
                DrawDate = DateTime.ParseExact(date + " " + time, "MMM dd, yyyy h:mm tt", CultureInfo.InvariantCulture),
                BlueNumbers = doc.QuerySelectorAll(".list-inline.htspt__cards--winning-numbers .list-inline-item.blue-num").Select(e => e.InnerText).ToList(),
                YellowNumber = doc.QuerySelectorAll(".list-inline.htspt__cards--winning-numbers .list-inline-item.yellow-num").Select(e => e.InnerText).ToString()
            };
            await _hotspotResultRepos.AddAsync(result);
        }
    }
}
