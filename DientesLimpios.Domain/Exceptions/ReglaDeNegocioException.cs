﻿namespace DientesLimpios.Domain.Exceptions
{
    public class ReglaDeNegocioException : Exception
    {
        public ReglaDeNegocioException(string mensaje) : base(mensaje)
        {
        }
    }
}
