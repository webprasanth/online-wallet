﻿using System;
using OnlineWallet.Core.Domain;
using Xunit;
using FluentAssertions;

namespace OnlineWallet.UnitTests.Domain
{
    public class AccountTests
    {
        private readonly Account _account;

        public AccountTests()
        {
            _account = Account.NewAccount();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(15.50)]
        [InlineData(213)]
        public void Balance_should_be_equal_to_(decimal value)
        {
            _account.IncreaseBalance(value);

            _account.Balance.ShouldBeEquivalentTo(value);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(15.50)]
        [InlineData(213)]
        public void IncreaseBalance_shouldnt_throw_exception_with_given_positive_values(decimal value)
        {
           var exception = Record.Exception(() => _account.IncreaseBalance(value));

            exception.Should().BeNull();
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-5.50)]
        [InlineData(-213)]
        public void IncreaseBalance_should_throw_exception_with_given_non_positive_values(decimal value)
        {

            var exception = Record.Exception(() => _account.IncreaseBalance(value));

            exception.Should().BeOfType<InvalidOperationException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(15.50)]
        [InlineData(213)]
        public void ReduceBalance_shouldnt_throw_exception_with_given_positive_sufficient_values(decimal value)
        {
            _account.IncreaseBalance(1000);

            var exception = Record.Exception(() => _account.ReduceBalance(value));

            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(10)]
        [InlineData(15.50)]
        [InlineData(213)]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-32)]
        [InlineData(-4.4)]
        public void ReduceBalance_should_throw_exception_with_given_exceeded_and_non_positive_values(decimal value)
        {
            _account.IncreaseBalance(5);

            var exception = Record.Exception(() => _account.ReduceBalance(value));

            exception.Should().BeOfType<InvalidOperationException>();
        }

    }
}