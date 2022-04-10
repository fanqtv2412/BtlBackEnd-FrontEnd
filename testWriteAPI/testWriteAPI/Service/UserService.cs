using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using testWriteAPI.DataAccess;
using testWriteAPI.Function;
using testWriteAPI.Models;

namespace testWriteAPI.Service
{

    public class UserService
    {
        static ConnectDB cn = new ConnectDB();
        SqlConnection con = cn.getcon();

        public List<UserModel> getAllUser()
        {
            List<UserModel> listUsers = new List<UserModel>();

            string proc = "exec dbo.spGetAllUserAccount";

            SqlCommand cmd = new SqlCommand(proc, con);

            con.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
            {
                UserModel model = new UserModel
                {
                    Id = sqlDataReader["UserID"].ToString(),
                    Email = sqlDataReader["Email"].ToString(),
                    Name = sqlDataReader["FullName"].ToString()
                };
                listUsers.Add(model);

            }
            con.Close();
            return listUsers;
        }

        public UserModel getUserByEmail(string email)
        {
            UserModel model = new UserModel();
            string proc = "exec spGetUserByEmail @Email = '" + email + "'";
            SqlCommand cmd = new SqlCommand(proc, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model.Id = reader["UserID"].ToString();
                model.Email = reader["Email"].ToString();
                model.Name = reader["FullName"].ToString();
                model.Password = reader["UserPassword"].ToString();
                model.Management = bool.Parse(reader["Management"].ToString());
                model.IsDeleted = bool.Parse(reader["IsDeleted"].ToString());
            }
            con.Close();
            return model;

        }

        public int createNewUser(UserModel user)
        {
            try
            {
                string procCheck = "exec spGetUserByEmail @Email = '" + user.Email + "'";
                SqlCommand cmdCheck = new SqlCommand(procCheck, con);
                con.Open();
                SqlDataReader reader = cmdCheck.ExecuteReader();
                
                if(reader.Read())
                {
                    return 0;
                }
                else
                {
                    con.Close();
                    string proc = "exec spUserInsert '" + user.Email + "', '" + user.Name + "', '" + user.Password + "', '" + user.IsDeleted + "', '" + user.Management + "'";
                    SqlCommand cmd = new SqlCommand(proc, con);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a == 0)
                    {
                        return 2;
                    }
                    else return 1;
                }
                
            }
            catch (Exception ex)
            {
                return 3;
            }

        }
        public bool updateUserAccount(UserModel user)
        {
            try
            {
                string proc = "exec spUserInsert '" + user.Email + "', '" + user.Name + "', '" + user.Password + "', '" + user.IsDeleted + "', '" + user.Management + "'";
                SqlCommand cmd = new SqlCommand(proc, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
                else return true;
                con.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool deleteUserAccount(UserModel user)
        {
            try
            {
                string proc = "exec spDeleteUser '" + user.Email + "'";
                SqlCommand cmd = new SqlCommand(proc, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
                else return true;
                con.Close ();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool phanQuyenAccount(UserModel user)
        {
            try
            {
                string proc = "exec spPhanQuyen '" + user.Email + "'";
                SqlCommand cmd = new SqlCommand(proc, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a == 0)
                {
                    return false;
                }
                else return true;
                con.Close () ;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int ReSetPassword(string email)
        {
            Random rd = new Random();

            int rand_num = rd.Next(1000, 9999);
            string subject = "Reset your password";
            string content = "Your verification is " + rand_num.ToString();
            ClsMail.Send(email, subject, content);
            return rand_num;
        }
    }
}