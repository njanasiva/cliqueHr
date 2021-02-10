using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;

namespace CliqueHR.BL
{
    public class OrgUnitsService : IOrgUnitsService
    {
        private IOrgUnitsRepository _OrgUnitsRepository;
        private IEntityRepository entityRepository;
        private OrgUnitsValidation _modelValidation;
        public OrgUnitsService()
        {
            _OrgUnitsRepository = new OrgUnitsRepository();
            _modelValidation = new OrgUnitsValidation();
            entityRepository = new EntityRepository();
        }

        public void AddUpdateOrgUnits(OrgUnits model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelValidation.Validate(SecuritySettingValidation.ValidateAll_key, model, "OrgUnits model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                if (model.Id == 0)
                    model.CreatedBy = objUser.EmployeeId;
                else
                    model.ModifiedBy = objUser.EmployeeId;

                var data = _OrgUnitsRepository.AddUpdateOrgUnits(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "OrgUnits" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<OrgUnits> GetOrgUnits(PaginationModel model, UserContextModel objUser)
        {
            try
            {

                var data = _OrgUnitsRepository.GetOrgUnits(model, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public OrgUnits GetOrgUnitsById(int Id, UserContextModel objUser)
        {
            try
            {
                var data = _OrgUnitsRepository.GetOrgUnitsById(Id, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<EntityOrgunitTreeVM> GetEntityOrgunitTreeData(UserContextModel objUser, int? OrUnitId)
        {
            List<EntityOrgunitTreeVM> responseData = null;
            try
            {
                var rootEntity = entityRepository.GetEntity(objUser.CompanyCode);
                if (rootEntity != null && rootEntity.Count != 0)
                {
                    var data = _OrgUnitsRepository.GetEntityOrgTreeData(objUser.CompanyCode, OrUnitId);

                    responseData = rootEntity.Select(orgu => new EntityOrgunitTreeVM
                    {
                        Name = orgu.Name,
                        EntityId = orgu.Id,
                        Childs = new List<EntityOrgunitTreeVM>()
                    }).ToList();
                    if (data != null)
                    {
                        for (var i = 0; i < data.Count; i++)
                        {
                            var orgunit = data[i];
                            var model = new EntityOrgunitTreeVM
                            {
                                Name = orgunit.Name,
                                EntityId = orgunit.ParentEntityId,
                                OrgUnitId = orgunit.OrgUnitId,
                                DepartmentId = orgunit.DepartmentId,
                                Childs = new List<EntityOrgunitTreeVM>()
                            };
                            if (orgunit.OrgUnitId != 0)
                            {
                                if (orgunit.ParentOrgUnitId != 0)
                                {
                                    int j = data.FindIndex(x => x.OrgUnitId == orgunit.ParentOrgUnitId);
                                    if (i > j)
                                    {
                                        continue;
                                    }
                                }
                                model = new EntityOrgUnitTree().GenarateTree(orgunit.OrgUnitId, data, model);
                                responseData.First(x => x.EntityId == orgunit.ParentEntityId).Childs.Add(model);
                            }
                        }
                    }
                }
                return responseData;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
    }
}
