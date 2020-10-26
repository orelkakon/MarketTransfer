using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class Token
    {
        private int nonce;
        public string token;


        public Token()
        {
            Random RN = new Random();
            this.nonce = RN.Next(Int32.MinValue, Int32.MaxValue);

        }
        public string createToken()
        {
            string token = MarketClient.Utils.SimpleCtyptoLibrary.CreateToken("user20_" + nonce, @"-----BEGIN RSA PRIVATE KEY-----
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
-----END RSA PRIVATE KEY-----");
            return token;
        }

        public int getNonce()
        {
            return nonce;
        }
        public string getPrivateKey()
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
        public string getUrl()
        {
            string x = "http://ise172.ise.bgu.ac.il";
            return x;
        }
        public string getUserName()
        {
            string y = "user20";
            return y;
        }




    }


}
