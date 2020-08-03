using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankBranch : IClonable
    {

        private int bankNumber;

        public int BankNumber { get => bankNumber; set => bankNumber = value; }
        private string bankName;

        public string BankName { get => bankName; set => bankName = value; }
        private int branchNumber;
        public int BranchNumber { get => branchNumber; set => branchNumber = value; }


        private string branchAddress;
        public string BranchAddress { get => branchAddress; set => branchAddress = value; }
        private string branchCity;
        public string BranchCity { get => branchCity; set => branchCity = value; }

        private int bankAccountNumber;
        public int BankAccountNumber { get => bankAccountNumber; set => bankAccountNumber = value; }

        public override string ToString()
        {
            string BankAccount = " ";
            return BankAccount += string.Format("Bank Number: {0}\n" + "Bank Name:{1}\n" + "Branch Number:{2}\n" + "Branch Address: {3}\n" + "Branch City: {4}\n" + "BankAccountNumber: {4}\n",
                BankNumber, BankName, BranchNumber, BranchAddress, BranchCity, BankAccountNumber);
        }
   
    }
 }

