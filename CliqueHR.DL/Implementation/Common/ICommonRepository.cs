using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;


namespace CliqueHR.DL
{
    public interface ICommonRepository
    {
        List<CompanyType> GetAllCompanyType(string CompanyCode);
        List<Country> GetAllCountry(string CompanyCode);
        List<State> GetAllState(int Countryid, string CompanyCode);
        List<City> GetAllCity(int StateId, string CompanyCode);
    }
}
