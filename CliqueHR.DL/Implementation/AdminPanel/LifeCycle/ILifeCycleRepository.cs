using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL
{
    public interface ILifeCycleRepository
    {

        #region Probation

        ApplicationResponse AddModifyProbationDetail(Probation model, string DBName);
        PaginationData<Probation> GetProbationDetailList(string DBName, ListModel model);
        ConfirmationMasterModel GetMastersList(string DbName, ListModel model);
        DataSet GetMovementReasonsField(string DBName, ListModel model);
        DataSet AddMovement(string DBName, LifeCycleMovement model);
        PaginationData<LifeCycleMovement> GetMovementList(string DBName, LifeCycleMovement model);
        PaginationData<NoticePeriodDetail> GetNoticePeriodDetail(string DBName, NoticePeriodDetail model);
        DataSet AddNoticePeriodDetail(string DBName, NoticePeriodDetail model);
        DataSet GetSeparationType(string DBName, NoticePeriodDetail model);
        DataSet AddModifySeparationReason(string DBName, LFSeparationReason model);
        PaginationData<LFSeparationReason> GetSeparationReason(string DBName, LFSeparationReason model);
        DataSet GetMasterForSeparationTask(string DBName, LFSeparationReason model);
        DataSet AddModifySeparationTask(string DBName, LFSeparationTask model);
        PaginationData<LFSeparationTask> GetSeparationTask(string DBName, LFSeparationTask model);
        DataSet GetFieldType(string DBName, LFSeparationTask model);
        DataSet AddModifySeparation(string DBName, Separation model);
        DataSet GetSeparation(string DBName, Separation model);
        DataSet AddModifyExitInterview(string DBName, ExitInterview model);
        PaginationData<ExitInterview> ExitInterviewList(string DBName, ExitInterview model);
        DataSet LifeCycleSetting(string DBName, LifeCycleSetting model);
        PaginationData<LifeCycleSetting> AddModifyApprovalPath(string DBName, LifeCycleSetting model);

        PaginationData<LifeCycleWorkFlow> GetWorkFlowList(string DbName, ListModel model);
        DataSet GetLifeCycleWorkFlowLevel(string DbName, int Id);
        string SaveWorkFlowLevel(string DbName, WorkFlowLevelDetails model);
        string SaveSeparationAndUserDefinedData(string DbName, SeparationData model);
        #endregion
    }
}
