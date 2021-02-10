using CliqueHR.Common.Application;
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
    public class AuthRepository : IAuthRepository
    {
        private readonly DBHelper _dbHelper;
        public AuthRepository()
        {
            this._dbHelper = new DBHelper();
        }
        public ApplicationResponse CheckCompanyCodeExists(string companyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("CompanyCode", companyCode, DataType.AsString);
                DataTable dt = _dbHelper.GetDataTable(Constants.CommonDB, "[Auth].[CheckCompanyCodeExists]", sqlParameterd);
                return new ApplicationResponse
                {
                    Code = Convert.ToInt32(dt.Rows[0]["Code"]),
                    Message = Convert.ToString(dt.Rows[0]["Message"])
                };
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public ApplicationResponse<LoginInfo> LoginUser(string CompanyCode, string EmployeeCode, string Password)
        {
            try
            {
                ApplicationResponse<LoginInfo> data = null;
                var sqlParameterd = _dbHelper.CreateSqlParameter("EmployeeCode", EmployeeCode, DataType.AsString);
                sqlParameterd = _dbHelper.UpdateSqlParameter("Password", Password, DataType.AsString, sqlParameterd);
                DataSet ds = _dbHelper.GetDataSet(CompanyCode, "[Auth].[LoginUser]", sqlParameterd);
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    var dt = ds.Tables[0];
                    data = new ApplicationResponse<LoginInfo>
                    {
                        Code = Convert.ToInt32(dt.Rows[0]["Code"]),
                        Message = Convert.ToString(dt.Rows[0]["Message"])
                    };
                    if (data.Code == 1)
                    {
                        data.Data = new LoginInfo
                        {
                            CompanyCode = CompanyCode,
                            EmployeeCode = EmployeeCode,
                            EmployeeId = Convert.ToInt64(dt.Rows[0]["EmployeeId"]),
                            EntityId = Convert.ToInt32(dt.Rows[0]["EntityId"]),
                            RoleParam = Convert.ToString(dt.Rows[0]["RoleParam"]),
                            SystemTimeOut = Convert.ToInt32(dt.Rows[0]["SystemTimeOut"] is DBNull ? 0 : dt.Rows[0]["SystemTimeOut"]),
                        };
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public void AddRefreshToken(string companyCode, string token, byte[] tokenTicket)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Token", token, DataType.AsString);
                sqlParameterd = _dbHelper.UpdateSqlParameter("TokenTicket", tokenTicket, DataType.AsByteArray, sqlParameterd);
                _dbHelper.ExecuteNonQuery(companyCode, "[Auth].[AddRefreshToken]", sqlParameterd);
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public void RemoveRefreshToken(string companyCode, string token)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Token", token, DataType.AsString);
                _dbHelper.ExecuteNonQuery(companyCode, "[Auth].[RemoveRefreshToken]", sqlParameterd);
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public byte[] GetRefreshTokenTicket(string companyCode, string token)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Token", token, DataType.AsString);
                var ticket = _dbHelper.ExecuteScalar(companyCode, "[Auth].[GetRefreshTokenTicket]", sqlParameterd);
                return ticket is DBNull ? null : (byte[]) ticket;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public List<string> GetCacheConfig(string companyCode, long employeeId, int entityId)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("EmployeeId", employeeId, DataType.AsInt);
                sqlParameterd = _dbHelper.UpdateSqlParameter("EntityId", entityId, DataType.AsInt, sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(companyCode, "[Cache].[GetCacheConfig]", sqlParameterd);
                if (dt != null && dt.Rows.Count != 0)
                {
                    return dt.AsEnumerable().Select(x => Convert.ToString(x["TimeStamp"])).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
    }
}
