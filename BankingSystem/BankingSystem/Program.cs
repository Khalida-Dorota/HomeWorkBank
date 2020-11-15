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
                Console.WriteLine();

                int userOption = GetIntFromUser("** Select option by number: **");

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
                        Console.WriteLine();
                        Console.WriteLine("!!!!!!!!!!!!!!!!!");
                        Console.WriteLine("Unknown option.");
                        Console.WriteLine("!!!!!!!!!!!!!!!!!");
                        break;
                }

            } while (true);
        }

        void UserWantsToCreateAccount()
        {
            var newAccountName = "";
            do
            {
                newAccountName = GetUserInputBasedOnMessage("** Enter your account name: **");
                bool exist = _accountsAdmin.CheckIfAccountExistByName(newAccountName);

                if (exist != true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                    Console.WriteLine("Account with that name exists.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                }

            } while (true);


            float newAccountValue = 1000;

            var account = new Account(newAccountName, newAccountValue);

            Console.WriteLine();
            Console.WriteLine("\t Your account details :");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Name Account: " + account.AccountName);
            Console.WriteLine("Number account: " + account.AccountNumber);
            Console.WriteLine("Value: " + "{0:C} ", account.AccountValue);
            Console.WriteLine("-----------------------------");
            _accountsAdmin.AddNewAccountToList(account);

        }

        void DomesticTransfer()

        {
            if (!DoesUserHaveEnoughAccounts(2))
            {
                Console.WriteLine();
                Console.WriteLine("!!!!!!!!!!!!!!!!!");
                Console.WriteLine("No account");
                Console.WriteLine("!!!!!!!!!!!!!!!!!");
                return;
            }

            string fromAccount = "";
            
            do
            {
                ShowAllOwnAccounts();
                fromAccount = GetUserInputBasedOnMessage("** Enter the account name from which you want to make the transfer: **");
                bool exist = _accountsAdmin.CheckIfAccountExistByName(fromAccount);

                if (exist == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                    Console.WriteLine("Account with that name does't exists.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                }

            } while (true);

            string toAccount = "";

            do
            {
                ShowAllOwnAccounts();
                Console.WriteLine("***********************************");
                Console.WriteLine("Account name from which you make the transfer : " + fromAccount);
                Console.WriteLine("***********************************");
                toAccount = GetUserInputBasedOnMessage("** Enter the account name which you want to make the transfer: **");
                bool exist = _accountsAdmin.CheckIfAccountExistByName(toAccount);

                if (exist == true && toAccount != fromAccount)
                {
                    break;
                }
                else if (toAccount == fromAccount)
                {
                    Console.WriteLine();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                    Console.WriteLine("Cannot transfer to the same account.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                    Console.WriteLine("Account with that name does't exists.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                }

            } while (true);

            string nameOfTransfer = GetUserInputBasedOnMessage("** Enter name transfer: **");

            float howManyCashwantTranfer = 0;

            do
            {
                howManyCashwantTranfer = (GetFloatFromUser("** Enter value of transfer: **"));

                bool possible = _accountsAdmin.CheckIfPossibleTransfer(fromAccount, howManyCashwantTranfer);

                if (possible == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                    Console.WriteLine("There is not sufficient funding.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                }

            } while (true);

            DateTime today = DateTime.Now;

            Console.WriteLine("Date of the transfer : " + today);

            _accountsAdmin.CashTransfer(fromAccount, toAccount, nameOfTransfer, howManyCashwantTranfer, today);

        }

        void OutgoingTransfer()
        {
            if (!DoesUserHaveEnoughAccounts(1))
            {
                Console.WriteLine();
                Console.WriteLine("!!!!!!!!!!!!!!!!!");
                Console.WriteLine("No account");
                Console.WriteLine("!!!!!!!!!!!!!!!!!");
                return;
            }

            string fromAccount = "";

            do
            {
                ShowAllOwnAccounts();
                fromAccount = GetUserInputBasedOnMessage("** Enter the account name from which you want to make the transfer: **");
                bool exist = _accountsAdmin.CheckIfAccountExistByName(fromAccount);

                if (exist == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                    Console.WriteLine("Account with that name does't exists.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                }

            } while (true);



            string toAccount = "C6A8C9AC-CEEB-4F50-AC92-DE355AB6E699";

            Console.WriteLine($"Account number which you make the transfer :  {toAccount}");


            string nameOfTransfer = GetUserInputBasedOnMessage("** Enter name transfer: **");

            float howManyCashwantTranfer = 0;

            do
            {
                howManyCashwantTranfer = (GetFloatFromUser("** Enter value of transfer: **"));

                bool possible = _accountsAdmin.CheckIfPossibleTransfer(fromAccount, howManyCashwantTranfer);

                if (possible == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                    Console.WriteLine("There is not sufficient funding.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                }

            }
            while (true);

            DateTime today = DateTime.Now;

            Console.WriteLine("Date of the transfer : " + today);

            _accountsAdmin.CashTransferOut(fromAccount, toAccount, nameOfTransfer, howManyCashwantTranfer, today);

        }

        void ShowAllOwnAccounts()
        {
            Console.WriteLine();
            Console.WriteLine("\t All available accounts:");
            Console.WriteLine("-----------------------------");
            _accountsAdmin.ShowAllAccountsInList();
            Console.WriteLine();
        }

        void TransferHistory()
        {
            Console.WriteLine();
            Console.WriteLine("\t Transfer History:");
            Console.WriteLine("-----------------------------");
            _accountsAdmin.ShowHistoryTransfer();
        }

        void Exit()
        {
            Console.WriteLine("End program.");
            Environment.Exit(0);
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
                    Console.WriteLine();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                    Console.WriteLine("Input was empty");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
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
                    Console.WriteLine();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                    Console.WriteLine("This is not int:" + userIntOption);
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
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
                    Console.WriteLine();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                    Console.WriteLine("This is not float:" + userFloatOption);
                    Console.WriteLine("!!!!!!!!!!!!!!!!!");
                }

            } while (!parseSuccess);

            return userFloatNumber;
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
    }
}
