using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;

namespace CliqueHR.DL
{
    public class CommonRepository : ICommonRepository
    {
        private readonly DBHelper _dbHelper;
        public CommonRepository()
        {
            this._dbHelper = new DBHelper();
        }
        public List<CompanyType> GetAllCompanyType(string CompanyCode)
        {
            try
            {
                return _dbHelper.GetDataTableToList<CompanyType>(CompanyCode, "[Company].[TypeMasterDetails]");
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public List<Country> GetAllCountry(string CompanyCode)
        {
            try
            {
                return _dbHelper.GetDataTableToList<Country>(CompanyCode, "[Common].[CountryMasterDetails]");
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public List<State> GetAllState(int CountryId, string CompanyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("CountryId", CountryId, DataType.AsInt);
                return _dbHelper.GetDataTableToList<State>(CompanyCode, "[Common].[StateMasterDetails]", sqlParameterd);
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
        public List<City> GetAllCity(int StateId, string CompanyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("StateId", StateId, DataType.AsInt);
                return _dbHelper.GetDataTableToList<City>(CompanyCode, "[Common].[CityMasterDetails]", sqlParameterd);
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
    }
}
