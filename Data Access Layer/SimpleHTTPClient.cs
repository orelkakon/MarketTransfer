using System.Net.Http;
using MarketClient.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace DataAccessLayer
{
    public class SimpleHTTPClient
    {
        /// <summary>
        /// Send an object of type T1, @item, parsed as json string embedded with the 
        /// authentication token, that is build using @user and @token, 
        /// as an HTTP post request to server at address @url.
        /// This method parse the reserver response using JSON to object of type T2.
        /// </summary>
        /// <typeparam name="T1">Type of the data object to send</typeparam>
        /// <typeparam name="T2">Type of the return object</typeparam>
        /// <param name="url">address of the server</param>
        /// <param name="user">username for authentication data</param>
        /// <param name="token">token for authentication data</param>
        /// <param name="item">the data item to send in the reuqest</param>
        /// <returns>the server response parsed as T2 object in json format</returns>
        public T2 SendPostRequest<T1, T2>(string url, string user, string nonce, string token, T1 item) where T2 : class
        {
            var response = SendPostRequest(url, user, nonce, token, item);
            string x = SimpleCtyptoLibrary.decrypt(response, GetPK());
            return x == null ? null : FromJson<T2>(x);
        }

        /// <summary>
        /// Send an object of type T1, @item, parsed as json string embedded with the 
        /// authentication token, that is build using @user and @token, 
        /// as an HTTP post request to server at address @url.
        /// This method reutens the server response as is.
        /// </summary>
        /// <typeparam name="T1">Type of the data object to send</typeparam>
        /// <param name="url">address of the server</param>
        /// <param name="user">username for authentication data</param>
        /// <param name="token">token for authentication data</param>
        /// <param name="item">the data item to send in the reuqest</param>
        /// <returns>the server response</returns>
        public string SendPostRequest<T1>(string url, string user, string nonce, string token, T1 item)
        {
            var auth = new { user, nonce, token };
            JObject jsonItem = JObject.FromObject(item);
            jsonItem.Add("auth", JObject.FromObject(auth));
            StringContent content = new StringContent(jsonItem.ToString());
            using (var client = new HttpClient())
            {
                var result = client.PostAsync(url, content).Result;
                var responseContent = result?.Content?.ReadAsStringAsync().Result;
                return responseContent;
            }
        }

        private static T FromJson<T>(string response) where T : class
        {
            if (response == null)
            {
                return null;
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(response, new JsonSerializerSettings
                {
                    Error = delegate
                    {
                        throw new JsonException(response);
                    }
                });
            }
            catch
            {
                throw new MarketException(response);
            }
        }
        private string GetPK()
        {
            string key = @"-----BEGIN RSA PRIVATE KEY-----
MIICXQIBAAKBgQDmp2oArPBMskwyFQYmzwsL4ztSgjJEFoOWizQAObXk6zRS8BsU
JbvbSVf3MmWNgaO9IQ6/P0Rvxbw7K42ZTRd2Hsj0dwE8ANBfGKp4yC7rY+5HbhO+
jD7HrnGmOpTewXFwbsHmU5JZn/RsVEfjIXFhe5Otyr7zc/bHEvJ2UafgiwIDAQAB
AoGAHOw9VJxa/aMV/um3/cHUpsb1t2DwIK2aDpSiDq0t6+i6hPPq0Vnx9ot55luB
dH8fIyY0DTNHx6RJ+Dl75g9Os5ofWchX/IN8CzH7eUkDFxC7wg9urq/CJIRnXCo0
SmVeMXtZJo6MCfNbGLY5sr4lNXxSUEbTxZu2aej1+qdyB+ECQQDm9VQjfyj8zhga
OglXD6ERh0xRId3pCT8WQeiKNn0wxcWZF30s+43H8k340q88irr307RtxXHB9mK3
orljXqfZAkEA/6mjM53VmiPWSc3ZZyRljbA5QO6yRVsn0nFjzOOQjTnAl6e8FZ/Q    
iQnnfXZLQypyYd0c5vj+oJ5XCyHFXAqRAwJBAI2j5vXeBkUEH8P108SQ0Tbuwt7+
5hkEkqwTv4kD4cMHhydcQGhV3Z3B/A+dJdr7Oa7DJuQrMpjBgckdApTueAECQQC7
bvO+UNWbxvBAdZEQZAer4+llqFm8LUM5rnW7bY65awC+bnOe6uaowUEcoxA0crce
9ktNLnkG6m7oM18MxpZfAkAoM9QvRasnSBQEdilJFtPah7Io3fSbQYuj+DdduanD
a+rPncqpxVerHAGS2iBMnXH4wnkQqJGKGR1ErjfF/T3E
-----END RSA PRIVATE KEY-----";
            return key;
        }
    }
}
