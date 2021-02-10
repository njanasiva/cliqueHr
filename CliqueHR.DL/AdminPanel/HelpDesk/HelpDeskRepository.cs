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
        public class HelpDeskRepository : IHelpDeskRepository
        {

            private readonly DBHelper _dbHelper;
            public HelpDeskRepository()
            {
                this._dbHelper = new DBHelper();
            }

        public DataSet GetHelpDiskMaster(string DbName, GetHelpDiskMaster model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt); 
                _dbHelper.UpdateSqlParameter("ActionTypeId", model.ActionTypeId, DataType.AsInt, sqlParameterd);
                var ds = _dbHelper.GetDataSet(DbName, "[GetHelpDiskMaster]", sqlParameterd);
                var paginationData = new PaginationData<Probation>();
                if (ds != null)
                {
                    // paginationData.Total = Convert.ToInt32(dt.Rows[0]["Total"]);
                    // paginationData.Data = _dbHelper.ConvertDataTableToList<Probation>(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }


        public PaginationData<AddModifyCategory> AddModifyCategory(string DbName, AddModifyCategory model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("CategoryName", model.CategoryName, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Prefix", model.Prefix, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Description", model.Description, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("CategoryLeadId", model.CategoryLeadId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("TeamMembers", model.TeamMembers, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsDoNotUse", model.IsDoNotUse, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("CategoryId", model.CategoryId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("StartRow", model.StartRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsInt, sqlParameterd); 
                _dbHelper.UpdateSqlParameter("ActionTypeId", model.ActionTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsString, sqlParameterd);
                if (model.Sort != null)
                {
                    _dbHelper.UpdateSqlParameter("PropertyName", model.Sort.PropertyName, DataType.AsString, sqlParameterd);
                    _dbHelper.UpdateSqlParameter("Direction", model.Sort.Direction, DataType.AsString, sqlParameterd);
                }
                var dt = _dbHelper.GetDataTable(DbName, "[AddModifyCategory]", sqlParameterd);
                var paginationData = new PaginationData<AddModifyCategory>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["Total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<AddModifyCategory>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }


    }
}
