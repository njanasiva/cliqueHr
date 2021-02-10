using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;
using System;
using CliqueHR.Helpers.ExceptionHelper;
using System.Collections.Generic;
using System.Data;

namespace CliqueHR.DL
{
    public class AutoNumberingRepository : IAutoNumberingRepository
    {
        private readonly DBHelper _dbHelper;
        public AutoNumberingRepository()
        {
            this._dbHelper = new DBHelper();
        }
        public AutoNumbering GetAutoNumbering(string CompanyCode)
        {
            try
            {

                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[GetAutoNumberingDetails]");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return new AutoNumbering
                        {
                            AppendNumber = Convert.ToInt32(dt.Rows[0]["AppendNumber"]),
                            Prefix = Convert.ToString(dt.Rows[0]["Prefix"])
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

        public ApplicationResponse AddUpdateAutoNumbering(AutoNumbering model, string CompanyCode)
        {
            try
            {
                var parameters = new string[] { "Id", "Prefix", "AppendNumber", "CreatedBy", "ModifiedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[AutoNumberingDetails]", sqlParameterd);
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
