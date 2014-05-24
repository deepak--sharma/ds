using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BL
{
    public static class UtilityClass
    {
        public static string Decrypt(string inputValue)
        {
            //Hard Code RSA Decrypt Key
            const string decryptKey = "<RSAKeyValue><Modulus>tyOPvDDu+6yTvfON47LM48xmFkhsqP8kEgVIkmHKvtp9RRTcwnRiSS+OtIRNQ1y4suc8xRiRCX7d7yiQAV2N7SGShETtIWMhQ1Kno/hqP+DNruQL62zDU2vdeZ9GCkbehxIQmUW2CCBDSzKrKkGfreFf3Y+eDZHZDMYI0kg6lD8=</Modulus><Exponent>AQAB</Exponent><P>87UWNEu/uz92WX8ODTqoa2cQHQ6LN5P99HbRblpSzTBvcdqUxcjx/7IIOoOKAA+pISHVnLxW2wTKxgInZt4zCQ==</P><Q>wGBfxfKREUFssKDk42ZiO2kDx/Hw+Cr8BdZmDeaPIkZ/+WLfSqLkFptTg7XJO6RBVThQMHAgz71R85PDkH53Bw==</Q><DP>2OVm4J42AtmFZDtu7xkwgX4VWjbyckF1OJhy5kre/J1J4kOOOsUPk+kH58PgExPdC47IRZldl8mZCkcqeCPzuQ==</DP><DQ>uBP/0LOYqEBINoLeQdHYMSz9Vzdk8rJ+0T8kDC0PzSZUkldPfmV7hz49nYw28ADuGxN1d8PzQZTQdBhySzMXxQ==</DQ><InverseQ>LzRQyO4xirsPgjRGWXNRfDJLFVEz0cx/xZM47B9ZEO11Q0nw8XJB7WoNWrWM6/22xHYnMa5nq0hevbC4H7vdWw==</InverseQ><D>tUlEfTvLNeKNlVjEugNCgrTQ0Xn75gY6RIRqZEzdj7Nkkb+nD+55ZIzpLJRSjz33r1DrfQdmewnU02tJsbBUThph0oXnKqAQPX9OotVcPY4cFxQk04aJCak67d0Qu/BZoWsIVCq+OW5WvyR7lChOS3FjhZW7LnJE6HbKP2RzZ+E=</D></RSAKeyValue>";
            var rsaProvider = new System.Security.Cryptography.RSACryptoServiceProvider();
            var parameters = new System.Security.Cryptography.RSAParameters();
            rsaProvider.FromXmlString(decryptKey);
            var encryptedBlock = "";
            var encryptedBlocks = new System.Collections.Queue();
            while (inputValue.Length != 0)
            {
                if (rsaProvider.KeySize == 1024)
                {
                    encryptedBlock = inputValue.Substring(0, inputValue.IndexOf("=") + 1);
                    encryptedBlocks.Enqueue(encryptedBlock);
                    inputValue = inputValue.Remove(0, encryptedBlock.Length);
                }
                else
                {
                    encryptedBlock = inputValue.Substring(0, inputValue.IndexOf("==") + 2);
                    encryptedBlocks.Enqueue(encryptedBlock);
                    inputValue = inputValue.Remove(0, encryptedBlock.Length);
                }
            }
            encryptedBlocks.TrimToSize();
            var numberOfBlocks = encryptedBlocks.Count;
            var outputValue = "";
            for (var i = 1; i <= numberOfBlocks; i++)
            {
                encryptedBlock = (String)encryptedBlocks.Dequeue();
                outputValue += Encoding.ASCII.GetString(rsaProvider.Decrypt(Convert.FromBase64String(encryptedBlock), true));
            }
            return outputValue;
        }

        public static string Encrypt(string inputValue)
        {
            //Hard Code RSA Encrypt Key
            const string encryptKey = "<RSAKeyValue><Modulus>tyOPvDDu+6yTvfON47LM48xmFkhsqP8kEgVIkmHKvtp9RRTcwnRiSS+OtIRNQ1y4suc8xRiRCX7d7yiQAV2N7SGShETtIWMhQ1Kno/hqP+DNruQL62zDU2vdeZ9GCkbehxIQmUW2CCBDSzKrKkGfreFf3Y+eDZHZDMYI0kg6lD8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var rsaProvider = new System.Security.Cryptography.RSACryptoServiceProvider();
            var parameters = new System.Security.Cryptography.RSAParameters();
            rsaProvider.FromXmlString(encryptKey);
            var numberOfBlocks = (inputValue.Length / 32) + 1;
            var charArray = inputValue.ToCharArray();
            var byteBlockArray = new byte[numberOfBlocks][];
            var incrementer = 0;
            for (var i = 1; i <= numberOfBlocks; i++)
            {
                if (i == numberOfBlocks)
                {
                    byteBlockArray[i - 1] = Encoding.ASCII.GetBytes(charArray, incrementer, charArray.Length - incrementer);
                }
                else
                {
                    byteBlockArray[i - 1] = Encoding.ASCII.GetBytes(charArray, incrementer, 32);
                    incrementer += 32;
                }
            }
            return byteBlockArray.Aggregate("", (current, b) => current + Convert.ToBase64String(rsaProvider.Encrypt(b, true)));
        }
    }
}
