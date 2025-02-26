using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Communication.Response.Conta
{
    public class ResponseRegistrarContaJson
    {
        public int NumeroConta { get; set; }
        public Domain.Entities.Conta Conta { get; set; }
    }
}