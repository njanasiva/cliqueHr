using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface IEngagementService
    {
        PaginationData<EngagementGroups> EngagementMarketPlace(UserContextModel objUser, EngagementGroups engagementGroups);
        DataSet EngagementMaster(UserContextModel objUser, EngagementGroups engagementGroups);
        DataSet AddUpdateMarketPlace(UserContextModel objUser, AddUpdateMarketPlace addUpdateMarketPlace);
        DataSet AddUpdateEngagementSurvey(UserContextModel objUser, AddUpdateMarketPlace addUpdateMarketPlace);
        PaginationData<DailyContent> AddModifyDailyContent(UserContextModel objUser, DailyContent dailyContent);
    }
}
