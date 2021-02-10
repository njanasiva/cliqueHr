using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL
{

    public interface IEngagementRepository
    {
         PaginationData<EngagementGroups> EngagementMarketPlace(string DBName, EngagementGroups model);
        DataSet EngagementMaster(string DBName, EngagementGroups model);
        DataSet AddUpdateMarketPlace(string DBName, AddUpdateMarketPlace model);
        DataSet AddUpdateEngagementSurvey(string DBName, AddUpdateMarketPlace model);
        PaginationData<DailyContent> AddModifyDailyContent(string DBName, DailyContent model);

    }
}
