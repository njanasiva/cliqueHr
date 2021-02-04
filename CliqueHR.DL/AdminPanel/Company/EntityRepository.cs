using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;
using System;
using CliqueHR.Helpers.ExceptionHelper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace CliqueHR.DL
{
    public class EntityRepository : IEntityRepository
    {
        private readonly DBHelper _dbHelper;
        public EntityRepository()
        {
            this._dbHelper = new DBHelper();
        }
        public PaginationData<Entity> GetEntity(PaginationModel model, string CompanyCode)
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
                var dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[GetEntityDetails]", sqlParameterd);
                var paginationData = new PaginationData<Entity>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<Entity>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public Entity GetEntityById(int Id, string CompanyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("TransType", "FETCHBYENTITYID", DataType.AsString);
                _dbHelper.UpdateSqlParameter("Id", Id, DataType.AsInt, sqlParameterd);

                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[EntityDetails]", sqlParameterd);
                if (dt != null && dt.Rows.Count != 0)
                {
                    return new Entity
                    {
                        Id = Id,
                        Name = Convert.ToString(dt.Rows[0]["Name"]),
                        Code = Convert.ToString(dt.Rows[0]["Code"]),
                        TypeId = Convert.ToInt32(dt.Rows[0]["TypeId"]),
                        IncorporationDate = Convert.ToString(dt.Rows[0]["IncorporationDate"]),
                        Address = Convert.ToString(dt.Rows[0]["Address"]),
                        CountryId = Convert.ToInt32(dt.Rows[0]["CountryId"]),
                        StateId = Convert.ToInt32(dt.Rows[0]["StateId"]),
                        CityId = Convert.ToInt32(dt.Rows[0]["CityId"]),
                        PinCode = Convert.ToInt32(dt.Rows[0]["PinCode"]),
                        ContcatNo = Convert.ToString(dt.Rows[0]["ContcatNo"]),
                        WebSite = Convert.ToString(dt.Rows[0]["WebSite"]),
                        PAN = Convert.ToString(dt.Rows[0]["PAN"]),
                        TAN = Convert.ToString(dt.Rows[0]["TAN"]),
                        GSTIN = Convert.ToString(dt.Rows[0]["GSTIN"]),
                        PF = Convert.ToString(dt.Rows[0]["PF"]),
                        ESIC = Convert.ToString(dt.Rows[0]["ESIC"]),
                        Logo = Convert.ToString(dt.Rows[0]["Logo"]),
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

        public ApplicationResponse AddEntity(Entity model, string CompanyCode)
        {
            try
            {
                var parameters = new string[] { "TransType", "Id", "Name", "Code", "TypeId", "IncorporationDate", "Address", "CountryId", "StateId", "CityId", "PinCode", "ContcatNo", "WebSite", "PAN", "TAN", "GSTIN", "PF", "ESIC", "Logo", "IsDoNotUse", "CreatedBy", "ModifiedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[EntityDetails]", sqlParameterd);
                model.Id = Convert.ToInt32(dt.Rows[0][2]);
                return new ApplicationResponse
                {
                    Code = Convert.ToInt32(dt.Rows[0][0]),
                    Message = Convert.ToString(dt.Rows[0][1])

                };
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public ApplicationResponse UpdateEntity(Entity model, string CompanyCode)
        {
            try
            {
                var parameters = new string[] { "TransType", "Id", "Name", "Code", "TypeId", "IncorporationDate", "Address", "CountryId", "StateId", "CityId", "PinCode", "ContcatNo", "WebSite", "PAN", "TAN", "GSTIN", "PF", "ESIC", "Logo", "IsDoNotUse", "CreatedBy", "ModifiedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[EntityDetails]", sqlParameterd);
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

        public string UpdateLogo(int EntityId, string Logo, string CompanyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("TransType", "UPDATELOGO", DataType.AsString);
                _dbHelper.UpdateSqlParameter("Logo", Logo, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Id", EntityId, DataType.AsInt, sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[EntityDetails]", sqlParameterd);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return Convert.ToString(dt.Rows[0]["OldLogo"]);
                    }
                }
                return string.Empty;

            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
    }
}
