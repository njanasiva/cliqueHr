using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Helpers.Validation;

namespace CliqueHR.Common.Models
{
    public class CommonMasterModel
    {

    }

    public class CompanyType
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
    }

    public class Country
    {
        public int CountryId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class State
    {
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
