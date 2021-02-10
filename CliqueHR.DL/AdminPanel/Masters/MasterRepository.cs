using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CliqueHR.DL
{
    public class MasterRepository : IMasterRepository
    {
        private readonly DBHelper _dbHelper;
        public MasterRepository()
        {
            this._dbHelper = new DBHelper();
        }

        #region Currency
        public List<Currancy> GetAllCurrency(string DbName)
        {
            try
            {
                return _dbHelper.GetDataTableToList<Currancy>(DbName, "[Common].[Master_GetAllCurrency]");
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<CurrancyMapping> GetAllCurrencyMapping(string DbName, PaginationModel model)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[GetAllCurrencyMapping]", sqlParameterd);
                var paginationData = new PaginationData<CurrancyMapping>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<CurrancyMapping>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public ApplicationResponse AddCurrencyMapping(CurrancyMapping model, string DbName)
        {
            try
            {
                var parameters = new string[] { "CurrencyId", "IsDefault", "IsDoNotUse", "CreatedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[AddCurrencyMapping]", sqlParameterd);
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
        public CurrancyMapping GetCurrencyMappingById(int CurrancyMapId, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", CurrancyMapId, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[GetCurrencyById]", sqlParameterd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new CurrancyMapping
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                        CurrencyId = Convert.ToInt32(dt.Rows[0]["CurrencyId"]),
                        CurrencyCode = Convert.ToString(dt.Rows[0]["CurrencyCode"]),
                        CurrencyDesc = Convert.ToString(dt.Rows[0]["CurrencyDesc"]),
                        IsDefault = Convert.ToBoolean(dt.Rows[0]["IsDefault"]),
                        CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                        CreatedDate = Convert.ToString(dt.Rows[0]["CreatedDate"]),
                        IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"])
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
        public ApplicationResponse UpdateCurrencyMapping(CurrancyMapping model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "CurrencyId", "IsDefault", "IsDoNotUse", "CreatedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[EditCurrencyMapping]", sqlParameterd);
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
        #endregion
        #region Region code
        public ApplicationResponse AddRegion(RegionModel model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Name", "StateId", "CityId", "RegionHead", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_AddRegion]", sqlParameterd);
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

        public ApplicationResponse UpdateRegion(RegionModel model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "Name", "StateId", "CityId", "RegionHead", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_EditRegion]", sqlParameterd);
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

        public RegionModel GetRegionById(int Id, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", Id, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetRegionById]", sqlParameterd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new RegionModel
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                        Name = Convert.ToString(dt.Rows[0]["Name"]),
                        StateId = Convert.ToInt32(dt.Rows[0]["StateId"]),
                        CityId = Convert.ToInt32(dt.Rows[0]["CityId"]),
                        RegionHead = Convert.ToInt64(dt.Rows[0]["RegionHead"]),
                        CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                        CreatedDate = Convert.ToString(dt.Rows[0]["CreatedDate"]),
                        IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"])
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

        public PaginationData<RegionModel> GetAllRegionData(PaginationModel model, string DbName)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetAllRegion]", sqlParameterd);
                var paginationData = new PaginationData<RegionModel>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<RegionModel>(dt);
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
        #region Designation
        public ApplicationResponse AddDesignation(DesignationModel model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Name", "Code", "AttributesMap", "CreatedBy", "IsDoNotUse" };

                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                this._dbHelper.UpdateSqlParameterByDataTable("AttributesMap", model.DTAttributesMap, "UT_Common_DesignationAttrMapping", sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_AddDesignation]", sqlParameterd);
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

        public ApplicationResponse UpdateDesignation(DesignationModel model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "Name", "Code", "AttributesMap", "CreatedBy", "IsDoNotUse" };

                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                this._dbHelper.UpdateSqlParameterByDataTable("AttributesMap", model.DTAttributesMap, "UT_Common_DesignationAttrMapping", sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_EditDesignation]", sqlParameterd);
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

        public DesignationResponseModel GetDesignationById(int Id, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", Id, DataType.AsInt);
                DataSet ds = _dbHelper.GetDataSet(DbName, "[Common].[Master_GetDesignationById]", sqlParameterd);
                if (ds == null && ds.Tables.Count == 0)
                {
                    return null;
                }
                var dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    var response = new DesignationResponseModel
                    {
                        DesignationID = Convert.ToInt32(dt.Rows[0]["DesignationID"]),
                        Designation = Convert.ToString(dt.Rows[0]["Designation"]),
                        DesignationCode = Convert.ToString(dt.Rows[0]["DesignationCode"]),
                        CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                        IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"]),
                        entityOrgunitDepartment = new List<EntityOrgunitDepartmentModel>()
                    };
                    if (ds.Tables.Count > 1)
                    {
                        var treeDt = ds.Tables[1];
                        if (treeDt != null && treeDt.Rows.Count > 0)
                        {
                            var data = _dbHelper.ConvertDataTableToList<EntityOrgunitDepartmentModel>(treeDt);
                            response.entityOrgunitDepartment = data;
                        }
                    }
                    return response;
                }
                return null;

            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<DesignationResponseModel> GetAllDesignation(PaginationModel model, string DbName)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetAllDesignation]", sqlParameterd);
                var paginationData = new PaginationData<DesignationResponseModel>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<DesignationResponseModel>(dt);
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

        #region Functional Role
        public ApplicationResponse AddFunctionalRole(FunctionalRole model, string DbName)
        {
            try
            {
                var parameters = new string[] { "FRoleName", "AttachmentFile", "FRoleCode", "FRoleDesc", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_AddFunctionalRole]", sqlParameterd);
                model.Id = Convert.ToInt32(dt.Rows[0][2]);
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

        public ApplicationResponse UpdateFunctionalRole(FunctionalRole model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "FRoleName", "AttachmentFile", "FRoleDesc", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_EditFunctionalRole]", sqlParameterd);
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

        public FunctionalRole GetFunctionalRoleById(int Id, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", Id, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetFunctionalRoleById]", sqlParameterd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new FunctionalRole
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                        FRoleName = Convert.ToString(dt.Rows[0]["FRoleName"]),
                        FRoleCode = Convert.ToString(dt.Rows[0]["FRoleCode"]),
                        FRoleDesc = Convert.ToString(dt.Rows[0]["FRoleDesc"]),
                        AttachmentFile = Convert.ToString(dt.Rows[0]["AttachmentFile"]),
                        CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                        CreatedDate = Convert.ToString(dt.Rows[0]["CreatedDate"]),
                        IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"])
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

        public PaginationData<FunctionalRole> GetAllFunctionalRole(PaginationModel model, string DbName)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetAllFunctionalRole]", sqlParameterd);
                var paginationData = new PaginationData<FunctionalRole>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<FunctionalRole>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public string UpdateFunctRoleAttachment(int Id, string Attachment, int CreatedBy, string CompanyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("CreatedBy", CreatedBy, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("Attachment", Attachment, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Id", Id, DataType.AsInt, sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Common].[Master_UpdateFunctionRoleAttachment]", sqlParameterd);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return Convert.ToString(dt.Rows[0]["OldAttachment"]);
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
        #endregion

        #region Location
        public ApplicationResponse AddLocation(Location model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Name", "Code", "CenterType", "IsRegisteredOffice", "Address", "CountryId", "StateId", "CityId", "PinCode", "Phone", "LocHeadEmpId", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_AddLocation]", sqlParameterd);
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
        public ApplicationResponse UpdateLocation(Location model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "Name", "Code", "CenterType", "IsRegisteredOffice", "Address", "CountryId", "StateId", "CityId", "PinCode", "Phone", "LocHeadEmpId", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_EditLocation]", sqlParameterd);
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
        public Location GetLocationById(int Id, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", Id, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetLocationById]", sqlParameterd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new Location
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                        Name = Convert.ToString(dt.Rows[0]["Name"]),
                        Code = Convert.ToString(dt.Rows[0]["Code"]),
                        CenterType = Convert.ToInt32(dt.Rows[0]["CenterType"]),
                        IsRegisteredOffice = Convert.ToBoolean(dt.Rows[0]["IsRegisteredOffice"]),
                        Address = Convert.ToString(dt.Rows[0]["Address"]),
                        CenterTypeName = Convert.ToString(dt.Rows[0]["CenterTypeName"]),
                        PinCode = Convert.ToString(dt.Rows[0]["PinCode"]),
                        Phone = Convert.ToString(dt.Rows[0]["Phone"]),
                        CountryId = Convert.ToInt32(dt.Rows[0]["CountryId"]),
                        StateId = Convert.ToInt32(dt.Rows[0]["StateId"]),
                        CityId = Convert.ToInt32(dt.Rows[0]["CityId"]),
                        LocHeadEmpId = Convert.ToInt64(dt.Rows[0]["LocHeadEmpId"]),
                        CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                        CreatedDate = Convert.ToString(dt.Rows[0]["CreatedDate"]),
                        IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"])
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
        public PaginationData<Location> GetAllLocation(PaginationModel model, string DbName)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetAllLocation]", sqlParameterd);
                var paginationData = new PaginationData<Location>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<Location>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public List<Location> GetLocationList(string DbName)
        {
            try
            {
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetLocationList]");
                List<Location> locationList = _dbHelper.ConvertDataTableToList<Location>(dt);
                return locationList;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public ApplicationResponse GetLocationCode(string DbName)
        {
            try
            {
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[GetLocationSeries]");
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
        #endregion
        #region Designation Auto Numbering
        public ApplicationResponse AddDesignationAutoNum(DesiAutoNumberingModel model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Prefix", "Suffix", "AppendNumber", "AttributesMap", "CreatedBy", "IsDoNotUse" };

                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                this._dbHelper.UpdateSqlParameterByDataTable("AttributesMap", model.DTAttributesMap, "UT_Common_DesignationAttrMapping", sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_AddDesignationAutoNumber]", sqlParameterd);
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

        public ApplicationResponse UpdateDesignationAutoNum(DesiAutoNumberingModel model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "Prefix", "Suffix", "AppendNumber", "AttributesMap", "CreatedBy", "IsDoNotUse" };

                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                this._dbHelper.UpdateSqlParameterByDataTable("AttributesMap", model.DTAttributesMap, "UT_Common_DesignationAttrMapping", sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_EditDesignationAutoNumber]", sqlParameterd);
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

        public DesiAutoNumberingByidModel GetDesignationAutoNumById(int Id, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", Id, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetDesignationAutoNumById]", sqlParameterd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new DesiAutoNumberingByidModel
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                        Prefix = Convert.ToString(dt.Rows[0]["Prefix"]),
                        Suffix = Convert.ToString(dt.Rows[0]["Suffix"]),
                        AppendNumber = Convert.ToInt32(dt.Rows[0]["AppendNumber"]),
                        Entity = Convert.ToString(dt.Rows[0]["Entity"]),
                        OrgUnit = Convert.ToString(dt.Rows[0]["OrgUnit"]),
                        Departments = Convert.ToString(dt.Rows[0]["Departments"]),
                        EntityId = Convert.ToInt32(dt.Rows[0]["EntityId"]),
                        DeptId = Convert.ToInt32(dt.Rows[0]["DeptId"]),
                        OrgUnitId = Convert.ToInt32(dt.Rows[0]["OrgUnitId"]),
                        CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                        IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"])
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

        public PaginationData<DesignationResponseModel> GetAllDesignationAutoNum(PaginationModel model, string DbName)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetAllDesignationAutoNumber]", sqlParameterd);
                var paginationData = new PaginationData<DesignationResponseModel>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<DesignationResponseModel>(dt);
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
