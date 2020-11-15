using System;

namespace Bank
{
    class Account
    {
        public string AccountName { get; private set; }
        public Guid AccountNumber { get; private set; }
        public float AccountValue { get; set; }

        public Account(string accountName, float accountValue)
        {
            AccountName = accountName;

            AccountValue = accountValue;

            AccountNumber = Guid.NewGuid();
        }
    }
}
