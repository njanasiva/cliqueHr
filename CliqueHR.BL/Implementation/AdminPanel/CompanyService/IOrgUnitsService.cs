using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;

namespace CliqueHR.BL
{
    public interface IOrgUnitsService
    {
        void AddUpdateOrgUnits(OrgUnits model, UserContextModel objUser);
        PaginationData<OrgUnits> GetOrgUnits(PaginationModel model, UserContextModel objUser);
        OrgUnits GetOrgUnitsById(int Id, UserContextModel objUser);
        List<EntityOrgunitTreeVM> GetEntityOrgunitTreeData(UserContextModel objUser, int? OrUnitId);
    }
}
