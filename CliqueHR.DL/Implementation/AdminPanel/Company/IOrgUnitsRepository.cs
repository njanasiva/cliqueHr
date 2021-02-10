using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;

namespace CliqueHR.DL
{
    public interface IOrgUnitsRepository
    {
        ApplicationResponse AddUpdateOrgUnits(OrgUnits model, string CompanyCode);
        PaginationData<OrgUnits> GetOrgUnits(PaginationModel model, string CompanyCode);
        OrgUnits GetOrgUnitsById(int Id, string CompanyCode);
        List<EntityOrgTreeDataModel> GetEntityOrgTreeData(string CompanyCode, int? OrUnitId);

    }
}
