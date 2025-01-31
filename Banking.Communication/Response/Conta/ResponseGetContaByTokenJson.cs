﻿namespace Banking.Communication.Response.Conta
{
    public class ResponseGetContaByTokenJson
    {
        public int numeroAgencia { get; set; }  
        public int numeroBanco { get; set; } = default!;
        public int numeroConta { get; set; } = default!;       
        public DateTime dataCriacao { get; set; }

    }
}
