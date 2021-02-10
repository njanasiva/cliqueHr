using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface IEntityOrgUnitTree
    {
        EntityOrgunitTreeVM GenarateTree(int parentId, List<EntityOrgTreeDataModel> data, EntityOrgunitTreeVM model);
    }
}
