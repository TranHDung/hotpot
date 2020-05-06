using Common.DTO;
using Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Repositories
{
    public interface ICodeRepository: IRepository<Code>
    {
        IQueryable<Code> GetByFilter(FilterCode filter);
        IQueryable<Code> Sort(IQueryable<Code> entities, Sorting sorting);
    }
}
