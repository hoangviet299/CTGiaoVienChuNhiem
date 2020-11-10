using DAL;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using Helper;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public partial class LopHocBusiness : ILopHocBusiness
    {
        private ILopHocRepository _res;
        private string Secret;
        public LopHocBusiness(ILopHocRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        /*public LopHocModel Authenticate(string LopHocname, string password)
        {
            var LopHoc = _res.GetLopHoc(LopHocname, password);
            // return null if LopHoc not found
            if (LopHoc == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, LopHoc.tenlop.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return LopHoc;

        }*/

        public LopHocModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public bool Create(LopHocModel model)
        {
            return _res.Create(model);
        }
        public bool Update(LopHocModel model)
        {
            return _res.Update(model);
        }
        public List<LopHocModel> Search(int pageIndex, int pageSize, out long total, string tenlop)
        {
            return _res.Search(pageIndex, pageSize, out total, tenlop);
        }
    }

}
