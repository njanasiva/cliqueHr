using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Helpers.Validation;

namespace CliqueHR.Common.Models
{
    public class PageSettingsModel
    {
    }

    public class PageSettings
    {
        public string TransType { get; set; }
        public bool IsBirthdayVisible { get; set; }
        public bool IsWorkAnniversaryVisible { get; set; }
        public bool IsMarriageAnniversaryVisible { get; set; }
        public bool IsNewJoineeVisible { get; set; }
        public bool IsExitVisible { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class PageSettingImages
    {
        public int Id { get; set; }
        public string ImageType { get; set; }
        public string ImagePath { get; set; }
        public int CreatedBy { get; set; }
    }
}
