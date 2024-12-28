﻿namespace Banking.Domain.Repositories.Conta
{
    public interface IGravarContaRepository
    {
        public Task Add(Entities.Conta conta);
        public Task DeletarConta(Entities.Conta conta);
    }
}
