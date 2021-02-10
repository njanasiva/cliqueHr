using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;
using System;
using CliqueHR.Helpers.ExceptionHelper;
using System.Collections.Generic;
using System.Data;

namespace CliqueHR.DL
{
    public class SecuritySettingsRepository : ISecuritySettingsRepository
    {
        private readonly DBHelper _dbHelper;
        public SecuritySettingsRepository()
        {
            this._dbHelper = new DBHelper();
        }
        public SecuritySettings GetSecuritySettings(SecuritySettings model, string CompanyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("TransType", "FETCH", DataType.AsString);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[SecuritySetting]", sqlParameterd);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return new SecuritySettings
                        {
                            PasswordExpiryIndays = Convert.ToInt32(dt.Rows[0]["PasswordExpiryIndays"]),
                            SessionTimeOutInMins = Convert.ToInt32(dt.Rows[0]["SessionTimeOutInMins"]),
                            HideMobileNumberFromEd = Convert.ToString(dt.Rows[0]["HideMobileNumberFromEd"]),
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
            return null;
        }

        public ApplicationResponse AddUpdateSecuritySettings(SecuritySettings model, string CompanyCode)
        {
            try
            {
                var parameters = new string[] { "TransType", "PasswordExpiryIndays", "SessionTimeOutInMins", "HideMobileNumberFromEd", "CreatedBy", "ModifiedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[SecuritySetting]", sqlParameterd);
                return new ApplicationResponse
                {
                    Code = Convert.ToInt32(dt.Rows[0][0]),
                    Message = Convert.ToString(dt.Rows[0][1]),
                };
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
    }
}

