using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface ITimeStampCaching
    {
        long? GetData(string key, long timeStamp, string section, string company, int id);
        void UpdateCacheTimestamp(ICacheModelParam param);
    }
    public class TimeStampCaching : BaseCachingService<long?>, ITimeStampCaching
    {
        private static readonly ITimeStampCaching instance;
        private ICacheDbService cacheDbService;
        public TimeStampCaching()
        {
            cacheDbService = new CacheDbService();
        }
        public long? GetData(string key, long timeStamp, string section, string company, int id)
        {
            return this.GetData(new TimestampParams { Key = key, TimeStamp = timeStamp, Section = section, Company = company, Id = id });
        }

        static TimeStampCaching()
        {
            instance = new TimeStampCaching();
        }

        public static ITimeStampCaching Instance {
            get
            {
                return instance;
            }
        }

        protected override bool IsValidCacheData(IParamManager param, long? data)
        {
            var l_param = param as TimestampParams;
            if (data == null || data < l_param.TimeStamp)
            {
                return false;
            }
            else if (data > l_param.TimeStamp)
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
            // Get Data From DataBase and add into cache
            var l_param = param as TimestampParams;
            var timeStamp = cacheDbService.GetTimeStampValue(l_param.Key, l_param.Id, l_param.Section, l_param.Company);
            this.AddData(l_param.Key, timeStamp);
        }

        public override long? GetData(IParamManager param)
        {
            Log.Info("TimeStampCaching:GetData", "Get Cache Data", string.Format("Key={0}, TimeStamp = {1}", param.Key, param.TimeStamp));
            var data = GetLocal(param.Key);
            var isValid = IsValidCacheData(param, data);
            if (!isValid || data == null)
            {
                ReConstructCache(param);
                Log.Info("TimeStampCaching:GetData", "ReConstructed Cache Data", string.Format("IsValidCache={0}, CacheData = {1}", isValid, data));
            }
            return data;
        }

        protected override void UpdateTimestamp(ICacheModelParam param)
        {
            var paramObj = param as TimestampModelParam;
            // update individual timestamp in database;
            cacheDbService.UpdateTimeStamp(paramObj.Key, paramObj.Id, paramObj.Section, paramObj.Company);
        }
        public override void UpdateCacheTimestamp(ICacheModelParam param)
        {
            var paramObj = param as TimestampModelParam;
            var lData = GetLocal(paramObj.Key);
            if (lData == null || lData != paramObj.Data)
            {
                Log.Info("TimeStampCaching:UpdateCacheTimestamp", "Changes Found Updating timestamp", string.Format("Key={0}", param.Key));
                UpdateTimestamp(param);
            }
        }
    }
}
