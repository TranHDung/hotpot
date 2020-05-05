using Common.DTO;
using Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IGroupRepository: IRepository<Group>
    {
        IQueryable<Group> GetByFilter(FilterGroup filter);
        IQueryable<Group> Sort(IQueryable<Group> entities, Sorting sorting);
    }
}
