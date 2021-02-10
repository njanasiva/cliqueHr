using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface IParamManager
    {
        long TimeStamp { get; set; }
        string Key { get; set; }
        string Company { get; set; }
    }
    public interface ICacheModelParam
    {
        string Key { get; set; }
        string Company { get; set; }
    }
}
