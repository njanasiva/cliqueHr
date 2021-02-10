using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CliqueHR.Common.Models
{
    public class Employee
    {
        [XmlElement]
        public Int64 ID { get; set; }
        [XmlElement]
        public string EmployeeCode { get; set; }
        [XmlIgnore]
        public string Salutation { get; set; }
        [XmlElement]
        public string FirstName { get; set; }
        [XmlElement]
        public string LastName { get; set; }
        [XmlIgnore]
        public string MiddleName { get; set; }
        [XmlIgnore]
        public string DateOfJoining { get; set; }
        [XmlIgnore]
        public string GroupDOJ { get; set; }
        [XmlIgnore]
        public string DateOfConfirmation { get; set; }
        [XmlIgnore]
        public string RetirementDate { get; set; }
        [XmlIgnore]
        public string Password { get; set; }
        [XmlIgnore]
        public string LastLogin { get; set; }
    }

    public class DropdownList
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }

    
}
