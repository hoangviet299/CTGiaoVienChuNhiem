using DAL.Helper;
using Model;
using Helper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace DAL
{
    public partial class GVCNRepository : IGVCNRepository
    {
        private IDatabaseHelper _dbHelper;
        public GVCNRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(GVCNModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_GVCN_create",
                "@magv", model.magv,
                "@tengv", model.tengv,
                "@gioitinh", model.gioitinh,
                "@ngaysinh", model.ngaysinh,
                "@quequan", model.quequan,
                "@malop", model.malop,
                "@namcn", model.namcn,
                "@sdt", model.sdt,
                "@avata", model.avata,
                "@trangthai", model.trangthai,
                "@ngaysua", model.ngaysua,
                "@ngaytao", model.ngaytao);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_GVCN_delete",
                "@magv", id);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(GVCNModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_GVCN_update",
                "@magv", model.magv,
                "@tengv", model.tengv,
                "@gioitinh", model.gioitinh,
                "@ngaysinh", model.ngaysinh,
                "@quequan", model.quequan,
                "@malop", model.malop,
                "@namcn", model.namcn,
                "@sdt", model.sdt,
                "@avata", model.avata,
                "@trangthai", model.trangthai,
                "@ngaysua", model.ngaysua,
                "@ngaytao", model.ngaytao);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /*public GVCNModel GetGVCN(string GVCNname, string password)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GVCN_get_by_GVCNname_password",
                     "@taikhoan", GVCNname,
                     "@matkhau", password);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<GVCNModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        public GVCNModel GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GVCN_get_by_id",
                     "@magv", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<GVCNModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GVCNModel> Search(int pageIndex, int pageSize, out long total, string tengv)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GVCN_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@tengv", tengv);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<GVCNModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
