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
        //   Area, Adults,children,pool,jacuzzi,Garden,childrensAttractions);
        static bool[,] Diary()
        {
            bool[,] Diary = new bool[32, 13];
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 13; j++)
                    Diary[i, j] = false;
            }
            return Diary;
        }
        public static List<HostingUnit> hostingUnitList = new List<HostingUnit>()
        {                              new HostingUnit(){ HostingUnitKey=11111111,  Owner =new Host() { HostKey = 200000001, PrivateName = "yael", FamilyName = "levi", PhoneNumber = 020000001, MailAddress = "yael@gmail.com" ,
                                       BankAccount = new BankBranch() { BankNumber = 111, BankName = "Discont", BranchNumber = 1111, BranchAddress = "beit hadfous 65", BranchCity = "Jerusalem"} , CollectionClearance = false }
                                       ,HostingUnitName="yael hotel", Area = AreasInTheCountry.דרום,SubArea= All.אופקים ,Room = 4,Adults=2,Children=1,Pool= true ,Jacuzzi=true ,Garden=true,Dinner =true ,},
                                       new HostingUnit(){ HostingUnitKey=11111115,  Owner =new Host() { HostKey = 200000001, PrivateName = "yael", FamilyName = "levi", PhoneNumber = 020000001, MailAddress = "yael@gmail.com" ,
                                       BankAccount = new BankBranch() { BankNumber = 111, BankName = "Discont", BranchNumber = 1111, BranchAddress = "beit hadfous 65", BranchCity = "Jerusalem" } , CollectionClearance = false }
                                       ,HostingUnitName="yy hotel", Area = AreasInTheCountry.דרום,Adults=2,Children=1,Pool= true ,Jacuzzi=false ,Garden=false},
                                       new HostingUnit(){ HostingUnitKey=11111112,  Owner =new Host() { HostKey = 200000002, PrivateName = "ruthi", FamilyName = "cohen", PhoneNumber = 02-0000002, MailAddress = "ruthi@gmail.com",
                                       BankAccount = new BankBranch() { BankNumber = 222, BankName = "Igood", BranchNumber = 2222, BranchAddress =  "kanfey nesharim 3", BranchCity = "Jerusalem" } , CollectionClearance = true }
                                       ,HostingUnitName="ruthi houses" ,Area = AreasInTheCountry.צפון ,Adults=4 ,Children=3,Pool= true,ChildrensAttractions=false ,Jacuzzi=false,Garden=false},
                                       new HostingUnit(){ HostingUnitKey=11111113,  Owner =new Host() { HostKey = 200000003, PrivateName = "odelia", FamilyName = "melloul", PhoneNumber = 02-0000003, MailAddress = "odelia@gmail.com",
                                       BankAccount = new BankBranch() { BankNumber =333, BankName = "Igood", BranchNumber = 3333, BranchAddress =  "smilanski 14", BranchCity = "Netanya" } , CollectionClearance =false } ,
                                       HostingUnitName = "odelia's tsimer", Area = AreasInTheCountry.ירושלים,Adults=3,Pool= true,Jacuzzi=true,Garden=true }
        };

        public static List<Order> ordersList = new List<Order>() { new Order(){ HostingUnitKey=11111111,GuestRequestKey= 22222221,OrderKey =Configuration.orderKey++, Status= OrderStatus.נשלח_מייל,CreateDate= new DateTime(2019,12,01),OrderDate=new DateTime( 2019,12,12) },
                                                                  new Order(){ HostingUnitKey=11111113,GuestRequestKey= 22222223,OrderKey =Configuration.orderKey++,Status= OrderStatus.נשלח_מייל,CreateDate= new DateTime(2019,08,02),OrderDate=new DateTime(2019,09,02) }
       };

        public static List<GuestRequest> guestRequestsList = new List<GuestRequest>(){new GuestRequest(){GuestRequestKey= Configuration.guestRequestKey++,PrivateName="yaniv",FamilyName="ashkenazi",MailAddress="odeliamel04@gmail.com",Status=OrderStatus.נשלח_מייל, RegistrationDate= new DateTime(2019,06,11),
                                                                                     EntryDate= new DateTime(2019,10,11),ReleaseDate= new DateTime (2019,10,26),Area= AreasInTheCountry.ירושלים,SubArea=  All.הכל,
                                                                                      Type= TypesOfVacation.דירת_אירוח,Adults= 2,Children=1,Pool= ChoosingAttraction.הכרחי,Jacuzzi= ChoosingAttraction.הכרחי,
                                                                                     Garden= ChoosingAttraction.הכרחי,ChildrensAttractions= ChoosingAttraction.אפשרי},

                                                                                     new GuestRequest() {GuestRequestKey = Configuration.guestRequestKey++,PrivateName = "Sara",FamilyName = "cohen",MailAddress = "odeliamel04@gmail.com",Status = OrderStatus.לא_טופל, RegistrationDate = new DateTime(2019,10 ,30),
                                                                                     EntryDate = new DateTime(2019,11,06),ReleaseDate = new DateTime(2019 / 11 / 12),Area = AreasInTheCountry.הכל,SubArea =All.כרמיאל,
                                                                                     Type = TypesOfVacation.מלון,Adults = 2,Children = 1,Pool = ChoosingAttraction.לא_מעוניין,Jacuzzi = ChoosingAttraction.אפשרי,
                                                                                     Garden = ChoosingAttraction.לא_מעוניין,ChildrensAttractions = ChoosingAttraction.הכרחי },

                                                                                     new GuestRequest() {GuestRequestKey = Configuration.guestRequestKey++,PrivateName = "Haya",FamilyName = "aharonov",MailAddress = "odeliamel04@gmail.com",Status = OrderStatus.נשלח_מייל, RegistrationDate = new DateTime(2019,11,06),
                                                                                     EntryDate =  new DateTime(2019,03,22),ReleaseDate =new DateTime (2019,03,26),Area =  AreasInTheCountry.מרכז,SubArea =  All.נתניה,
                                                                                     Type = TypesOfVacation.קמפינג,Adults = 2,Children = 2,Pool =ChoosingAttraction.לא_מעוניין,Jacuzzi = ChoosingAttraction.לא_מעוניין,
                                                                                     Garden = ChoosingAttraction.הכרחי,ChildrensAttractions = ChoosingAttraction.הכרחי},


                                                                                     new GuestRequest() {GuestRequestKey = Configuration.guestRequestKey++,PrivateName = "maeva",FamilyName = "melloul",MailAddress = "odeliamel04@gmail.com",Status = OrderStatus.נסגרה_עסקה, RegistrationDate = new DateTime(2019,11,06),
                                                                                     EntryDate =  new DateTime(2019,03,22),ReleaseDate =new DateTime (2019,03,26),Area =  AreasInTheCountry.מרכז,SubArea =  All.נתניה,
                                                                                     Type = TypesOfVacation.קמפינג,Adults = 2,Children = 2,Pool =ChoosingAttraction.לא_מעוניין,Jacuzzi = ChoosingAttraction.לא_מעוניין,
                                                                                     Garden = ChoosingAttraction.הכרחי,ChildrensAttractions = ChoosingAttraction.אפשרי }

        };

        public static List<Host> hostList = new List<Host>()
        {                              {new Host() { HostKey = 200000001, PrivateName = "yael", FamilyName = "levi", PhoneNumber = 0585577022, MailAddress = "yael@gmail.com" ,
                                       BankAccount = new BankBranch() { BankNumber = 111, BankName = "Discont", BranchNumber = 1111, BranchAddress = "beit hadfous 65", BranchCity = "Jerusalem"} , CollectionClearance = false }},

                                       {new Host() { HostKey = 200000002, PrivateName = "ruthi", FamilyName = "cohen", PhoneNumber = 0554466221, MailAddress = "ruthi@gmail.com",
                                       BankAccount = new BankBranch() { BankNumber = 222, BankName = "Igood", BranchNumber = 2222, BranchAddress =  "kanfey nesharim 3", BranchCity = "Jerusalem"} , CollectionClearance = true } },

                                       {new Host() { HostKey = 200000003, PrivateName = "odelia", FamilyName = "melloul", PhoneNumber = 0585588096, MailAddress = "odelia@gmail.com",
                                       BankAccount = new BankBranch() { BankNumber =333, BankName = "Igood", BranchNumber = 3333, BranchAddress =  "smilanski 14", BranchCity = "Netanya"} , CollectionClearance =false } }

        };
        
    }
}



