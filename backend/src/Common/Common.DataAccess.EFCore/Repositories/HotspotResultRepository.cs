using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DataAccess.EFCore.Repositories
{
    public class HotspotResultRepository: Repository<HotspotResult>, IHotspotResultRepository
    {
        protected readonly DataContext Context;

        public HotspotResultRepository(DataContext context) : base(context)
        {
            Context = context;
        }
    }
}
