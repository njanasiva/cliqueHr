using CliqueHR.Common;
using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface IEmployeeGeneralInfoCaching
    {
        EmployeeGeneralInfoCacheModels GetData(int employeeId, long timeStamp, string company);
        void UpdateCacheTimestamp(ICacheModelParam param);
    }
    public class EmployeeGeneralInfoCaching : BaseNonPersistCachingService<EmployeeGeneralInfoCacheModels>, IEmployeeGeneralInfoCaching
    {
        private static readonly IEmployeeGeneralInfoCaching instance;
        private ICacheDbService cacheDbService;
        public EmployeeGeneralInfoCaching()
        {
            cacheDbService = new CacheDbService();
        }
        static EmployeeGeneralInfoCaching()
        {
            instance = new EmployeeGeneralInfoCaching();
        }

        public static IEmployeeGeneralInfoCaching Instance
        {
            get
            {
                return instance;
            }
        }
        public EmployeeGeneralInfoCacheModels GetData(int employeeId, long timeStamp, string company)
        {
            return this.GetData(new EmployeeGeneralInfoParams { Key = employeeId.ToString(), TimeStamp = timeStamp, EmployeeId = employeeId, Company = company});
        }

        protected override bool IsValidCacheData(IParamManager param, EmployeeGeneralInfoCacheModels data)
        {
            var l_param = param as EmployeeGeneralInfoParams;
            var timeStamp = TimeStampCaching.Instance.GetData(param.Company+"_"+ Cache.Constants.EmployeeGeneralInfo + l_param.Key, l_param.TimeStamp, Cache.Constants.EmployeeGeneralInfo, 
                param.Company, Convert.ToInt32(l_param.Key));
            if (timeStamp == null || timeStamp < l_param.TimeStamp)
            {
                return false;
            }
            else if (timeStamp > l_param.TimeStamp)
            {
                throw GetAccessDeniedStrategy();
            }
            else
            {
                return true;
            }
        }
        protected override void ReConstructCache(IParamManager param)
        {
            var l_param = param as EmployeeGeneralInfoParams;
            // Database call
            var data = GetEmployeeGeneralInfo(l_param.EmployeeId, l_param.Company);
            AddData(param.Key, data);
        }
        protected override void UpdateTimestamp(ICacheModelParam param)
        {
            TimeStampCaching.Instance.UpdateCacheTimestamp(new TimestampModelParam { 
                Key = param.Company+"_"+ Cache.Constants.EmployeeGeneralInfo + param.Key, 
                Section = Cache.Constants.EmployeeGeneralInfo, 
                Company = param.Company,
                Id = Convert.ToInt32(param.Key)});
        }

        private EmployeeGeneralInfoCacheModels GetEmployeeGeneralInfo(int employeeId, string company)
        {
            return cacheDbService.GetEmployeeGeneralInfo(employeeId, company);
        }
    }
}
