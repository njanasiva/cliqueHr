using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;


namespace CliqueHR.BL
{
    public interface IEmployeeGroupService
    {
        void AddEmployeeGroup(EmployeeGroup model, UserContextModel objUser);
        void UpdateEmployeeGroup(EmployeeGroup model, UserContextModel objUser);
        EmployeeGroupByIdResponse GetEmployeeGroupById(int Id, PaginationModel model, UserContextModel objUser);
        PaginationData<EmployeeGroupResponse> GetAllEmployeeGroup(PaginationModel model, UserContextModel objUser);
    }
}
