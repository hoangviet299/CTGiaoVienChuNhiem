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
    public class KhoaController : ControllerBase
    {
        private IKhoaBusiness _KhoaBusiness;
        private string _path;
        public KhoaController(IKhoaBusiness KhoaBusiness, IConfiguration configuration)
        {
            _KhoaBusiness = KhoaBusiness;
            _path = configuration["AppSettings:PATH"];
        }

        /*[AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var Khoa = _KhoaBusiness.Authenticate(model.Khoaname, model.Password);

            if (Khoa == null)
                return BadRequest(new { message = "Khoaname or password is incorrect" });

            return Ok(Khoa);
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

        [Route("delete-Khoa")]
        [HttpPost]
        public IActionResult DeleteKhoa([FromBody] Dictionary<string, object> formData)
        {
            string Khoa_id = "";
            if (formData.Keys.Contains("makhoa") && !string.IsNullOrEmpty(Convert.ToString(formData["makhoa"]))) { Khoa_id = Convert.ToString(formData["makhoa"]); }
            _KhoaBusiness.Delete(Khoa_id);
            return Ok();
        }

        [Route("create-Khoa")]
        [HttpPost]
        public KhoaModel CreateKhoa([FromBody] KhoaModel model)
        {
            _KhoaBusiness.Create(model);
            return model;
        }

        [Route("update-Khoa")]
        [HttpPost]
        public KhoaModel UpdateKhoa([FromBody] KhoaModel model)
        {
            _KhoaBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public KhoaModel GetDatabyID(string id)
        {
            return _KhoaBusiness.GetDatabyID(id);
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
                if (formData.Keys.Contains("tenkhoa") && !string.IsNullOrEmpty(Convert.ToString(formData["tenkhoa"]))) { tenlop = Convert.ToString(formData["tenkhoa"]); }
                long total = 0;
                var data = _KhoaBusiness.Search(page, pageSize, out total, tenlop);
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
