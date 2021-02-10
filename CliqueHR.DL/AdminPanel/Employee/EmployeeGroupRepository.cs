using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;
namespace CliqueHR.DL
{
    public class EmployeeGroupRepository : IEmployeeGroupRepository

    {

        private readonly DBHelper _dbHelper;
        public EmployeeGroupRepository()
        {
            this._dbHelper = new DBHelper();
        }

        public ApplicationResponse AddEmployeeGroup(EmployeeGroup model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "EmployeeGroupCode", "EmployeesMap", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                this._dbHelper.UpdateSqlParameterByDataTable("EmployeesMap", model.DTEmpGroupEmpIdMapping, "UT_Common_EmpGroupEmployeeMapping", sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[AddEditEmployeeGroup]", sqlParameterd);
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

        public ApplicationResponse UpdateEmployeeGroup(EmployeeGroup model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "EmployeeGroupCode", "EmployeesMap", "ModifiedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                this._dbHelper.UpdateSqlParameterByDataTable("EmployeesMap", model.DTEmpGroupEmpIdMapping, "UT_Common_EmpGroupEmployeeMapping", sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[AddEditEmployeeGroup]", sqlParameterd);
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

        public EmployeeGroupByIdResponse GetEmployeeGroupById(int Id, PaginationModel model, string DbName)
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
                _dbHelper.UpdateSqlParameter("Id", Id, DataType.AsInt, sqlParameterd);
                var ds = _dbHelper.GetDataSet(DbName, "[Common].[GetEmployeeGroupById]", sqlParameterd);
                var paginationData = new PaginationData<EmployeeGrid>();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[1].Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(ds.Tables[1].Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<EmployeeGrid>(ds.Tables[1]);
                }
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    return new EmployeeGroupByIdResponse
                    {
                        EmployeeGroupCode = Convert.ToString(ds.Tables[0].Rows[0]["EmployeeGroup"]),
                        Id = Id,
                        EmployeeGrid = paginationData,
                        CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedBy"]),
                        IsDoNotUse = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDoNotUse"])
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<EmployeeGroupResponse> GetAllEmployeeGroup(PaginationModel model, string DbName)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[GetEmployeeGroup]", sqlParameterd);
                var paginationData = new PaginationData<EmployeeGroupResponse>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<EmployeeGroupResponse>(dt);
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
