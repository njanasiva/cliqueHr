using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL.Implementation.AdminPanel.Setup
{
    public interface ISetupRepository
    {
        #region Module
        Module GetModuleById(int ModuleId, string DbName, int roleid);
        List<Module> GetAllModule(string DbName, int roleid);
        #endregion Module

        #region SubModule
        List<SubModule> GetSubModuleByModuleId(string ModuleId, string DbName, int roleid);
        List<SubModuleField> GetSubModuleFieldByModuleId(string ModuleId, string DbName, int roleid);
        
        #endregion SubModule

        #region role
        List<Role> GetAllRole(string DbName);
        Role GetRoleById(int RoleId, string DbName);
        ApplicationResponse RoleCreation(CreateRolePostModel model, string DbName);
        ApplicationResponse AssignRole(AssignRole model, string DbName);
        AssignRole GetAssignRoleById(int AssignId, string DbName);
        PaginationData<DTAssignRole> GetAssignRoleList(PaginationModel model, string DbName);
        PaginationData<Role> GetRoleList(PaginationModel model, string DbName);
        #endregion

        #region Get Selected Employee
        PaginationData<DropdownList> GetEmployeeMultiselect(EmployeeFilter employeeFilter,string DbName);
        #endregion Get Selected Employee
    }
    
}
