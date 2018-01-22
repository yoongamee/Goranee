using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;

// based on 
// http://www.slideshare.net/williamyang3910/unitekorea2013-protecting-your-android-content-21713675?from_search=1


public class EncryptedPlayerPrefs
{
    public static string privateKey = "a8b48";

    private static MD5          md5HashMaker = MD5.Create();
    private static TripleDES    desMaker = new TripleDESCryptoServiceProvider();

    public EncryptedPlayerPrefs()
    {
        
        Debug.Log("Create encryptedplayer prrefs");
    }

    ~EncryptedPlayerPrefs()
    {
        md5HashMaker.Clear();
        desMaker.Clear();
    }
    public static void SetString(string _key, string _value)
    {
        
        // Hide '_key' string.  
        byte[] hashData = md5HashMaker.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key));
        string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

        // Encrypt '_value' into a byte array  
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(_value);

        // Eecrypt '_value' with 3DES.  
        desMaker.Key = md5HashMaker.ComputeHash(System.Text.Encoding.UTF8.GetBytes(privateKey)); ;
        desMaker.Mode = CipherMode.ECB;

        ICryptoTransform xform = desMaker.CreateEncryptor();
        byte[] encrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

        // Convert encrypted array into a readable string.  
        string encryptedString = Convert.ToBase64String(encrypted);

        // Set the ( key, encrypted value ) pair in regular PlayerPrefs.  
        PlayerPrefs.SetString(hashKey, encryptedString);
        //Debug.Log("SetString hashKey: " + hashKey + " Encrypted Data: " + encryptedString);
		PlayerPrefs.Save ();
        
    }
    public static string GetString(string _key)
    {
        // Hide '_key' string.  
        byte[] hashData = md5HashMaker.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key));
        string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

        // Retrieve encrypted '_value' and Base64 decode it.  
        string _value = PlayerPrefs.GetString(hashKey);

		if (string.IsNullOrEmpty (_value) == true) 
		{
			return string.Empty;
		}
        byte[] bytes = Convert.FromBase64String(_value);

        // Decrypt '_value' with 3DES.  
        desMaker.Key = md5HashMaker.ComputeHash(System.Text.Encoding.UTF8.GetBytes(privateKey)); ;
        desMaker.Mode = CipherMode.ECB;

        ICryptoTransform xform = desMaker.CreateDecryptor();
        byte[] decrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

        // decrypte_value as a proper string.  
        string decryptedString = System.Text.Encoding.UTF8.GetString(decrypted);

        //Debug.Log("GetString hashKey: " + hashKey + " GetData: " + _value + " Decrypted Data: " + decryptedString);
        return decryptedString;
    }
}
