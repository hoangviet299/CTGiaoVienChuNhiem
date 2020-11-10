using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GVCNController : ControllerBase
    {
        private IGVCNBusiness _GVCNBusiness;
        private string _path;
        public GVCNController(IGVCNBusiness GVCNBusiness, IConfiguration configuration)
        {
            _GVCNBusiness = GVCNBusiness;
            _path = configuration["AppSettings:PATH"];
        }

        /*[AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var GVCN = _GVCNBusiness.Authenticate(model.GVCNname, model.Password);

            if (GVCN == null)
                return BadRequest(new { message = "GVCNname or password is incorrect" });

            return Ok(GVCN);
        }*/
        public string SaveFileFromBase64String(string RelativePathFileName, string dataFromBase64String)
        {
            if (dataFromBase64String.Contains("base64,"))
            {
                dataFromBase64String = dataFromBase64String.Substring(dataFromBase64String.IndexOf("base64,", 0) + 7);
            }
            return WriteFileToAuthAccessFolder(RelativePathFileName, dataFromBase64String);
        }
        public string WriteFileToAuthAccessFolder(string RelativePathFileName, string base64StringData)
        {
            try
            {
                string result = "";
                string serverRootPathFolder = _path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                System.IO.File.WriteAllBytes(fullPathFile, Convert.FromBase64String(base64StringData));
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("delete-GVCN")]
        [HttpPost]
        public IActionResult DeleteGVCN([FromBody] Dictionary<string, object> formData)
        {
            string GVCN_id = "";
            if (formData.Keys.Contains("magv") && !string.IsNullOrEmpty(Convert.ToString(formData["magv"]))) { GVCN_id = Convert.ToString(formData["magv"]); }
            _GVCNBusiness.Delete(GVCN_id);
            return Ok();
        }

        [Route("create-GVCN")]
        [HttpPost]
        public GVCNModel CreateGVCN([FromBody] GVCNModel model)
        {
            _GVCNBusiness.Create(model);
            return model;
        }

        [Route("update-GVCN")]
        [HttpPost]
        public GVCNModel UpdateGVCN([FromBody] GVCNModel model)
        {
            _GVCNBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public GVCNModel GetDatabyID(string id)
        {
            return _GVCNBusiness.GetDatabyID(id);
        }

        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string tengv = "";
                if (formData.Keys.Contains("tengv") && !string.IsNullOrEmpty(Convert.ToString(formData["tengv"]))) { tengv = Convert.ToString(formData["tengv"]); }
                long total = 0;
                var data = _GVCNBusiness.Search(page, pageSize, out total, tengv);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
