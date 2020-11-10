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
    public partial class GVCNBusiness : IGVCNBusiness
    {
        private IGVCNRepository _res;
        private string Secret;
        public GVCNBusiness(IGVCNRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        /*public GVCNModel Authenticate(string GVCNname, string password)
        {
            var GVCN = _res.GetGVCN(GVCNname, password);
            // return null if GVCN not found
            if (GVCN == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, GVCN.tenlop.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return GVCN;

        }*/

        public GVCNModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public bool Create(GVCNModel model)
        {
            return _res.Create(model);
        }
        public bool Update(GVCNModel model)
        {
            return _res.Update(model);
        }
        public List<GVCNModel> Search(int pageIndex, int pageSize, out long total, string tengv)
        {
            return _res.Search(pageIndex, pageSize, out total, tengv);
        }
    }

}
