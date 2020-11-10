using DAL.Helper;
using Model;
using Helper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace DAL
{
    public partial class SinhVienRepository : ISinhVienRepository
    {
        private IDatabaseHelper _dbHelper;
        public SinhVienRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(SinhVienModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_SinhVien_create",
                "@masv", model.masv,
                "@tensv", model.tensv,
                "@gioitinh", model.gioitinh,
                "@ngaysinh", model.ngaysinh,
                "@quequan", model.quequan,
                "@malop", model.malop,
                "@magv", model.magv,
                "@namhoc", model.namhoc,
                "@sdt", model.sdt,
                "@avata", model.avata,
                "@ngaysua", model.ngaysua,
                "@ngaytao", model.ngaytao,
                "@trangthai", model.trangthai);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_SinhVien_delete",
                "@masv", id);
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
        public bool Update(SinhVienModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_SinhVien_update",
                "@masv", model.masv,
                "@tensv", model.tensv,
                "@gioitinh", model.gioitinh,
                "@ngaysinh", model.ngaysinh,
                "@quequan", model.quequan,
                "@malop", model.malop,
                "@magv", model.magv,
                "@namhoc", model.namhoc,
                "@sdt", model.sdt,
                "@avata", model.avata,
                "@ngaysua", model.ngaysua,
                "@ngaytao", model.ngaytao,
                "@trangthai", model.trangthai);
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


        /*public SinhVienModel GetSinhVien(string SinhVienname, string password)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SinhVien_get_by_SinhVienname_password",
                     "@taikhoan", SinhVienname,
                     "@matkhau", password);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<SinhVienModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        public SinhVienModel GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SinhVien_get_by_id",
                     "@masv", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<SinhVienModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SinhVienModel> Search(int pageIndex, int pageSize, out long total, string tensv)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SinhVien_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@tensv", tensv);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<SinhVienModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
