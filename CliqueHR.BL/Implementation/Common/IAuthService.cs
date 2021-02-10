using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface IAuthService
    {
        LoginInfo LoginUser(string CompanyCode, string EmployeeCode, string Password);
        void AddRefreshToken(string companyCode, string token, object tokenTicket);
        void RemoveRefreshToken(string companyCode, string token);
        object GetRefreshTokenTicket(string companyCode, string token);
        string GetCacheConfig(string companyCode, long employeeId, int entityId);
        LoginPageModel GetLoginPageDetails(string companyCode);
    }
}
