using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories
{
    public class CodeRepository: Repository<Code>, ICodeRepository
    {
        protected readonly DataContext Context;

        public CodeRepository(DataContext context) : base(context)
        {
            Context = context;
        }

        public IQueryable<Code> GetByFilter(FilterCode filter)
        {
            var query = GetAll();
            if (filter.EndNotAppeareCount != null)
            {
                query = query.Where(e => e.NotAppeareCount <= filter.EndNotAppeareCount );
            }

            if (filter.StartNotAppeareCount != null)
            {
                query = query.Where(e => e.NotAppeareCount >= filter.StartNotAppeareCount);
            }

            if (filter.NotAppeareCount != null)
            {
                query = query.Where(e => e.NotAppeareCount == filter.NotAppeareCount);
            }

            if (string.IsNullOrWhiteSpace(filter.CodeName))
            {
                filter.CodeName = filter.CodeName.Trim();
                query = query
                    .Where(e => e.Name.Contains(filter.CodeName));
            }

            if (string.IsNullOrWhiteSpace(filter.GroupName))
            {
                filter.GroupName = filter.GroupName.Trim();
                query = query
                    .Include(q => q.Group)
                    .Where(e => e.Group.Name.Contains(filter.GroupName));
            }

            return query;
        }

        public IQueryable<Code> Sort(IQueryable<Code> entities, Sorting sorting)
        {
            if (sorting != null)
            {
                if (sorting.SortDerection == "asc")
                {
                    switch (sorting.ColumnName)
                    {
                        case "name":
                            entities = entities.OrderBy(p => p.Name);
                            break;
                        case "notAppeareCount":
                            entities = entities.OrderBy(p => p.NotAppeareCount);
                            break;
                        case "reappeareCount":
                            entities = entities.OrderBy(p => p.ReappeareCount);
                            break;
                        case "group":
                            entities = entities.OrderBy(p => p.GroupId);
                            break;
                        default:
                            entities = entities.OrderBy(p => p.CreateAt);
                            break;
                    }
                }
                if (sorting.SortDerection == "desc")
                {
                    switch (sorting.ColumnName)
                    {
                        case "name":
                            entities = entities.OrderBy(p => p.Name);
                            break;
                        case "notAppeareCount":
                            entities = entities.OrderBy(p => p.NotAppeareCount);
                            break;
                        case "reappeareCount":
                            entities = entities.OrderBy(p => p.ReappeareCount);
                            break;
                        case "group":
                            entities = entities.OrderBy(p => p.GroupId);
                            break;
                        default:
                            entities = entities.OrderBy(p => p.CreateAt);
                            break;
                    }
                }
            }

            return entities;
        }
    }
}
