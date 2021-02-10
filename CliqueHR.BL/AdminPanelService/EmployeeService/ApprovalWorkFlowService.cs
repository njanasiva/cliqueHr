using CliqueHR.BL.Implementation.AdminPanel.EmployeeService;
using CliqueHR.Common.Models;
using CliqueHR.DL.AdminPanel.Employee;
using CliqueHR.DL.Implementation.AdminPanel.Employee;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL.AdminPanelService.EmployeeService
{
    public class ApprovalWorkFlowService : IApprovalWorkFlowService
    {
        private ApprovalWorkFlowRepository  _iApprovalWorkFlow;
        private ApprovalWorkflowValidation _workFlowValidation;
        public ApprovalWorkFlowService()
        {
            _iApprovalWorkFlow = new ApprovalWorkFlowRepository();
            _workFlowValidation = new ApprovalWorkflowValidation();


        }

        public PaginationData<ApprovalWorkflow> GetAllWorkflow(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _iApprovalWorkFlow.GetAllWorkFlow(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public ApprovalWorkflow GetWorkflowByID(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _iApprovalWorkFlow.GetWorkFlowById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void AddWorkFlowData(ApprovalWorkflow model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _workFlowValidation.Validate(ApprovalWorkflowValidation.ValidateAll_key, model, "ApprovalWorkflow can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;

                var data = _iApprovalWorkFlow.AddWorkFlow(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "ApprovalWorkflow" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateWorkFlowData(ApprovalWorkflow model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _workFlowValidation.Validate(ApprovalWorkflowValidation.ValidateEditFields_key, model, "Approval Workflow model can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _iApprovalWorkFlow.UpdateWorkFlow(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Approval Workflow" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<ApprovalPathType> GetAllApproval(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _iApprovalWorkFlow.GetAllApprovalPathType(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<Approver> GetAllApprover(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _iApprovalWorkFlow.GetAllApprover(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

       
    }
}
