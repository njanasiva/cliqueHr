using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;
using System.Data;

namespace CliqueHR.DL
{
    public class EngagementRepository : IEngagementRepository
    {

        private readonly DBHelper _dbHelper;
        public EngagementRepository()
        {
            this._dbHelper = new DBHelper();
        }

        public PaginationData<EngagementGroups> EngagementMarketPlace(string DbName, EngagementGroups model)
        {
            try
            {

                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("Start", model.Start, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("NoofData", model.NoofData, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("GroupName", model.GroupName, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("GroupModerators", model.GroupModerators, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EmployeeGroup", model.EmployeeGroup, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Entity", model.Entity, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("OrgUnit", model.OrgUnit, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Department", model.Department, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Location", model.Location, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("EmployeeSearch", model.EmployeeSearch, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Icon", model.Icon, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("PostStatus", model.PostStatus, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("PostPoll", model.PostPoll, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("PostEvent", model.PostEvent, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsApprovalStatus", model.IsApprovalStatus, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsApprovalPostPoll", model.IsApprovalPostPoll, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsApprovalPostEvent", model.IsApprovalPostEvent, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsDoNotUse", model.IsDoNotUse, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("GroupId", model.GroupId, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsString, sqlParameterd);

                if (model.Sort != null)
                {
                    //_dbHelper.UpdateSqlParameter("PropertyName", model.Sort.PropertyName, DataType.AsString, sqlParameterd);
                    //_dbHelper.UpdateSqlParameter("Direction", model.Sort.Direction, DataType.AsString, sqlParameterd);
                }
                var dt = _dbHelper.GetDataTable(DbName, "[EngagementGroup]", sqlParameterd);
                var paginationData = new PaginationData<EngagementGroups>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["Total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<EngagementGroups>(dt);
                }
                return paginationData;
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }



        public DataSet EngagementMaster(string DbName, EngagementGroups model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                if (model.Sort != null)
                {
                    _dbHelper.UpdateSqlParameter("PropertyName", model.Sort.PropertyName, DataType.AsString, sqlParameterd);
                    _dbHelper.UpdateSqlParameter("Direction", model.Sort.Direction, DataType.AsString, sqlParameterd);
                }
                var ds = _dbHelper.GetDataSet(DbName, "[EngagementMaster]", sqlParameterd);
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


        public DataSet AddUpdateMarketPlace(string DbName, AddUpdateMarketPlace model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("IsPostSaleVehicle", model.IsPostSaleVehicle, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsPostSaleProperty", model.IsPostSaleProperty, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsPostSaleHouseholdGood", model.IsPostSaleHouseholdGood, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsPostRentalProperty", model.IsPostRentalProperty, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsDoNotUse", model.IsDoNotUse, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);

                var ds = _dbHelper.GetDataSet(DbName, "[AddUpdateMarketPlace]", sqlParameterd);
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


        public DataSet AddUpdateEngagementSurvey(string DbName, AddUpdateMarketPlace model)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("IsCreateSurvey", model.IsCreateSurvey, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsPostSurvey", model.IsPostSurvey, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsDoNotUse", model.IsDoNotUse, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);
                var ds = _dbHelper.GetDataSet(DbName, "[AddUpdateEngagementSurvey]", sqlParameterd);
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


        public PaginationData<DailyContent> AddModifyDailyContent(string DbName, DailyContent model)
        {
            try
            {

                var sqlParameterd = _dbHelper.CreateSqlParameter("UserId", model.UserId, DataType.AsInt);
                _dbHelper.UpdateSqlParameter("DailyContentId ", model.DailyContentId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("ActionId", model.ActionId, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Start", model.Start, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("NoofData", model.NoofData, DataType.AsInt, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsShuffle", model.IsShuffle, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("IsDate", model.IsDate, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Recurring", model.Recurring, DataType.AsBoolean, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Entity", model.Entity, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("OrgUnit", model.OrgUnit, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Content", model.Content, DataType.AsString, sqlParameterd);
                _dbHelper.UpdateSqlParameter("Department", model.Department, DataType.AsString, sqlParameterd);
                if (model.StartDate.ToString() == "1/1/0001 12:00:00 AM")
                {
                   // _dbHelper.UpdateSqlParameter("StartDate", null, DataType.AsDataTime, sqlParameterd);
                }
                else
                {
                    _dbHelper.UpdateSqlParameter("StartDate", model.StartDate, DataType.AsDataTime, sqlParameterd);
                }
                if (model.EndDate.ToString() == "1/1/0001 12:00:00 AM")
                {
                   // _dbHelper.UpdateSqlParameter("EndDate", null, DataType.AsDataTime, sqlParameterd);
                }
                else
                {
                    _dbHelper.UpdateSqlParameter("EndDate", model.EndDate, DataType.AsDataTime, sqlParameterd);
                }

                var dt = _dbHelper.GetDataTable(DbName, "[AddModifyDailyContent]", sqlParameterd);
                var paginationData = new PaginationData<DailyContent>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    paginationData.Total = Convert.ToInt32(dt.Rows[0]["Total"]);
                    paginationData.Data = _dbHelper.ConvertDataTableToList<DailyContent>(dt);
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
