using System;

namespace taller_backend.Domain.Entities;

public class Country
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public virtual ICollection<Region> Regions { get; set; } = new HashSet<Product>();
    public County() { }
    public Country(string name) { Name = name; }
}
