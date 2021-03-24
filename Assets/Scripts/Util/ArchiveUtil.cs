using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System;
using System.IO;
using System.Threading.Tasks;

public class ArchiveUtil : MonoBehaviour
{
    // 文本加密密钥
    private const string PRIVATE_KEY = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
    private static readonly string archiveFile = Path.Combine(Application.persistentDataPath + "0.sav");

    public static void SaveArchive(object data, Action onCompleted)
    {
        string json = JsonConvert.SerializeObject(data);
        string encryptStr = RijndaelEncrypt(json, PRIVATE_KEY);
        IOHelper.WriteText(archiveFile, encryptStr, onCompleted);
    }

    public static void LoadArchive(Action<object> onCompleted)
    {
        IOHelper.ReadText(archiveFile, (str) =>
        {
            string json = RijndaelDecrypt(str, PRIVATE_KEY);
            object ret = JsonConvert.DeserializeObject(json);
            onCompleted?.Invoke(ret);
        });
    }

    /// <summary>
    /// Rijndael加密算法
    /// </summary>
    /// <param name="pString">待加密的明文</param>
    /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param>
    /// <param name="iv">iv向量,长度为128（byte[16])</param>
    /// <returns></returns>
    private static string RijndaelEncrypt(string pString, string pKey)
    {
        //密钥
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
        //待加密明文数组
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(pString);

        //Rijndael解密算法
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateEncryptor();

        //返回加密后的密文
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// <summary>
    /// Rijndael解密算法
    /// </summary>
    /// <param name="pString">待解密的密文</param>
    /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param>
    /// <param name="iv">iv向量,长度为128（byte[16])</param>
    /// <returns></returns>
    private static string RijndaelDecrypt(string pString, string pKey)
    {
        //解密密钥
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
        //待解密密文数组
        byte[] toEncryptArray = Convert.FromBase64String(pString);

        //Rijndael解密算法
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateDecryptor();

        //返回解密后的明文
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    public static bool ArchiveExist()
    {
        return File.Exists(archiveFile);
    }
}

public class IOHelper
{
    public static async void WriteText(string path, string content, Action onCompleted = null)
    {
        using (StreamWriter writer = File.CreateText(path))
        {
            await writer.WriteAsync(content);
            writer.Close();
        }
        onCompleted?.Invoke();
    }

    public static async void ReadText(string path, Action<string> onCompleted)
    {
        char[] result;
        StringBuilder builder = new StringBuilder();

        using (StreamReader reader = File.OpenText(path))
        {
            result = new char[reader.BaseStream.Length];
            await reader.ReadAsync(result, 0, (int)reader.BaseStream.Length);
        }

        foreach (char c in result)
        {
            if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
            {
                builder.Append(c);
            }
        }
        onCompleted?.Invoke(builder.ToString());
    }
}
