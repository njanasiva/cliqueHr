using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface IEmployeeCreationService
    {
        PaginationData<DropdownList> GetAllEmployees(UserContextModel objUser, PaginationModel paginationModel);
        PaginationData<DropdownList> GetEmployeeMultiselect(UserContextModel objUser,EmployeeFilter employeeFilter);
    }
}
