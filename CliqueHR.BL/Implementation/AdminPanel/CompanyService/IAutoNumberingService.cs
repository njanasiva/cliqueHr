using System;
using CliqueHR.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface IAutoNumberingService
    {
        void AddUpdateAutoNumbering(AutoNumbering model, UserContextModel objUser);
        AutoNumbering GetAutoNumbering(UserContextModel objUser);

    }
}
