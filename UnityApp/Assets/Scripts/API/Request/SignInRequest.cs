[System.Serializable]
public class SignInRequest : BaseRequest
{
    public string username;
    public string password;

    public SignInRequest(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}
