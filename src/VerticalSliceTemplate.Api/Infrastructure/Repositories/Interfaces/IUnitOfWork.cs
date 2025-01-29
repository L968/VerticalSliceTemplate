﻿namespace VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

internal interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
