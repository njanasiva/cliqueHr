using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public class LifeCycleService : ILifeCycleService
    {
        private ILifeCycleRepository _lifeCycleRepository;
        private ProbationValidation _modelValidation;
        private CourseTitleValidation _titleValidation;
        private MajorValidation _majorValidation;
        private UniversityValidation _universityValidation;
        private InstituteValidation _instituteValidation;

        public LifeCycleService()
        {
            _lifeCycleRepository = new LifeCycleRepository();
            _modelValidation = new ProbationValidation();
            _titleValidation = new CourseTitleValidation();
            _majorValidation = new MajorValidation();
            _universityValidation = new UniversityValidation();
            _instituteValidation = new InstituteValidation();
        }

        #region Probation

        public void AddModifyProbationDetail(Probation model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelValidation.Validate(ProbationValidation.ValidateAll_key, model, "Probation can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                //   model.CreatedBy = objUser.UserID;
                var data = _lifeCycleRepository.AddModifyProbationDetail(model, objUser.CompanyCode);
                // if (data.Code == 0)
                //{
                responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = Convert.ToString(data.Code) });
                throw new ValidationException(responseValidation);
                // }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<Probation> GetProbationDetailList(UserContextModel objUser, ListModel paginationModel)
        {
            try
            {
                var data = _lifeCycleRepository.GetProbationDetailList(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public ConfirmationMasterModel GetMastersList(UserContextModel objUser, ListModel paginationModel)
        {
            try
            {
                var data = _lifeCycleRepository.GetMastersList(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }


        public DataSet GetMovementReasonsField(UserContextModel objUser, ListModel paginationModel)
        {
            try
            {
                var data = _lifeCycleRepository.GetMovementReasonsField(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }


        public DataSet AddMovement(UserContextModel objUser, LifeCycleMovement lifeCycleMovement)
        {
            try
            {
                var data = _lifeCycleRepository.AddMovement(objUser.CompanyCode, lifeCycleMovement);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }


        public PaginationData<LifeCycleMovement> GetMovementList(UserContextModel objUser, LifeCycleMovement lifeCycleMovement)
        {
            try
            {
                var data = _lifeCycleRepository.GetMovementList(objUser.CompanyCode, lifeCycleMovement);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }


        public PaginationData<NoticePeriodDetail> GetNoticePeriodDetail(UserContextModel objUser, NoticePeriodDetail noticePeriodDetail)
        {
            try
            {
                var data = _lifeCycleRepository.GetNoticePeriodDetail(objUser.CompanyCode, noticePeriodDetail);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DataSet AddNoticePeriodDetail(UserContextModel objUser, NoticePeriodDetail noticePeriodDetail)
        {
            try
            {
                var data = _lifeCycleRepository.AddNoticePeriodDetail(objUser.CompanyCode, noticePeriodDetail);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DataSet GetSeparationType(UserContextModel objUser, NoticePeriodDetail noticePeriodDetail)
        {
            try
            {
                var data = _lifeCycleRepository.GetSeparationType(objUser.CompanyCode, noticePeriodDetail);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DataSet AddModifySeparationReason(UserContextModel objUser, LFSeparationReason lfseparationReason)
        {
            try
            {
                var data = _lifeCycleRepository.AddModifySeparationReason(objUser.CompanyCode, lfseparationReason);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }


        public PaginationData<LFSeparationReason> GetSeparationReason(UserContextModel objUser, LFSeparationReason lfseparationReason)
        {
            try
            {
                var data = _lifeCycleRepository.GetSeparationReason(objUser.CompanyCode, lfseparationReason);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }


        public DataSet GetMasterForSeparationTask(UserContextModel objUser, LFSeparationReason lfseparationReason)
        {
            try
            {
                var data = _lifeCycleRepository.GetMasterForSeparationTask(objUser.CompanyCode, lfseparationReason);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }



        public DataSet AddModifySeparationTask(UserContextModel objUser, LFSeparationTask lfSeparationTask)
        {
            try
            {
                var data = _lifeCycleRepository.AddModifySeparationTask(objUser.CompanyCode, lfSeparationTask);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<LFSeparationTask> GetSeparationTask(UserContextModel objUser, LFSeparationTask lfSeparationTask)
        {
            try
            {
                var data = _lifeCycleRepository.GetSeparationTask(objUser.CompanyCode, lfSeparationTask);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DataSet GetFieldType(UserContextModel objUser, LFSeparationTask lfSeparationTask)
        {
            try
            {
                var data = _lifeCycleRepository.GetFieldType(objUser.CompanyCode, lfSeparationTask);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DataSet AddModifySeparation(UserContextModel objUser, Separation separation)
        {
            try
            {
                var data = _lifeCycleRepository.AddModifySeparation(objUser.CompanyCode, separation);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }


        public DataSet GetSeparation(UserContextModel objUser, Separation separation)
        {
            try
            {
                var data = _lifeCycleRepository.GetSeparation(objUser.CompanyCode, separation);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }


        public DataSet AddModifyExitInterview(UserContextModel objUser, ExitInterview exitInterview)
        {
            try
            {
                var data = _lifeCycleRepository.AddModifyExitInterview(objUser.CompanyCode, exitInterview);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<ExitInterview> ExitInterviewList(UserContextModel objUser, ExitInterview exitInterview)
        {
            try
            {
                var data = _lifeCycleRepository.ExitInterviewList(objUser.CompanyCode, exitInterview);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DataSet LifeCycleSetting(UserContextModel objUser, LifeCycleSetting lifeCycleSetting)
        {
            try
            {
                var data = _lifeCycleRepository.LifeCycleSetting(objUser.CompanyCode, lifeCycleSetting);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<LifeCycleSetting> AddModifyApprovalPath(UserContextModel objUser, LifeCycleSetting lifeCycleSetting)
        {
            try
            {
                var data = _lifeCycleRepository.AddModifyApprovalPath(objUser.CompanyCode, lifeCycleSetting);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }



        public PaginationData<LifeCycleWorkFlow> GetWorkFlowList(UserContextModel objUser, ListModel model)
        {
            try
            {
                var data = _lifeCycleRepository.GetWorkFlowList(objUser.CompanyCode, model);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DataSet GetLifeCycleWorkFlowLevel(string DbName, int Id)
        {
            var data = _lifeCycleRepository.GetLifeCycleWorkFlowLevel(DbName, Id);
            return data;
        }

        public string SaveWorkFlowLevel(string DbName, WorkFlowLevelDetails model)
        {
            var data = _lifeCycleRepository.SaveWorkFlowLevel(DbName, model);
            return data;
        }

        public string SaveSeparationAndUserDefinedData(string DbName, SeparationData model)
        {
            var data = _lifeCycleRepository.SaveSeparationAndUserDefinedData(DbName, model);
            return data;
        }

        #endregion

    }
}
