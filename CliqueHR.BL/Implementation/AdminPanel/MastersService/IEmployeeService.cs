using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL.Implementation.AdminPanel.MastersService
{
    public interface IEmployeeService
    {

        #region Employee Type
        void AddEmployeeTypeData(EmployeeType model, UserContextModel objUser);
        void UpdateEmployeeTypeData(EmployeeType model, UserContextModel objUser);
        EmployeeType GetEmployeeTypeByID(int ID, UserContextModel objUser);
        PaginationData<EmployeeType> GetAllEmployeeType(UserContextModel objUser, PaginationModel paginationModel);

        #endregion Employee Type

        #region Band Type
        void AddBandTypeData(BandType model, UserContextModel objUser);
        void UpdateBandTypeData(BandType model, UserContextModel objUser);
        BandType GetBandTypeByID(int ID, UserContextModel objUser);
        PaginationData<BandType> GetAllBandType(UserContextModel objUser, PaginationModel paginationModel);

        #endregion

        #region Grade Type
        void AddGradeTypeData(GradeType model, UserContextModel objUser);
        void UpdateGradeTypeData(GradeType model, UserContextModel objUser);
        GradeType GetGradeTypeByID(int ID, UserContextModel objUser);
        PaginationData<GradeType> GetAllGradeType(UserContextModel objUser, PaginationModel paginationModel);
        List<GradeType> GetGradeList(UserContextModel objUser);
        #endregion

        #region Center Type

        List<CenterTypeModel> GetCenterType(UserContextModel objUser);
        void AddCenterTypeData(CenterTypeModel centerType, UserContextModel objUser);
        void UpdateCenterTypeData(CenterTypeModel centerType, UserContextModel objUser);
        CenterTypeModel GetCenterTypeByID(int ID, UserContextModel objUser);
        PaginationData<CenterTypeModel> GetAllCenterType(PaginationModel paginationModel,UserContextModel objUser);
        #endregion

        #region Cost Center Type
        void AddCostCenter(CostCenterModel costCenter, UserContextModel objUser);
        void UpdateCostCenter(CostCenterModel model, UserContextModel objUser);
        CostCenterModel GetCostCenterByID(int ID, UserContextModel objUser);
        PaginationData<CostCenterModel> GetAllCostCenter(PaginationModel paginationModel, UserContextModel objUser);
        #endregion

    }
}
