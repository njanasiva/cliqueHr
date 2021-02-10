using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public class CacheDbService : ICacheDbService
    {
        private ICacheDbRepository cacheDbRepository;

        public CacheDbService()
        {
            cacheDbRepository = new CacheDbRepository();
        }

        public CompanyCacheModels GetCompanyGeneralInfo(string companyCode)
        {
            CompanyCacheModels companyCacheModels = null;
            try
            {
                var data = cacheDbRepository.GetCompanyGeneralInfo(companyCode);
                if(data != null && data.Tables.Count > 0)
                {
                    companyCacheModels = new CompanyCacheModels();
                    var pageSettingDt = data.Tables[0];
                    if (pageSettingDt != null && pageSettingDt.Rows.Count > 0)
                    {
                        companyCacheModels.pageSettingImages = new List<PageSettingImages>();
                        foreach (DataRow row in pageSettingDt.Rows)
                        {
                            companyCacheModels.pageSettingImages.Add(new PageSettingImages
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                ImagePath = Convert.ToString(row["ImagePath"]),
                                ImageType = Convert.ToString(row["ImageType"])
                            });
                        }
                    }
                }
                return companyCacheModels;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public EmployeeGeneralInfoCacheModels GetEmployeeGeneralInfo(int id, string companyCode)
        {
            try
            {
                return cacheDbRepository.GetEmployeeGeneralInfo(id, companyCode);
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public EntityCacheModels GetEntityCache(int id, string companyCode)
        {
            try
            {
                return cacheDbRepository.GetEntityCache(id, companyCode);
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public long GetTimeStampValue(string key, int id, string section, string companyCode)
        {
            try
            {
                return cacheDbRepository.GetTimeStampValue(key, id, section, companyCode);
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateTimeStamp(string key, int id, string section, string companyCode)
        {
            try
            {
                cacheDbRepository.UpdateTimeStamp(key, id, section, companyCode);
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
    }
}
