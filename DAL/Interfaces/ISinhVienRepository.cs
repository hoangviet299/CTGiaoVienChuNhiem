using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface ISinhVienRepository
    {
        /*SinhVienModel GetSinhVien(string SinhVienname, string password)*/
        SinhVienModel GetDatabyID(string id);
        bool Create(SinhVienModel model);
        bool Update(SinhVienModel model);
        bool Delete(string id);
        List<SinhVienModel> Search(int pageIndex, int pageSize, out long total, string tensv);
    }
}
