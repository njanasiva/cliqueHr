using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface ICacheDbService
    {
        long GetTimeStampValue(string key, int id, string section, string companyCode);
        void UpdateTimeStamp(string key, int id, string section, string companyCode);
        EmployeeGeneralInfoCacheModels GetEmployeeGeneralInfo(int id, string companyCode);
        EntityCacheModels GetEntityCache(int id, string companyCode);
        CompanyCacheModels GetCompanyGeneralInfo(string companyCode);
    }
}
