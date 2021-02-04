using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL
{
    public interface IHelpDeskRepository
    {
        DataSet GetHelpDiskMaster(string DBName, GetHelpDiskMaster model);
        PaginationData<AddModifyCategory> AddModifyCategory(string DBName, AddModifyCategory model);
    }
}
