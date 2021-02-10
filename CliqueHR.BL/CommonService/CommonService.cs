using System;
using System.Collections.Generic;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;

namespace CliqueHR.BL
{
    public class CommonService : ICommonService
    {
        private ICommonRepository _CommonRepository;

        public CommonService()
        {
            _CommonRepository = new CommonRepository();
        }

        public List<CompanyType> GetAllCompanyType(UserContextModel objUser)
        {
            try
            {
                var data = _CommonRepository.GetAllCompanyType(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<Country> GetAllCountry(UserContextModel objUser)
        {
            try
            {
                var data = _CommonRepository.GetAllCountry(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<State> GetAllState(int CountryId, UserContextModel objUser)
        {
            try
            {
                var data = _CommonRepository.GetAllState(CountryId, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<City> GetAllCity(int StateId, UserContextModel objUser)
        {
            try
            {
                var data = _CommonRepository.GetAllCity(StateId, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
    }
}
