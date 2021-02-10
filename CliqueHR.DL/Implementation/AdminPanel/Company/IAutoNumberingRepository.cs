using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;

namespace CliqueHR.DL
{
    public interface IAutoNumberingRepository
    {
        ApplicationResponse AddUpdateAutoNumbering(AutoNumbering model, string CompanyCode);
        AutoNumbering GetAutoNumbering(string CompanyCode);
        
    }
}
