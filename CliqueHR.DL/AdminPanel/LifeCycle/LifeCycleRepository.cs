using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL
{
    public class LifeCycleRepository : ILifeCycleRepository
    {

        private readonly DBHelper _dbHelper;
        public LifeCycleRepository()
        {
            this._dbHelper = new DBHelper();
        }

        #region Probation


        public PaginationData<Probation> GetProbationDetailList(string DbName, ListModel model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("StartRow", model.StartRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ProbationDetailId", model.ProbationDetailId, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Action", model.Action, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsString, sqlParameterd);
                if (model.Sort != null)
                {
                    _dbHelper.UpdateSqlParameter("PropertyName", model.Sort.PropertyName, DataType.AsString, sqlParameterd);
                    _dbHelper.UpdateSqlParameter("Direction", model.Sort.Direction, DataType.AsString, sqlParameterd);
                }
                var dt = _dbHelper.GetDataTable(DbName, "[GetProbationDetailList]", sqlParameterd);
                var paginationData = new PaginationData<Probation>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["Total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<Probation>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public ApplicationResponse AddModifyProbationDetail(Probation model, string DbName)
        {
            try
            {
                var parameters = new string[] { "UserId", "ProbationName", "ProbationPeriod", "AllowExtension", "NumberExtensionsAllowed"
                , "ExtendProbationDay", "IncrementExtensionDay"
                , "ConfirmationDay", "IsDoNotUse"
                , "Entity", "OrgUnit"
                , "Department", "CentreType", "Region", "Location", "EmployeeType", "Position", "CostCentre", "Grade","Action","ProbationDetailId","AssessmentRequired",
                "AssessmentFormId"};
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(DbName, "[AddModifyProbationDetail]", sqlParameterd);
                return new ApplicationResponse
                {
                    Code = Convert.ToInt32(dt.Rows[0]["Success"]),
                    Message = Convert.ToString(dt.Rows[0]["Message"]),
                };
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public ConfirmationMasterModel GetMastersList(string DbName, ListModel model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                if (model.Sort != null)
                {
                    _dbHelper.UpdateSqlParameter("PropertyName", model.Sort.PropertyName, DataType.AsString, sqlParameterd);
                    _dbHelper.UpdateSqlParameter("Direction", model.Sort.Direction, DataType.AsString, sqlParameterd);
                }
                var ds = _dbHelper.GetDataSet(DbName, "[GetPublishMaster]", sqlParameterd);
                var paginationData = new PaginationData<Probation>();
                if (ds != null)
                {
                    // paginationData.Total = Convert.ToInt32(dt.Rows[0]["Total"]);
                    // paginationData.Data = _dbHelper.ConvertDataTableToList<Probation>(ds);
                }
                return this.GetMasterData(ds);
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        private ConfirmationMasterModel GetMasterData(DataSet ds)
        {
            ConfirmationMasterModel confirmationMasterModel = new ConfirmationMasterModel();
            if (ds.Tables.Count > 0)
            {
                confirmationMasterModel.Entity = this.GetDropDownValues(new Tuple<string, string>("EntityId", "EntityName"), ds.Tables[0]);
                confirmationMasterModel.OrgUnit = ds.Tables.Count >= 2 ? this.GetDropDownValues(new Tuple<string, string>("OrgUnitId", "OrgUnit"), ds.Tables[1]) : new List<DropdownModel>();
                confirmationMasterModel.Department = ds.Tables.Count >= 3 ? this.GetDropDownValues(new Tuple<string, string>("DepartmentId", "Department"), ds.Tables[2]) : new List<DropdownModel>();
                confirmationMasterModel.CentreType = ds.Tables.Count >= 4 ? this.GetDropDownValues(new Tuple<string, string>("CentreTypeId", "CentreType"), ds.Tables[3]) : new List<DropdownModel>();
                confirmationMasterModel.Region = ds.Tables.Count >= 5 ? this.GetDropDownValues(new Tuple<string, string>("RegionId", "Region"), ds.Tables[4]) : new List<DropdownModel>();
                confirmationMasterModel.Location = ds.Tables.Count >= 6 ? this.GetDropDownValues(new Tuple<string, string>("LocationId", "Location"), ds.Tables[5]) : new List<DropdownModel>();
                confirmationMasterModel.EmployeeType = ds.Tables.Count >= 7 ? this.GetDropDownValues(new Tuple<string, string>("EmployeeTypeId", "EmployeeType"), ds.Tables[6]) : new List<DropdownModel>();
                confirmationMasterModel.Position = ds.Tables.Count >= 8 ? this.GetDropDownValues(new Tuple<string, string>("PositionId", "Position"), ds.Tables[7]) : new List<DropdownModel>();
                confirmationMasterModel.CostCentre = ds.Tables.Count >= 9 ? this.GetDropDownValues(new Tuple<string, string>("CostCentreId", "CostCentre"), ds.Tables[8]) : new List<DropdownModel>();
                confirmationMasterModel.Grade = ds.Tables.Count >= 10 ? this.GetDropDownValues(new Tuple<string, string>("GradeId", "Grade"), ds.Tables[9]) : new List<DropdownModel>();
                confirmationMasterModel.AssessmentForm = ds.Tables.Count >= 11 ? this.GetDropDownValues(new Tuple<string, string>("AssessmentFormId", "FormName"), ds.Tables[10], this.GetDefaultValue("Select Form", 0)) : new List<DropdownModel>();
            }
            return confirmationMasterModel;
        }

        private KeyValuePair<string, int>? GetDefaultValue(string key, int value)
        {
            return new KeyValuePair<string, int>(key, value);
        }

        private List<DropdownModel> GetDropDownValues(Tuple<string, string> columns, DataTable dt, KeyValuePair<string, int>? defaultValue = null)
        {
            List<DropdownModel> dropdownModels = new List<DropdownModel>();
            foreach (DataRow item in dt.Rows)
            {
                dropdownModels.Add(new DropdownModel()
                {
                    Value = Convert.ToInt32(item[columns.Item1]),
                    Text = Convert.ToString(item[columns.Item2])
                });
            }
            if (defaultValue != null)
            {
                dropdownModels.Insert(0, new DropdownModel()
                {
                    Text = Convert.ToString(defaultValue.Value.Key),
                    Value = defaultValue.Value.Value
                });
            }
            return dropdownModels;
        }




        public DataSet GetMovementReasonsField(string DbName, ListModel model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                if (model.Sort != null)
                {
                    _dbHelper.UpdateSqlParameter("PropertyName", model.Sort.PropertyName, DataType.AsString, sqlParameterd);
                    _dbHelper.UpdateSqlParameter("Direction", model.Sort.Direction, DataType.AsString, sqlParameterd);
                }
                var ds = _dbHelper.GetDataSet(DbName, "[GetMovementReasonsField]", sqlParameterd);
                var paginationData = new PaginationData<Probation>();
                //if (ds != null)
                //   {
                //       paginationData.Total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                //       paginationData.Data = _dbHelper.ConvertDataTableToList<Probation>(ds.Tables[0]);
                //  }
                return ds;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public DataSet AddMovement(string DbName, LifeCycleMovement model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("MovementReason", model.MovementReason, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("AllowManagerInitiate", model.AllowManagerInitiate, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("AllowRelocationExpense", model.AllowRelocationExpense, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EditableFieldsId", model.EditableFieldsId, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsDoNotUse", model.IsDoNotUse, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("MovementId", model.MovementId, DataType.AsInt, sqlParameterd);

                var ds = _dbHelper.GetDataSet(DbName, "[AddMovement]", sqlParameterd);
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

        public PaginationData<LifeCycleMovement> GetMovementList(string DbName, LifeCycleMovement model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("StartRow", model.StartRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("MovementId", model.MovementId, DataType.AsInt, sqlParameterd);
                var ds = _dbHelper.GetDataSet(DbName, "[GetMovementList]", sqlParameterd);
                var paginationData = new PaginationData<LifeCycleMovement>();
                if (ds != null)
                {
                    paginationData.Total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<LifeCycleMovement>(ds.Tables[0]);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<NoticePeriodDetail> GetNoticePeriodDetail(string DbName, NoticePeriodDetail model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("StartRow", model.StartRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("NoticePeriodDetailId", model.NoticePeriodDetailId, DataType.AsInt, sqlParameterd);
                // _dbHelper.UpdateSqlParameter("Start", model.Start, DataType.AsInt, sqlParameterd);
                // _dbHelper.UpdateSqlParameter("NoofData", model.NoofData, DataType.AsInt, sqlParameterd);  

                var ds = _dbHelper.GetDataSet(DbName, "[GetNoticePeriodDetail]", sqlParameterd);
                var paginationData = new PaginationData<NoticePeriodDetail>();
                if (ds != null)
                {
                    paginationData.Total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<NoticePeriodDetail>(ds.Tables[0]);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public DataSet AddNoticePeriodDetail(string DbName, NoticePeriodDetail model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("NoticePeriodName", model.NoticePeriodName, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("NoticePeriodDays", model.NoticePeriodDays, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Entity", model.Entity, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("OrgUnit", model.OrgUnit, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Department", model.Department, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("CentreType", model.CentreType, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Region", model.Region, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Location", model.Location, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EmployeeType", model.EmployeeType, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Position", model.Position, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("CostCentre", model.CostCentre, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Grade", model.Grade, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsDoNotUse", model.IsDoNotUse, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ConfirmationDays", model.ConfirmationDays, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("NoticePeriodDetailId", model.NoticePeriodDetailId, DataType.AsInt, sqlParameterd);
                var ds = _dbHelper.GetDataSet(DbName, "[AddNoticePeriodDetail]", sqlParameterd);
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

        public DataSet GetSeparationType(string DbName, NoticePeriodDetail model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                var ds = _dbHelper.GetDataSet(DbName, "[GetSeparationType]", sqlParameterd);
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

        public DataSet AddModifySeparationReason(string DbName, LFSeparationReason model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("SeparationReason", model.SeparationReason, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SeparationTypeId", model.SeparationTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SeparationReasonId", model.SeparationReasonId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsDoNotUse", model.IsDoNotUse, DataType.AsBoolean, sqlParameterd);
                var ds = _dbHelper.GetDataSet(DbName, "[AddModifySeparationReason]", sqlParameterd);
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

        public PaginationData<LFSeparationReason> GetSeparationReason(string DbName, LFSeparationReason model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                // _dbHelper.UpdateSqlParameter("Start", model.Start, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("StartRow", model.StartRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsInt, sqlParameterd);
                //_dbHelper.UpdateSqlParameter("NoofData", model.NoofData, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SeparationReasonId", model.SeparationReasonId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);
                var ds = _dbHelper.GetDataSet(DbName, "[GetSeparationReason]", sqlParameterd);
                var paginationData = new PaginationData<LFSeparationReason>();
                if (ds != null)
                {
                    paginationData.Total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<LFSeparationReason>(ds.Tables[0]);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public DataSet GetMasterForSeparationTask(string DbName, LFSeparationReason model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                var ds = _dbHelper.GetDataSet(DbName, "[GetMasterForSeparationTask]", sqlParameterd);
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

        public DataSet AddModifySeparationTask(string DbName, LFSeparationTask model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("SeparationTask", model.SeparationTask, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SeparationTypeId", model.SeparationTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("TaskOwnerId", model.TaskOwnerId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EscalateLevelId", model.EscalateLevelId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("InitiateTaskDays", model.InitiateTaskDays, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EscalatePostDay", model.EscalatePostDay, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EscalateDayTypeId", model.EscalateDayTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EscalateTypeId", model.EscalateTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Entity", model.Entity, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("OrgUnit", model.OrgUnit, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Department", model.Department, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("CentreType", model.CentreType, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Region", model.Region, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Location", model.Location, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EmployeeType", model.EmployeeType, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Position", model.Position, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("CostCentre", model.CostCentre, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Grade", model.Grade, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EmployeeGroup", model.EmployeeGroup, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("DesignationType", model.Designation, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsDoNotUse", model.IsDoNotUse, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("DayTypeId", model.DayTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("InterviewTypeId", model.InterviewTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SeparationTaskId", model.SeparationTaskId, DataType.AsInt, sqlParameterd);


                var ds = _dbHelper.GetDataSet(DbName, "[AddModifySeparationTask]", sqlParameterd);
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

        public PaginationData<LFSeparationTask> GetSeparationTask(string DbName, LFSeparationTask model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("SeparationTaskId", model.SeparationTaskId, DataType.AsInt, sqlParameterd);
                //  _dbHelper.UpdateSqlParameter("Start", model.Start, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("StartRow", model.StartRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsString, sqlParameterd);
                //_dbHelper.UpdateSqlParameter("NoofData", model.NoofData, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);

                var ds = _dbHelper.GetDataSet(DbName, "[GetSeparationTask]", sqlParameterd);
                var paginationData = new PaginationData<LFSeparationTask>();
                if (ds != null)
                {
                    paginationData.Total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<LFSeparationTask>(ds.Tables[0]);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public DataSet GetFieldType(string DbName, LFSeparationTask model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("FieldTypeId", model.FieldTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);
                var ds = _dbHelper.GetDataSet(DbName, "[GetFieldType]", sqlParameterd);
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

        public DataSet AddModifySeparation(string DbName, Separation model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);

                _dbHelper.UpdateSqlParameter("SeparationDaysUpto", model.SeparationDaysUpto, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("RelievingDateTypeId", model.RelievingDateTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ApproveManagerType", model.ApproveManagerType, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("AutomaticallytriggerAbscondingWorkflow", model.AutomaticallytriggerAbscondingWorkflow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ContinuousAbsenteeismDay", model.ContinuousAbsenteeismDay, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IncludeRelievingDateType", model.IncludeRelievingDateType, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("AutomaticallyTriggerRetirementWorkflow", model.AutomaticallyTriggerRetirementWorkflow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("RetirementAge", model.RetirementAge, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsDoNotUse", model.IsDoNotUse, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("RetirementDays", model.RetirementDays, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("UserDefinedField", model.UserDefinedField, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionTypeId", model.ActionTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SeparationId", model.SeparationId, DataType.AsInt, sqlParameterd);
                // var jsonString = "[{ 'FieldName': 'F1', 'FieldTypeId': 1,  'FieldValueId' : 2, 'IsMandatory': false }," +
                //    "{ 'FieldName': 'F2', 'FieldTypeId': 2,  'FieldValueId' : 3, 'IsMandatory': true}]";
                var ds = _dbHelper.GetDataSet(DbName, "[AddModifySeparation]", sqlParameterd);


                var jsonString = model.UserDefinedField;
                JArray array = JArray.Parse(jsonString);
                var FieldName = "";
                var FieldTypeId = "";
                var FieldValueId = "";
                var IsMandatory = "";
                foreach (JObject obj in array.Children<JObject>())
                {
                    foreach (JProperty singleProp in obj.Properties())
                    {
                        string name = singleProp.Name;
                        string value = singleProp.Value.ToString();
                        if (name == "FieldName")
                        {
                            FieldName = value;
                            if (FieldName == "")
                            {
                                FieldName = null;
                            }
                        }
                        else if (name == "FieldTypeId")
                        {
                            FieldTypeId = value;
                            if (FieldTypeId == "")
                            {
                                FieldTypeId = null;
                            }
                        }
                        else if (name == "FieldValueId")
                        {
                            FieldValueId = value;
                            if (FieldValueId == "")
                            {
                                FieldValueId = null;
                            }
                        }
                        else if (name == "IsMandatory")
                        {
                            IsMandatory = value;
                            if (IsMandatory == "")
                            {
                                IsMandatory = null;
                            }
                        }
                        //Do something with name and value
                        //System.Windows.MessageBox.Show("name is "+name+" and value is "+value);
                    }

                    var sqlParameterdUserDefinedField = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                    _dbHelper.UpdateSqlParameter("FieldName", Convert.ToString(FieldName), DataType.AsString, sqlParameterdUserDefinedField);
                    _dbHelper.UpdateSqlParameter("FieldTypeId", Convert.ToInt32(FieldTypeId), DataType.AsInt, sqlParameterdUserDefinedField);
                    _dbHelper.UpdateSqlParameter("FieldValueId", Convert.ToInt32(FieldValueId), DataType.AsInt, sqlParameterdUserDefinedField);
                    _dbHelper.UpdateSqlParameter("IsMandatory", Convert.ToBoolean(IsMandatory), DataType.AsBoolean, sqlParameterdUserDefinedField);
                    var dt = _dbHelper.GetDataSet(DbName, "[AddSeparationUserDefindData]", sqlParameterdUserDefinedField);

                }

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



        public DataSet GetSeparation(string DbName, Separation model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                var ds = _dbHelper.GetDataSet(DbName, "[GetSeparation]", sqlParameterd);
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

        public DataSet AddModifyExitInterview(string DbName, ExitInterview model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("PolicyName", model.PolicyName, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SeparationTypeId", model.SeparationTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsSendMail", model.IsSendMail, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsExitInterviewMandatory", model.IsExitInterviewMandatory, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ExitInterviewDay", model.ExitInterviewDay, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ExitInterviewDayTypeId", model.ExitInterviewDayTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ExitInterviewTypeId", model.ExitInterviewTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("FormId", model.FormId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Entity", model.Entity, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("OrgUnit", model.OrgUnit, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Department", model.Department, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("CentreType", model.CentreType, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Region", model.Region, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Location", model.Location, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EmployeeType", model.EmployeeType, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Position", model.Position, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("CostCentre", model.CostCentre, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Grade", model.Grade, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EmployeeGroup", model.EmployeeGroup, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("DesignationType", model.Designation, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsDoNotUse", model.IsDoNotUse, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ExitInterviewId", model.ExitInterviewId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionTypeId", model.ActionTypeId, DataType.AsInt, sqlParameterd);
                var ds = _dbHelper.GetDataSet(DbName, "[AddModifyExitInterview]", sqlParameterd);
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

        public PaginationData<ExitInterview> ExitInterviewList(string DbName, ExitInterview model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("ExitInterviewId", model.ExitInterviewId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionTypeId", model.ActionTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("StartRow", model.StartRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsString, sqlParameterd);

                var ds = _dbHelper.GetDataSet(DbName, "[ExitInterviewList]", sqlParameterd);
                var paginationData = new PaginationData<ExitInterview>();
                if (ds != null)
                {
                    paginationData.Total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<ExitInterview>(ds.Tables[0]);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public DataSet LifeCycleSetting(string DbName, LifeCycleSetting model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("InitiatePIP", model.InitiatePIP, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("RecommendSalaryHike", model.RecommendSalaryHike, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EnablePEP", model.EnablePEP, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("AutoTriggerPEP", model.AutoTriggerPEP, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("AutoTriggerPEPDay", model.AutoTriggerPEPDay, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("LifeCycleSettingsId", model.LifeCycleSettingsId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionTypeId", model.ActionTypeId, DataType.AsInt, sqlParameterd);

                var ds = _dbHelper.GetDataSet(DbName, "[LifeCycleSetting]", sqlParameterd);
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

        public PaginationData<LifeCycleSetting> AddModifyApprovalPath(string DbName, LifeCycleSetting model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("ApprovalPathTypeId", model.ApprovalPathTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ApprovalReasonsId", model.ApprovalReasonsId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ApprovalPathName", model.ApprovalPathName, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("NumberofLevels", model.NumberofLevels, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("DoNotUse", model.DoNotUse, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("LifeCycleWorkFlowId", model.LifeCycleWorkFlowId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionTypeId", model.ActionTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("LifeCycleWorkFlowLevel", model.LifeCycleWorkFlowLevel, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("StartRow", model.StartRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("SearchText", model.SearchText, DataType.AsString, sqlParameterd);
                if (model.Sort != null)
                {
                    _dbHelper.UpdateSqlParameter("PropertyName", model.Sort.PropertyName, DataType.AsString, sqlParameterd);
                    _dbHelper.UpdateSqlParameter("Direction", model.Sort.Direction, DataType.AsString, sqlParameterd);
                }
                var ds = _dbHelper.GetDataSet(DbName, "[AddModifyApprovalPath]", sqlParameterd);
                var jsonString = model.LifeCycleWorkFlowLevel;
                JArray array = JArray.Parse(jsonString);
                var ApproverId = "";
                var Followup = "";
                var Escalateto = "";
                var EscalateAfter = "";
                foreach (JObject obj in array.Children<JObject>())
                {
                    foreach (JProperty singleProp in obj.Properties())
                    {
                        string name = singleProp.Name;
                        string value = singleProp.Value.ToString();
                        if (name == "ApproverId")
                        {
                            ApproverId = value;
                            if (ApproverId == "")
                            {
                                ApproverId = null;
                            }
                        }
                        else if (name == "AllowFollowUp")
                        {
                            Followup = value;
                            if (Followup == "")
                            {
                                Followup = null;
                            }
                        }
                        else if (name == "EscalateTo")
                        {
                            Escalateto = value;
                            if (Escalateto == "")
                            {
                                Escalateto = null;
                            }
                        }
                        else if (name == "EscalateAfter")
                        {
                            EscalateAfter = value;
                            if (EscalateAfter == "")
                            {
                                EscalateAfter = null;
                            }
                        }
                        //Do something with name and value
                        //System.Windows.MessageBox.Show("name is "+name+" and value is "+value);
                    }

                    var sqlParameterdUserDefinedField = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                    _dbHelper.UpdateSqlParameter("ApproverId", Convert.ToInt32(ApproverId), DataType.AsInt, sqlParameterdUserDefinedField);
                    _dbHelper.UpdateSqlParameter("Followup", Convert.ToInt32(Followup), DataType.AsInt, sqlParameterdUserDefinedField);
                    _dbHelper.UpdateSqlParameter("Escalateto", Convert.ToInt32(Escalateto), DataType.AsInt, sqlParameterdUserDefinedField);
                    _dbHelper.UpdateSqlParameter("EscalateAfter", Convert.ToInt32(EscalateAfter), DataType.AsInt, sqlParameterdUserDefinedField);
                    _dbHelper.UpdateSqlParameter("ActionId", model.ActionTypeId, DataType.AsInt, sqlParameterdUserDefinedField);
                    _dbHelper.UpdateSqlParameter("LifeCycleWorkFlowId", ds.Tables[0].Rows[0]["LifeCycleWorkFlowId"], DataType.AsInt, sqlParameterdUserDefinedField);

                    var dt = _dbHelper.GetDataSet(DbName, "[AddLifeCycleWorkFlowLevel]", sqlParameterdUserDefinedField);

                }

                var paginationData = new PaginationData<LifeCycleSetting>();
                if (ds != null)
                {
                    paginationData.Total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<LifeCycleSetting>(ds.Tables[0]);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<LifeCycleWorkFlow> GetWorkFlowList(string DbName, ListModel model)
        {
            //LoadWorkFlowList
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("StartRow", model.StartRow, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EndRow", model.EndRow, DataType.AsInt, sqlParameterd);
                var ds = _dbHelper.GetDataSet(DbName, "[LoadWorkFlowList]", sqlParameterd);
                var paginationData = new PaginationData<LifeCycleWorkFlow>();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    paginationData.Total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<LifeCycleWorkFlow>(ds.Tables[0]);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public DataSet GetLifeCycleWorkFlowLevel(string DbName, int Id)
        {
            var sqlParameterd = _dbHelper.CreateSqlParameter("LifeCycleWorkFlowId", Id, DataType.AsInt);
            var dt = _dbHelper.GetDataSet(DbName, "[GetLifeCycleWorkFlowLevel]", sqlParameterd);
            return dt;
        }
        public string SaveWorkFlowLevel(string DbName, WorkFlowLevelDetails model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("LifeCycleWorkFlowId", model.LifeCycleWorkFlowId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("ApprovalPathTypeId", model.ApprovalPathTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ApprovalReasonsId", model.ApprovalReasonsId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ApprovalPathName", model.ApprovalPathName, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("NumberofLevels", model.NumberOfLevels, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("DoNotUse", !model.IsDoNotUse.HasValue ? false : model.IsDoNotUse.Value, DataType.AsBoolean, sqlParameterd);
                object id = _dbHelper.ExecuteScalar(DbName, "[AddModifyWorkFlow]", sqlParameterd);
                if (id != null)
                {
                    int lintID = 0;
                    if (int.TryParse(id.ToString(), out lintID))
                    {
                        if (model.LifeCycleWorkFlowId > 0)
                        {
                            var sqlParameterd1 = _dbHelper.CreateSqlParameter("LifeCycleWorkFlowId", model.LifeCycleWorkFlowId, DataType.AsInt);
                            _dbHelper.ExecuteNonQuery(DbName, "[DeleteWorkFlowLevel]", sqlParameterd1);
                        }
                        foreach (var item in model.WorkFlowLevelData)
                        {
                            sqlParameterd = _dbHelper.CreateSqlParameter("ApproverId", item.ApproverId, DataType.AsInt);
                            _dbHelper.UpdateSqlParameter("Followup", item.Followup, DataType.AsInt, sqlParameterd);
                            _dbHelper.UpdateSqlParameter("Escalateto", item.Escalateto, DataType.AsInt, sqlParameterd);
                            _dbHelper.UpdateSqlParameter("EscalateAfter", item.EscalateAfter, DataType.AsInt, sqlParameterd);
                            _dbHelper.UpdateSqlParameter("LifeCycleWorkFlowId", lintID, DataType.AsInt, sqlParameterd);
                            _dbHelper.UpdateSqlParameter("NumberOfLevels", model.NumberOfLevels, DataType.AsInt, sqlParameterd);
                            _dbHelper.ExecuteNonQuery(DbName, "[AddWorkFlowLevel]", sqlParameterd);

                        }
                    }
                }
                return "Saved Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string SaveSeparationAndUserDefinedData(string DbName, SeparationData model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("SeparationDaysUpto", model.SeparationDaysUpto, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("AllowUpTo", model.AllowUpTo, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("RelievingDateTypeId", model.RelievingDateTypeId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("AllowManagerResignOnBehalf", model.AllowManagerResignOnBehalf, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EditExitDate", model.EditExitDate, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EditRecoveryDates", model.EditRecoveryDates, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("RaiseTermination", model.RaiseTermination, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("AutomaticallyTriggerRetirementWorkflow", model.AutomaticallyTriggerRetirementWorkflow, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("RetirementDays", model.RetirementDays, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("RetirementAge", model.RetirementAge, DataType.AsInt, sqlParameterd);
                object id = _dbHelper.ExecuteScalar(DbName, "[SaveSeparationDetails]", sqlParameterd);
                if (id != null)
                {
                    int lintID = 0;
                    if (int.TryParse(id.ToString(), out lintID))
                    {
                        _dbHelper.ExecuteNonQuery(DbName, "[DeleteUserDefined]");
                        foreach (var item in model.UserDefinedData)
                        {
                            sqlParameterd = _dbHelper.CreateSqlParameter("FieldName", item.FieldName, DataType.AsString);
                            _dbHelper.UpdateSqlParameter("FieldTypeId", item.FieldType, DataType.AsInt, sqlParameterd);
                            _dbHelper.UpdateSqlParameter("FieldValueId", item.FieldValueId, DataType.AsInt, sqlParameterd);
                            _dbHelper.UpdateSqlParameter("IsMandatory", item.IsMandatory, DataType.AsBoolean, sqlParameterd);
                            _dbHelper.UpdateSqlParameter("IsActive", item.IsActive, DataType.AsBoolean, sqlParameterd);
                            _dbHelper.UpdateSqlParameter("SeparationId", lintID, DataType.AsInt, sqlParameterd);
                            _dbHelper.UpdateSqlParameter("FieldValue", item.FieldValue, DataType.AsString, sqlParameterd);
                            _dbHelper.ExecuteNonQuery(DbName, "[SaveUserDefinedData]", sqlParameterd);
                        }
                    }
                }
                return "Saved Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
