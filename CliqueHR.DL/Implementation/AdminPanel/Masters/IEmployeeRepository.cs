using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL.Implementation.AdminPanel.Masters
{
    public interface IEmployeeRepository
    {

        #region Employee Type
        ApplicationResponse AddEmployeeType(EmployeeType model, string DbName);

        ApplicationResponse UpdateEmployeeType(EmployeeType model, string DbName);

        EmployeeType GetEmployeeTypeById(int EmployeeTypeId, string DbName);

        PaginationData<EmployeeType> GetAllEmployeeType(string DbName, PaginationModel model);

        #endregion Employee Type

        #region Band Type
        ApplicationResponse AddBandType(BandType model, string DbName);
        ApplicationResponse UpdateBandType(BandType model, string DbName);
        BandType GetBandTypeById(int BandTypeId, string DbName);
        PaginationData<BandType> GetAllBandType(string DbName, PaginationModel model);
        #endregion

        #region Grade Type

        ApplicationResponse AddGradeType(GradeType model, string DbName);
        ApplicationResponse UpdateGradeType(GradeType model, string DbName);
        GradeType GetGradeTypeById(int GradeTypeId, string DbName);
        PaginationData<GradeType> GetAllGradeType(string DbName, PaginationModel model);
        List<GradeType> GetGradeList(string DbName);
        #endregion

        #region Center Type
        List<CenterTypeModel> GetCenterType(string DBName);
        ApplicationResponse AddCenterTypeData(CenterTypeModel centerType, string DBName);
        ApplicationResponse UpdateCenterTypeData(CenterTypeModel centerType, string DBName);
        CenterTypeModel GetCenterTypeByID(int Id, string DBName);
        PaginationData<CenterTypeModel> GetAllCenterType(PaginationModel paginationModel, string DBName);
        #endregion

        #region Cost Center Type
        ApplicationResponse AddCostCenter(CostCenterModel costCenter, string DBName);
        ApplicationResponse UpdateCostCenter(CostCenterModel model, string DBName);
        CostCenterModel GetCostCenterByID(int Id, string DBName);
        PaginationData<CostCenterModel> GetAllCostCenter(PaginationModel paginationModel, string DBName);
        #endregion
    }
}
