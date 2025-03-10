﻿using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Conta.Transacoes.Sacar.ExecutarSaque
{
    public interface ISaqueUseCase
    {
        public Task<ResponseSaqueJson> Execute(RequestSaqueJson request);
    }
}