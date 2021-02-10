using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL
{
    public interface IMasterRepository
    {
        #region Currency
        List<Currancy> GetAllCurrency(string DbName);
        PaginationData<CurrancyMapping> GetAllCurrencyMapping(string DbName, PaginationModel model);
        ApplicationResponse AddCurrencyMapping(CurrancyMapping model, string DbName);
        CurrancyMapping GetCurrencyMappingById(int CurrancyMapId, string DbName);
        ApplicationResponse UpdateCurrencyMapping(CurrancyMapping model, string DbName);
        #endregion
        
        #region region code
        ApplicationResponse AddRegion(RegionModel model, string DbName);
        ApplicationResponse UpdateRegion(RegionModel model, string DbName);
        RegionModel GetRegionById(int Id, string DbName);
        PaginationData<RegionModel> GetAllRegionData(PaginationModel model, string DbName);
        #endregion

        #region Functional Role 
        ApplicationResponse AddFunctionalRole(FunctionalRole model, string DbName);
        ApplicationResponse UpdateFunctionalRole(FunctionalRole model, string DbName);
        FunctionalRole GetFunctionalRoleById(int Id, string DbName);
        PaginationData<FunctionalRole> GetAllFunctionalRole(PaginationModel model, string DbName);
        string UpdateFunctRoleAttachment(int Id, string Attachment, int CreatedBy, string CompanyCode);
        #endregion

        #region Location 
        ApplicationResponse AddLocation(Location model, string DbName);
        ApplicationResponse UpdateLocation(Location model, string DbName);
        Location GetLocationById(int Id, string DbName);
        PaginationData<Location> GetAllLocation(PaginationModel model, string DbName);
        List<Location> GetLocationList(string DbName);
        ApplicationResponse GetLocationCode(string DbName);

        #endregion

        #region Designation
        ApplicationResponse AddDesignation(DesignationModel model, string DbName);
        ApplicationResponse UpdateDesignation(DesignationModel model, string DbName);
        DesignationResponseModel GetDesignationById(int Id, string DbName);
        PaginationData<DesignationResponseModel> GetAllDesignation(PaginationModel model, string DbName);

        #endregion
        #region Designation Auto Numbering
        ApplicationResponse AddDesignationAutoNum(DesiAutoNumberingModel model, string DbName);
        ApplicationResponse UpdateDesignationAutoNum(DesiAutoNumberingModel model, string DbName);
        DesiAutoNumberingByidModel GetDesignationAutoNumById(int Id, string DbName);
        PaginationData<DesignationResponseModel> GetAllDesignationAutoNum(PaginationModel model, string DbName);
        #endregion
    }
}
