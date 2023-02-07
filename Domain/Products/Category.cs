using Flunt.Validations;
using System.Diagnostics.Contracts;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }
    //public string Email { get; set; }

    public bool Active { get; private set; }
    public Category(string name, string createdBy, string editBy, string description)
    {

        Name = name;
        Description = description;
        Active = true;
        CreatedBy = createdBy;
        EditBy = editBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();

    }

    private void Validate()
    {
        var contract = new Contract<Category>()
                    .IsNotNullOrEmpty(Name, "Name")
                    .IsGreaterOrEqualsThan(Name, 3, "Name")
                    .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
                    .IsNotNullOrEmpty(EditBy, "EditBy");
        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active)
    {
        Active = active;
        Name = name;
        Validate();
    }
}