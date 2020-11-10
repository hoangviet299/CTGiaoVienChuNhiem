using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IGVCNRepository
    {
        /*GVCNModel GetGVCN(string GVCNname, string password)*/
        GVCNModel GetDatabyID(string id);
        bool Create(GVCNModel model);
        bool Update(GVCNModel model);
        bool Delete(string id);
        List<GVCNModel> Search(int pageIndex, int pageSize, out long total, string tengv);
    }
}
