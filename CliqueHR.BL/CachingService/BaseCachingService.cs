using CliqueHR.Common.Models;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Logger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public abstract class BaseCachingService<T> : ICachingService<T>
    {
        public static ConcurrentDictionary<string, object> cacheData = new ConcurrentDictionary<string, object>();
        public BaseCachingService()
        {

        }


        protected bool AddData(string key, T data)
        {
            if (cacheData.ContainsKey(key))
            {
                cacheData[key] = data;
                return true;
            }
            else
            {
                return cacheData.TryAdd(key, data);
            }
        }
        protected T GetLocal(string key)
        {
            object dataObj;
            if (cacheData.TryGetValue(key, out dataObj))
            {
                return (T)dataObj;
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

        public virtual void UpdateCacheTimestamp(ICacheModelParam param)
        {
            var data = GetLocal(param.Company + "-" + param.Key);
            if (data is ICacheModelComparator)
            {
                var dataObj = data as ICacheModelComparator;
                if (dataObj.DetectChanges(param))
                {
                    Log.Info("BaseCachingService:UpdateCacheTimestamp", "Changes Found Updating timestamp", string.Format("Key={0}", param.Key));
                    UpdateTimestamp(param);
                }
            }
            else
            {
                Log.Info("BaseCachingService:UpdateCacheTimestamp", "Changes Found Updating timestamp", string.Format("Key={0}", param.Key));
                UpdateTimestamp(param);
            }
        }
        public virtual T GetData(IParamManager param)
        {
            Log.Info("BaseCachingService:GetData", "Get Cache Data", string.Format("Key={0}, TimeStamp = {1}", param.Key, param.TimeStamp));
            var data = GetLocal(param.Key);
            object dataObj;
            var isValid = IsValidCacheData(param, data);
            if (!isValid || data == null)
            {
                ReConstructCache(param);
                Log.Info("BaseCachingService:GetData", "ReConstructed Cache Data", string.Format("IsValidCache={0}, CacheData = {1}", isValid, data));
            }
            if (cacheData.TryGetValue(param.Key, out dataObj))
            {
                return (T)dataObj;
            }
            return default(T);
        }

    }
}
