using BhashaguruModel;
using BusinessLayerService;
using EmailSender;
using HelperComponent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BhashaGuruApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private  IConfiguration _iconfiguration;
        private  IUnicastEmailSender _unicastEmailSender;
        public UserController(IConfiguration iconfiguration,IUnicastEmailSender unicastEmailSender)
        {
            _iconfiguration = iconfiguration;
            _unicastEmailSender = unicastEmailSender;
        }

        
      
        // POST: api/User
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResponse> Post(RegisterUser registerUser)
        {
            UserService userService = new UserService(_iconfiguration, _unicastEmailSender);
            try
            {
                var result = await userService.AddUser(registerUser);
                GenericResponse genericResponse = new GenericResponse();
                genericResponse.status = "success";
                genericResponse.Message = result;

                return genericResponse;
               
            }
            catch (Exception ex)
            {
                var error = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
                throw new System.Web.Http.HttpResponseException(error);
            }
           
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpGet("Token")]
        public string GetToken()
        {
            TokenService tokenService = new TokenService();
            return tokenService.GetToken();
        }

        
        /// <summary>
        /// Perform user login
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<User> Login(LoginUser loginUser)
        {
            UserService userService = new UserService(_iconfiguration, _unicastEmailSender);
            try
            {
                var result = await userService.validateUserLogin(loginUser.emailId,loginUser.password);
                if(result==null)
                {
                    var error = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent(ApplicationMessage.LoginCreadentialNotCorrect)
                    };
                    return new System.Web.Http.HttpResponseException(error);
                }
                else
                {
                    return result;
                }
                
            }
            catch (Exception ex)
            {
                var error = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
                throw new System.Web.Http.HttpResponseException(error);
            }
        }

    }
}
