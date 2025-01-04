[System.Serializable]
public class SignupRequest : BaseRequest
{
    public string username;
    public string password;
    public string repeatPassword;

    public SignupRequest(string username, string password, string repeatPassword)
    {
        this.username = username;
        this.password = password;
        this.repeatPassword = repeatPassword;
    }
}
