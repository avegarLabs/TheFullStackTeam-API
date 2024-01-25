namespace TheFullStackTeam.Domain.Entities.Enumerations;

public class Role
{
    public string Name { get; set; }

    public Role(string name)
    {
        Name = name;
    }

    public static Role User = new(nameof(User).ToLowerInvariant());
    public static Role Admin = new(nameof(Admin).ToLowerInvariant());
    public static Role Professional = new(nameof(Professional).ToLowerInvariant());

    public static IEnumerable<Role> List() => new[] { User, Admin, Professional };

    public static Role FromName(string name)
    {
        var value = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (value == null)
        {
            throw new Exception($"Possible values for Role: {string.Join(",", List().Select(s => s.Name))}");
        }

        return value;
    }
}