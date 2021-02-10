using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL
{
    public class EmployeeCreationRepository : IEmployeeCreationRepository
    {
        private readonly DBHelper _dbHelper;
        public EmployeeCreationRepository()
        {
            this._dbHelper = new DBHelper();
        }
        public PaginationData<DropdownList> GetAllEmployees(PaginationModel model, string DbName)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[GetAllEmployeeSearch]", sqlParameterd);
                var paginationData = new PaginationData<DropdownList>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<DropdownList>(dt);
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
