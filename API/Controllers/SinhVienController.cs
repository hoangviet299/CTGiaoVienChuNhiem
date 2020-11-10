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
    public class SinhVienController : ControllerBase
    {
        private ISinhVienBusiness _SinhVienBusiness;
        private string _path;
        public SinhVienController(ISinhVienBusiness SinhVienBusiness, IConfiguration configuration)
        {
            _SinhVienBusiness = SinhVienBusiness;
            _path = configuration["AppSettings:PATH"];
        }

        /*[AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var SinhVien = _SinhVienBusiness.Authenticate(model.SinhVienname, model.Password);

            if (SinhVien == null)
                return BadRequest(new { message = "SinhVienname or password is incorrect" });

            return Ok(SinhVien);
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

        [Route("delete-SinhVien")]
        [HttpPost]
        public IActionResult DeleteSinhVien([FromBody] Dictionary<string, object> formData)
        {
            string SinhVien_id = "";
            if (formData.Keys.Contains("masv") && !string.IsNullOrEmpty(Convert.ToString(formData["masv"]))) { SinhVien_id = Convert.ToString(formData["masv"]); }
            _SinhVienBusiness.Delete(SinhVien_id);
            return Ok();
        }

        [Route("create-SinhVien")]
        [HttpPost]
        public SinhVienModel CreateSinhVien([FromBody] SinhVienModel model)
        {
            _SinhVienBusiness.Create(model);
            return model;
        }

        [Route("update-SinhVien")]
        [HttpPost]
        public SinhVienModel UpdateSinhVien([FromBody] SinhVienModel model)
        {
            _SinhVienBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public SinhVienModel GetDatabyID(string id)
        {
            return _SinhVienBusiness.GetDatabyID(id);
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
                string tensv = "";
                if (formData.Keys.Contains("tensv") && !string.IsNullOrEmpty(Convert.ToString(formData["tensv"]))) { tensv = Convert.ToString(formData["tensv"]); }
                long total = 0;
                var data = _SinhVienBusiness.Search(page, pageSize, out total, tensv);
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
