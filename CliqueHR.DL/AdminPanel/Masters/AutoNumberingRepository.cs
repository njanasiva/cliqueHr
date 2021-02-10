using CliqueHR.Common.Models;
using CliqueHR.DL.Implementation.AdminPanel.Masters;
using CliqueHR.Helpers.DataHelper;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace CliqueHR.DL.AdminPanel.Masters
{
    public class AutoNumberingRepository : Implementation.AdminPanel.Masters.IAutoNumberingRepository
    {
        private readonly DBHelper _dbHelper;
        public AutoNumberingRepository()
        {
            this._dbHelper = new DBHelper();
        }


        #region LocationAutoNumbers
        public ApplicationResponse AddLocationAutoNumber(AutoNumber model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Prefix", "AppendNumber", "IsDoNotUse", "CreatedBy"};
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_AddLocationAutoNumber]", sqlParameterd);
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

        public ApplicationResponse UpdateLocationAutoNumber(AutoNumber model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "Prefix", "AppendNumber", "IsDoNotUse", "CreatedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_EditLocationAutoNumber]", sqlParameterd);
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

        public AutoNumber GetLocationAutoNumberById(int AutoNumberId, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", AutoNumberId, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetLocationAutoNumberById]", sqlParameterd);
                return new AutoNumber
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    Prefix = Convert.ToString(dt.Rows[0]["Prefix"]),
                    AppendNumber = Convert.ToInt32(dt.Rows[0]["AppendNumber"]),
                    CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]),
                    IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"])
                };
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<AutoNumber> GetAllLocationAutoNumber(string DbName, PaginationModel model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("StartRow", model.StartRow, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsString, sqlParameterd);
                if (model.Sort != null)
                {
                    _dbHelper.UpdateSqlParameter("PropertyName", model.Sort.PropertyName, DataType.AsString, sqlParameterd);
                    _dbHelper.UpdateSqlParameter("Direction", model.Sort.Direction, DataType.AsString, sqlParameterd);
                }
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetAllLocationAutoNumber]", sqlParameterd);
                var paginationData = new PaginationData<AutoNumber>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<AutoNumber>(dt);
                }
                return paginationData;

            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        #endregion
    }
}
