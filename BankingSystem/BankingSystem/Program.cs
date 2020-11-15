using System;

namespace Bank
{
    class Start
    {
        Administrator _accountsAdmin = new Administrator();

        static void Main(string[] args)
        {
            new Start().Run();
        }

        void Run()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("\t MENU: ");
                Console.WriteLine("1. Create account");
                Console.WriteLine("2. Domestic transfer");
                Console.WriteLine("3. Outgoing transfer");
                Console.WriteLine("4. Accounts balance");
                Console.WriteLine("5. Transfer History");
                Console.WriteLine("6. Exit");

                int userOption = GetIntFromUser("Select option by number:");

                switch (userOption)
                {
                    case 1:
                        UserWantsToCreateAccount();
                        break;
                    case 2:
                        DomesticTransfer();
                        break;
                    case 3:
                        OutgoingTransfer();
                        break;
                    case 4:
                        ShowAllOwnAccounts();
                        break;
                    case 5:
                        TransferHistory();
                        break;
                    case 6:
                        Exit();
                        break;
                    default:
                        Console.WriteLine("Unknown option");
                        break;
                }

            } while (true);
        }

        void TransferHistory()
        {
            _accountsAdmin.ShowHistoryTransfer();
        }

        void UserWantsToCreateAccount()
        {
            var newAccountName = "";
            do
            {
                newAccountName = GetUserInputBasedOnMessage("Enter your account name");
                bool exist = _accountsAdmin.CheckIfAccountExistByName(newAccountName);

                if (exist != true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Konto o takiej nazwie istnieje");
                }

            } while (true);


            float newAccountValue = 1000;

            var account = new Account(newAccountName, newAccountValue);

            Console.WriteLine();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("\t Your account details :");
            Console.WriteLine("Name Account: " + account.AccountName);
            Console.WriteLine("Number account: " + account.AccountNumber);
            Console.WriteLine("Value: " + "{0:C}", account.AccountValue);
            Console.WriteLine("-----------------------------");
            _accountsAdmin.AddNewAccountToList(account);

        }

        void DomesticTransfer()

        {
            if (!DoesUserHaveEnoughAccounts(2))
            {
                return;
            }

            string fromAccount = "";

            do
            {
                ShowAllOwnAccounts();
                fromAccount = GetUserInputBasedOnMessage("Podaj nazwe kona z ktorego chcesz wykonac przelew:");
                bool exist = _accountsAdmin.CheckIfAccountExistByName(fromAccount);

                if (exist == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Takie konto nie istnieje.");
                }

            } while (true);

            string toAccount = "";

            do
            {
                ShowAllOwnAccounts();
                Console.WriteLine("Z ktrego konta wykonujesz przelew : " + fromAccount);
                toAccount = GetUserInputBasedOnMessage("Podaj nazwe kona do ktorego chcesz wykonac przelew:");
                bool exist = _accountsAdmin.CheckIfAccountExistByName(toAccount);

                if (exist == true && toAccount != fromAccount)
                {
                    break;
                }
                else if (toAccount == fromAccount)
                {
                    Console.WriteLine(" Nie moesz wykonywac przelewu na to samo konto");
                }
                else
                {
                    Console.WriteLine("Takie konto nie istnieje.");
                }

            } while (true);

            string nameOfTransfer = GetUserInputBasedOnMessage("Podah tytu przelewu");

            float howManyCashwantTranfer = 0;

            do
            {
                howManyCashwantTranfer = (GetFloatFromUser("Podaj ile hasju chcesz przelac (korzystaj z \".\")"));

                bool possible = _accountsAdmin.CheckIfPossibleTransfer(fromAccount, howManyCashwantTranfer);

                if (possible == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Nie masz wystarczajacych sriodkow na koncie.");
                }

            } while (true);

            DateTime today = DateTime.Now;

            Console.WriteLine("Data wykonania przelewu to : " + today);

            _accountsAdmin.CashTransfer(fromAccount, toAccount, nameOfTransfer, howManyCashwantTranfer, today);

        }

        void OutgoingTransfer()
        {
            if (!DoesUserHaveEnoughAccounts(1))
            {
                return;
            }

            string fromAccount = "";

            do
            {
                ShowAllOwnAccounts();
                fromAccount = GetUserInputBasedOnMessage("Podaj nazwe kona z ktorego chcesz wykonac przelew:");
                bool exist = _accountsAdmin.CheckIfAccountExistByName(fromAccount);

                if (exist == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Takie konto nie istnieje.");
                }

            } while (true);



            string toAccount = "C6A8C9AC-CEEB-4F50-AC92-DE355AB6E699";

            Console.WriteLine($"Wykonujesz przelew na konto XYZ. nr konta : {toAccount}");


            string nameOfTransfer = GetUserInputBasedOnMessage("Podah tytu przelewu");

            float howManyCashwantTranfer = 0;

            do
            {
                howManyCashwantTranfer = (GetFloatFromUser("Podaj ile hasju chcesz przelac (korzystaj z \".\")"));

                bool possible = _accountsAdmin.CheckIfPossibleTransfer(fromAccount, howManyCashwantTranfer);

                if (possible == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Nie masz wystarczajacych sriodkow na koncie.");
                }

            }
            while (true);

            DateTime today = DateTime.Now;

            Console.WriteLine("Data wykonania przelewu to : " + today);

            _accountsAdmin.CashTransferOut(fromAccount, toAccount, nameOfTransfer, howManyCashwantTranfer, today);

        }

        bool DoesUserHaveEnoughAccounts(int minimalQuantity)
        {
            int accountsQuantity = _accountsAdmin.GetUserAccountsCount();

            if (accountsQuantity >= minimalQuantity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void ShowAllOwnAccounts()
        {
            Console.WriteLine();
            Console.WriteLine("**************************************************");
            Console.WriteLine();
            Console.WriteLine("\t All available accounts:");
            Console.WriteLine("-----------------------------");
            _accountsAdmin.ShowAllAccountsInList();
            Console.WriteLine();
            Console.WriteLine("**************************************************");
        }



        string GetUserInputBasedOnMessage(string messageForUser)
        {
            string input = "";

            do
            {
                Console.WriteLine(messageForUser);
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input was empty");
                }
            }
            while (string.IsNullOrEmpty(input));

            return input;
        }

        int GetIntFromUser(string getNumberMessage)
        {
            int userNumber = 0;
            bool parseSuccess = false;

            do
            {
                string userIntOption = GetUserInputBasedOnMessage(getNumberMessage);
                parseSuccess = int.TryParse(userIntOption, out userNumber);

                if (parseSuccess == false)
                {
                    Console.WriteLine("Nie podałeś liczby:" + userIntOption);
                }

            } while (!parseSuccess);

            return userNumber;

        }

        float GetFloatFromUser(string getNumberMessage)
        {
            float userFloatNumber = 0;
            bool parseSuccess = false;

            do
            {
                string userFloatOption = GetUserInputBasedOnMessage(getNumberMessage);

                userFloatOption = userFloatOption.Replace(",", ".");

                parseSuccess = float.TryParse(userFloatOption, out userFloatNumber);

                if (parseSuccess == false)
                {
                    Console.WriteLine("Nie podałeś liczby:" + userFloatOption);
                }

            } while (!parseSuccess);

            return userFloatNumber;
        }

        void Exit()
        {
            Console.WriteLine("Good Bye!");
            Environment.Exit(0);
        }

    }
}
