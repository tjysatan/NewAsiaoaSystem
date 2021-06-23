using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewAsiaOASystem.Web.Controllers
{
     
    public class Default1Controller : ApiController
    {
        // GET api/default1
       
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/default1/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/default1
        [ApiAuthorize]
        [HttpPost]
        public IEnumerable<string> Post([FromBody] UserLogin LoginInfo)
        {
            return new string[] { "value1:"+ LoginInfo.username }; 
        }

        // PUT api/default1/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/default1/5
        public void Delete(int id)
        {

        }
        //测试获取toten
        [HttpGet]
        public string Gettoten([FromBody] Authorizationuser Authoruser)
        {
            var authTime = DateTime.Now;
            var expiresAt = authTime.AddHours(2);//到期时间
            //格式如下
            var payload = new Dictionary<string, object>
            {
                { "username",Authoruser.username },
                { "userId", Authoruser.userId },
                { "authTime",authTime},
                { "expiresAt",expiresAt}
            };
            string dd = JwtHelp.SetJwtEncode(payload);
            return dd;
        }
        //测试解析toten
        [ApiAuthorize]
        [HttpGet]
        public string Getjsontoten([FromBody] string toten)
        {

            string str = JwtHelp.GetJwtDecode(toten).username;
            return str;

        }
    }
}
