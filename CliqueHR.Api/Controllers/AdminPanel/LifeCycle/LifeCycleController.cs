using CliqueHR.Api.Application;
using CliqueHR.BL;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CliqueHR.Api.Controllers
{
    //[EnableCors(origins: "", headers: "*", methods: "*")]
    public class LifeCycleController : ApiController
    {
        private ILifeCycleService _lifeCycleService;
        public LifeCycleController()
        {
            _lifeCycleService = new LifeCycleService();
        }

        #region Probation
        [HttpPost]
        public HttpResponseMessage GetProbationDetailList(ListModel paginationModel)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetProbationDetailList(objUser, paginationModel);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddModifyProbationDetail(Probation model)
        {
            try
            {
                //Log.Info("QualificationController:AddCourseType", "Add CourseType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                _lifeCycleService.AddModifyProbationDetail(model, objUser);
                // Log.Info("QualificationController:AddCourseType", "Add CourseType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetMastersList(ListModel paginationModel)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetMastersList(objUser, paginationModel);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetMovementReasonsField(ListModel paginationModel)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetMovementReasonsField(objUser, paginationModel);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage AddMovement(LifeCycleMovement lifeCycleMovement)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.AddMovement(objUser, lifeCycleMovement);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }



        [HttpPost]
        public HttpResponseMessage GetMovementList(LifeCycleMovement lifeCycleMovement)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetMovementList(objUser, lifeCycleMovement);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetNoticePeriodDetail(NoticePeriodDetail noticePeriodDetail)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetNoticePeriodDetail(objUser, noticePeriodDetail);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage AddNoticePeriodDetail(NoticePeriodDetail noticePeriodDetail)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.AddNoticePeriodDetail(objUser, noticePeriodDetail);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetSeparationType(NoticePeriodDetail noticePeriodDetail)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetSeparationType(objUser, noticePeriodDetail);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage AddModifySeparationReason(LFSeparationReason lfseparationReason)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.AddModifySeparationReason(objUser, lfseparationReason);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetSeparationReason(LFSeparationReason lfseparationReason)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetSeparationReason(objUser, lfseparationReason);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetMasterForSeparationTask(LFSeparationReason lfseparationReason)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetMasterForSeparationTask(objUser, lfseparationReason);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage AddModifySeparationTask(LFSeparationTask lfSeparationTask)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.AddModifySeparationTask(objUser, lfSeparationTask);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetSeparationTask(LFSeparationTask lfSeparationTask)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetSeparationTask(objUser, lfSeparationTask);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetFieldType(LFSeparationTask lfSeparationTask)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetFieldType(objUser, lfSeparationTask);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddModifySeparation(Separation separation)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.AddModifySeparation(objUser, separation);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }



        [HttpPost]
        public HttpResponseMessage GetSeparation(Separation separation)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetSeparation(objUser, separation);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage AddModifyExitInterview(ExitInterview exitInterview)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.AddModifyExitInterview(objUser, exitInterview);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExitInterviewList(ExitInterview exitInterview)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.ExitInterviewList(objUser, exitInterview);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage LifeCycleSetting(LifeCycleSetting lifeCycleSetting)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.LifeCycleSetting(objUser, lifeCycleSetting);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage AddModifyApprovalPath(LifeCycleSetting lifeCycleSetting)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.AddModifyApprovalPath(objUser, lifeCycleSetting);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetWorkFlowList(ListModel model)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetWorkFlowList(objUser, model);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetLifeCycleWorkFlowLevel(int Id)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _lifeCycleService.GetLifeCycleWorkFlowLevel(objUser.CompanyCode, Id);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage SaveWorkFlow(Dictionary<string, object> adicValues)
        {
            try
            {
                string objValue = JsonConvert.SerializeObject(adicValues);
                WorkFlowLevelDetails lobjWorkFlowLevelDetails = JsonConvert.DeserializeObject<WorkFlowLevelDetails>(objValue);
                var ObjLifeCycleWorkFlowLevel = JsonConvert.DeserializeObject<List<LifeCycleWorkFlowLevel>>(lobjWorkFlowLevelDetails.ApproverList);
                lobjWorkFlowLevelDetails.WorkFlowLevelData = new List<WorkFlowLevel>();
                foreach (var item in ObjLifeCycleWorkFlowLevel)
                {
                    if (item.ApproverId.Contains(","))
                    {
                        string[] larr = item.ApproverId.Split(',');
                        foreach (var items in larr)
                        {
                            lobjWorkFlowLevelDetails.WorkFlowLevelData.Add(
                                new WorkFlowLevel()
                                {
                                    ApproverId = Convert.ToInt32(items),
                                    EscalateAfter = item.EscalateAfter,
                                    Escalateto = item.Escalateto,
                                    Followup = item.Followup,
                                    LifeCycleWorkFlowId = lobjWorkFlowLevelDetails.LifeCycleWorkFlowId
                                }
                            );
                        }
                    }
                    else
                    {
                        lobjWorkFlowLevelDetails.WorkFlowLevelData.Add(
                            new WorkFlowLevel()
                            {
                                ApproverId = Convert.ToInt32(item.ApproverId),
                                EscalateAfter = item.EscalateAfter,
                                Escalateto = item.Escalateto,
                                Followup = item.Followup,
                                LifeCycleWorkFlowId = lobjWorkFlowLevelDetails.LifeCycleWorkFlowId
                            }
                        );
                    }
                }
                if (lobjWorkFlowLevelDetails != null)
                {
                    var data = _lifeCycleService.SaveWorkFlowLevel("cliquehrsql", lobjWorkFlowLevelDetails);
                    return this.Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
            return null;
        }



        #endregion
    }

}
