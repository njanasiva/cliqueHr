using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CliqueHR.BL
{
    public interface IMasterService
    {
        #region Currency
        void AddCurrencyMapping(CurrancyMapping model, UserContextModel objUser);
        PaginationData<CurrancyMapping> GetAllCurrencyMapping(UserContextModel objUser, PaginationModel paginationModel);
        CurrancyMapping GetCurrencyMappingById(int ID, UserContextModel objUser);
        void UpdateCurrencyMapping(CurrancyMapping model, UserContextModel objUser);
        List<Currancy> GetAllCurrency(UserContextModel objUser);
        #endregion
        
        #region region Code
        void AddRegion(RegionModel model, UserContextModel objUser);
        void UpdateRegion(RegionModel model, UserContextModel objUser);
        RegionModel GetRegionById(int Id, UserContextModel objUser);
        PaginationData<RegionModel> GetAllRegionData(PaginationModel model,UserContextModel objUser);
        #endregion

        #region Functinal Role 
        void AddFunctionalRole(HttpRequest request, UserContextModel objUser);
        void UpdateFunctionalRole(HttpRequest request, UserContextModel objUser);
        FunctionalRole GetFunctionalRoleById(int Id, UserContextModel objUser);
        PaginationData<FunctionalRole> GetAllFunctionalRole(PaginationModel model, UserContextModel objUser);
        #endregion

        #region Location 
        void AddLocation(Location model, UserContextModel objUser);
        void UpdateLocation(Location model, UserContextModel objUser);
        Location GetLocationById(int Id, UserContextModel objUser);
        PaginationData<Location> GetAllLocationData(PaginationModel model, UserContextModel objUser);
        List<Location> GetLocationList(UserContextModel objUser);
        string GetLocationCode(UserContextModel objUser);
        #endregion

        #region Designation
        void AddDesignation(DesignationModel model, UserContextModel objUser);
        void UpdateDesignation(DesignationModel model, UserContextModel objUser);
        DesignationResponseModel GetDesignationById(int Id, UserContextModel objUser);
        PaginationData<DesignationResponseModel> GetAllDesignation(PaginationModel model, UserContextModel objUser);
        #endregion

        #region Designation Auto Number
        void AddDesignationAutoNum(DesiAutoNumberingModel model, UserContextModel objUser);
        void UpdateDesignationAutoNum(DesiAutoNumberingModel model, UserContextModel objUser);
        DesiAutoNumberingByidModel GetDesignationAutoNumById(int Id, UserContextModel objUser);
        PaginationData<DesignationResponseModel> GetAllDesignationAutoNum(PaginationModel model, UserContextModel objUser);
        #endregion
    }
}
