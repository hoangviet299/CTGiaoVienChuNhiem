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
    public partial class SinhVienBusiness : ISinhVienBusiness
    {
        private ISinhVienRepository _res;
        private string Secret;
        public SinhVienBusiness(ISinhVienRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        /*public SinhVienModel Authenticate(string SinhVienname, string password)
        {
            var SinhVien = _res.GetSinhVien(SinhVienname, password);
            // return null if SinhVien not found
            if (SinhVien == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, SinhVien.tenlop.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return SinhVien;

        }*/

        public SinhVienModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public bool Create(SinhVienModel model)
        {
            return _res.Create(model);
        }
        public bool Update(SinhVienModel model)
        {
            return _res.Update(model);
        }
        public List<SinhVienModel> Search(int pageIndex, int pageSize, out long total, string tensv)
        {
            return _res.Search(pageIndex, pageSize, out total, tensv);
        }
    }

}
