using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class TimestampParams : IParamManager
    {
        public long TimeStamp { get; set; }
        public string Key { get; set; }
        public string Section { get; set; }
        public string Company { get; set; }
        public int Id { get; set; }
    }

    public class EmployeeGeneralInfoParams : IParamManager
    {
        public long TimeStamp { get; set; }
        public string Key { get; set; }
        public int EmployeeId { get; set; }
        public string Company { get; set; }
    }

    public class EntityParams : IParamManager
    {
        public long TimeStamp { get; set; }
        public string Key { get; set; }
        public string Company { get; set; }
        public int Id { get; set; }
    }

    public class CompanyParams : IParamManager
    {
        public long TimeStamp { get; set; }
        public string Key { get; set; }
        public string Company { get; set; }
    }

    public class EntityModelParams : ICacheModelParam
    {
        public string Key { get; set; }
        public string Company { get; set; }
        public Entity entity { get; set; }
    }

    public class CompanyModelParam : ICacheModelParam {
        public string Key { get; set; }
        public string Company { get; set; }
        public PageSettingImages pageSettingImages { get; set; }
}


    public class TimestampModelParam : ICacheModelParam
    {
        public string Key { get; set; }
        public string Section { get; set; }
        public long Data { get; set; }
        public string Company { get; set; }
        public int Id { get; set; }
    }
}
