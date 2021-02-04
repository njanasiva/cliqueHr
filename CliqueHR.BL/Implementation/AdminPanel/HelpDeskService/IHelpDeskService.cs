using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL 
{
    public interface IHelpDeskService
    {
        DataSet GetHelpDiskMaster(UserContextModel objUser, GetHelpDiskMaster getHelpDiskMaster);
        PaginationData<AddModifyCategory> AddModifyCategory(UserContextModel objUser, AddModifyCategory addModifyCategory);
    }
}
