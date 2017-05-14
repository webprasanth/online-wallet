﻿using System;
using System.Collections.Generic;

namespace OnlineWallet.Core.Domain
{
    public class Account
    {
        protected Account()
        {
            Balance = 0;
        }

        public decimal Balance { get; protected set; }

        public static Account NewAccount()
            => new Account();

        public void IncreaseBalance(decimal value)
        {
            if (value <= 0)
            {
                throw new InvalidOperationException("Cannot increase with non positive value");
            }
            else
            {
                Balance += value;
            }
        }

        public void ReduceBalance(decimal value)
        {
            if (value <= 0)
            {
                throw new InvalidOperationException("Cannot reduce non positive value");
            }
            else if (value > Balance)
            {
                throw new InvalidOperationException("Insuficient funds");
            }
            else
            {
                Balance -= value;
            }
        }


    }
}