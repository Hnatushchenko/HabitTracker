﻿using Application.Data;
using Domain;

namespace Infrastructure;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationContext _applicationContext;

    public UnitOfWork(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
}