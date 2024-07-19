namespace UserServiceApi.Configurations;

public class JwtSettings{
    public const string SectionName = "jwtSettings";

    public string Issuer {get; set;} = null!;

    public string Audience {get; set;} = null!;

    public string SigningKey {get; set;} = null!;

}