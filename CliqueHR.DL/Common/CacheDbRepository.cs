using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL
{
    public class CacheDbRepository: ICacheDbRepository
    {
        private readonly DBHelper _dbHelper;
        public CacheDbRepository()
        {
            this._dbHelper = new DBHelper();
        }

        public DataSet GetCompanyGeneralInfo(string companyCode)
        {
            try
            {
                return _dbHelper.GetDataSet(companyCode, "[Cache].[GetCompanyGeneralInfo]");
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public EmployeeGeneralInfoCacheModels GetEmployeeGeneralInfo(int id, string companyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", id, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(companyCode, "[Cache].[GetEmployeeGeneralInfo]", sqlParameterd);
                if (dt.Rows.Count != 0)
                {
                    return new EmployeeGeneralInfoCacheModels
                    {
                        Id = id,
                        EmployeeCode = Convert.ToString(dt.Rows[0]["EmployeeCode"]),
                        Salutation = Convert.ToString(dt.Rows[0]["Salutation"]),
                        FirstName = Convert.ToString(dt.Rows[0]["FirstName"]),
                        MiddleName = Convert.ToString(dt.Rows[0]["MiddleName"]),
                        LastName = Convert.ToString(dt.Rows[0]["LastName"]),
                        DateOfJoining = Convert.ToDateTime(dt.Rows[0]["DateOfJoining"]),
                        EntityId = Convert.ToInt32(dt.Rows[0]["EntityId"]),
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public EntityCacheModels GetEntityCache(int id, string companyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", id, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(companyCode, "[Cache].[GetEntityCache]", sqlParameterd);
                if (dt.Rows.Count != 0)
                {
                    return new EntityCacheModels
                    {
                        Id = id,
                        Name = Convert.ToString(dt.Rows[0]["Name"]),
                        Address = Convert.ToString(dt.Rows[0]["Address"]),
                        Code = Convert.ToString(dt.Rows[0]["Code"]),
                        ContcatNo = Convert.ToString(dt.Rows[0]["ContcatNo"]),
                        Logo = Convert.ToString(dt.Rows[0]["Logo"]),
                        CityId = Convert.ToInt32(dt.Rows[0]["CityId"]),
                        CountryId = Convert.ToInt32(dt.Rows[0]["CountryId"]),
                        StateId = Convert.ToInt32(dt.Rows[0]["StateId"]),
                        IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"]),
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public long GetTimeStampValue(string key, int id, string section, string companyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("key", key, DataType.AsString);
                sqlParameterd = _dbHelper.UpdateSqlParameter("Id", id, DataType.AsInt, sqlParameterd);
                sqlParameterd = _dbHelper.UpdateSqlParameter("section", section, DataType.AsString, sqlParameterd);
                long timestamp = Convert.ToInt64(_dbHelper.ExecuteScalar(companyCode, "[Cache].[GetTimeStampValue]", sqlParameterd));
                return timestamp;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateTimeStamp(string key, int id, string section, string companyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("key", key, DataType.AsString);
                sqlParameterd = _dbHelper.UpdateSqlParameter("Id", id, DataType.AsInt, sqlParameterd);
                sqlParameterd = _dbHelper.UpdateSqlParameter("section", section, DataType.AsString, sqlParameterd);
                _dbHelper.ExecuteNonQuery(companyCode, "[Cache].[UpdateTimeStamp]", sqlParameterd);
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
    }
}
