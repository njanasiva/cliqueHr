using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.DataHelper;

namespace CliqueHR.DL
{
    public class GroupCompanyRepository : IGroupCompanyRepository
    {
        private readonly DBHelper _dbHelper;
        public GroupCompanyRepository()
        {
            this._dbHelper = new DBHelper();
        }
        public GroupCompany GetGroupCompany(GroupCompany model, string CompanyCode)
        {
            try
            {
                var sqlParameterd = _dbHelper.CreateSqlParameter("TransType", "FETCH", DataType.AsString);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[GroupCompanyDetails]", sqlParameterd);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return new GroupCompany
                        {
                            Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                            Name = Convert.ToString(dt.Rows[0]["Name"]),
                            Code = Convert.ToString(dt.Rows[0]["Code"]),
                            TypeId = Convert.ToInt32(dt.Rows[0]["TypeId"]),
                            IncorporationDate = Convert.ToDateTime(dt.Rows[0]["IncorporationDate"]),
                            Address = Convert.ToString(dt.Rows[0]["Address"]),
                            CountryId = Convert.ToInt32(dt.Rows[0]["CountryId"]),
                            StateId = Convert.ToInt32(dt.Rows[0]["StateId"]),
                            CityId = Convert.ToInt32(dt.Rows[0]["CityId"]),
                            PinCode = Convert.ToInt32(dt.Rows[0]["PinCode"]),
                            ContcatNo = Convert.ToString(dt.Rows[0]["ContcatNo"]),
                            WebSite = Convert.ToString(dt.Rows[0]["WebSite"]),
                            PAN = Convert.ToString(dt.Rows[0]["PAN"]),
                            TAN = Convert.ToString(dt.Rows[0]["TAN"]),
                            GSTIN = Convert.ToString(dt.Rows[0]["GSTIN"]),
                            PF = Convert.ToString(dt.Rows[0]["PF"]),
                            ESIC = Convert.ToString(dt.Rows[0]["ESIC"])
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

        public ApplicationResponse AddUpdateGroupCompany(GroupCompany model, string CompanyCode)
        {
            try
            {
                var parameters = new string[] { "TransType", "Id", "Name", "Code", "TypeId", "IncorporationDate", "Address", "CountryId", "StateId", "CityId", "PinCode", "ContcatNo", "WebSite", "PAN", "TAN", "GSTIN", "PF", "ESIC", "CreatedBy", "ModifiedBy" };
                var sqlParameterd = _dbHelper.CreateSqlParamByObj(model, parameters);
                DataTable dt = _dbHelper.GetDataTable(CompanyCode, "[Company].[GroupCompanyDetails]", sqlParameterd);
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

    }
}
