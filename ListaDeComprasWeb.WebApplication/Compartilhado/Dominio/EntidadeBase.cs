using System;
using System.Security.Cryptography;

namespace ListaDeComprasWeb.WebApplication.Compartilhado.Dominio;

public abstract class EntidadeBase<T>
{
    public string Id { get; set; } = Convert
            .ToHexStringLower(RandomNumberGenerator.GetBytes(20))
            .Substring(0, 7);

    public abstract List<string> Validar();
    public abstract void Atualizar(T entidadeAtualizada);
}

