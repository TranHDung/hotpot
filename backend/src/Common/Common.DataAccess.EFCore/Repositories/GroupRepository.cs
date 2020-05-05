using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories
{
    public class GroupRepository: Repository<Group>, IGroupRepository
    {
        protected readonly DataContext Context;

        public GroupRepository(DataContext context) : base(context)
        {
            Context = context;
        }

        public IQueryable<Group> GetByFilter(FilterGroup filter)
        {
            var query = GetAll();
            if (string.IsNullOrWhiteSpace(filter.name))
            {
                filter.name = filter.name.Trim();
                query.Where(g => g.Name.Contains(filter.name));
            }
            
            return query;
        }

        public IQueryable<Group> Sort(IQueryable<Group> entities, Sorting sorting)
        {
            if (sorting != null)
            {
                if (sorting.sortDerection == "asc")
                {
                    switch (sorting.columnName)
                    {
                        case "name":
                            entities = entities.OrderBy(p => p.Name);
                            break;
                        default:
                            entities = entities.OrderBy(p => p.CreateAt);
                            break;
                    }
                }
                if (sorting.sortDerection == "desc")
                {
                    switch (sorting.columnName)
                    {
                        case "drawNumber":
                            entities = entities.OrderByDescending(p => p.Name);
                            break;
                        default:
                            entities = entities.OrderByDescending(p => p.CreateAt);
                            break;
                    }
                }
            }

            return entities;
        }
    }
}
