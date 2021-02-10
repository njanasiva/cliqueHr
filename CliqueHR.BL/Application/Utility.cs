using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CliqueHR.BL
{
    public static class Utility
    {
        public static string GetFolderPath(params string[] FolderParam)
        {
            var seperator = Path.DirectorySeparatorChar.ToString();
            string path = string.Empty;
            if(FolderParam != null && FolderParam.Length != 0)
            {
                path  += (seperator + string.Join(seperator, FolderParam) + seperator);
            }
            return path;
        }
       
    }
}
