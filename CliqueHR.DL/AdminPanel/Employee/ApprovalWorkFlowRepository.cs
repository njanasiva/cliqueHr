using CliqueHR.Common.Models;
using CliqueHR.DL.Implementation.AdminPanel.Employee;
using CliqueHR.Helpers.DataHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL.AdminPanel.Employee
{
   public class ApprovalWorkFlowRepository : IApprovalWorkFlow
    {
        private readonly DBHelper _dbHelper;
        public ApprovalWorkFlowRepository()
        {
            _dbHelper = new DBHelper();
        }

        public PaginationData<ApprovalWorkflow> GetAllWorkFlow(string DbName, PaginationModel model)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Employee].[GetAllApprovalWorkflow]", sqlParameterd);
                var paginationData = new PaginationData<ApprovalWorkflow>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<ApprovalWorkflow>(dt);
                }
                return paginationData;

            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public ApprovalWorkflow GetWorkFlowById(int WorkFlowId, string DbName)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", WorkFlowId, DataType.AsInt);
                var ds = _dbHelper.GetDataSet(DbName, "[Employee].[GetApprovalWorkFlowById]", sqlParameterd);

                ApprovalWorkflow approvalWorkflow = new ApprovalWorkflow();
    
                if (ds == null)
                    return approvalWorkflow;

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)                              
                       approvalWorkflow.ApprovalPathMap = ds.Tables[0];
                
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)   
                        approvalWorkflow.WorkflowLevelApprover = ds.Tables[1];
                
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)  
                    approvalWorkflow.WorkflowLevelMap = ds.Tables[2];              

                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {                    
                        approvalWorkflow.Id = Convert.ToInt32(ds.Tables[3].Rows[0]["Id"]);
                        approvalWorkflow.PathName = Convert.ToString(ds.Tables[3].Rows[0]["PathName"]);
                        approvalWorkflow.Levels = Convert.ToInt32(ds.Tables[3].Rows[0]["Levels"]);
                        approvalWorkflow.CreatedBy = Convert.ToInt32(ds.Tables[3].Rows[0]["CreatedBy"]);
                        approvalWorkflow.CreatedDate = Convert.ToDateTime(ds.Tables[3].Rows[0]["CreatedDate"]);
                        approvalWorkflow.IsDoNotUse = Convert.ToBoolean(ds.Tables[3].Rows[0]["IsDoNotUse"]);                                         
                }                
           
                return approvalWorkflow;
             
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public ApplicationResponse AddWorkFlow(ApprovalWorkflow model, string DbName)
        {
           
            try
            {
                var parameters = new string[] { "PathName", "Levels", "CreatedBy", "IsDoNotUse", "ApprovalPathMap", "WorkflowLevelMap", "WorkflowLevelApprover" };

                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                
                this._dbHelper.UpdateSqlParameterByDataTable("ApprovalPathMap", model.ApprovalPathMap, "UDT_WorkFlowApprovalType", sqlParameterd);               
                this._dbHelper.UpdateSqlParameterByDataTable("WorkflowLevelMap", model.WorkflowLevelMap, "UDT_WorkflowLevel", sqlParameterd);
                this._dbHelper.UpdateSqlParameterByDataTable("WorkflowLevelApprover", model.WorkflowLevelApprover, "UDT_WorkflowLevelApprover", sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Employee].[AddApprovalWorkFlow]", sqlParameterd);

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
        public ApplicationResponse UpdateWorkFlow(ApprovalWorkflow model, string DbName)
        {

            try
            {
                var parameters = new string[] { "Id", "PathName", "Levels", "CreatedBy", "IsDoNotUse", "ApprovalPathMap", "WorkflowLevelMap", "WorkflowLevelApprover" };

                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);

                this._dbHelper.UpdateSqlParameterByDataTable("ApprovalPathMap", model.ApprovalPathMap, "UDT_WorkFlowApprovalType", sqlParameterd);
                this._dbHelper.UpdateSqlParameterByDataTable("WorkflowLevelMap", model.WorkflowLevelMap, "UDT_WorkflowLevel", sqlParameterd);
                this._dbHelper.UpdateSqlParameterByDataTable("WorkflowLevelApprover", model.WorkflowLevelApprover, "UDT_WorkflowLevelApprover", sqlParameterd);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[Employee].[EditApprovalWorkFlow]", sqlParameterd);

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
        public PaginationData<ApprovalPathType> GetAllApprovalPathType(string DbName, PaginationModel model)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Employee].[GetApprovalList]", sqlParameterd);
                var paginationData = new PaginationData<ApprovalPathType>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<ApprovalPathType>(dt);
                }
                return paginationData;

            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<Approver> GetAllApprover(string DbName, PaginationModel model)
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
                var dt = _dbHelper.GetDataTable(DbName, "[Employee].[GetApproverList]", sqlParameterd);
                var paginationData = new PaginationData<Approver>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<Approver>(dt);
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
