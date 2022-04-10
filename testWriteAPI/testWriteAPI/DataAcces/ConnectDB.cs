using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace testWriteAPI.DataAccess
{
    class ConnectDB
    {
        SqlConnection con;
        SqlCommand sqlcom;
        SqlDataReader sqldr;

        public SqlConnection getcon()
        {

            return new SqlConnection(@"Data Source=DESKTOP-I73ODGC\SQLEXPRESS; Initial Catalog =WebHocTiengAnh; Integrated Security = True");

        }
        public void ExcuteNonQuery(string sql)
        {
            con = getcon();
            sqlcom = new SqlCommand(sql, con);
            con.Open();
            sqlcom.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }

        public bool kiemtra(string sql)
        {
            con = getcon();
            con.Open();
            sqlcom = new SqlCommand(sql, con);
            int n = (int)sqlcom.ExecuteScalar();
            con.Close();
            if (n > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public int returnscalarnumber(string sql)
        {
            con = getcon();
            con.Open();
            sqlcom = new SqlCommand(sql, con);
            int n = (int)sqlcom.ExecuteScalar();
            con.Close();
            return n;
        }

        public string LoadLable(string sql)
        {
            string ketqua = "";
            con = getcon();
            con.Open();
            sqlcom = new SqlCommand(sql, con);
            sqldr = sqlcom.ExecuteReader();
            while (sqldr.Read())
            {
                ketqua = sqldr[0].ToString();
            }
            con.Close();
            return ketqua;
        }

        public bool kiemtrauser(string sql, string user, string pass)
        {
            con = getcon();
            bool a = true;
            sqlcom = new SqlCommand(sql, con);
            sqldr = sqlcom.ExecuteReader();
            while (sqldr.Read())
            {
                if (user == sqldr[0].ToString() && pass == sqldr[1].ToString())
                {
                    a = false;
                }
                else
                {
                    a = true;
                }
            }
            return a;
        }

        public bool KiemtraUsername(string strsql)
        {
            con = getcon();
            con.Open();
            sqlcom = new SqlCommand(strsql, con);
            int tontai = (int)(sqlcom.ExecuteScalar());
            con.Close();
            if (tontai > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
