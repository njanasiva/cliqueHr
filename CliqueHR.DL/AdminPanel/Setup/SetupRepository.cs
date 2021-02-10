using CliqueHR.Common.Models;
using CliqueHR.DL.Implementation.AdminPanel.Setup;
using CliqueHR.Helpers.DataHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CliqueHR.DL.AdminPanel.Setup
{
    public class SetupRepository : ISetupRepository
    {
        private readonly DBHelper _dbHelper;
        public SetupRepository()
        {
            this._dbHelper = new DBHelper();
        }

        #region Module
        public List<Module> GetAllModule(string DbName, int roleid)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("roleid", roleid, DataType.AsInt);

                return _dbHelper.GetDataTableToList<Module>(DbName, "[Setup].[Master_GetAllModule]", sqlParameterd);

            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public Module GetModuleById(int ModuleId, string DbName, int roleid)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", ModuleId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("roleid", roleid, DataType.AsInt, sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Setup].[Master_GetModuleById]", sqlParameterd);
                return new Module
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    Name = Convert.ToString(dt.Rows[0]["Name"]),
                    Description = Convert.ToString(dt.Rows[0]["Description"]),
                    CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                    IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"]),
                    IsSelected = Convert.ToBoolean(dt.Rows[0]["IsSelected"]),
                    Code = Convert.ToString(dt.Rows[0]["Code"])
                };
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        #endregion Module

        #region SubModule
        public List<SubModule> GetSubModuleByModuleId(string ModuleId, string DbName, int roleid)
        {

            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("ModuleId", ModuleId, DataType.AsString);
                _dbHelper.UpdateSqlParameter("roleid", roleid, DataType.AsInt, sqlParameterd);
                return _dbHelper.GetDataTableToList<SubModule>(DbName, "[Setup].[Master_GetSubModuleByModuleId]", sqlParameterd);
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public List<SubModuleField> GetSubModuleFieldByModuleId(string ModuleId, string DbName, int roleid)
        {

            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("ModuleId", ModuleId, DataType.AsString);
                _dbHelper.UpdateSqlParameter("roleid", roleid, DataType.AsInt, sqlParameterd);
                return _dbHelper.GetDataTableToList<SubModuleField>(DbName, "[Setup].[Master_GetSubModuleFieldByModuleId]", sqlParameterd);
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }


        #endregion SubModule

        #region role
        public List<Role> GetAllRole(string DbName)
        {
            try
            {
                return _dbHelper.GetDataTableToList<Role>(DbName, "[Setup].[Master_GetAllRole]");
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public Role GetRoleById(int RoleId, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", RoleId, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Setup].[Master_GetRoleById]", sqlParameterd);
                return new Role
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    Name = Convert.ToString(dt.Rows[0]["Name"]),
                    Description = Convert.ToString(dt.Rows[0]["Description"]),
                    CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                    IsDoNotUse = Convert.ToBoolean(dt.Rows[0]["IsDoNotUse"])
                };
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public ApplicationResponse RoleCreation(CreateRolePostModel model, string DbName)
        {
            try
            {
                CreateRolePostModelXML obj = new CreateRolePostModelXML();
                string strxml = XMLHelper.Serialize(model);
                obj.strxml = strxml;
                var parameters = new string[] { "strxml" };

                var sqlParameterd = _dbHelper.CreateSqlParamByObj(obj, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Setup].[RoleConfiguration]", sqlParameterd);
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

        public ApplicationResponse AssignRole(AssignRole model, string DbName)
        {
            try
            {
                CreateRolePostModelXML obj = new CreateRolePostModelXML();
                string strxml = XMLHelper.Serialize(model);
                obj.strxml = strxml;
                var parameters = new string[] { "strxml" };

                var sqlParameterd = _dbHelper.CreateSqlParamByObj(obj, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Setup].[AccessRole]", sqlParameterd);
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

        public AssignRole GetAssignRoleById(int AssignId, string DbName)
        {
            try
            {
                AssignRole objAssignRole = new AssignRole();
                var sqlParameterd = _dbHelper.CreateSqlParameter("assignid", AssignId, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Setup].[GetAssignRoleXML]", sqlParameterd);
                string strXML = dt.Rows[0]["XMLObject"].ToString();
                var serializer = new XmlSerializer(typeof(AssignRole));
                using (TextReader reader = new StringReader(strXML))
                {
                    objAssignRole = (AssignRole)serializer.Deserialize(reader);
                }
                return objAssignRole;

            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<DTAssignRole> GetAssignRoleList(PaginationModel model, string DbName)
        {
            try
            {
                //var sqlParameterd = _dbHelper.CreateSqlParameter("OffsetRows", (Convert.ToInt32(param.iDisplayStart) / Convert.ToInt32(param.iDisplayLength)), DataType.AsInt);
                //_dbHelper.UpdateSqlParameter("FetchRows", Convert.ToInt16(param.iDisplayLength), DataType.AsInt, sqlParameterd);
                //_dbHelper.UpdateSqlParameter("SearchText", (param.sSearch == null ? "" : param.sSearch.ToString()), DataType.AsString, sqlParameterd);

                var sqlParameterd = _dbHelper.CreateSqlParameter("StartRow", model.StartRow, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsString, sqlParameterd);
                if (model.Sort != null)
                {
                    _dbHelper.UpdateSqlParameter("PropertyName", model.Sort.PropertyName, DataType.AsString, sqlParameterd);
                    _dbHelper.UpdateSqlParameter("Direction", model.Sort.Direction, DataType.AsString, sqlParameterd);
                } 

                var dt = _dbHelper.GetDataTable(DbName, "[Setup].[usp_GetAssignRoleList]", sqlParameterd);
                var paginationData = new PaginationData<DTAssignRole>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<DTAssignRole>(dt);
                }
                return paginationData;

            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<Role> GetRoleList(PaginationModel model, string DbName)
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

                var dt = _dbHelper.GetDataTable(DbName, "[Setup].[GetAllRolesWithModules]", sqlParameterd);
                var paginationData = new PaginationData<Role>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<Role>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        #endregion role
        public PaginationData<DropdownList> GetEmployeeMultiselect(EmployeeFilter model,string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("StartRow", model.StartRow, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("TransactionId", model.TransactionId, DataType.AsInt, sqlParameterd);
                
                var dt = _dbHelper.GetDataTable(DbName, "[Common].[GetSelectedEmployees]", sqlParameterd);
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
