using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Entities.Account;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Domain.Entities.Branches;

public class Branch
{
    public Guid Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public BranchStatus Status { get; private set; }

    public ICollection<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();
    public ICollection<RoleBranch> RoleBranches { get; private set; } = new List<RoleBranch>();

    private Branch() { }

    public Branch(string name, string city, string address, string phone)
    {
        ApplyBusinessRules(name, city, address, phone);

        Id = Guid.CreateVersion7();
        Name = name;
        City = city;
        Address = address;
        Phone = phone;
        Status = BranchStatus.Active;
    }

    public void UpdateBranch(string name, string city, string address, string phone)
    {
        ApplyBusinessRules(name, city, address, phone);

        Name = name;
        City = city;
        Address = address;
        Phone = phone;
    }

    public void Activate() => Status = BranchStatus.Active;

    public void Deactivate() => Status = BranchStatus.Inactive;

    private void ApplyBusinessRules(string name, string city, string address, string phone)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Length > 50)
            throw new BusinessRulesException($"El {nameof(name)} es requerido (3-50 caracteres).");

        if (string.IsNullOrWhiteSpace(city) || city.Length < 3 || city.Length > 20)
            throw new BusinessRulesException($"La {nameof(city)} es requerida (3-20 caracteres).");

        if (string.IsNullOrWhiteSpace(address) || address.Length < 10 || address.Length > 50)
            throw new BusinessRulesException($"La {nameof(address)} debe ser específica (10-50 caracteres).");

        if (string.IsNullOrWhiteSpace(phone) || phone.Length < 7 || phone.Length > 12)
            throw new BusinessRulesException($"El {nameof(phone)} debe tener entre 7 y 12 dígitos.");
    }
}