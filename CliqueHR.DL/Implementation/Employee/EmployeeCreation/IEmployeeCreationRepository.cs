using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL
{ 
    public interface IEmployeeCreationRepository
    {
        PaginationData<DropdownList> GetAllEmployees(PaginationModel model, string DbName);
    }
}
