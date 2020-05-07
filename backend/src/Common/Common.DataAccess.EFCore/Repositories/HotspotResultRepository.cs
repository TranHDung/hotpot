using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories
{
    public class HotspotResultRepository: Repository<HotspotResult>, IHotspotResultRepository
    {
        protected readonly DataContext Context;

        public HotspotResultRepository(DataContext context) : base(context)
        {
            Context = context;
        }

        public IQueryable<HotspotResult> GetByFilter(FilterHotspotResult filter)
        {
            var query = GetAll();
            if (filter.StartDrawDate != null) 
            {
                query = query.Where(e => e.DrawDate >= filter.StartDrawDate);
            }

            if (filter.EndDrawDate != null)
            {
                query = query.Where(e => e.DrawDate <= filter.EndDrawDate);
            }

            if (filter.StartSession > 0)
            {
                query = query.Where(e => e.DrawNumber >= filter.StartSession);
            }

            if (filter.EndSession > 0)
            {
                query = query.Where(e => e.DrawNumber <= filter.EndSession);
            }

            if (filter.TopSeccion > 0)
            {
                query = query.OrderByDescending(e => e.DrawNumber).Take(filter.TopSeccion);
            }

            if (filter.CodeId > 0)
            {
                var hotspotResultWasWon = Context.WonCodes
                                          .Where(e => e.CodeId == filter.CodeId).Select(e => e.HotspotResultId);
                query = query
                       .Where(e => hotspotResultWasWon.Contains(e.Id));
            }

            if (filter.GroupId > 0)
            {
                var hotspotResultWasWon = Context.WonCodes
                                                 .Include(wc => wc.Code)
                                                 .ThenInclude(c => c.Group)
                                                 .Where(wc => wc.Code.GroupId == filter.GroupId)
                                                 .Select(wc => wc.HotspotResultId);
                                          
                query = query
                       .Where(e => hotspotResultWasWon.Contains(e.Id));
            }
            
            return query;
        }

        public async Task<int> GetNewestDrawNumberAsync()
        {
            var hotspotResult = await Context.HotspotResults.OrderByDescending(e => e.DrawNumber).FirstOrDefaultAsync();
            return hotspotResult != null ? hotspotResult.DrawNumber : 0 ;
        }

        public IQueryable<HotspotResult> Sort(IQueryable<HotspotResult> entities, Sorting sorting)
        {
            if (sorting != null)
            {
                if (sorting.SortDerection == "asc")
                {
                    switch (sorting.ColumnName)
                    {
                        case "drawNumber":
                            entities = entities.OrderBy(p => p.DrawNumber);
                            break;
                        default:
                            entities = entities.OrderBy(p => p.DrawDate);
                            break;
                    }
                }
                if (sorting.SortDerection == "desc")
                {
                    switch (sorting.ColumnName)
                    {
                        case "drawNumber":
                            entities = entities.OrderByDescending(p => p.DrawNumber);
                            break;
                        default:
                            entities = entities.OrderByDescending(p => p.DrawDate);
                            break;
                    }
                }
            }

            return entities;
        }

        public bool Update(HotspotResultDTO dto)
        {
            var entity = FirstOrDefault(e => e.Id == dto.Id);
            if (entity == null)
            {
                return false;
            }

            entity.DrawNumber = dto.DrawNumber;
            entity.DrawDate = dto.DrawDate;
            entity.BlueNumbers = dto.BlueNumbers;

            return Update(entity);
        }
    }
}
