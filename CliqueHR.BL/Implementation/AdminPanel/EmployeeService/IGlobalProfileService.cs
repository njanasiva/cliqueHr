using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL.Implementation.AdminPanel.Employee
{
    public interface IGlobalProfileService
    {
        ApplicationResponse UpdateGlobalProfile(List<GlobalProfileModel> model, UserContextModel objUser);
        List<GlobalProfileModel> GetAllGlobalProfile(UserContextModel objUser);
        GlobalProfileModel GetGlobalProfileById(int ID, UserContextModel objUser);
        void SaveProfileSetting(ProfileFieldSetting model, UserContextModel objUser);
        //void UpdateProfileSetting(ProfileFieldSetting model, UserContextModel objUser);
        ProfileFieldSetting GetProfileSettingById(int ID, UserContextModel objUser);
        ProfileFieldSetting GetProfileSetting(UserContextModel objUser);
    }
}
