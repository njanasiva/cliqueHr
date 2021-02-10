using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL.Implementation.AdminPanel.EmployeeService
{
    public interface IApprovalWorkFlowService
    {
        PaginationData<ApprovalWorkflow> GetAllWorkflow(UserContextModel objUser, PaginationModel paginationModel);
        ApprovalWorkflow GetWorkflowByID(int ID, UserContextModel objUser);
        void AddWorkFlowData(ApprovalWorkflow model, UserContextModel objUser);
        void UpdateWorkFlowData(ApprovalWorkflow model, UserContextModel objUser);
        PaginationData<ApprovalPathType> GetAllApproval(UserContextModel objUser, PaginationModel paginationModel);
        PaginationData<Approver> GetAllApprover(UserContextModel objUser, PaginationModel paginationModel);
      

    }
}
