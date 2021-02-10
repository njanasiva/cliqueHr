using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL.Implementation.AdminPanel.Employee
{
    public interface IApprovalWorkFlow
    {
        PaginationData<ApprovalWorkflow> GetAllWorkFlow(string DbName, PaginationModel model);
        ApprovalWorkflow GetWorkFlowById(int WorkFlowId, string DbName);
        ApplicationResponse AddWorkFlow(ApprovalWorkflow model, string DbName);
        ApplicationResponse UpdateWorkFlow(ApprovalWorkflow model, string DbName);
        PaginationData<ApprovalPathType> GetAllApprovalPathType(string DbName, PaginationModel model);
        PaginationData<Approver> GetAllApprover(string DbName, PaginationModel model);
       
    }
}
