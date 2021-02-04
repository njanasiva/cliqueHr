using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class HelpDeskModel
    {
       
    }
    public class GetHelpDiskMaster
    {
        public int UserId { get; set; }
        public int ActionTypeId { get; set; }

    }
    public class AddModifyCategory
    {
        public int UserId { get; set; }
        public int ActionTypeId { get; set; }
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public SortType Sort { get; set; }
        public string SearchText { get; set; }
        public int CategoryLeadId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Prefix { get; set; }
        public string Description { get; set; }
        public string TeamMembers { get; set; }
        public bool IsDoNotUse { get; set; }
        public int Success { get; set; }
        public int Total { get; set; }

    }

}
