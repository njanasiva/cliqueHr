using System;
using CliqueHR.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CliqueHR.BL
{
    public interface IPageSettingService
    {
        void AddUpdatePageSettings(PageSettings model, UserContextModel objUser);
        PageSettings GetPageSettings(UserContextModel objUser);
        PageSettingImages AddUpdatePageSettingImages(HttpRequest request, UserContextModel objUser);
        void DeletePageSettingImage(PageSettingImages model, UserContextModel objUser);
        List<PageSettingImages> GetPageSettingImages(UserContextModel objUser);
    }
}
