using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL
{
    public interface IEmployeeGroupRepository
    {

        ApplicationResponse AddEmployeeGroup(EmployeeGroup model, string CompanyCode);
        ApplicationResponse UpdateEmployeeGroup(EmployeeGroup model, string CompanyCode);
        EmployeeGroupByIdResponse GetEmployeeGroupById(int Id, PaginationModel model, string CompanyCode);
        PaginationData<EmployeeGroupResponse> GetAllEmployeeGroup(PaginationModel model, string CompanyCode);

    }
}
