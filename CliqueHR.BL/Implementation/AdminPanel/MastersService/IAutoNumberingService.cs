using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL.Implementation.AdminPanel.MastersService
{
    public interface IAutoNumberingService
    {
        #region LocationAutoNumber
        void AddLocationAutoNumber(AutoNumber model, UserContextModel objUser);
        void UpdateLocationAutoNumber(AutoNumber model, UserContextModel objUser);
        AutoNumber GetLocationAutoNumberById(int ID, UserContextModel objUser);
        PaginationData<AutoNumber> GetAllLocationAutoNumber(UserContextModel objUser, PaginationModel paginationModel);

        #endregion LocationAutoNumber
    }
}
