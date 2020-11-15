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
                Console.WriteLine("Value:" + "{0:C}", savedAccount.AccountValue);
                Console.WriteLine("-----------------------------");
            }

            if (_accounts.Count == 0)
            {
                Console.WriteLine("No accounts has been created");
            }
        }

        public int GetUserAccountsCount()
        {
            return _accounts.Count;
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

        public void ShowHistoryTransfer()
        {
            foreach (HistoryTransfer saveHistory in _history)
            {
                Console.WriteLine($"Data wykonania przelewu: {saveHistory.Date}");
                Console.WriteLine($"Nr konta żródłowego : {saveHistory.AccountFrom}");
                Console.WriteLine($"Nr konto docolowego: {saveHistory.AccountTo }");
                Console.WriteLine($"Tytuł przelewu: {saveHistory.NameTransfer}");
                Console.WriteLine($"Kwota przelewu: {saveHistory.ValueTransfer}");
                Console.WriteLine($"Jaki jest to przelew: {saveHistory.TypeTransfer}");
            }

            if (_history.Count == 0)
            {
                Console.WriteLine("No transfers has been sent");
            }
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
        private void informAboutTransfer(string to, string nameTransfer, float cash, DateTime date, string accontfrom)
        {
            Console.WriteLine("\t Podsumowanie wukonanego przelewu:");
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Przelew został wykonany z konta: {accontfrom} ");
            Console.WriteLine($"Przelew został wykonany na konto: {to}");
            Console.WriteLine($"Tytul przelewu:{nameTransfer}");
            Console.WriteLine($"Kwota pzrelewu:" + "{0:C}", cash);
            Console.WriteLine($"Data wykonanai przelewu:{date}");
            Console.WriteLine("-----------------------------");
        }
    }
}
