using System;

namespace taller_backend.Domain.Entities;

public class Branch
{
    public int Id { get; private set; }
    public int NumberComercial { get; private set; }
    public string Contact_name { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public int Cityid { get; private set; } 
    public int Phone { get; private set; }
    public City City { get; private set; }
    public Company Company { get; private set; }
    public Branch() { }
    public Branch(int numbercomercial, string contact_name, int cityid, int companyid, string address, string email, int phone)
    {
        NumberComercial = numbercomercial;
        Contact_name = contact_name;
        Cityid = cityid;
        Phone = phone;
        Companyid = companyid;
        Address = address;
        Email = email;
    }

}
