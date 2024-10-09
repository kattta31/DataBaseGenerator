using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseGenerator.Test.Core
{
    public interface ITestService
    {
        bool IsStarted { get; }

        Task<bool> StartAsync();

        Task<bool> StopAsync();

    }
}
