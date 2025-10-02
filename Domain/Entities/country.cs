using System;

namespace taller_backend.Domain.Entities;

public class Country
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = null!;
    public virtual ICollection<Region> Regions { get; set; } = new HashSet<Region>();
    public Country() { }
    public Country(string name)
    {
        Name = name;
    }
}