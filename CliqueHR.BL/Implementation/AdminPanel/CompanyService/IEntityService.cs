using System;
using CliqueHR.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CliqueHR.BL
{
    public interface IEntityService
    {
        void AddEntity(HttpRequest request, UserContextModel objUser);
        void UpdateEntity(HttpRequest request, UserContextModel objUser);
        PaginationData<Entity> GetEntity(PaginationModel model, UserContextModel objUser);
        Entity GetEntityById(int Id, UserContextModel objUser);
    }
}
