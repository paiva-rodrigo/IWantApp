using Flunt.Notifications;
using IWantApp.Domain.Products;

namespace IWantApp.Domain;

//classe abstrata é aquela que não pode ser instanciada, apenas herdada por outra classe
public abstract class Entity : Notifiable<Notification>
{
    //toda vez que instanciar alguma classe fiha dessa já vai instanciar o id
    public Entity()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public string Description { get; set; }
    public bool HasStock { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string EditBy { get; set; }
    public DateTime EditedOn { get; set; }
}
