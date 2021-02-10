using System;
using CliqueHR.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface IDepartmentService
    {
        void AddUpdateDepartment(Department model, UserContextModel objUser);
        PaginationData<Department> GetDepartments(PaginationModel model, UserContextModel objUser);
        Department GetDepartmentById(int Id, UserContextModel objUser);
        List<EntityOrgunitTreeVM> GetEntityOrgunitDeptTreeData(UserContextModel objUser, int? DepartmentId);
        string GetDepartmentCode(UserContextModel objUser);

        string GetDeptCode(UserContextModel objUser);
    }
}
