using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;


namespace CliqueHR.DL
{
    public interface IPageSettingsRepository
    {
        ApplicationResponse AddUpdatePageSettings(PageSettings model, string CompanyCode);
        ApplicationResponse AddUpdatePageSettingImages(PageSettingImages model, string CompanyCode);
        PageSettings GetPageSettings(PageSettings model, string CompanyCode);
        List<PageSettingImages> GetPageSettingImages(string CompanyCode);
        string DeleteImage(int Id, string CompanyCode);
    }
}
