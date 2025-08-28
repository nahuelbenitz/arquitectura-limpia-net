﻿namespace DientesLimpios.Application.Utilities.Mediator
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }
}
