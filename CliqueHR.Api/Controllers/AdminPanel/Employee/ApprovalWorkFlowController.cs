using CliqueHR.Api.Application;
using CliqueHR.BL.AdminPanelService.EmployeeService;
using CliqueHR.BL.Implementation.AdminPanel.EmployeeService;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CliqueHR.Api.Controllers.AdminPanel.Employee
{
    public class ApprovalWorkFlowController : ApiController
    {
        private IApprovalWorkFlowService _iApprovalWorkFlowService;

        public ApprovalWorkFlowController()
        {
            _iApprovalWorkFlowService = new ApprovalWorkFlowService();
        }


        [HttpPost]
        public HttpResponseMessage GetAllWorkflow(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("ApprovalWorkFlowController:GetAllWorkflow", "GetAllWorkflow START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _iApprovalWorkFlowService.GetAllWorkflow(objUser, paginationModel);
                Log.Info("ApprovalWorkFlowController:GetAllWorkflow", "GetAllWorkflow END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetWorkFlowById(int Id)
        {
            try
            {
                Log.Info("ApprovalWorkFlowController:GetWorkFlowById", "GetWorkFlowById START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _iApprovalWorkFlowService.GetWorkflowByID(Id, objUser);
                Log.Info("ApprovalWorkFlowController:GetWorkFlowById", "GetWorkFlowById END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddWorkFlow(ApprovalWorkflow workFlowModel)
        {
            try
            {
                Log.Info("ApprovalWorkFlowController:AddWorkFlow", "AddWorkFlow START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _iApprovalWorkFlowService.AddWorkFlowData(workFlowModel, objUser);
                Log.Info("ApprovalWorkFlowController:AddWorkFlow", "AddWorkFlow END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateWorkFlow(ApprovalWorkflow workFlowModel)
        {
            try
            {
                Log.Info("ApprovalWorkFlowController:UpdateWorkFlow", "UpdateWorkFlow START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _iApprovalWorkFlowService.UpdateWorkFlowData(workFlowModel, objUser);
                Log.Info("ApprovalWorkFlowController:UpdateWorkFlow", "UpdateWorkFlow END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data updated Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAllApprovalPath(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("ApprovalWorkFlowController:GetAllApprovalPath", "GetAllApprovalPath START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _iApprovalWorkFlowService.GetAllApproval(objUser, paginationModel);
                Log.Info("ApprovalWorkFlowController:GetAllApprovalPath", "GetAllApprovalPath END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAllApprover(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("ApprovalWorkFlowController:GetAllApprover", "GetAllApprover START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _iApprovalWorkFlowService.GetAllApprover(objUser, paginationModel);
                Log.Info("ApprovalWorkFlowController:GetAllApprover", "GetAllApprover END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        

    }
}
