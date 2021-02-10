using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public class EntityCacheModels: ICacheModelComparator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string ContcatNo { get; set; }
        public string Logo { get; set; }
        public bool IsDoNotUse { get; set; }

        public bool DetectChanges(ICacheModelParam param)
        {
            var entity = (param as EntityParams).entity;
            if (entity == null)
            {
                return true;
            }
            if (this.Name != entity.Name || this.IsDoNotUse != entity.IsDoNotUse || this.Logo != entity.Logo || this.Address != entity.Address ||
                this.StateId != entity.StateId || this.CountryId != entity.CountryId ||this.ContcatNo != entity.ContcatNo ||
                this.Code != entity.Code || this.CityId != entity.CityId)
            {
                return true;
            }
            return false;
        }
    }

    public class EmployeeGeneralInfoCacheModels : ICacheModelComparator
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfJoining { get; set; }

        public bool DetectChanges(ICacheModelParam param)
        {
            var employeeParam = (param as EmployeeGeneralInfoParams);
            // Add comparison logic
            return false;
        }
    }
}
