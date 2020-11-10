using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface ILopHocRepository
    {
        /*LopHocModel GetLopHoc(string LopHocname, string password)*/
        LopHocModel GetDatabyID(string id);
        bool Create(LopHocModel model);
        bool Update(LopHocModel model);
        bool Delete(string id);
        List<LopHocModel> Search(int pageIndex, int pageSize, out long total, string tenlop);
    }
}
