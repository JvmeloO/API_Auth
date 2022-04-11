namespace Auth.Domain.Configurations
{
    public class BaseConfigurations
    {
        public string ConnectionString_authdb { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
    }
}
