using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking.Communication.Response.Conta;

namespace Banking.Application.UseCases.Conta
{
    public interface IRegistrarContaUseCase
    {
        public Task<ResponseRegistrarContaJson> Execute();
    }
}
