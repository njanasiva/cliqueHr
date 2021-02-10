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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DBHelper _dbHelper;
        public EmployeeRepository()
        {
            this._dbHelper = new DBHelper();
        }

        #region Employee Type
        public ApplicationResponse AddEmployeeType(EmployeeType model, string DbName)
        {
            try
            {
                var parameters = new string[] { "TypeName", "SelfService", "MinAge", "CreatedBy","IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_AddEmployeeType]", sqlParameterd);
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

        public ApplicationResponse UpdateEmployeeType(EmployeeType model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "TypeName", "SelfService", "MinAge", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_EditEmployeeType]", sqlParameterd);
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

        public EmployeeType GetEmployeeTypeById(int EmployeeTypeId, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", EmployeeTypeId, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetEmployeeTypeById]", sqlParameterd);
                return new EmployeeType
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    TypeName = Convert.ToString(dt.Rows[0]["TypeName"]),
                    SelfService = Convert.ToBoolean(dt.Rows[0]["SelfService"]),
                    CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                    MinAge = Convert.ToInt32(dt.Rows[0]["MinAge"]),
                    CreatedDate = Convert.ToString(dt.Rows[0]["CreatedDate"]),
                    IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"])
                };
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<EmployeeType> GetAllEmployeeType(string DbName, PaginationModel model)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetAllEmployeeType]", sqlParameterd);
                var paginationData = new PaginationData<EmployeeType>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<EmployeeType>(dt);
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

        #region Band Type
        public ApplicationResponse AddBandType(BandType model, string DbName)
        {
            try
            {
                var parameters = new string[] { "TypeName", "GradeMapping", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt =_dbHelper.GetDataTable(DbName, "[Common].[Master_AddBandType]", sqlParameterd);
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

        public ApplicationResponse UpdateBandType(BandType model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "TypeName", "GradeMapping", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_EditBandType]", sqlParameterd);
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

        public BandType GetBandTypeById(int BandTypeId, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", BandTypeId, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetBandTypeById]", sqlParameterd);
                return new BandType
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    TypeName = Convert.ToString(dt.Rows[0]["TypeName"]),
                    GradeMapping = Convert.ToString(dt.Rows[0]["GradeMapping"]),
                    CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                    CreatedDate = Convert.ToString(dt.Rows[0]["CreatedDate"]),
                    IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"])
                };
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<BandType> GetAllBandType(string DbName, PaginationModel model)
        {
            try
            {
                //return _dbHelper.GetDataTableToList<BandType>(DbName, "[Common].[Master_GetAllBandType]");

                var sqlParameterd = _dbHelper.CreateSqlParameter("StartRow", model.StartRow, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsString, sqlParameterd);
                if (model.Sort != null)
                {
                    _dbHelper.UpdateSqlParameter("PropertyName", model.Sort.PropertyName, DataType.AsString, sqlParameterd);
                    _dbHelper.UpdateSqlParameter("Direction", model.Sort.Direction, DataType.AsString, sqlParameterd);
                }
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetAllBandType]", sqlParameterd);
                var paginationData = new PaginationData<BandType>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<BandType>(dt);
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

        #region Grade Type
        public ApplicationResponse AddGradeType(GradeType model, string DbName)
        {
            try
            {
                var parameters = new string[] { "TypeName", "MinSalary", "MaxSalary", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_AddGradeType]", sqlParameterd);
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
        public ApplicationResponse UpdateGradeType(GradeType model, string DbName)
        {
            try
            {
                var parameters = new string[] { "Id", "TypeName", "MinSalary", "MaxSalary", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_EditGradeType]", sqlParameterd);
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
        public GradeType GetGradeTypeById(int GradeTypeId, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", GradeTypeId, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetGradeTypeById]", sqlParameterd);
                return new GradeType
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    TypeName = Convert.ToString(dt.Rows[0]["TypeName"]),
                    MaxSalary = Convert.ToDecimal(dt.Rows[0]["MaxSalary"]),
                    MinSalary = Convert.ToDecimal(dt.Rows[0]["MinSalary"]),
                    CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                    CreatedDate = Convert.ToString(dt.Rows[0]["CreatedDate"]),
                    IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"])
                };
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<GradeType> GetAllGradeType(string DbName, PaginationModel model)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetAllGradeType]", sqlParameterd);
                List<GradeType> gradeList = new List<GradeType>();
               
                var paginationData = new PaginationData<GradeType>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<GradeType>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public List<GradeType> GetGradeList(string DbName)
        {
            try
            {               
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[Master_GetGradeTypeList]");
                List<GradeType> gradeList = _dbHelper.ConvertDataTableToList<GradeType>(dt);   
                return gradeList;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        #endregion

        #region Center Type
        public ApplicationResponse AddCenterTypeData(CenterTypeModel model, string DBName)
        {
            try
            {
                var parameters = new string[] { "CenterTypeName", "IsDoNotUse", "CreatedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DBName, "[Common].[Master_AddCenterType]", sqlParameterd);
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

        public ApplicationResponse UpdateCenterTypeData(CenterTypeModel model, string DBName)
        {
            try
            {
                var parameters = new string[] { "Id", "CenterTypeName", "IsDoNotUse", "CreatedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DBName, "[Common].[Master_EditCenterType]", sqlParameterd);
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

        public PaginationData<CenterTypeModel> GetAllCenterType(PaginationModel model, string DBName)
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
                var dt = _dbHelper.GetDataTable(DBName, "[Common].[Master_GetAllCenterType]", sqlParameterd);
                var paginationData = new PaginationData<CenterTypeModel>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<CenterTypeModel>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public CenterTypeModel GetCenterTypeByID(int Id, string DBName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", Id, DataType.AsInt);
                var dt = _dbHelper.GetDataTable(DBName, "[Common].[Master_GetCenterTypeById]", sqlParameterd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new CenterTypeModel
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                        CenterTypeName = Convert.ToString(dt.Rows[0]["CenterTypeName"]),
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

        public List<CenterTypeModel> GetCenterType(string DBName)
        {
            try
            {

                return _dbHelper.GetDataTableToList<CenterTypeModel>(DBName, "[Common].[Master_GetCenterType]");
              
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }


        #endregion

        #region Cost Center Type
        public ApplicationResponse AddCostCenter(CostCenterModel model, string DBName)
        {
            try
            {
                var parameters = new string[] { "TypeName", "Code", "Head", "CreatedBy", "IsDoNotUse"};
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DBName, "[Common].[Master_AddCostCenter]", sqlParameterd);
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
        public ApplicationResponse UpdateCostCenter(CostCenterModel model, string DBName)
        {
            try
            {
                var parameters = new string[] { "Id", "TypeName", "Code", "Head", "CreatedBy", "IsDoNotUse" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DBName, "[Common].[Master_EditCostCenter]", sqlParameterd);
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

        public CostCenterModel GetCostCenterByID(int Id, string DBName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", Id, DataType.AsInt);
                var dt = _dbHelper.GetDataTable(DBName, "[Common].[Master_GetCostCenterById]", sqlParameterd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new CostCenterModel
                    {
                        Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                        TypeName = Convert.ToString(dt.Rows[0]["TypeName"]),
                        Code= Convert.ToString(dt.Rows[0]["Code"]),
                        Head = Convert.ToInt32(dt.Rows[0]["Head"]),
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

        public PaginationData<CostCenterModel> GetAllCostCenter(PaginationModel model, string DBName)
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
                var dt = _dbHelper.GetDataTable(DBName, "[Common].[Master_GetAllCostCenter]", sqlParameterd);
                var paginationData = new PaginationData<CostCenterModel>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<CostCenterModel>(dt);
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
