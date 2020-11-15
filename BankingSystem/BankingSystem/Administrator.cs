using System;
using System.Collections.Generic;

namespace Bank
{
    class Administrator
    {
        private List<Account> _accounts = new List<Account>();
        private List<HistoryTransfer> _history = new List<HistoryTransfer>();

        public void AddNewAccountToList(Account accountToAdd)
        {
            _accounts.Add(accountToAdd);
        }

        public void ShowAllAccountsInList()
        {
            foreach (Account savedAccount in _accounts)
            {
                Console.WriteLine($"Name Account: {savedAccount.AccountName}");
                Console.WriteLine($"Number account: {savedAccount.AccountNumber}");
                Console.WriteLine("Account value:" + "{0:C} ", savedAccount.AccountValue);
                Console.WriteLine("-----------------------------");
            }

            if (_accounts.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("!!!!!!!!!!!!!!!!!");
                Console.WriteLine("No accounts has been created");
                Console.WriteLine("!!!!!!!!!!!!!!!!!");
            }
        }

        public void ShowHistoryTransfer()
        {
            foreach (HistoryTransfer saveHistory in _history)
            {
                Console.WriteLine($"Date of the transfer:  {saveHistory.Date}");
                Console.WriteLine($"Number account from : {saveHistory.AccountFrom}");
                Console.WriteLine($"Number account to: {saveHistory.AccountTo }");
                Console.WriteLine($"Name of the transfer: {saveHistory.NameTransfer}");
                Console.WriteLine($"Transfer value:"+"{0:C} ",saveHistory.ValueTransfer);
                Console.WriteLine($"Transfer type: {saveHistory.TypeTransfer}");
                Console.WriteLine("-----------------------------");
            }

            if (_history.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("!!!!!!!!!!!!!!!!!");
                Console.WriteLine("No transfers has been sent");
                Console.WriteLine("!!!!!!!!!!!!!!!!!");
            }
        }

        public void CashTransfer(string from, string to, string nameTransfer, float cash, DateTime date)
        {
            Account accontfrom = getAccountByName(from);
            Account accontTo = getAccountByName(to);

            accontfrom.AccountValue = accontfrom.AccountValue - cash;
            accontTo.AccountValue = accontTo.AccountValue + cash;

            informAboutTransfer(accontTo.AccountName, nameTransfer, cash, date, accontfrom.AccountName);

            HistoryTransfer transferHistoryDomestic = new HistoryTransfer(date, accontfrom.AccountNumber.ToString(), accontTo.AccountNumber.ToString(),
                                                            nameTransfer, cash, "Domestic");

            _history.Add(transferHistoryDomestic);
        }

        public void CashTransferOut(string from, string to, string nameTransfer, float cash, DateTime date)

        {
            Account accontfrom = getAccountByName(from);

            accontfrom.AccountValue = accontfrom.AccountValue - cash;

            informAboutTransfer(to, nameTransfer, cash, date, accontfrom.AccountName);

            HistoryTransfer transferHistoryOutgoing = new HistoryTransfer(date, accontfrom.AccountNumber.ToString(), to,
                                                           nameTransfer, cash, "Outgoing");

            _history.Add(transferHistoryOutgoing);
        }

        private void informAboutTransfer(string to, string nameTransfer, float cash, DateTime date, string accontfrom)
        {
            Console.WriteLine();
            Console.WriteLine("\t Transfer summary:");
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Name account from: {accontfrom} ");
            Console.WriteLine($"Name account to: {to}");
            Console.WriteLine($"Name of the transfer: {nameTransfer}");
            Console.WriteLine($"Transfer value: " + "{0:C} ", cash);
            Console.WriteLine($"Date of the transfer: {date}");
            Console.WriteLine("-----------------------------");
        }

        public bool CheckIfPossibleTransfer(string from, float cash)
        {
            Account accontfrom = null;
            bool possible = false;

            foreach (Account toSearch in _accounts)
            {
                if (toSearch.AccountName == from)
                {
                    accontfrom = toSearch;
                    break;
                }
            }

            if (cash <= accontfrom.AccountValue)
            {
                possible = true;

            }

            return possible;
        }

        public bool CheckIfAccountExistByName(string nametoSearch)
        {
            bool found = false;

            Account account = getAccountByName(nametoSearch);

            if (account != null)
            {
                found = true;
            }

            return found;
        }

        private Account getAccountByName(string nametoSearch)
        {
            Account foundAccount = null;

            foreach (Account toSearch in _accounts)
            {
                if (toSearch.AccountName == nametoSearch)
                {
                    foundAccount = toSearch;
                    break;
                }
            }

            return foundAccount;
        }
        public int GetUserAccountsCount()
        {
            return _accounts.Count;
        }
    }
}
