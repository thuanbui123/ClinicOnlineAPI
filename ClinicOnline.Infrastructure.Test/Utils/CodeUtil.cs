namespace ClinicOnline.Infrastructure.Test.Utils;

public class CodeUtil
{
    public static string CreateCode(int digit, int code)
    {
        var codeText = code.ToString();
        return Enumerable.Range(0, digit - codeText.Length).Aggregate("", (s, i) => s + "0") + codeText;
    }
}
