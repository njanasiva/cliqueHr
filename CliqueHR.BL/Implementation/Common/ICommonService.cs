using System;
using System.Collections.Generic;
using CliqueHR.Common.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface ICommonService
    {
        List<CompanyType> GetAllCompanyType(UserContextModel objUser);
        List<Country> GetAllCountry(UserContextModel objUser);
        List<State> GetAllState(int CountryId, UserContextModel objUser);
        List<City> GetAllCity(int StateId, UserContextModel objUser);
    }
}
