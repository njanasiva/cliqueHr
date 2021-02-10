using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;
using System;
using CliqueHR.Helpers.ExceptionHelper;
using System.Collections.Generic;
using System.Data;

namespace CliqueHR.DL
{
    public class PageSettingsRepository : IPageSettingsRepository
    {
        private readonly DBHelper _dbHelper;
        public PageSettingsRepository()
        {
            this._dbHelper = new DBHelper();
        }
        public PageSettings GetPageSettings(PageSettings model, string CompanyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("TransType", "FETCH", DataType.AsString);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[PageSetting]", sqlParameterd);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return new PageSettings
                        {
                            IsBirthdayVisible = Convert.ToBoolean(dt.Rows[0]["IsBirthdayVisible"]),
                            IsMarriageAnniversaryVisible = Convert.ToBoolean(dt.Rows[0]["IsMarriageAnniversaryVisible"]),
                            IsExitVisible = Convert.ToBoolean(dt.Rows[0]["IsExitVisible"]),
                            IsNewJoineeVisible = Convert.ToBoolean(dt.Rows[0]["IsNewJoineeVisible"]),
                            IsWorkAnniversaryVisible = Convert.ToBoolean(dt.Rows[0]["IsWorkAnniversaryVisible"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
            return null;
        }

        public List<PageSettingImages> GetPageSettingImages(string CompanyCode)
        {
            try
            {
                return _dbHelper.GetDataTableToList<PageSettingImages>(CompanyCode, "[Company].[GetPageSetting_Images]");
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public ApplicationResponse AddUpdatePageSettings(PageSettings model, string CompanyCode)
        {
            try
            {
                var parameters = new string[] { "TransType", "IsBirthdayVisible","IsWorkAnniversaryVisible","IsMarriageAnniversaryVisible",
                    "IsExitVisible","IsNewJoineeVisible", "CreatedBy", "ModifiedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[PageSetting]", sqlParameterd);
                return new ApplicationResponse
                {
                    Code = Convert.ToInt32(dt.Rows[0][0]),
                    Message = Convert.ToString(dt.Rows[0][1]),
                };
            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }

        public ApplicationResponse AddUpdatePageSettingImages(PageSettingImages model, string CompanyCode)
        {
            try
            {
                var parameters = new string[] { "Id", "ImageType", "ImagePath", "CreatedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[PageSetting_Images]", sqlParameterd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    return new ApplicationResponse
                    {
                        Code = Convert.ToInt32(dt.Rows[0]["Code"]),
                        Message = Convert.ToString(dt.Rows[0]["Message"]),
                    };
                }

            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
            return null;
        }

        public string DeleteImage(int Id, string CompanyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("Id", Id, DataType.AsInt);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[DeletePageSetting_Images]", sqlParameterd);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return Convert.ToString(dt.Rows[0]["OldImage"]);
                    }
                }
                return string.Empty;

            }
            catch (Exception ex)
            {
                var helper = new Helpers.ExceptionHelper.DataException(ex);
                throw helper.GetException();
            }
        }
    }
}
