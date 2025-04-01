namespace RegisteersTable.Models
{
    public class NameModel
    {
        public string Name { get; set; } // Renamed to match the controller's usage
    }

    public class NRegisterModel
    {
        public NameModel Name { get; set; } // Nested NameModel in NRegisterModel
    }
}
