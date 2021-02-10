using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CliqueHR.DL.Implementation.AdminPanel.Masters
{
    public interface IAutoNumberingRepository
    {
        #region LocationAutoNumber
        ApplicationResponse AddLocationAutoNumber(AutoNumber model, string DbName);

        ApplicationResponse UpdateLocationAutoNumber(AutoNumber model, string DbName);

        AutoNumber GetLocationAutoNumberById(int AutoNumberId, string DbName);

        PaginationData<AutoNumber> GetAllLocationAutoNumber(string DbName, PaginationModel model);



        #endregion

    }
}
