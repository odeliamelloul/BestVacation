using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {

            //    HostingUnit h1 = new HostingUnit()
            //    {

            //        Owner = new Host()
            //        {
            //            PrivateName = "yael",
            //            FamilyName = "levi",
            //            PhoneNumber = 020000001,
            //            MailAddress = "yael@gmail.com",
            //            BankAccount = new BankBranch() { BankNumber = 111, BankName = Bank_Name.Discont, BranchNumber = 1111, BranchAddress = "beit hadfous 65", BranchCity = "Jerusalem", BankAccountNumber = 123111 },
            //            CollectionClearance = false
            //        },
            //        HostingUnitName = "yael hotel",
            //        Area = AreasInTheCountry.ירושלים
            //    };
            //    HostingUnit h2 = new HostingUnit()
            //    {
            //        Owner = new Host()
            //        {
            //            PrivateName = "ruthi",
            //            FamilyName = "cohen",
            //            PhoneNumber = 02 - 0000055,
            //            MailAddress = "ruthi@gmail.com",
            //            BankAccount = new BankBranch() { BankNumber = 222, BankName = Bank_Name.Igood, BranchNumber = 2222, BranchAddress = "kanfey nesharim 3", BranchCity = "Jerusalem", BankAccountNumber = 123222 },
            //            CollectionClearance = false
            //        },
            //        HostingUnitName = "ruthi houses",
            //        Area = AreasInTheCountry.מרכז
            //    };
            //    HostingUnit h3 = new HostingUnit()
            //    {
            //        HostingUnitKey = 10000003,
            //        Owner = new Host()
            //        {
            //            HostKey = 200000003,
            //            PrivateName = "odelia",
            //            FamilyName = "melloul",
            //            PhoneNumber = 02 - 0000003,
            //            MailAddress = "odelia@gmail.com",
            //            BankAccount = new BankBranch() { BankNumber = 333, BankName = Bank_Name.Igood, BranchNumber = 3333, BranchAddress = "smilanski 14", BranchCity = "Netanya", BankAccountNumber = 123333 },
            //            CollectionClearance = true 
            //        },
            //        HostingUnitName = "odelia's tsimer",

            //        Area = AreasInTheCountry.ירושלים,
            //        Adults = 3,
            //        Pool = true,
            //        Jacuzzi = false,
            //        Garden = true

            //    };
            //    Order o1 = new Order()
            //    { HostingUnitKey = 10000001,
            //        GuestRequestKey = 10000001,
            //        OrderKey = Configuration.orderKey++,
            //        Status = OrderStatus.לא_טופל,
            //        CreateDate = new DateTime(2019, 12,01),
            //        OrderDate = new DateTime(2019, 12, 12) };
            //    Order o2 = new Order()
            //    {
            //        HostingUnitKey = 10000007,
            //        GuestRequestKey = 22222223,
            //        OrderKey = Configuration.orderKey++,
            //        Status = OrderStatus.נשלח_מייל,
            //        CreateDate = new DateTime(2019, 08, 02),
            //        OrderDate = new DateTime(2019, 09, 02)
            //    };
            //    GuestRequest g1 = new GuestRequest()
            //    {
            //        GuestRequestKey = Configuration.guestRequestKey++,
            //        PrivateName = "yaniv",
            //        FamilyName = "ashkenazi",
            //        MailAddress = "yanivA@gmail.com",
            //        Status = OrderStatus.נשלח_מייל,
            //        RegistrationDate = new DateTime(2019, 06, 11),
            //        EntryDate = new DateTime(2019, 10, 11),
            //        ReleaseDate = new DateTime(2019, 10, 26),
            //        Area = AreasInTheCountry.ירושלים,
            //        SubArea = All.אשקלון,
            //        Type = TypesOfVacation.מלון,
            //        Adults = 2,
            //        Children = 1,
            //        Pool = ChoosingAttraction.הכרחי,
            //        Jacuzzi = ChoosingAttraction.אפשרי,
            //        Garden = ChoosingAttraction.הכרחי,
            //        ChildrensAttractions = ChoosingAttraction.לא_מעוניין,
            //        Its_Signed = true,
            //        Breakfast = true,
            //        Dinner = false,
            //        Lunch = true
            //    };
            //    BL.IBL bl = BL.FactoryBL.GetBL();

            //    bl.AddHostingUnitB(h2);
            //    try
            //    {
            //    bl.UpdateHostingUnitB(h3);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }

            //    bl.RemoveHostingUnitB(h3);
            //    foreach (var item in bl.Get_HostingUnitsListB())
            //        Console.WriteLine(item.ToString());
            //    bl.AddGuestRequestB(g1);
            //    try
            //    {
            //     bl.AddOrderB(o1);
            //    }
            //    catch (Exception e )
            //    {
            //        Console.WriteLine(e.Message);
            //    }
            //    try
            //    {
            //     bl.OrderChangedB(o2);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }

            //    foreach (var item in bl.Get_OrdersB())
            //        Console.WriteLine(item.ToString());
            //    Console.ReadKey();
        }


    }




    //public static List<GuestRequest> guestRequestsList = new List<GuestRequest>(){new GuestRequest(){GuestRequestKey= Configuration.guestRequestKey,PrivateName="yaniv",FamilyName="ashkenazi",MailAddress="yanivA@gmail.com",Status=OrderStatus.mail_has_been_sent, RegistrationDate= new DateTime(11,06,2019),
    //                                                                             EntryDate= new DateTime(10/11/2019),ReleaseDate= new DateTime (03/26/2019),Area= AreasInTheCountry.Jerusalem,SubArea= "yafo",
    //                                                                              Type= TypesOfVacation.Zimmer,Adults= 2,Children=1,Pool= ChoosingAttraction.necessary,Jacuzzi= ChoosingAttraction.not_interested,
    //                                                                             Garden= ChoosingAttraction.necessary,ChildrensAttractions= ChoosingAttraction.possible, Its_Signed= true,IsApproved= true},

    //                                                                             new GuestRequest() {GuestRequestKey = Configuration.GuestRequestKey,PrivateName = "Sara",FamilyName = "cohen",MailAddress = "SaraC@gmail.com",Status = OrderStatus.Not_treated, RegistrationDate = new DateTime(10, 30, 2019),
    //                                                                             EntryDate = new DateTime(11 / 06 / 2019),ReleaseDate = new DateTime(03 / 26 / 2019),Area = AreasInTheCountry.North,SubArea = "givat shaul",
    //                                                                             Type = TypesOfVacation.Zimmer,Adults = 2,Children = 1,Pool = ChoosingAttraction.necessary,Jacuzzi = ChoosingAttraction.not_interested,
    //                                                                             Garden = ChoosingAttraction.necessary,ChildrensAttractions = ChoosingAttraction.possible,Its_Signed= true,IsApproved = true },

    //                                                                             new GuestRequest() {GuestRequestKey = Configuration.GuestRequestKey,PrivateName = "Haya",FamilyName = "aharonov",MailAddress = "haya770@gmail.com",Status = OrderStatus.mail_has_been_sent, RegistrationDate = new DateTime(06,11,2019),
    //                                                                             EntryDate =  new DateTime(03/22/2019),ReleaseDate =new DateTime (03/26/2019),Area =  AreasInTheCountry.North,SubArea = "Tveria",
    //                                                                             Type = TypesOfVacation.Camping,Adults = 2,Children = 2,Pool =ChoosingAttraction.not_interested,Jacuzzi = ChoosingAttraction.not_interested,
    //                                                                             Garden = ChoosingAttraction.necessary,ChildrensAttractions = ChoosingAttraction.possible,Its_Signed= false,IsApproved = true }
    //};
}

//כאשר נשלח לעדכון יחידה שאינה קיימת נזרקה שגיאה
//Unhandled Exception: System.Collections.Generic.KeyNotFoundException:  יחידת האירוח שאותה אנו רוצים לעדכן אינה קיימת
//   at DAL.Dal_imp.UpdateHostingUnit(HostingUnit T) in Z:\שנה ג'\c#\Project\BE\DAL\Dal_imp.cs:line 21
//   at BL.BL_imp.UpdateHostingUnitB(HostingUnit T) in Z:\שנה ג'\c#\Project\BE\BL\BL_imp.cs:line 339
//   at PL.Program.Main(String[] args) in Z:\שנה ג'\c#\Project\BE\PL\Program.cs:line 63

// הרצה של הוספה ועדכון של host unit 
//Hosting Unit Key:10000001
//Owner: Host Key: 200000001
//Name: yael levi
//Fhone Number: 20000001
//Mail: yael @gmail.com
// Bank Account:  Bank Number: 111
//Bank Name: Discont
//Branch Number: 1111
//Branch Address: beit hadfous 65
// Branch City: Jerusalem
//Bank Account Number:123111

//Collection Clearance:False

//Hosting Unit Name:yael hotel
//Hosting Unit Area:Jerusalem/nHosting Unit Adults:2
//Hosting Unit Children:1
//Hosting Unit Pool:necessary
//Hosting Unit Jacuzzi:possible
//Hosting UnitGarden:possible
//Hosting Unit Area:necessary

// Hosting Unit Key:10000002
//Owner: Host Key: 200000002
//Name: ruthi cohen
//Fhone Number: 0
//Mail: ruthi @gmail.com
// Bank Account:  Bank Number: 222
//Bank Name: Igood
//Branch Number: 2222
//Branch Address: kanfey nesharim 3
// Branch City: Jerusalem
//Bank Account Number:123222

//Collection Clearance:True

//Hosting Unit Name:ruthi houses
//Hosting Unit Area:Jerusalem/nHosting Unit Adults:4
//Hosting Unit Children:3
//Hosting Unit Pool:not_interested
//Hosting Unit Jacuzzi:possible
//Hosting UnitGarden:possible
//Hosting Unit Area:not_interested

// Hosting Unit Key:10000003
//Owner: Host Key: 200000003
//Name: odelia melloul
//Fhone Number: -1
//Mail: odelia @gmail.com
// Bank Account:  Bank Number: 333
//Bank Name: Igood
//Branch Number: 3333
//Branch Address: smilanski 14
// Branch City: Netanya
//Bank Account Number:123333

//Collection Clearance:False

//Hosting Unit Name:odelia's tsimer
//Hosting Unit Area:Center/nHosting Unit Adults:3
//Hosting Unit Children:0
//Hosting Unit Pool:necessary
//Hosting Unit Jacuzzi:possible
//Hosting UnitGarden:possible
//Hosting Unit Area:necessary

// Hosting Unit Key:10000004
//Owner: Host Key: 0
//Name: ruthi cohen
//Fhone Number: -53
//Mail: ruthi @gmail.com
// Bank Account:  Bank Number: 222
//Bank Name: Igood
//Branch Number: 2222
//Branch Address: kanfey nesharim 3
// Branch City: Jerusalem
//Bank Account Number:123222

//Collection Clearance:False

//Hosting Unit Name:ruthi houses
//Hosting Unit Area:Jerusalem/nHosting Unit Adults:0
//Hosting Unit Children:0
//Hosting Unit Pool:necessary
//Hosting Unit Jacuzzi:necessary
//Hosting UnitGarden:necessary
//Hosting Unit Area:necessary


//Unhandled Exception: System.Exception: לא ניתן למחוק יחידת אירוח כל עוד יש הצעה הקשורה אליה במצב פתוח
//   at BL.BL_imp.RemoveHostingUnitB(HostingUnit H) in Z:\שנה ג'\c#\Project\BE\BL\BL_imp.cs:line 130
//   at PL.Program.Main(String[] args) in Z:\שנה ג'\c#\Project\BE\PL\Program.cs:line 71


//Unhandled Exception: System.NullReferenceException: Object reference not set to an instance of an object.
//   at BL.BL_imp.AvailableDate(HostingUnit T, GuestRequest F) in Z:\שנה ג'\c#\Project\BE\BL\BL_imp.cs:line 51
//   at BL.BL_imp.AddOrderB(Order O) in Z:\שנה ג'\c#\Project\BE\BL\BL_imp.cs:line 323
//   at PL.Program.Main(String[] args) in Z:\שנה ג'\c#\Project\BE\PL\Program.cs:line 90