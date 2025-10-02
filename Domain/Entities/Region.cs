using System;

namespace taller_backend.Domain.Entities;

public class Region
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = null!;

    public Guid CountryId { get; set; }
    public virtual Country? Country { get; set; }
    public virtual ICollection<City> Cities { get; set; } = new HashSet<City>();

    private Region() { } // EF
    public Region(string name)
    {
        Name = name;
    }
}