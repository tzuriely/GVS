using DataMining.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Shared.Interfaces
{
    public interface IRobot<T>
    {
        Task<RobotResult<T>> Run();
    }
}
