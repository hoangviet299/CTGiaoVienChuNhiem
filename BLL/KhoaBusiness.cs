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
    public partial class KhoaBusiness : IKhoaBusiness
    {
        private IKhoaRepository _res;
        private string Secret;
        public KhoaBusiness(IKhoaRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        /*public KhoaModel Authenticate(string Khoaname, string password)
        {
            var Khoa = _res.GetKhoa(Khoaname, password);
            // return null if Khoa not found
            if (Khoa == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Khoa.tenlop.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Khoa;

        }*/

        public KhoaModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public bool Create(KhoaModel model)
        {
            return _res.Create(model);
        }
        public bool Update(KhoaModel model)
        {
            return _res.Update(model);
        }
        public List<KhoaModel> Search(int pageIndex, int pageSize, out long total, string tenkhoa)
        {
            return _res.Search(pageIndex, pageSize, out total, tenkhoa);
        }
    }

}
