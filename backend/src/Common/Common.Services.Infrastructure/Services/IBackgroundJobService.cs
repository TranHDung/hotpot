using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services.Infrastructure.Services
{
    public interface IBackgroundJobService
    {
        void Enqueue();
        void Schedule();
        void Recurring();
        void Continuations();
    }
}
