using CliqueHR.BL.Implementation.AdminPanel.SetupService;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.DL.AdminPanel.Setup;
using CliqueHR.DL.Implementation.AdminPanel.Setup;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CliqueHR.BL.AdminPanelService.SetupService
{
    public class SetupService : ISetupService
    {
        private ISetupRepository _roleRepository;
        private RoleValidation _modelValidation;
        private AssignRoleValidation _modelAssignValidation;

        public SetupService()
        {
            _roleRepository = new SetupRepository();
            _modelValidation = new RoleValidation();
            _modelAssignValidation = new AssignRoleValidation();
        }

        #region Role Creation Service
        public List<Module> GetAllModule(int roleid, UserContextModel objUser)
        {
            try
            {
                var data = _roleRepository.GetAllModule(objUser.CompanyCode, roleid);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<Role> GetAllRole(UserContextModel objUser)
        {

            try
            {
                var data = _roleRepository.GetAllRole(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public Module GetModuleById(int roleid, int ModuleId, UserContextModel objUser)
        {
            try
            {
                var data = _roleRepository.GetModuleById(ModuleId, objUser.CompanyCode, roleid);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public Role GetRoleById(int RoleId, UserContextModel objUser)
        {
            try
            {
                var data = _roleRepository.GetRoleById(RoleId, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();

            }
        }

        public List<SubModule> GetSubModuleByModuleId(int roleid, string ModuleId, UserContextModel objUser)
        {
            try
            {
                var data = _roleRepository.GetSubModuleByModuleId(ModuleId, objUser.CompanyCode, roleid);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();

            }
        }

        public List<SubModuleField> GetSubModuleFieldByModuleId(int roleid, string ModuleId, UserContextModel objUser)
        {
            try
            {
                var data = _roleRepository.GetSubModuleFieldByModuleId(ModuleId, objUser.CompanyCode, roleid);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();

            }
        }

        public ApplicationResponse RoleCreation(CreateRolePostModel model, UserContextModel objUser)
        {
            try
            {
                List<ValidationResponse> objerror = new List<ValidationResponse>();
                var validationRespons = _modelValidation.Validate(RoleValidation.ValidateAll_key, model, null);
                List<Module> enableModuleList = new List<Module>();
                List<SubModule> enableList = new List<SubModule>();
                if (model.objSubModule == null || model.objSubModule.Count == 0)
                {
                    objerror.Add(new ValidationResponse {
                     Messages =
                         new List<ValidationMessage> {
                                new ValidationMessage { Message = "Select At least one sub module", Property = "Selection" }
                         }
                 });
                    //enableList = model.objSubModule.Where(x => x.IsEnable == true).ToList();
                    //if (enableList.Count <= 0)
                    //{

                    //}
                }
                if (model.objModule == null || model.objModule.Count == 0)
                {

                    objerror.Add(
            new ValidationResponse
            {
                Messages =
                    new List<ValidationMessage> {
                                new ValidationMessage { Message = "Select At least one module", Property = "Selection" }
                    }
            }
        );

                }


                if (validationRespons != null && validationRespons.Messages != null)
                {
                    objerror.Add(validationRespons);
                }
                if (objerror != null && objerror.Count > 0)
                {
                    throw new ValidationException(objerror);
                }


                //model.ForEach(x => x.CreatedBy = objUser.EmployeeId);
                model.CreatedBy = objUser.EmployeeId;
                var data = _roleRepository.RoleCreation(model, objUser.CompanyCode);
                return data;

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public ApplicationResponse AssignRole(AssignRole model, UserContextModel objUser)
        {
            try
            {
                List<ValidationResponse> objerror = new List<ValidationResponse>();
                var validationRespons = _modelAssignValidation.Validate(RoleValidation.ValidateAll_key, model, null);

                if (validationRespons != null && validationRespons.Messages != null)
                {
                    objerror.Add(validationRespons);
                }
                if (objerror != null && objerror.Count > 0)
                {
                    throw new ValidationException(objerror);
                }


                //model.ForEach(x => x.CreatedBy = objUser.UserID);
                model.CreatedBy = objUser.EmployeeId;
                var data = _roleRepository.AssignRole(model, objUser.CompanyCode);
                return data;

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public AssignRole GetAssignRoleById(int AssignId, UserContextModel objUser)
        {
            try
            {
                var data = _roleRepository.GetAssignRoleById(AssignId, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();

            }
        }

        public PaginationData<DTAssignRole> GetAssignRoleList(PaginationModel param, UserContextModel objUser)
        {
            try
            {
                var data = _roleRepository.GetAssignRoleList(param, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<Role> GetRoleList(PaginationModel param, UserContextModel objUser)
        {
            try
            {
                var data = _roleRepository.GetRoleList(param, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion Role Creation Service
    }
}
