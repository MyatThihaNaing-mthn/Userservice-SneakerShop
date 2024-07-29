namespace UserServiceApi.Configurations;

public class Eureka
    {
        public Client Client { get; set; } = null!;
        public Instance Instance { get; set; } = null!;    

    }

public class Client
    {
        public bool ShouldRegisterWithEureka { get; set; }
        public string ServiceUrl { get; set; } = null!;
        public bool ValidateCertificates { get; set; }
    }

public class Instance
    {
        public string AppName { get; set; } = null!;
        public string HostName { get; set; } = null!;
    }
