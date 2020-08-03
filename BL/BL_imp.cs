using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using BE;
using DAL;
using System.Windows;
using System.Net.Mail;
using System.IO;

namespace BL
{
    public class BL_imp : IBL
    {
        DAL.IDAL dal = DAL.FactoryDal.GetDal();

 
        #region bank
        public IEnumerable<BankBranch> Brunch_bankB()
        {
           
            return dal.GetBrunch_bank();
        }
        public IEnumerable<BankBranch> ListOfBanks(string searchString)
        {
            return dal.GetAllbanks(t => ((t.BankName.Contains(searchString) || t.BranchAddress.Contains(searchString) || t.BranchCity.Contains(searchString))));
        }
        //public void Signed_bank_debit_authoization(GuestRequest x)// הסטטוס של הזזמנת הלקוח תהיה לפי אם הוא חתם על הרשאת גישה עבור חשבון בנק
        //{
        //    try
        //    {
        //        if (x.Its_Signed == true)
        //            x.Status = OrderStatus.נשלח_מייל;
        //        else
        //        {
        //            throw new OverflowException("לא חתמתה על השראת גישה לחיוב חשבון הבנק");//איזה סוג של  זריקת חריגה צריך פה???
        //        }
        //    }
        //    catch (OverflowException a)
        //    {
        //        throw a;
        //    }
        //}
        #endregion

        #region GuestRequest
        public bool AvailableUnit(DateTime Entry, int sumDay)//בודק אם יש תאריכים פונים לדרישת לקוח
        {
            IEnumerable<HostingUnit> hostingUnits = dal.Get_HostingUnitsList();
            DateTime Release = Entry.AddDays(sumDay);
            var v = from item in hostingUnits
                    where AvailableDate(item, new GuestRequest { EntryDate = Entry, ReleaseDate = Release})
                    select item;
            foreach (var item in v)
                if (v.ToList() == null)
                    throw new OverflowException("כל יחידות האירוח תפוסות בתאריכים אילו");
            return true;
        }

        public int CalculateDate(DateTime firstDate, DateTime secondDate = default(DateTime))//הפונקציה מחזירה את מספר הימים שעברו
        {
            if (secondDate == default(DateTime))
                return (DateTime.Now - firstDate).Days;
            return (secondDate - firstDate).Days;
        }
        public int NumOfOrder(GuestRequest gu)//מספר ההזמנות שנשלחו ללקוח לפי דרישת לקוח
        {
            IEnumerable<Order> order = dal.Get_Orders();
            var countOfOrder = (from item in order
                                where gu.GuestRequestKey == item.GuestRequestKey
                                select new { OKey = item.OrderKey }).Count();
            return countOfOrder;
        }
        public IEnumerable<IGrouping<AreasInTheCountry, GuestRequest>> VacationArea()//קיבוץ לקוחות לפי תת אדור
        {
            IEnumerable<GuestRequest> guestRequest = dal.Get_GuestRequestList();
            var Request_With_Same_Area = from item in guestRequest
                                         group item by item.Area into groupByArea
                                         select groupByArea;
            return Request_With_Same_Area;
        }
        public IEnumerable<IGrouping<int, GuestRequest>> SumOfVacationers()//קיבוף לקוחות לפי מסר נופשים
        {
            IEnumerable<GuestRequest> guestRequest = dal.Get_GuestRequestList();
            var sum_of_Vacationers = from item in guestRequest
                                     group item by (item.Adults + item.Children) into count_person
                                     select count_person;
            return sum_of_Vacationers;
        }
        public void CheackMail(string adress)//בדקת את כתובת המייל
        {
            if ((adress.IndexOf("@") < 2) || (adress.IndexOf('.') < adress.IndexOf("@") + 3) || adress.IndexOf(" ") > 0)
                throw new KeyNotFoundException("המייל שהכנסת שגוי או שלא הכנסת מייל, נסה שוב!");
        }
        void CheackNumOfPeople(int sum)//בדק שמספר הלקוחות גדול מ1
        {
                if (sum < 1)
                    throw new FormatException("לא הזנת מספר תקין של אנשים");
        }
        public IEnumerable<GuestRequest> Get_GuestRequestListB()//מחזיר רשימת כל הלקוחות
        {
            
            return dal.Get_GuestRequestList();
        }
        public void UpdateGuestRequestB(GuestRequest T)//מעדכן מארח
        {
 
            CheckSumDay(T);
            dal.UpdateGuestRequest(T);
            
        }
        public void AddGuestRequestB(GuestRequest T)//הוספת יחידת לקוח
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            try
            {
                CheckSumDay(T);
                CheackMail(T.MailAddress);
                CheackNumOfPeople(T.Adults + T.Children);
                AvailableUnit(T.EntryDate, (T.ReleaseDate - T.EntryDate).Days);
                dal.AddGuestRequest(T);
            }
            catch (Exception b)
            {
                throw b;
            }
        }
        public string KeyToNameGR(int key)//מקבלת מפתח של דרישת לקוח ומחזירה את השם הלקוח
        {
            IEnumerable<GuestRequest> gr = dal.Get_GuestRequestList();
            var v = gr.FirstOrDefault(X => key == X.GuestRequestKey);
            return v.PrivateName += v.FamilyName;

        }
        #endregion

        #region Host
        public Host CheckId(int id)//מקבל ת"ז ומחזיר את המארח שלו
        {
            
            IEnumerable<Host> hostList = dal.Get_HostList();
            var H = hostList.FirstOrDefault(X => X.HostKey == id);
            if (H != null)
                return H;
            throw new KeyNotFoundException("הת.ז לא קיימת במערכת ");

        }
        public GuestRequest CheckGr(int id)//מקבל ת"ז ומחזיר את הלקוח שלו
        {

            IEnumerable<GuestRequest> GRList = dal.Get_GuestRequestList();

            var g = GRList.FirstOrDefault(X => X.ID == id);
            if (g.Status == OrderStatus.נסגר_מחוסר_הענות || g.Status == OrderStatus.נסגרה_עסקה)
                throw new Exception("מצטערים אין באפשרותכם לעדכן את הזמנתכם מכיוון שסטטוס ההזמנה כבר השת");
            if (g != null)
                return g;
            throw new KeyNotFoundException("הת.ז לא קיימת במערכת ");

           

        }

        public void AddHostB(Host T)//הוספת מארח
        {
            
            try
            {
                dal.AddHost(T);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public IEnumerable<Host> Get_Host()//מחזיר רשימת המארחים
        {
           
            IEnumerable<Host> host = dal.Get_HostList();
            var v = from item in host
                    select item;
            return v.ToList();
        }
        #endregion

        #region Order;
        public void BusyDate(Order O)//כאשר סטטוס ההזמנה משתנה בגלל סגירת עסקה – יש לסמן במטריצה את התאריכים
        {
            GuestRequest guestRequest = dal.Get_GuestRequestList().FirstOrDefault(g => O.GuestRequestKey == g.GuestRequestKey);
            HostingUnit hostingUnit = dal.Get_HostingUnitsList().FirstOrDefault(g => O.HostingUnitKey == g.HostingUnitKey);
            DateTime Date = guestRequest.EntryDate;
            while (Date < guestRequest.ReleaseDate)
            {
                int day = Date.Day;
                int month = Date.Month;
                hostingUnit.Diary[month, day] = true;
                Date = Date.AddDays(1);
            }
        }
        public int StatusDone(Order O)//כאשר סטטוס ההזמנה משתנה בגלל סגירת עסקה – יש לבצע חישוב עמלה 
        {
            
            GuestRequest v = dal.Get_GuestRequestList().FirstOrDefault(g => O.GuestRequestKey == g.GuestRequestKey);
            if (O.Status == OrderStatus.נסגרה_עסקה)
                if (v != null)
                    return ((v.ReleaseDate - v.EntryDate).Days *dal.Get_commission());
            return 0;
        }
        public void CheckStatus(Order O)//לאחר שסטטוס ההזמנה השתנה לסגירת עסקה – לא ניתן לשנות יותר את הסטטוס שלה.
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<Order> order = dal.Get_Orders();
            var v = order.First(o => O.OrderKey == o.OrderKey);
            if (v.Status == OrderStatus.נסגר_מחוסר_הענות || v.Status == OrderStatus.נסגרה_עסקה)
                throw new KeyNotFoundException("אינך יכול לשנות את סטטוס הזמנתך");// צריך למצוא זריקת חריגה נכונה
        }
        public IEnumerable<Order> DateOfcreateOrder(int sumDay)//בודקת אם פג התוקף של ההזמנה
        {
            
            IEnumerable<Order> order = dal.Get_Orders();
            var v = from item in order
                    where (DateTime.Now - item.OrderDate).Days >= sumDay
                    select item.Clone();
            return v.ToList();
        }
        public int NumOfOrderClosed(HostingUnit hu)//מספר ההזמנות שניסגרו
        {
            
            IEnumerable<Order> order = dal.Get_Orders();
            var countOfClosed = (from item in order
                                 let closed = item.Status == OrderStatus.נסגרה_עסקה
                                 where hu.HostingUnitKey == item.HostingUnitKey && closed == true
                                 select item).Count();
            return countOfClosed;
        }
        public IEnumerable<Order> Get_OrdersB()//מחזיר את רשימת כל ההזמנות
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            return dal.Get_Orders();
        }
        public bool AvailableDate(HostingUnit T, GuestRequest F)//ימים פנויים
        {
            DateTime Date = F.EntryDate;
            while (Date < F.ReleaseDate)
            {
                int day = Date.Day - 1;
                int month = Date.Month - 1;
                if (T.Diary[month, day] == true)
                    return false;
                Date = Date.AddDays(1);
            }
            return true;
        }
        public void CheckSumDay(GuestRequest guest)//בודק את מספר הימים של ההזמנה ותקינותם 
        {
            try
            {
                if ((guest.ReleaseDate - guest.EntryDate).Days < 1 || guest.EntryDate < DateTime.Now || guest.ReleaseDate < DateTime.Now
                    )
                    throw new KeyNotFoundException("תאריכי הנופש שהזנת אינם תקינים");
            }
            catch (KeyNotFoundException a)
            {
                throw a;
            }
        }
 
        public void SendMail(HostingUnit unit, GuestRequest g)//שליחת מייל
        {


            MailMessage mail = new MailMessage();
            mail.To.Add(g.MailAddress);
            mail.From = new MailAddress("odeliamel04@gmail.com");
            mail.Subject = "הצעה לחופשה ";

            mail.Body = "שלום וברכה "+  g.PrivateName +" "+ g.FamilyName +" "+ 
                "<br /> ראינו שהתענינת בחופשה ,אנחנו מצאנו חופשה שתואמת לדרישות שלך אם הינך מעוניין צור איתנו קשר במספר הטלפון" + "<br /> 0" + unit.PhoneNumber + 
                "<br /> תוכל להתרשם מהמלון בתמונות שצורפו לך"+
                "<br /> " + "ה" + unit.Type + " " + "בעל"+" "+ unit.NumOfStars + " " + " כוכבים";

            for (int i=1;i<unit.CntImg+1;i++)
            { 
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(@"..\..\..\HostigUnitImages\" + unit.HostingUnitKey + i + @".jpg");
                mail.Attachments.Add(attachment);
            }
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 25;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("vacationodeliaruthi@gmail.com", "308508rc");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
                throw new Exception("ההזמנה התבצעה בהצלחה ");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void AddOrderB(Host Ho, HostingUnit unit,Order O)//הוספת הזמנה
        {
           
            IEnumerable<HostingUnit> hostingUnit = dal.Get_HostingUnitsList();
            var H = hostingUnit.FirstOrDefault(X => O.HostingUnitKey == X.HostingUnitKey);
            IEnumerable<GuestRequest> guestReques = dal.Get_GuestRequestList();
            var G = guestReques.First(Y => O.GuestRequestKey == Y.GuestRequestKey);
            if (H == null)
                throw new OverflowException("דרישת הלקוח לא תקינה");
            if (G == null)
                throw new OverflowException("דרישת האירוח לא תקינה");

            try
            {

                AvailableDate(H, G);
                //ConditionHU(guestReques,H);
                SendMail(unit,G);
                O.Status = OrderStatus.נשלח_מייל;
                O.OrderDate = DateTime.Now;
                dal.AddOrder(O);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            throw new OverflowException("ההזמנה נשלחה בהצלחה");
        }

        public IEnumerable<GuestRequest> Condition_Guest_Request1(Predicate<GuestRequest> conditions, IEnumerable<GuestRequest> g)//מקבלת תנאי ובדקת את הלקוחות לפי התנאי
        {
            var applicable = from item in g
                             where conditions(item)
                             select item;
            foreach (var item in applicable)
                if (applicable.ToList() == null)
                    throw new KeyNotFoundException("לא קיימת דרישת לקוח מתאימה ליחידה זו ");
            return applicable.ToList();


        }
        public IEnumerable<GuestRequest> Condition_Guest_Request2(Func< GuestRequest, bool> conditions, IEnumerable<GuestRequest> g)//מקבלת תנאי ובדקת את הלקוחות לפי התנאי
        {
            var applicable = from item in g
                             where conditions(item)
                             select item;
            foreach (var item in applicable)
                if (applicable.ToList() == null)
                    throw new KeyNotFoundException("לא קיימת דרישת לקוח מתאימה ליחידה זו ");
            return applicable.ToList();

        }

        public bool PoolT(GuestRequest g)
        {
            return g.Pool == ChoosingAttraction.אפשרי || g.Pool == ChoosingAttraction.הכרחי ? true : false;
        }
        public bool PoolF(GuestRequest g)
        {
            return g.Pool == ChoosingAttraction.אפשרי || g.Pool == ChoosingAttraction.לא_מעוניין ? false : true;
        }
        public bool GardenT(GuestRequest g)
        {
            return g.Garden == ChoosingAttraction.אפשרי || g.Garden == ChoosingAttraction.הכרחי ? true : false;
        }
        public bool GardenF(GuestRequest g)
        {
            return g.Garden == ChoosingAttraction.אפשרי || g.Garden == ChoosingAttraction.לא_מעוניין ? false : true;
        }
        public bool jakuzziT(GuestRequest g)
        {
            return g.Jacuzzi == ChoosingAttraction.אפשרי || g.Jacuzzi == ChoosingAttraction.הכרחי ? true : false;
        }
        public bool jakuzziF(GuestRequest g)
        {
            return g.Jacuzzi == ChoosingAttraction.אפשרי || g.Jacuzzi == ChoosingAttraction.לא_מעוניין ? false : true;
        }
        public bool AttractionT(GuestRequest g)
        {
            return g.ChildrensAttractions == ChoosingAttraction.אפשרי || g.ChildrensAttractions == ChoosingAttraction.הכרחי ? true : false;
        }
        public bool AttractionF(GuestRequest g)
        {
            return g.ChildrensAttractions == ChoosingAttraction.אפשרי || g.ChildrensAttractions == ChoosingAttraction.לא_מעוניין ? false : true;
        }
        public bool Breakfast(GuestRequest g)
        {
            return g.Breakfast ? false : true;
        }
        public bool Lunch(GuestRequest g)
        {
            return g.Lunch ? false : true;
        }
        public bool Dinner(GuestRequest g)
        {
            return g.Dinner ? false : true;
        }
        public bool Type(GuestRequest g, TypesOfVacation a)
        {
            return g.Type == a ? true : false;
        }
        public bool SubArea(GuestRequest g, All a)
        {
            return g.SubArea == a ? true : false;
        }
        public void StatusChange(Order O)//שינוי סטטוס
        {
           
            IEnumerable<GuestRequest> guestRequest = dal.Get_GuestRequestList();
            var gu = guestRequest.FirstOrDefault(g => O.GuestRequestKey == g.GuestRequestKey);
            gu.Status = OrderStatus.נסגרה_עסקה;//סטטוס ההזמנה נסגרה עסקה
            try
            {
                dal.UpdateGuestRequest(gu);
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            IEnumerable<Order> order = dal.Get_Orders();
            foreach (var item in order)
            {
                if (gu.GuestRequestKey == item.GuestRequestKey)// צריך לבדוק שהלולואה לא אנסופית
                {
                    item.Status = OrderStatus.נסגרה_עסקה;
                    try
                    {
                        dal.OrderChanged(item);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        
        public void OrderChangedB(Host H,Order O)//עדכון הזמנה
        {
            try
            {
                if (O.Status == OrderStatus.נסגרה_עסקה)
                 StatusChange(O);
            }
            catch (Exception)
            {
                throw;
            }
            Accumulation(StatusDone(O));
            
            BusyDate(O);
           
            IEnumerable<GuestRequest> guestReques = dal.Get_GuestRequestList();
            var G = guestReques.FirstOrDefault(Y => O.GuestRequestKey == Y.GuestRequestKey);
            //if (O.Status == OrderStatus.נשלח_מייל)
            //    try
            //    {
            //        SendMail(H,,G);
            //    }
            //    catch (KeyNotFoundException ex)
                 
            //    {
            //        throw ex;
            //    }
             O.OrderDate = DateTime.Now;
        }
        #endregion

        #region siteOwner
        public IEnumerable<GuestRequest> CloseGR ()//שאילתת רשימת לקוחות שנסגרה עסקה
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<GuestRequest> guestReques = dal.Get_GuestRequestList();
            var V = from item in guestReques
                    where item.Status == OrderStatus.נסגרה_עסקה 
                    select item;
            return V.ToList();
        }
        public IEnumerable<GuestRequest> NoTreat()//שאילתת רשימת לקוחות שלא טופלו
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<GuestRequest> guestReques = dal.Get_GuestRequestList();
            var V = from item in guestReques
                    where item.Status == OrderStatus.לא_טופל 
                    select item;
            return V.ToList();
        }
        public IEnumerable<GuestRequest> MailwasSent()//שאילתת רשימת לקוחות שטופלו
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<GuestRequest> guestReques = dal.Get_GuestRequestList();
            var V = from item in guestReques
                    where item.Status == OrderStatus.נשלח_מייל 
                    select item;
            return V.ToList();
        }
        public IEnumerable<HostingUnit> AvailableUnitList(DateTime Entry, int sumDay)//מחזירה את כל רשימת יחידות אירוח פנויות
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> hostingUnits = dal.Get_HostingUnitsList();
            DateTime Release = Entry.AddDays(sumDay);
            var v = from item in hostingUnits
                    where AvailableDate(item, new GuestRequest { EntryDate = Entry, ReleaseDate = Release })
                    select item;
            return v.ToList();
        }
        public IEnumerable<HostingUnit> BusyUnitList(DateTime Entry, int sumDay)//בודק את הימים התפוסים
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> hostingUnits = dal.Get_HostingUnitsList();
            DateTime Release = Entry.AddDays(sumDay);
            var v = from item in hostingUnits
                    where (!AvailableDate(item, new GuestRequest { EntryDate = Entry, ReleaseDate = Release }))
                    select item;
            return v.ToList();
        }
        public IEnumerable<Order> pagTokef()
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();

            IEnumerable<Order> orders = dal.Get_Orders();//gets the list of orders

            var createResult = from item in orders //creates a list of all orders that need to be closed
                               where item.OrderDate.AddDays(Configuration.orderValidity) < DateTime.Now && item.Status == OrderStatus.נשלח_מייל
                               select item;

            return createResult;


        }
        public void Accumulation(int t)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            int acu = dal.Get_accumulation() + t;
            dal.updateaccumulation(acu);
        }
        public int Accumulation()
        {
            return dal.Get_accumulation();
        }

        public int updatepasswords()
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            return dal.updatepasswords(SiteOwner1.Passwords);

        }
        #endregion

        #region HostingUnit
        

        public void AddhostingUnitImage(int id, string newImagePath,int i)
        {
            string photoPath = @"..\..\..\HostigUnitImages\" + id +i+ @".jpg";
            (File.Create(photoPath)).Close();
            System.IO.File.Copy(newImagePath, photoPath, true);
             
        }
        public List<string> AllMyImage(HostingUnit unit)//לתקן
        {
            List<string> imgLst = new List<string>();

            for (int i = 1; i < unit.CntImg + 1; i++)
            {
               string photoPath=@"..\..\..\HostigUnitImages\" + unit.HostingUnitKey + i + @".jpg";
               imgLst.Add(photoPath);
            }
            return imgLst;
        }



        public List<HostingUnit> FindHostingUnit(int id)//מוצא מארח לפי תז
        {
            IEnumerable<HostingUnit> hostingUnits = dal.Get_HostingUnitsList();
            var v = from item in hostingUnits
                    where id == item.Owner.HostKey
                    select item;
            return v.ToList();
        }
        public void RemoveHostingUnitB(HostingUnit H)//לפני מחיקת יחידת אירוח  בודק אם אין הזמנות במצב פתוח
        {
            IEnumerable<Order> order = dal.Get_Orders();
            var v = from item in order
                    where item.HostingUnitKey == H.HostingUnitKey && (item.Status == OrderStatus.נשלח_מייל || item.Status == OrderStatus.לא_טופל && item.HostingUnitKey == H.HostingUnitKey)
                    select item;
            if (v.ToList() == null)
                throw new OverflowException("לא ניתן למחוק יחידת אירוח כל עוד יש הצעה הקשורה אליה במצב פתוח");
            else
            {
                try
                {
                    dal.DeleteHostingUnit(H);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public void RemoveGuestRequestB(GuestRequest T)
        {
            dal.DeleteGuestRequest(T);
        }
        public IEnumerable<IGrouping<int, Host>> SumOfHostingunit()//רשימת מארחים מקובצת  ע"פ מספר יחידות האירוח שהם מחזיקים
        {
            IEnumerable<HostingUnit> hostingUnit = dal.Get_HostingUnitsList();
            var numUnit = from item in hostingUnit
                          group item by item.Owner into unit
                          group unit.Key by unit.Count() into count_unit
                          select count_unit;
            return numUnit.ToList();
        }
        public IEnumerable<IGrouping<AreasInTheCountry, HostingUnit>> RegionOfUnit()//מקבף יחדות אירוח לפי תת אזור
        {
           
            IEnumerable<HostingUnit> hostingUnit = dal.Get_HostingUnitsList();
            var unit_area = from item in hostingUnit
                            group item by item.Area into Ua
                            select Ua;
            return unit_area.ToList();
        }
        public IEnumerable<string> NameOfUnit(Host HO)//מקבץ לי שמות יחדות אירוח לפי מספר זהות של המארח
        {
            IEnumerable<HostingUnit> hostingUnit = dal.Get_HostingUnitsList();
            var unit_Name = from item in hostingUnit
                            where item.Owner.HostKey == HO.HostKey
                            select item.HostingUnitName;
            foreach (var item in unit_Name)
                if (item.ToList() == null)
                    throw new KeyNotFoundException("אין יחידות אירוח למארח זה במערכת ");
            return unit_Name.ToList();
        }
        public void AddHostingUnitB(HostingUnit T)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            try
            {
                dal.AddHostingUnit(T);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void UpdateHostingUnitB(HostingUnit T)//מעדכן יחידת אירוח
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            try
            {
                dal.UpdateHostingUnit(T);
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
        }
        public IEnumerable<HostingUnit> Get_HostingUnitsListB()//מחזיר רשימת של יחידות אירוח
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            return dal.Get_HostingUnitsList();
        }
        public HostingUnit Host_ToHostingUnit(Host h, string name)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> HU = dal.Get_HostingUnitsList();
            var V = HU.First(X => X.HostingUnitName == name && X.Owner.HostKey == h.HostKey);
            return V;
        }
        public List<HostingUnit> Host_ToHostingUnits(Host h)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> HU = dal.Get_HostingUnitsList();
            var V = from item in HU
                    where item.Owner.HostKey == h.HostKey
                    select item;
            return V.ToList();
        }
        public IEnumerable<Order> UnitToOrder(HostingUnit H)//מקבלת את היחדת אירוח ומחזירה את ההזמנות ששיכות ליחדה זו 
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
                IEnumerable<Order> orders = dal.Get_Orders();
            return (from item in orders
                    where item.HostingUnitKey == H.HostingUnitKey
                    select item).ToList();
            
        }
        public string KeyToNameHu(int key)//מקבלת מפתח של יחידת אירוח ומחזירה את השם של היחידה
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> hu = dal.Get_HostingUnitsList();
            var v = hu.FirstOrDefault(X => key == X.HostingUnitKey);
            return v.HostingUnitName;

        }

        public void sumAdult(GuestRequest g,HostingUnit H)
        {
            if (H.Adults < g.Adults)
                throw new KeyNotFoundException("יחידת האירוח אינה תואמת לדרישת הלקוח");
        }
        public void sumChilds(GuestRequest g, HostingUnit H)
        {
            if (H.Children < g.Children)
                throw new KeyNotFoundException("יחידת האירוח אינה תואמת לדרישת הלקוח");
        }
        #endregion

        public Order GrToOrder(GuestRequest g)
        {
            IEnumerable<Order> o = dal.Get_Orders();
            IEnumerable<GuestRequest> gr = dal.Get_GuestRequestList();
            var v = (from item in gr
                     join item1 in o
                     on item.GuestRequestKey equals item1.GuestRequestKey
                     where item.GuestRequestKey == g.GuestRequestKey
                     select item1).FirstOrDefault();
            return v;             
        }
        public IEnumerable<Order> HostToOrder(Host h)
        {
            IEnumerable<Order> o = dal.Get_Orders();
            IEnumerable<HostingUnit> hu = dal.Get_HostingUnitsList();

            var v = from item in hu
                    where item.Owner.HostKey == h.HostKey
                    select UnitToOrder(item);
            return v.FirstOrDefault();
 
        }
        public IEnumerable<GuestRequest>GetOrderWasNotTreated(IEnumerable<GuestRequest> g)
        {
                    return( from item in g
                    where item.Status == OrderStatus.לא_טופל 
                    select item).ToList();

     
        }




    }

    }
    


