using Common.DTO;
using Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IHotspotResultRepository: IRepository<HotspotResult>
    {
        IQueryable<HotspotResult> GetByFilter(FilterHotspotResult filter);
        Task<int> GetNewestDrawNumberAsync();
        IQueryable<HotspotResult> Sort(IQueryable<HotspotResult> entities, Sorting sorting);

        bool Update(HotspotResultDTO entity);
    }
}
