using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CliqueHR.Common.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Modules { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDoNotUse { get; set; }
    }

    public class SubModule
    {
        [XmlElement]
        public int Id { get; set; }
        [XmlElement]
        public int ModuleId { get; set; }
        [XmlIgnore]
        public string Name { get; set; }
        [XmlIgnore]
        public string Code { get; set; }
        [XmlIgnore]
        public int CreatedBy { get; set; }
        [XmlIgnore]
        public bool IsDoNotUse { get; set; }
        [XmlElement]
        public bool IsSelected { get; set; }
        [XmlIgnore]
        public int Sequence { get; set; }
    }

    public class SubModuleField
    {
        [XmlElement]
        public int Id { get; set; }
        [XmlIgnore]
        public string SectionName { get; set; }
        [XmlIgnore]
        public string SectionCode { get; set; }
        [XmlIgnore]
        public int SectionSequence { get; set; }
        [XmlIgnore]
        public string FieldName { get; set; }
        [XmlIgnore]
        public int FieldSequence { get; set; }
        [XmlElement]
        public bool IsView { get; set; }
        [XmlElement]
        public bool IsAddEdit { get; set; }
        [XmlElement]
        public bool IsMandatory { get; set; }
        [XmlElement]
        public int ModuleId { get; set; }
        [XmlIgnore]
        public int CreatedBy { get; set; }
        [XmlIgnore]
        public bool IsDoNotUse { get; set; }
        [XmlIgnore]
        public string Code { get; set; }

    }

    [XmlRoot("CreateRolePostModel")]
    public class CreateRolePostModel
    {
        [XmlElement]
        public int RoleId { get; set; }
        [XmlElement]
        public string RoleName { get; set; }
        //[XmlElement]
        //public int ModuleId { get; set; }
        [XmlElement]
        public int CreatedBy { get; set; }
        [XmlElement]
        public List<SubModuleField> objSubModuleFields { get; set; }
        [XmlElement]
        public List<SubModule> objSubModule { get; set; }
        [XmlElement]
        public List<Module> objModule { get; set; }
    }

    public class CreateRolePostModelXML
    {
        public string strxml { get; set; }
    }

    public class Module
    {
        [XmlElement]
        public int Id { get; set; }
        [XmlIgnore]
        public string Name { get; set; }
        [XmlIgnore]
        public string Description { get; set; }
        [XmlIgnore]
        public int CreatedBy { get; set; }
        [XmlIgnore]
        public bool IsDoNotUse { get; set; }
        [XmlElement]
        public bool IsSelected { get; set; }
        [XmlElement]
        public string Code { get; set; }
    }



    public class RoleValidation : AbstractValidator<CreateRolePostModel>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public RoleValidation()
        {
            this[ValidateAll_key] = ValidateAll;
        }
        private List<ValidationMessage> ValidateAll(CreateRolePostModel model)
        {
            var message = new List<ValidationMessage>();
            if (string.IsNullOrEmpty(model.RoleName))
            {
                message.Add(new ValidationMessage
                {
                    Property = "RoleName",
                    Message = "Role Name can not be blank."
                });
            }

            return message;
        }


    }
    public class AssignRoleValidation : AbstractValidator<AssignRole>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public AssignRoleValidation()
        {
            this[ValidateAll_key] = ValidateAll;
        }

        private List<ValidationMessage> ValidateAll(AssignRole model)
        {
            var message = new List<ValidationMessage>();
            if (model.RoleId == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Role",
                    Message = "Role can not be blank."
                });
            }
            else if (model.employees == null || model.employees.Count == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Employees",
                    Message = "Employees can not be blank."
                });
            }
            else if (model.objAttributeMaster == null || model.objAttributeMaster.Count == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Access Level",
                    Message = "Access level can not be blank."
                });
            }
            else if (model.objAttributeMaster != null)
            {
                foreach (AttributeMaster obj in model.objAttributeMaster)
                {
                    bool isvalid = true;
                    string code = obj.Code;
                    List<AccessLevelMaster> list = obj.masters;

                    if (list == null || list.Count <= 0)
                    {
                        isvalid = false;
                        if (code == "Entity")
                        {
                            message.Add(new ValidationMessage
                            {
                                Property = "Entity",
                                Message = "Entities can not be blank."
                            });
                        }
                        else if (code == "OU")
                        {
                            message.Add(new ValidationMessage
                            {
                                Property = "ORG UNIT",
                                Message = "ORG Unit can not be blank."
                            });
                        }
                        else if (code == "DEPT")
                        {
                            message.Add(new ValidationMessage
                            {
                                Property = "Department",
                                Message = "Department can not be blank."
                            });
                        }
                        else if (code == "CT")
                        {
                            message.Add(new ValidationMessage
                            {
                                Property = "Centre Type",
                                Message = "Centre Type can not be blank."
                            });
                        }
                        else if (code == "REG")
                        {
                            message.Add(new ValidationMessage
                            {
                                Property = "Region",
                                Message = "Region can not be blank."
                            });
                        }
                        else if (code == "LOC")
                        {
                            message.Add(new ValidationMessage
                            {
                                Property = "Location",
                                Message = "Location can not be blank."
                            });
                        }
                        else if (code == "ET")
                        {
                            message.Add(new ValidationMessage
                            {
                                Property = "Employee Type",
                                Message = "Employee Type can not be blank."
                            });
                        }
                        else if (code == "CC")
                        {
                            message.Add(new ValidationMessage
                            {
                                Property = "Cost Centre",
                                Message = "Cost Centre can not be blank."
                            });
                        }
                        else if (code == "GD")
                        {
                            message.Add(new ValidationMessage
                            {
                                Property = "Grade",
                                Message = "Grade can not be blank."
                            });
                        }

                    }
                    if (!isvalid)
                        break;


                }

            }
            return message;
        }
    }

    [XmlRoot("AssignRole")]
    public class AssignRole
    {
        [XmlElement]
        public int Id { get; set; }
        [XmlElement]
        public int RoleId { get; set; }
        [XmlElement]
        public List<Employee> employees { get; set; }
        [XmlElement]
        public List<AttributeMaster> objAttributeMaster { get; set; }
        [XmlElement]
        public int CreatedBy { get; set; }
    }

    public class DTAssignRole
    {
        public int totalCount { get; set; }
        public int AssignId { get; set; }
        public string RoleName { get; set; }
        public string ShortRoleName { get; set; }
        public string EmployeeName { get; set; }
        public string ShortEmployeeName { get; set; }
        public string AttributeUnits { get; set; }
        public string ShortAttributeUnits { get; set; }
    }

    public class AccessLevelMaster
    {
        [XmlElement]
        public int Id { get; set; }
        [XmlElement]
        public string Name { get; set; }
    }
    public class AttributeMaster
    {
        [XmlElement]
        public int Id { get; set; }
        [XmlElement]
        public string Code { get; set; }
        [XmlElement]
        public List<AccessLevelMaster> masters { get; set; }
    }

    public class AttributeMasterValue
    {
        public int Id { get; set; }
    }
}
