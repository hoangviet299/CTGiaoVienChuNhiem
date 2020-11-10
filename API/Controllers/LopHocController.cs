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
    public class LopHocController : ControllerBase
    {
        private ILopHocBusiness _LopHocBusiness;
        private string _path;
        public LopHocController(ILopHocBusiness LopHocBusiness, IConfiguration configuration)
        {
            _LopHocBusiness = LopHocBusiness;
            _path = configuration["AppSettings:PATH"];
        }

        /*[AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var LopHoc = _LopHocBusiness.Authenticate(model.LopHocname, model.Password);

            if (LopHoc == null)
                return BadRequest(new { message = "LopHocname or password is incorrect" });

            return Ok(LopHoc);
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

        [Route("delete-lophoc")]
        [HttpPost]
        public IActionResult DeleteLopHoc([FromBody] Dictionary<string, object> formData)
        {
            string LopHoc_id = "";
            if (formData.Keys.Contains("malop") && !string.IsNullOrEmpty(Convert.ToString(formData["malop"]))) { LopHoc_id = Convert.ToString(formData["malop"]); }
            _LopHocBusiness.Delete(LopHoc_id);
            return Ok();
        }

        [Route("create-lophoc")]
        [HttpPost]
        public LopHocModel CreateLopHoc([FromBody] LopHocModel model)
        {
            _LopHocBusiness.Create(model);
            return model;
        }

        [Route("update-lophoc")]
        [HttpPost]
        public LopHocModel UpdateLopHoc([FromBody] LopHocModel model)
        {
            _LopHocBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public LopHocModel GetDatabyID(string id)
        {
            return _LopHocBusiness.GetDatabyID(id);
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
                string tenlop = "";
                if (formData.Keys.Contains("tenlop") && !string.IsNullOrEmpty(Convert.ToString(formData["tenlop"]))) { tenlop = Convert.ToString(formData["tenlop"]); }
                long total = 0;
                var data = _LopHocBusiness.Search(page, pageSize, out total, tenlop);
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
