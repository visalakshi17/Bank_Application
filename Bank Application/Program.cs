using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;
using System.Reflection;

namespace Bank_Application
{
    class Bank
    {
        Dictionary<String, int> Currencies = new Dictionary<String, int>();
        public void AddNewCurrency(String Currency, int ExchangeRate)
        {
            Currencies.Add(Currency, ExchangeRate);
        }
        /*public void SameAddCharges(SenderBankId, ReceiverBankId, Amnt)
        {
            if (SenderBankId == ReceiverBankId)
            {
                Double imps = Convert.ToDouble(Amnt) * 0.05;
                AccountDictionary[SendAcId].Amount = AccountDictionary[SendAcId].Amount - Convert.ToInt32(imps);
                Console.WriteLine("IMPS charges = " + imps.ToString() + " debited");
            }
            else
            {
                Double rtgs = Convert.ToDouble(Amnt) * 0.02;
                Double imps = Convert.ToDouble(Amnt) * 0.06;
                AccountDictionary[SendAcId].Amount = AccountDictionary[SendAcId].Amount - Convert.ToInt32(imps + rtgs);
                Console.WriteLine("RTGS charges = " + imps.ToString() + " debited");
                Console.WriteLine("IMPS charges = " + imps.ToString() + " debited");
            }
        }*/

    }
    class Account
    {
        String UserName, Password;
        public int Amount = 2000,pointer=0;
        public String[] Transactions = new String[10];
        public String BankId;
        public String CreateAccount()
        {
            Console.WriteLine("Enter the name of the bank: ");
            BankId = Console.ReadLine();
            Console.WriteLine("Enter the name of the account holder: ");
            String Name = Console.ReadLine();
            DateTime Date = DateTime.Today;
            String AccountId = Name.Substring(0, 3) + Date.ToString("dd/MM/yyyy");
            Console.WriteLine("AccountId = "+ AccountId);
            UserName = Name + Date.ToString("dd/MM/yyyy").Substring(0, 2);
            Console.WriteLine("Username : " + UserName);
            Random res = new Random();
            int Size = 8;
            String str = "abcdefghijklmnopqrstuvwxyz0123456789";
            Password = "";
            for (int i = 0; i < Size; i++)
            {
                int x = res.Next(str.Length);
                Password = Password + str[x];
            }
            Console.WriteLine("Password : " + Password.ToString());
            return AccountId;
        }
        public void PrintDetails()
        {
            Console.WriteLine(UserName);
            Console.WriteLine(Password);
        }
        public void PrintTransactionHistory()
        {
            while(pointer>=0)
            {
                Console.WriteLine(Transactions[pointer]);
                pointer--;

            }
        }
        public int CurrencyConversion(int Value,int ConversionRate)
        {
            return Value * ConversionRate;
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<String, Account> AccountDictionary = new Dictionary<String, Account>();
            Dictionary<String, String> UserDetails = new Dictionary<String, String>();
            while (true)
            {
                Console.WriteLine("Choose one of the login option \n 1.Login as an bank staff \n 2. Login as Account Holder \n 3. Exit");
                int Choice = Convert.ToInt32(Console.ReadLine());
                if (Choice == 1)
                {
                    Console.WriteLine("Select your required service: \na.Create new account \nb.Update / Delete account at any time\nc.Add new Accepted currency with exchange rate \nd.Add service charge for same bank account\ne.Add service charge for other bank account\nf.Can view account transaction history\ng. Can revert any transaction\nh.exit");
                    Char Action = Convert.ToChar(Console.ReadLine());
                    if (Action == 'a')
                    {
                        Account x = new Account();
                        String AccountId = x.CreateAccount();
                        AccountDictionary.Add(AccountId, x);
                    }
                    else if (Action == 'b')
                    {
                            Console.WriteLine("Enter the accountId to be deleted: ");
                            String AcId = Console.ReadLine();
                            AccountDictionary.Remove(AcId);
                    }
                    else if(Action == 'c')
                    {
                        Console.WriteLine("Enter the new currency: ");
                        String CurrencyName = Console.ReadLine();
                        Console.WriteLine("Enter the exchange rate: ");
                        int ExcngRate = Convert.ToInt32(Console.ReadLine());
                        Bank x = new Bank();
                        x.AddNewCurrency(CurrencyName, ExcngRate);

                    }
                    else if (Action == 'd')
                    {
                        Console.WriteLine("Enter the updated RTGS value: ");
                        String RTGS= Console.ReadLine();
                        Console.WriteLine("Enter the updated IMPS value: ");
                        String IMPS= Console.ReadLine();
                        Console.WriteLine("Added successfully !!");
                    }
                    else if (Action == 'e')
                    {
                        Console.WriteLine("Enter the updated RTGS value: ");
                        String RTGS = Console.ReadLine();
                        Console.WriteLine("Enter the updated IMPS value: ");
                        String IMPS = Console.ReadLine();
                        Console.WriteLine("Added successfully !!");
                    }
                    else if (Action == 'f')
                    {
                        Console.WriteLine("Enter the AccountId: ");
                        String AcntId = Console.ReadLine();
                        AccountDictionary[AcntId].PrintTransactionHistory();
                    }
                    else if (Action == 'g')
                    {
                        Console.WriteLine("Enter the AccountId: ");
                        String AcntId = Console.ReadLine();
                        int i = AccountDictionary[AcntId].pointer;
                        String statement = AccountDictionary[AcntId].Transactions[i - 1];
                        if (statement.Contains("Transferred") == true)
                        {
                            var words = statement.Split(' ');
                            String receivedAcnt = words[3];
                            int Amnt = Convert.ToInt32(words[1]);
                            AccountDictionary[AcntId].Amount = AccountDictionary[AcntId].Amount + Amnt;
                            AccountDictionary[receivedAcnt].Amount = AccountDictionary[AcntId].Amount - Amnt;
                            String NewTxStatement = Amnt.ToString() + " credited";
                            AccountDictionary[AcntId].Transactions[i]= NewTxStatement;
                            String NewRxStatement = Amnt.ToString() + " debited";
                            AccountDictionary[receivedAcnt].Transactions[i] = NewRxStatement;
                            Console.WriteLine("Transaction reverted !!");
                        }
                        else if (statement.Contains("Received") == true)
                        {
                            var words = statement.Split(' ');
                            String TransferedAcnt = words[3];
                            int Amnt = Convert.ToInt32(words[1]);
                            AccountDictionary[AcntId].Amount = AccountDictionary[AcntId].Amount - Amnt;
                            AccountDictionary[TransferedAcnt].Amount = AccountDictionary[AcntId].Amount + Amnt;
                            String NewTxStatement = Amnt.ToString() + " Debited";
                            AccountDictionary[AcntId].Transactions[i] = NewTxStatement;
                            String NewRxStatement = Amnt.ToString() + " Credited";
                            AccountDictionary[TransferedAcnt].Transactions[i] = NewRxStatement;
                            Console.WriteLine("Transaction reverted !!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Action !!");
                        }
                    }
                    else if (Action == 'h')
                    {
                        return;
                    }
                }
                else if (Choice == 2)
                {
                    Console.WriteLine("Select your required service: \n 1.Deposit Amount \n 2.Withdraw amount  \n 3.Transfer funds  \n 4. view account transaction history\n5. Exit");
                    int opt = Convert.ToInt32(Console.ReadLine());
                    if (opt == 1)
                    {
                        Console.WriteLine("Enter your AccountId: ");
                        String AcId = Console.ReadLine();
                        Console.WriteLine("Enter the amount to be deposited: ");
                        int Amnt = Convert.ToInt32(Console.ReadLine());
                        AccountDictionary[AcId].Amount = AccountDictionary[AcId].Amount + Amnt;
                        int i = AccountDictionary[AcId].pointer;
                        String Word = "Deposited" + Amnt.ToString();
                        AccountDictionary[AcId].Transactions[i] = Word;
                        AccountDictionary[AcId].pointer++;
                        Console.WriteLine("Amount deposited succesfully !!");
                    }
                    else if (opt == 2)
                    {
                        Console.WriteLine("Enter your AccountId: ");
                        String AcId = Console.ReadLine();
                        Console.WriteLine("Enter the amount to be Withdrawn: ");
                        int Amnt = Convert.ToInt32(Console.ReadLine());
                        AccountDictionary[AcId].Amount = AccountDictionary[AcId].Amount - Amnt;
                        int i = AccountDictionary[AcId].pointer;
                        String Word = "Withdrawn" + Amnt.ToString();
                        AccountDictionary[AcId].Transactions[i] = Word;
                        AccountDictionary[AcId].pointer++;
                        Console.WriteLine("Amount withdrawn succesfully !!");
                    }
                    else if (opt == 3)
                    {
                        Console.WriteLine("Enter the AccountId to send money: ");
                        String SendAcId = Console.ReadLine();
                        Console.WriteLine("Enter the Sender bank name: ");
                        String SenderBankId = Console.ReadLine();
                        Console.WriteLine("Enter the amount to be sent: ");
                        int Amnt = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the AccountId to receive money: ");
                        String ReceiveAcId = Console.ReadLine();
                        Console.WriteLine("Enter the Receiver bank name: ");
                        String ReceiverBankId = Console.ReadLine();
                        AccountDictionary[SendAcId].Amount = AccountDictionary[SendAcId].Amount - Amnt;
                        AccountDictionary[ReceiveAcId].Amount = AccountDictionary[ReceiveAcId].Amount + Amnt;
                        int i = AccountDictionary[SendAcId].pointer;
                        String Word = "Transfered " + Amnt.ToString() + " to " + ReceiveAcId;
                        AccountDictionary[SendAcId].Transactions[i] = Word;
                        AccountDictionary[SendAcId].pointer++;
                        i = AccountDictionary[ReceiveAcId].pointer;
                        Word = "Received " + Amnt.ToString() + " from " + SendAcId;
                        AccountDictionary[ReceiveAcId].Transactions[i] = Word;
                        AccountDictionary[ReceiveAcId].pointer++;
                        Console.WriteLine("Amount transferred succesfully !!");

                        if (SenderBankId == ReceiverBankId)
                        {
                            Double imps = Convert.ToDouble(Amnt) * 0.05;
                            AccountDictionary[SendAcId].Amount = AccountDictionary[SendAcId].Amount - Convert.ToInt32(imps);
                            Console.WriteLine("IMPS charges = " + imps.ToString() + " debited");
                        }
                        else
                        {
                            Double rtgs = Convert.ToDouble(Amnt) * 0.02;
                            Double imps = Convert.ToDouble(Amnt) * 0.06;
                            AccountDictionary[SendAcId].Amount = AccountDictionary[SendAcId].Amount - Convert.ToInt32(imps+rtgs);
                            Console.WriteLine("RTGS charges = " + imps.ToString() + " debited");
                            Console.WriteLine("IMPS charges = " + imps.ToString() + " debited");
                        }
                    }
                    else if (opt == 4)
                    {
                        Console.WriteLine("Enter the AccountId: ");
                        String AcntId= Console.ReadLine();
                        AccountDictionary[AcntId].PrintTransactionHistory();
                    }
                    else if (opt == 5)
                    {
                        return;
                    }
                }
                else if (Choice == 3)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Enter valid option");
                }
            }
        }
    }
}
