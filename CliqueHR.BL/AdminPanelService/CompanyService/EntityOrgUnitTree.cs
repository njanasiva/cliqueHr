using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public class EntityOrgUnitTree : IEntityOrgUnitTree
    {
        IEntityOrgUnitTree departmentTree;
        public EntityOrgUnitTree()
        {
            departmentTree = new EntityOrgUnitDepartmentTree();
        }

        public EntityOrgunitTreeVM GenarateTree(int parentId, List<EntityOrgTreeDataModel> data, EntityOrgunitTreeVM model)
        {
            if (data.Count != 0)
            {
                var filteredData = data.Where(org => org.ParentOrgUnitId == parentId);
                if (filteredData.Count() != 0)
                {
                    foreach (var item in filteredData)
                    {
                        var orgModel = new EntityOrgunitTreeVM
                        {
                            Name = item.Name,
                            OrgUnitId = item.OrgUnitId,
                            EntityId = model.EntityId,
                            Childs = new List<EntityOrgunitTreeVM>()
                        };
                        if (item.DepartmentId == 0)
                        {
                            orgModel = GenarateTree(item.OrgUnitId, data, orgModel);
                        }
                        else
                        {
                            orgModel.DepartmentId = item.DepartmentId;
                            orgModel = departmentTree.GenarateTree(orgModel.DepartmentId, data, orgModel);
                        }
                        model.Childs.Add(orgModel);
                    }
                }
            }
            return model;
        }
    }

    public class EntityOrgUnitDepartmentTree : IEntityOrgUnitTree
    {
        public EntityOrgUnitDepartmentTree()
        {

        }

        public EntityOrgunitTreeVM GenarateTree(int parentId, List<EntityOrgTreeDataModel> data, EntityOrgunitTreeVM model)
        {
            if (data.Count != 0)
            {
                var filteredData = data.Where(org => org.ParentDepartmentId == parentId);
                if (filteredData.Count() != 0)
                {
                    foreach (var item in filteredData)
                    {
                        var orgModel = new EntityOrgunitTreeVM
                        {
                            Name = item.Name,
                            OrgUnitId = item.OrgUnitId,
                            EntityId = model.EntityId,
                            DepartmentId = item.DepartmentId,
                            Childs = new List<EntityOrgunitTreeVM>()
                        };
                        orgModel = GenarateTree(item.DepartmentId, data, orgModel);
                        model.Childs.Add(orgModel);
                    }
                }
            }
            return model;
        }
    }
}
