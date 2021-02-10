using System;
using CliqueHR.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface ISecuritySettingService
    {
        void AddUpdateSecuritySettings(SecuritySettings model, UserContextModel objUser);
        SecuritySettings GetSecuritySettings(UserContextModel objUser);

    }
}
