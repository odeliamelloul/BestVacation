using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Host : IClonable
    {
        private int hostKey;

        public  int HostKey { get => hostKey; set => hostKey = value; }

        private string privateName;
        public string PrivateName { get => privateName; set => privateName = value; }
        private string familyName;
        public string FamilyName { get => familyName; set => familyName = value; }
        private int fhoneNumber;
        public int FhoneNumber { get => fhoneNumber; set => fhoneNumber = value; }
        private string mailAddress;
        public string MailAddress { get => mailAddress; set => mailAddress = value; }
        private BankBranch bankAccount;
        public BankBranch BankAccount { get => bankAccount; set => bankAccount = value; }
        private bool collectionClearance;
        public bool CollectionClearance { get => collectionClearance; set => collectionClearance = value; }



        public override string ToString()
        {
            string Host = " ";
            return Host += string.Format("Host Key: {0}\n" + "Name: {1} {2}\n" + "Fhone Number: {3}\n" + "Mail: {4}\n" +" -Bank Account:\n" + "{5}\n" + "Collection Clearance:{6}",
                HostKey, PrivateName, FamilyName, FhoneNumber, MailAddress, BankAccount, CollectionClearance);
        }
    }
}
