using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DS
{
    // אתחול רשימות לשלב נוכחי של הפרוייקט אח"כ לא נצתרך את זה
   
    public class DataSource
    {
        public static List<HostingUnit> hostingUnitList = new List<HostingUnit>()
        {                              new HostingUnit(){ HostingUnitKey=Configuration.HostingUnitKey,  Owner =new Host() { HostKey = 200000001, PrivateName = "yael", FamilyName = "levi", FhoneNumber = 020000001, MailAddress = "yael@gmail.com" ,
                                       BankAccount = new BankBranch() { BankNumber = 111, BankName = Bank_Name.Discont, BranchNumber = 1111, BranchAddress = "beit hadfous 65", BranchCity = "Jerusalem", BankAccountNumber = 123111 } , CollectionClearance = false } ,HostingUnitName="yael hotel", Area = AreasInTheCountry.Jerusalem },
                                       new HostingUnit(){ HostingUnitKey=Configuration.HostingUnitKey,  Owner =new Host() { HostKey = 200000002, PrivateName = "ruthi", FamilyName = "cohen", FhoneNumber = 02-0000002, MailAddress = "ruthi@gmail.com",
                                       BankAccount = new BankBranch() { BankNumber = 222, BankName = Bank_Name.Igood, BranchNumber = 2222, BranchAddress =  "kanfey nesharim 3", BranchCity = "Jerusalem", BankAccountNumber =123222 } , CollectionClearance = true } ,HostingUnitName="ruthi houses" ,Area = AreasInTheCountry.Jerusalem  },
                                       new HostingUnit(){ HostingUnitKey=Configuration.HostingUnitKey,  Owner =new Host() { HostKey = 200000003, PrivateName = "odelia", FamilyName = "melloul", FhoneNumber = 02-0000003, MailAddress = "odelia@gmail.com",
                                       BankAccount = new BankBranch() { BankNumber =333, BankName = Bank_Name.Igood, BranchNumber = 3333, BranchAddress =  "smilanski 14", BranchCity = "Netanya", BankAccountNumber = 123333 } , CollectionClearance =false } ,HostingUnitName= "odelia's tsimer", Area = AreasInTheCountry.Center }
        };

       public static List<Order> ordersList = new List<Order>() { new Order(){ HostingUnitKey=10000005,GuestRequestKey= 22222221,OrderKey =Configuration.OrderKey, Status= OrderStatus.mail_has_been_sent,CreateDate= new DateTime(2019,12,01),OrderDate=new DateTime( 2019/12/12) },
                                                                  new Order(){ HostingUnitKey=10000007,GuestRequestKey= 22222223,OrderKey =Configuration.OrderKey,Status= OrderStatus.mail_has_been_sent,CreateDate= new DateTime(2019,08,02),OrderDate=new DateTime(2019/09/02) }
       };

        public static List<GuestRequest> guestRequestsList = new List<GuestRequest>(){new GuestRequest(){GuestRequestKey= Configuration.GuestRequestKey,PrivateName="yaniv",FamilyName="ashkenazi",MailAddress="yanivA@gmail.com",Status=OrderStatus.mail_has_been_sent, RegistrationDate= new DateTime(2019,06,11),
                                                                                     EntryDate= new DateTime(2019/10/11),ReleaseDate= new DateTime (2019/10/26),Area= AreasInTheCountry.Jerusalem,SubArea= "yafo",
                                                                                      Type= TypesOfVacation.Zimmer,Adults= 2,Children=1,Pool= ChoosingAttraction.necessary,Jacuzzi= ChoosingAttraction.not_interested,
                                                                                     Garden= ChoosingAttraction.necessary,ChildrensAttractions= ChoosingAttraction.possible, Its_Signed= true,IsApproved= true},

                                                                                     new GuestRequest() {GuestRequestKey = Configuration.GuestRequestKey,PrivateName = "Sara",FamilyName = "cohen",MailAddress = "SaraC@gmail.com",Status = OrderStatus.Not_treated, RegistrationDate = new DateTime(2019, 10, 30),
                                                                                     EntryDate = new DateTime(2019 / 11 / 06),ReleaseDate = new DateTime(2019 / 11 / 12),Area = AreasInTheCountry.North,SubArea = "givat shaul",
                                                                                     Type = TypesOfVacation.Zimmer,Adults = 2,Children = 1,Pool = ChoosingAttraction.necessary,Jacuzzi = ChoosingAttraction.not_interested,
                                                                                     Garden = ChoosingAttraction.necessary,ChildrensAttractions = ChoosingAttraction.possible,Its_Signed= true,IsApproved = true },

                                                                                     new GuestRequest() {GuestRequestKey = Configuration.GuestRequestKey,PrivateName = "Haya",FamilyName = "aharonov",MailAddress = "haya770@gmail.com",Status = OrderStatus.mail_has_been_sent, RegistrationDate = new DateTime(2019,11,06),
                                                                                     EntryDate =  new DateTime(2019/03/22),ReleaseDate =new DateTime (2019/03/26),Area =  AreasInTheCountry.North,SubArea = "Tveria",
                                                                                     Type = TypesOfVacation.Camping,Adults = 2,Children = 2,Pool =ChoosingAttraction.not_interested,Jacuzzi = ChoosingAttraction.not_interested,
                                                                                     Garden = ChoosingAttraction.necessary,ChildrensAttractions = ChoosingAttraction.possible,Its_Signed= false,IsApproved = true }
        };
 
    }
}

