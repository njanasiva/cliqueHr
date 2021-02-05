using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface ILifeCycleService
    {

        #region Probation

        ApplicationResponse AddModifyProbationDetail(Probation model, UserContextModel objUser);
        PaginationData<Probation> GetProbationDetailList(UserContextModel objUser, ListModel paginationModel);
        ConfirmationMasterModel GetMastersList(UserContextModel objUser, ListModel paginationModel);
        DataSet GetMovementReasonsField(UserContextModel objUser, ListModel paginationModel);
        DataSet AddMovement(UserContextModel objUser, LifeCycleMovement lifeCycleMovement);
        PaginationData<LifeCycleMovement> GetMovementList(UserContextModel objUser, LifeCycleMovement lifeCycleMovement);
        PaginationData<NoticePeriodDetail> GetNoticePeriodDetail(UserContextModel objUser, NoticePeriodDetail noticePeriodDetail);
        DataSet AddNoticePeriodDetail(UserContextModel objUser, NoticePeriodDetail noticePeriodDetail);
        DataSet GetSeparationType(UserContextModel objUser, NoticePeriodDetail noticePeriodDetail);
        DataSet AddModifySeparationReason(UserContextModel objUser, LFSeparationReason lfseparationReason);
        PaginationData<LFSeparationReason> GetSeparationReason(UserContextModel objUser, LFSeparationReason lfseparationReason);
        DataSet GetMasterForSeparationTask(UserContextModel objUser, LFSeparationReason lfseparationReason);
        DataSet AddModifySeparationTask(UserContextModel objUser, LFSeparationTask lfSeparationTask);
        PaginationData<LFSeparationTask> GetSeparationTask(UserContextModel objUser, LFSeparationTask lfSeparationTask);
        DataSet GetFieldType(UserContextModel objUser, LFSeparationTask lfSeparationTask);
        DataSet AddModifySeparation(UserContextModel objUser, Separation separation);
        DataSet GetSeparation(UserContextModel objUser, Separation separation);
        DataSet AddModifyExitInterview(UserContextModel objUser, ExitInterview exitInterview);
        PaginationData<ExitInterview> ExitInterviewList(UserContextModel objUser, ExitInterview exitInterview);
        DataSet LifeCycleSetting(UserContextModel objUser, LifeCycleSetting lifeCycleSetting);

        PaginationData<LifeCycleSetting> AddModifyApprovalPath(UserContextModel objUser, LifeCycleSetting lifeCycleSetting);

        PaginationData<LifeCycleWorkFlow> GetWorkFlowList(UserContextModel objUser, ListModel model);

        DataSet GetLifeCycleWorkFlowLevel(string DbName, int Id);

        string SaveWorkFlowLevel(string DbName, WorkFlowLevelDetails model);
        string SaveSeparationAndUserDefinedData(string DbName, SeparationData model);
        #endregion
    }
}
