using System;
using System.Collections.Generic;
using System.Linq;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
namespace CliqueHR.BL
{
    public class DepartmentService : IDepartmentService
    {
        private IDepartmentRepository _DepartmentRepository;
        private IOrgUnitsRepository orgUnitsRepository;
        private DepartmentValidation _modelValidation;
        private IEntityRepository entityRepository;
        public DepartmentService()
        {
            _DepartmentRepository = new DepartmentRepository();
            orgUnitsRepository = new OrgUnitsRepository();
            _modelValidation = new DepartmentValidation();
            entityRepository = new EntityRepository();
        }

        public void AddUpdateDepartment(Department model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelValidation.Validate(DepartmentValidation.ValidateAll_key, model, "Department model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                if (model.Id == 0)
                    model.CreatedBy = objUser.EmployeeId;
                else
                    model.ModifiedBy = objUser.EmployeeId;
                var data = _DepartmentRepository.AddUpdateDepartment(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Department" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<Department> GetDepartments(PaginationModel model, UserContextModel objUser)
        {
            try
            {

                var data = _DepartmentRepository.GetDepartments(model, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public Department GetDepartmentById(int Id, UserContextModel objUser)
        {
            try
            {
                var data = _DepartmentRepository.GetDepartmentById(Id, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<EntityOrgunitTreeVM> GetEntityOrgunitDeptTreeData(UserContextModel objUser, int? DepartmentId)
        {
            List<EntityOrgunitTreeVM> responseData = null;
            try
            {
                var rootEntity = entityRepository.GetEntity(objUser.CompanyCode);
                if (rootEntity != null && rootEntity.Count != 0)
                {
                    var data = orgUnitsRepository.GetEntityOrgTreeData(objUser.CompanyCode, 0);
                    if (data != null && data.Count != 0)
                    {
                        var deptData = _DepartmentRepository.GetEntityOrgDeptTreeData(objUser.CompanyCode, DepartmentId);
                        if (deptData != null && deptData.Count != 0)
                        {
                            data.AddRange(deptData);
                        }
                        responseData = rootEntity.Select(orgu => new EntityOrgunitTreeVM
                        {
                            Name = orgu.Name,
                            EntityId = orgu.Id,
                            Childs = new List<EntityOrgunitTreeVM>()
                        }).GroupBy(orgux => orgux.EntityId).Select(orguz => orguz.First()).ToList();
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
                            else if (orgunit.DepartmentId != 0)
                            {
                                if (orgunit.ParentOrgUnitId != 0)
                                {
                                    int j = data.FindIndex(x => x.OrgUnitId == orgunit.ParentOrgUnitId);
                                    if (i > j)
                                    {
                                        continue;
                                    }
                                }
                                if (orgunit.ParentDepartmentId != 0)
                                {
                                    int j = data.FindIndex(x => x.OrgUnitId == orgunit.ParentDepartmentId);
                                    if (i > j)
                                    {
                                        continue;
                                    }
                                }
                                model = new EntityOrgUnitDepartmentTree().GenarateTree(orgunit.DepartmentId, data, model);
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

        public string GetDepartmentCode(UserContextModel objUser)
        {
            try
            {
                var data = _DepartmentRepository.GetDepartmentCode(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public string GetDeptCode(UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var data = _DepartmentRepository.GetDeptCode(objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Department" });
                    throw new ValidationException(responseValidation);
                }
                return data.Message;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
    }
}
