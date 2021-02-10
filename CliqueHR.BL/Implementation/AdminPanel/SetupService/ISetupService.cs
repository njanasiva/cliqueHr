using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL.Implementation.AdminPanel.SetupService
{
    public interface ISetupService
    {
        #region Module
        Module GetModuleById(int roleid,int ModuleId, UserContextModel objUser);
        List<Module> GetAllModule(int roleid, UserContextModel objUser);
        #endregion Module

        #region SubModule
        List<SubModule> GetSubModuleByModuleId(int roleid, string ModuleId, UserContextModel objUser);
        List<SubModuleField> GetSubModuleFieldByModuleId(int roleid, string ModuleId, UserContextModel objUser);
        
        #endregion SubModule

        #region role
        List<Role> GetAllRole(UserContextModel objUser);
        Role GetRoleById(int RoleId, UserContextModel objUser);
        ApplicationResponse RoleCreation(CreateRolePostModel model, UserContextModel objUser);
        ApplicationResponse AssignRole(AssignRole model, UserContextModel objUser);
        AssignRole GetAssignRoleById(int AssignId, UserContextModel objUser);
        PaginationData<DTAssignRole> GetAssignRoleList(PaginationModel param, UserContextModel objUser);
        PaginationData<Role> GetRoleList(PaginationModel param, UserContextModel objUser);
        #endregion
    }
}
