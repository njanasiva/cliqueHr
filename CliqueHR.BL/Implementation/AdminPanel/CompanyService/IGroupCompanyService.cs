using System;
using CliqueHR.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CliqueHR.BL
{
    public interface IGroupCompanyService
    {
        void AddUpdateGroupCompany(GroupCompany model, UserContextModel objUser);
        GroupCompany GetGroupCompany(UserContextModel objUser);
    }
}
