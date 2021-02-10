using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL.CachingService
{
    public interface ICompanyCaching
    {
        CompanyCacheModels GetData(long timeStamp, string Company);
        void UpdateCacheTimestamp(ICacheModelParam param);
    }
    public class CompanyCaching : BaseCachingService<CompanyCacheModels>, ICompanyCaching
    {
        private static readonly ICompanyCaching instance;
        private ICacheDbService cacheDbService;

        static CompanyCaching()
        {
            instance = new CompanyCaching();
        }
        public CompanyCaching()
        {
            cacheDbService = new CacheDbService();
        }

        public static ICompanyCaching Instance
        {
            get
            {
                return instance;
            }
        }
        public CompanyCacheModels GetData(long timeStamp, string company)
        {
            return this.GetData(new CompanyParams { Key = company, TimeStamp = timeStamp, Company = company });
        }

        protected override bool IsValidCacheData(IParamManager param, CompanyCacheModels data)
        {
            var l_param = param as CompanyParams;
            var timeStamp = TimeStampCaching.Instance.GetData(l_param.Key + "_" + Cache.Constants.Company, l_param.TimeStamp, Cache.Constants.Company, param.Company, Convert.ToInt32(l_param.Key));
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
            var l_param = param as CompanyParams;
            var data = GetCompanyCache(l_param.Company);
            AddData(l_param.Key, data);
        }
        protected override void UpdateTimestamp(ICacheModelParam param)
        {
            TimeStampCaching.Instance.UpdateCacheTimestamp(new TimestampModelParam
            {
                Key = param.Key + "_" + Cache.Constants.Company,
                Section = Cache.Constants.Company,
                Company = param.Company,
                Id = Convert.ToInt32(param.Key)
            });
        }

        private CompanyCacheModels GetCompanyCache(string company)
        {
            return cacheDbService.GetCompanyGeneralInfo(company);
        }
    }
}
