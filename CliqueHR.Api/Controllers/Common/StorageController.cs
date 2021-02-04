using CliqueHR.Api.Application;
using CliqueHR.BL;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace CliqueHR.Api.Controllers.Common
{
    public class StorageController : ApiController
    {
        private IStorageService _storageService;
        public StorageController()
        {
            _storageService = new FileStorageService();
        }

        [HttpGet]
        public HttpResponseMessage GetPrivate(string path)
        {
            FileReadData data = null;
            try
            {
                Log.Info("StorageController:GetPrivate", "Get Private Start", string.Empty);
                data = _storageService.ReadFileData(path, true);
                var mimiType = MimeMapping.GetMimeMapping(data.FileName);
                mimiType = mimiType ?? "application/octet-stream";
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(data.FileStream);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = data.FileName;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(mimiType);
                Log.Info("StorageController:GetPrivate", "Get Private END", string.Empty);
                return response;
            }
            catch (Exception ex)
            {
                if (data != null && data.FileStream != null)
                {
                    data.FileStream.Dispose();
                }
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetPublic(string path)
        {
            FileReadData data = null;
            try
            {
                Log.Info("StorageController:GetPublic", "Get Public  Start", string.Empty);
                data = _storageService.ReadFileData(path, false);
                var mimiType = MimeMapping.GetMimeMapping(data.FileName);
                mimiType = mimiType ?? "application/octet-stream";
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(data.FileStream);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = data.FileName;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(mimiType);
                Log.Info("StorageController:GetPublic", "Get Public END", string.Empty);
                return response;
            }
            catch (Exception ex)
            {
                if (data != null && data.FileStream != null)
                {
                    data.FileStream.Dispose();
                }
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }

        }

    }
}
