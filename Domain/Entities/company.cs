using System;

namespace taller_backend.Domain.Entities;

public class Company
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Ukniu { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public int Cityid { get; private set; } = null!;
    public virtual ICollection<Branch> Branches { get; set; } = new HashSet<Branch>();
    
    public Company() { }
    public Company(string name, string ukniu, string address, string email, int cityid)
    {
        Name = name;
        Ukniu = ukniu;
        Address = address;
        Email = email;
        Cityid = cityid;
    }

}
