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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DBHelper _dbHelper;
        public DepartmentRepository()
        {
            this._dbHelper = new DBHelper();
        }
        public PaginationData<Department> GetDepartments(PaginationModel model, string CompanyCode)
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
                var dt = _dbHelper.GetDataTable(CompanyCode, "[Common].[GetDepartmentDetails]", sqlParameterd);
                var paginationData = new PaginationData<Department>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<Department>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public Department GetDepartmentById(int Id, string CompanyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", Id, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Common].[GetDepartmentDetailsById]", sqlParameterd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new Department
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                        Name = Convert.ToString(dt.Rows[0]["Name"]),
                        Code = Convert.ToString(dt.Rows[0]["Code"]),
                        ParentEntityId = Convert.ToInt32(dt.Rows[0]["ParentEntityId"]),
                        ParentOrgUnitId = Convert.ToInt32(dt.Rows[0]["ParentOrgUnitId"]),
                        ParentDepartmentId = Convert.ToInt32(dt.Rows[0]["ParentDepartmentId"]),
                        HODId = Convert.ToInt32(dt.Rows[0]["HodId"]),
                        IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"])
                    };
                }
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
            return null;
        }
        public ApplicationResponse AddUpdateDepartment(Department model, string CompanyCode)
        {
            try
            {
                var parameters = new string[] { "Id", "Name", "Code", "HODId", "ParentEntityId", "ParentOrgUnitId", "ParentDepartmentId", "IsDoNotUse", "CreatedBy", "ModifiedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Common].[AddEditDepartment]", sqlParameterd);
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
        public List<EntityOrgTreeDataModel> GetEntityOrgDeptTreeData(string CompanyCode, int? DepartmentId)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("DepartmentId", DepartmentId, DataType.AsInt);
                return _dbHelper.GetDataTableToList<EntityOrgTreeDataModel>(CompanyCode, "Common.GetEntityOrgDeptTreeData", sqlParameterd);
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public string GetDepartmentCode(string CompanyCode)
        {
            try
            {
                string Series = string.Empty;
                return Convert.ToString(_dbHelper.ExecuteScalar(CompanyCode, "[Company].[GetAutoNumberingSeries]"));
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public ApplicationResponse GetDeptCode(string CompanyCode)
        {
            try
            {
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[GetAutoNumberingSeries]");
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
