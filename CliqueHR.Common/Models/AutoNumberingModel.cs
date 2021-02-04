using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class AutoNumberingModel
    {
    }

    public class AutoNumbering
    {
        public string AutoType { get; set; }
        public string Prefix { get; set; }
        public int AppendNumber { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
