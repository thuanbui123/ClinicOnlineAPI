namespace ClinicOnline.Core.Exceptions;

public class ClinicValidateException : Exception
{
    private string MsgError = string.Empty;
    public ClinicValidateException(string error)
    {
        this.MsgError = error;
    }

    public override string Message => this.MsgError;
}
