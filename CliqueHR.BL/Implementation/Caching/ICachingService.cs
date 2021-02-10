using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface ICachingService<T>
    {
        T GetData(IParamManager param);
        void UpdateCacheTimestamp(ICacheModelParam param);
    }
}
