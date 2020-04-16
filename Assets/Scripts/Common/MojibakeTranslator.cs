using System.Text;

/// <summary>
/// 文字化けを直すクラス toyoda
/// </summary>
public class MojibakeTranslater
{
    /// <summary>
    /// ラテン語からutf8に変換する。
    /// </summary>
    /// <returns>The latin to UTF8.</returns>
    /// <param name="mojibakeString">ラテン語に文字化けしている文字列</param>
    public static string ConvertLatinToUtf8(string mojibakeString)
    {
        Encoding iso = Encoding.GetEncoding("ISO-8859-1");
        byte[] latinByte = Encoding.Default.GetBytes(mojibakeString);
        byte[] utf8Byte = Encoding.Convert(Encoding.UTF8, iso, latinByte);
        string convertedString = Encoding.UTF8.GetString(utf8Byte);

        return convertedString;
    }
}
