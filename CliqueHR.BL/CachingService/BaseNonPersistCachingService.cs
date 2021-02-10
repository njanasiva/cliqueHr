using CliqueHR.Common.Models;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public abstract class BaseNonPersistCachingService<T> : ICachingService<T>
    {
        public static ObjectCache cacheData = MemoryCache.Default;
        private CacheItemPolicy cacheItemPolicy;
        public BaseNonPersistCachingService()
        {
            cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
        }
        public T GetData(IParamManager param)
        {
            Log.Info("BaseNonPersistCachingService:GetData", "Get Cache Data", string.Format("Key={0}, TimeStamp = {1}", param.Key, param.TimeStamp));
            var data = GetLocal(param.Key);
            var isValid = IsValidCacheData(param, data);
            if (!isValid || data == null)
            {
                ReConstructCache(param);
                Log.Info("BaseNonPersistCachingService:GetData", "ReConstructed Cache Data", string.Format("IsValidCache={0}, CacheData = {1}", isValid, data));
            }
            return GetLocal(param.Key);
        }
        public virtual void UpdateCacheTimestamp(ICacheModelParam param)
        {
            var data = GetLocal(param.Key);
            if (data is ICacheModelComparator)
            {
                var dataObj = data as ICacheModelComparator;
                if (dataObj.DetectChanges(param))
                {
                    Log.Info("BaseNonPersistCachingService:UpdateCacheTimestamp", "Changes Found", string.Format("Key={0}", param.Key));
                    UpdateTimestamp(param);
                }
            }
            else
            {
                Log.Info("BaseNonPersistCachingService:UpdateCacheTimestamp", "Changes Found", string.Format("Key={0}", param.Key));
                UpdateTimestamp(param);
            }
        }

        protected bool AddData(string key, T data)
        {
            cacheData.Set(key, data, cacheItemPolicy);
            return true;
        }
        protected T GetLocal(string key)
        {
            if (cacheData.Contains(key))
            {
                return (T)cacheData.Get(key);
            }
            return default(T);
        }
        protected abstract bool IsValidCacheData(IParamManager param, T data);
        protected abstract void ReConstructCache(IParamManager param);
        protected abstract void UpdateTimestamp(ICacheModelParam param);
        protected Exception GetAccessDeniedStrategy()
        {
            return new AccessDeniedStrategy(new Exception("Access Denied"), Level.BL);
        }

    }
}
