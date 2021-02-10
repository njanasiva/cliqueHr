using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CliqueHR.BL
{
    public static class FileValidators
    {
        public static string ValidateHTMLFile(HttpPostedFile file, string strExtensions)
        {
            string strMsg = "Invalid file type";
            string strGetfileExt = System.IO.Path.GetExtension(file.FileName).ToUpper();
            string FileNameUpper = file.FileName;

            if (FileNameUpper.Contains("\\"))
            {
                FileNameUpper = FileNameUpper.Substring(FileNameUpper.LastIndexOf("\\") + 1);
            }
            if (FileNameUpper.Contains("/"))
            {
                FileNameUpper = FileNameUpper.Substring(1, FileNameUpper.Length - 1);
            }

            string[] Extensions = strExtensions.Split(',');
            string rgString = "^([a-zA-Z0-9\\s_\\-])+(";
            string pipe = "";
            foreach (string fileExt in Extensions)
            {
                rgString = rgString + pipe + fileExt.ToLower();
                pipe = "|";
            }
            rgString = rgString + ")$";
            Regex rg = new Regex(@rgString);
            bool FileValid = rg.IsMatch(FileNameUpper);

            if (!FileValid)
                return strMsg;

            string[] stringArray = { ".xls", ".xlsx", ".csv" };          

            foreach (string fileExt in Extensions)
            {
                if (fileExt != "" && fileExt != null)
                {
                    if (fileExt.ToUpper() == strGetfileExt.ToUpper())
                    {
                        strMsg = "Correct";
                        break;
                    }
                    else
                    {
                        strMsg = "Invalid file type";
                    }
                }
            }
            return strMsg;
        }
    }
}
