using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IKhoaRepository
    {
        /*KhoaModel GetKhoa(string Khoaname, string password)*/
        KhoaModel GetDatabyID(string id);
        bool Create(KhoaModel model);
        bool Update(KhoaModel model);
        bool Delete(string id);
        List<KhoaModel> Search(int pageIndex, int pageSize, out long total, string tenkhoa);
    }
}
