namespace EShop.Cart.Api.Data;

public class CartContext(DbContextOptions<CartContext> options) : DbContext(options)
{
    public DbSet<ProductCart> ProductsCarts { get; set; }
    public DbSet<Models.Cart> Carts { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartContext).Assembly);
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }
}