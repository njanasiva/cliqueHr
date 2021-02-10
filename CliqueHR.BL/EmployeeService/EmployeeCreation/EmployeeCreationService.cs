using CliqueHR.BL.AdminPanelService.SetupService;
using CliqueHR.BL.Implementation.AdminPanel.SetupService;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.DL.AdminPanel.Setup;
using CliqueHR.DL.Implementation.AdminPanel.Setup;
using CliqueHR.Helpers.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public class EmployeeCreationService : IEmployeeCreationService
    {
        private IEmployeeCreationRepository _employeeCreationRepository;
        private ISetupRepository _setupRepository;
        public EmployeeCreationService()
        {
            _employeeCreationRepository = new EmployeeCreationRepository();
            _setupRepository = new SetupRepository();
        }
        public PaginationData<DropdownList> GetAllEmployees(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _employeeCreationRepository.GetAllEmployees(paginationModel, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<DropdownList> GetEmployeeMultiselect(UserContextModel objUser, EmployeeFilter obj)
        {
            try
            {
                if (obj.ScreenType == "AssignRole")
                {
                    var data= _setupRepository.GetEmployeeMultiselect(obj, objUser.CompanyCode);
                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {

                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        }
    }
