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
    public class OrgUnitsRepository : IOrgUnitsRepository
    {
        private readonly DBHelper _dbHelper;
        public OrgUnitsRepository()
        {
            this._dbHelper = new DBHelper();
        }

        public PaginationData<OrgUnits> GetOrgUnits(PaginationModel model, string CompanyCode)
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
                var dt = _dbHelper.GetDataTable(CompanyCode, "[Common].[GetOrgUnitsDetails]", sqlParameterd);
                var paginationData = new PaginationData<OrgUnits>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<OrgUnits>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public OrgUnits GetOrgUnitsById(int Id, string CompanyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", Id, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Common].[GetOrgUnitDetailsById]", sqlParameterd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new OrgUnits
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                        Name = Convert.ToString(dt.Rows[0]["Name"]),
                        Code = Convert.ToString(dt.Rows[0]["Code"]),
                        UnitHead = Convert.ToString(dt.Rows[0]["UnitHead"]),
                        UnitHeadId = Convert.ToInt32(dt.Rows[0]["UnitHeadId"]),
                        IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"]),
                        ParentEntityId = Convert.ToInt32(dt.Rows[0]["ParentEntityId"]),
                        ParentOrgUnitId= Convert.ToInt32(dt.Rows[0]["ParentOrgUnitId"])
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
        public ApplicationResponse AddUpdateOrgUnits(OrgUnits model, string CompanyCode)
        {
            try
            {
                var parameters = new string[] { "Id", "Name", "Code", "UnitHeadId", "ParentEntityId", "ParentOrgUnitId", "IsDoNotUse", "CreatedBy", "ModifiedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Common].[AddEditOrgUnits]", sqlParameterd);
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
      

        public List<EntityOrgTreeDataModel> GetEntityOrgTreeData(string CompanyCode, int? OrUnitId)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("OrUnitId", OrUnitId, DataType.AsInt);
                return _dbHelper.GetDataTableToList<EntityOrgTreeDataModel>(CompanyCode, "Common.GetEntityOrgTreeData", sqlParameterd);
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
    }
}
