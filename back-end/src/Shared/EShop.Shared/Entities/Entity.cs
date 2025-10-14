namespace EShop.Shared.Entities;

public class Entity
{
    public Entity()
    {
        Id = Guid.NewGuid();
        CreateDate = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime UpdateDate { get; protected set; }
}