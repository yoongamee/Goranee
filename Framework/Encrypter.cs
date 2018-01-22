using System;
using System.Security.Cryptography;

// based on 
// http://www.slideshare.net/williamyang3910/unitekorea2013-protecting-your-android-content-21713675?from_search=1


public class Encrypter 
{

    private static MD5 md5HashMaker = MD5.Create();
    private static TripleDES desMaker = new TripleDESCryptoServiceProvider();

    public static byte[] GetEncryptedBytes(string privateKey, byte[] dataBytes)
    {
        if (dataBytes == null)
        {
            return null;
        }
        // Eecrypt '_value' with 3DES.  
        desMaker.Key = md5HashMaker.ComputeHash(System.Text.Encoding.UTF8.GetBytes(privateKey)); ;
        desMaker.Mode = CipherMode.ECB;

        ICryptoTransform xform = desMaker.CreateEncryptor();
        return xform.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
    }
    public static byte[] GetDecryptedBytes(string privateKey, byte[] dataBytes)
    {
        if (dataBytes == null)
        {
            return null;
        }
        // Decrypt '_value' with 3DES.  
        desMaker.Key = md5HashMaker.ComputeHash(System.Text.Encoding.UTF8.GetBytes(privateKey)); ;
        desMaker.Mode = CipherMode.ECB;

        ICryptoTransform xform = desMaker.CreateDecryptor();
        return xform.TransformFinalBlock(dataBytes, 0, dataBytes.Length);

      
    }
}
