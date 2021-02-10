using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL
{
    public interface IAuthRepository
    {
        ApplicationResponse CheckCompanyCodeExists(string companyCode);
        ApplicationResponse<LoginInfo> LoginUser(string CompanyCode, string EmployeeCode, string Password);
        void AddRefreshToken(string companyCode, string token, byte[] tokenTicket);
        void RemoveRefreshToken(string companyCode, string token);
        byte[] GetRefreshTokenTicket(string companyCode, string token);
        List<string> GetCacheConfig(string companyCode, long employeeId, int entityId);
    }
}
