using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;

namespace CliqueHR.DL
{
    public interface IEntityRepository
    {
        ApplicationResponse AddEntity(Entity model, string CompanyCode);
        ApplicationResponse UpdateEntity(Entity model, string CompanyCode);
        PaginationData<Entity> GetEntity(PaginationModel model, string CompanyCode);
        List<Entity> GetEntity(string CompanyCode);
        Entity GetEntityById(int Id, string CompanyCode);
        string UpdateLogo(int EntityId, string Logo, string CompanyCode);

    }
}
