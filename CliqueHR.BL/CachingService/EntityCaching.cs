using CliqueHR.Common;
using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface IEntityCaching
    {
        EntityCacheModels GetData(int Id, long timeStamp, string Company);
        void UpdateCacheTimestamp(ICacheModelParam param);
    }
    public class EntityCaching : BaseCachingService<EntityCacheModels>, IEntityCaching
    {
        private static readonly IEntityCaching instance;
        private ICacheDbService cacheDbService;

        static EntityCaching()
        {
            instance = new EntityCaching();
        }
        public EntityCaching()
        {
            cacheDbService = new CacheDbService();
        }

        public static IEntityCaching Instance
        {
            get
            {
                return instance;
            }
        }
        public EntityCacheModels GetData(int Id, long timeStamp, string company)
        {
            return this.GetData(new EntityParams { Key = company + "-" + Id.ToString(), TimeStamp = timeStamp, Company = company, Id = Id });
        }

        protected override bool IsValidCacheData(IParamManager param, EntityCacheModels data)
        {
            var l_param = param as EntityParams;
            var timeStamp = TimeStampCaching.Instance.GetData(param.Company + "_" + Cache.Constants.Entity + l_param.Key, l_param.TimeStamp, Cache.Constants.Entity, param.Company, Convert.ToInt32(l_param.Key));
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
            var l_param = param as EntityParams;
            var data = GetEntityCache(l_param.Id, l_param.Company);
            AddData(l_param.Key, data);
        }
        protected override void UpdateTimestamp(ICacheModelParam param)
        {
            TimeStampCaching.Instance.UpdateCacheTimestamp(new TimestampModelParam
            {
                Key = param.Company + "_" + Cache.Constants.Entity + param.Key,
                Section = Cache.Constants.Entity,
                Company = param.Company,
                Id = Convert.ToInt32(param.Key)
            });
        }

        private EntityCacheModels GetEntityCache(int entityId, string company)
        {
            return cacheDbService.GetEntityCache(entityId, company);
        }
    }
}
