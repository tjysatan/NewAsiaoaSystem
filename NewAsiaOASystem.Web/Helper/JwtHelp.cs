using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAsiaOASystem.Web
{
    public class JwtHelp
    {
        private static string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

        /// <summary>
        /// 生成JwtToken
        /// </summary>
        /// <param name="payload">不敏感的用户数据</param>
        /// <returns></returns>
        public static string SetJwtEncode(Dictionary<string, object> payload)
        {

            //格式如下
            //var payload = new Dictionary<string, object>
            //{
            //    { "username","admin" },
            //    { "pwd", "claim2-value" }
            //};


            

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);
            return token;
        }

        /// <summary>
        /// 根据jwtToken  获取实体
        /// </summary>
        /// <param name="token">jwtToken</param>
        /// <returns></returns>
        public static Authorizationuser GetJwtDecode(string token)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                var algorithm = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                var userInfo = decoder.DecodeToObject<Authorizationuser>(token, secret, verify: true);//token为之前生成的字符串
                return userInfo;
            } 
            catch (TokenExpiredException)
            {
                Authorizationuser s = new Authorizationuser();
                s.username = "超时";
                return s;
            }
            catch (SignatureVerificationException)
            {
                Authorizationuser s = new Authorizationuser();
                s.username = "签名验证失败，数据可能被篡改";
                return s;
            }
 
        }

    }
}