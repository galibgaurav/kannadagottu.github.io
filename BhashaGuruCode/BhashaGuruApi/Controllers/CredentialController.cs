using BhashaguruModel;
using BusinessLayerService;
using EmailSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BhashaGuruApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CredentialController : ControllerBase
    {
        private IConfiguration _iconfiguration;
        private IUnicastEmailSender _unicastEmailSender;
        public CredentialController(IUnicastEmailSender unicastEmailSender, IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
            _unicastEmailSender = unicastEmailSender;
        }

       
        // POST: api/Credential
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="newPassword"></param>
        /// <param name="oldpassword"></param>
        [HttpPut]
        [Authorize(Roles = "appuser")]
        public async Task<GenericResponse> Put(ChangeCredential changeCredential)
        {
            UserService userService = new UserService(_iconfiguration, _unicastEmailSender);
            try
            {
                var result = await userService.ChangePassword(changeCredential.loginId, changeCredential.newPassword, changeCredential.oldpassword);
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


        /// <summary>
        /// forgetCredential
        /// </summary>
        /// <param name="forgetCredential"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("ForgetCredential")]
        public async Task<GenericResponse> Put(ForgetCredential forgetCredential)
        {
            UserService userService = new UserService(_iconfiguration, _unicastEmailSender);

            try
            {
                var result = await userService.ForgetPassword(forgetCredential.EmailID).ConfigureAwait(false);
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


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
