[System.Serializable]
public class MeResponse
{
    public string username;
    public string role;
}

public enum UserRole
{
    ROLE_USER,
    ROLE_ADMIN
}