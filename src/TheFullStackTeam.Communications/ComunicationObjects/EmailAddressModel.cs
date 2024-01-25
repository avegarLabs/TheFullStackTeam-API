namespace TheFullStackTeam.Communications.ComunicationsObjects
{
    public class EmailAddressModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{Name} {LastName}"; } }
    }
}
