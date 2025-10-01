using System;

namespace taller_backend.Domain.Entities;

public class City
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    public int Regionid { get; private set; }
    public virtual ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    public virtual ICollection<Branch> Branches { get; set; } = new HashSet<Branch>();
    public City() { }
    public City(string name, string description, int regionid)
    {
        Name = name;
        Description = description;
        Regionid = regionid;
    }

}
