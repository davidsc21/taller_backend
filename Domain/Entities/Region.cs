using System;

namespace taller_backend.Domain.Entities;

public class Region
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    public int Countryid { get; private set; } = null!;
    public virtual ICollection<City> Cities { get; set; } = new HashSet<Product>();
    public Region() { }
    public Region(string name, string description, int countryid)
    {
        Name = name;
        Description = description;
        Countryid = countryid;
    }

}
