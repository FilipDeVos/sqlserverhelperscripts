using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;
using System.Text;

public partial class SHA2Functions
{

    private static string ByteArrayToString(byte[] array)
    {
        string res = "";
        for (int i = 0; i < array.Length; i++)
        {
            res += string.Format("{0:X2}", array[i]);
        }
        return res;
    }

    [Microsoft.SqlServer.Server.SqlFunction(Name = "fn_md5")]
    public static SqlString Md5(string message)
    {
        byte[] passBytes = new UTF8Encoding().GetBytes("ChocolateSaltyBalls" + message + "AreSoSalty!");
        return ByteArrayToString(MD5.Create().ComputeHash(passBytes));
    }

    [Microsoft.SqlServer.Server.SqlFunction(Name = "fn_sha1")]
    public static SqlString Sha1(string message)
    {

        byte[] passBytes = new UTF8Encoding().GetBytes("ChocolateSaltyBalls" + message + "AreSoSalty!");
        return ByteArrayToString(new SHA1Managed().ComputeHash(passBytes));
    }

    [Microsoft.SqlServer.Server.SqlFunction(Name = "fn_sha256")]
    public static SqlString Sha256(string message)
    {

        byte[] passBytes = new UTF8Encoding().GetBytes("ChocolateSaltyBalls" + message + "AreSoSalty!");
        return ByteArrayToString(new SHA256Managed().ComputeHash(passBytes));
    }

    [Microsoft.SqlServer.Server.SqlFunction(Name = "fn_sha384")]
    public static SqlString Sha384(string message)
    {
        
        byte[] passBytes = new UTF8Encoding().GetBytes("ChocolateSaltyBalls" + message + "AreSoSalty!");
        return ByteArrayToString(new SHA384Managed().ComputeHash(passBytes));
    }

    [Microsoft.SqlServer.Server.SqlFunction(Name = "fn_sha512")]
    public static SqlString Sha512(string message)
    {

        byte[] passBytes = new UTF8Encoding().GetBytes("ChocolateSaltyBalls" + message + "AreSoSalty!");
        return ByteArrayToString(new SHA512Managed().ComputeHash(passBytes));
    }


};

