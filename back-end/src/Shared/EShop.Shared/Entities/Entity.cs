﻿namespace EShop.Shared.Entities;

public class Entity(Guid? id = null)
{
    public Guid Id { get; private set; } = id ?? Guid.NewGuid();
    public DateTime CreateDate { get; private set; } = DateTime.UtcNow;
    public DateTime UpdateDate { get; protected set; }
}