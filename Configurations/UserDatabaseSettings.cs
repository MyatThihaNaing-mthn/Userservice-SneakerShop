namespace UserServiceApi.Configurations;

public class UserDatabaseSettings
{
    public const string SectionName = "UserDatabaseSetting";

    public string ConnectionName{get; set;} = null!;

    public string DatabaseName{get; set;} = null!;

    public string CollectionName{get; set;} = null!;
}