using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;

namespace CliqueHR.DL
{
    public interface IDepartmentRepository
    {
        ApplicationResponse AddUpdateDepartment(Department model, string CompanyCode);
        PaginationData<Department> GetDepartments(PaginationModel model, string CompanyCode);
        Department GetDepartmentById(int Id, string CompanyCode);
        List<EntityOrgTreeDataModel> GetEntityOrgDeptTreeData(string CompanyCode, int? DepartmentId);
        string GetDepartmentCode(string CompanyCode);

        ApplicationResponse GetDeptCode(string CompanyCode);
    }
}
