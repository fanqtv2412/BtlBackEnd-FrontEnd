using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using testWriteAPI.DataAccess;
using testWriteAPI.Models;
using testWriteAPI.Service;

namespace testWriteAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class UserController : ApiController
    {
        UserService userService = new UserService();
        //localhost203030/api/User
        [HttpGet]
        public List<UserModel> getListUser()
        {
            return userService.getAllUser();

        }
        [HttpGet]
        public UserModel GetUserByEmail(string email)
        {
            return userService.getUserByEmail(email);
        }
        // GET: User
        [HttpPost]
        
        public int CreateUser(UserModel userModel)
        {
            return userService.createNewUser(userModel);
        }

        [Route("update")]
        [HttpPut]
        public bool UpdateUser(UserModel userModel)
        {
            return userService.updateUserAccount(userModel);
        }

        [Route("delete")]
        [HttpPut]
        public bool DeleteUser(UserModel userModel)
        {
            return userService.deleteUserAccount(userModel);
        }

        [Route("phanquyen")]
        [HttpPut]
        public bool PhanQuyen(UserModel userModel)
        {
            return userService.phanQuyenAccount(userModel);
        }

        [Route("resetpassword")]
        [HttpGet]
        public int ResetPassword(string email)
        {
            return userService.ReSetPassword(email);
        }
        
    }
}