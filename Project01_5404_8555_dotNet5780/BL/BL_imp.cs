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


namespace BL
{
    public class BL_imp : IBL
    {
        public void CheckSumDay(GuestRequest guest)//בודק את מספר הימים של ההזמנה ותקינותם 
        {
            try
            {
                if ((guest.ReleaseDate - guest.EntryDate).Days < 1 && (guest.EntryDate - DateTime.Now).Days > 0)
                    throw new KeyNotFoundException("תאריכי הנופש שהזנת אינם תקינים");
            }
            catch (KeyNotFoundException a)
            {
                throw a;
            }
        }

        public void Signed_bank_debit_authoization(GuestRequest x)// הסטטוס של הזזמנת הלקוח תהיה לפי אם הוא חתם על הרשאת גישה עבור חשבון בנק
        {
            try
            {
                if (x.Its_Signed == true)
                    x.Status = OrderStatus.mail_has_been_sent;
                else
                {
                    throw new OverflowException("לא חתמתה על השראת גישה לחיוב חשבון הבנק");//איזה סוג של  זריקת חריגה צריך פה???
                }
            }
            catch (OverflowException a)
            {
                throw a;
            }
        }
        public bool AvailableDate(HostingUnit T, GuestRequest F)//ימים פנויים
        {
            DateTime Date = F.EntryDate;
            while (Date < F.ReleaseDate)
            {
                int day = Date.Day;
                int month = Date.Month;
                if (T.Diary[day, month] == true)
                    return false;
                 Date= Date.AddDays(1);
            }
            return true;
        }
        public void CheckStatus(Order O)
        {

            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<Order> order = dal.Get_Orders();
            var v = order.FirstOrDefault(o => O.OrderKey == o.OrderKey);
            try
            {
                if (v.Status == OrderStatus.Closes_out_of_customer_disrespect || v.Status == OrderStatus.Closes_with_customer_response)
                    throw new Exception("אינך יכול לשנות את סטטוס הזמנתך");// צריך למצוא זריקת חריגה נכונה
            }
            catch (Exception)
            {
                throw;
            }

        }
        public int StatusDone(Order O)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            GuestRequest v = dal.Get_GuestRequestList().FirstOrDefault(g => O.GuestRequestKey == g.GuestRequestKey);
            if (O.Status == OrderStatus.Closes_with_customer_response)
                if (v != null)
                    return ((v.ReleaseDate - v.EntryDate).Days * SiteOwner.Commission);
            return 0;
        }
        public void BusyDate(Order O)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            GuestRequest guestRequest = dal.Get_GuestRequestList().FirstOrDefault(g => O.GuestRequestKey == g.GuestRequestKey);
            HostingUnit hostingUnit = dal.Get_HostingUnitsList().FirstOrDefault(g => O.HostingUnitKey == g.HostingUnitKey);
            DateTime Date = guestRequest.EntryDate;
            while (Date < guestRequest.ReleaseDate)
            {
                int day = Date.Day;
                int month = Date.Month;
                hostingUnit.Diary[day, month] = true;
                Date.AddDays(1);
            }

        }
        public void StatusChange(Order O)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<GuestRequest> guestRequest = dal.Get_GuestRequestList();
            var gu = guestRequest.FirstOrDefault(g => O.GuestRequestKey == g.GuestRequestKey);
            gu.Status = OrderStatus.Closes_with_customer_response;//סטטוס ההזמנה נסגרה עסקה
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
                    item.Status = OrderStatus.Closes_with_customer_response;
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
        public List<HostingUnit> FindHostingUnit(int id)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> hostingUnits = dal.Get_HostingUnitsList();
            var v = from item in hostingUnits
                    where id == item.Owner.HostKey
                    select item;
            return v.ToList();
        }
        public void RemoveHostingUnitB(HostingUnit H)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<Order> order = dal.Get_Orders();
            var v = from item in order
                    where item.HostingUnitKey == H.HostingUnitKey && (item.Status == OrderStatus.mail_has_been_sent || item.Status == OrderStatus.Not_treated && item.HostingUnitKey == H.HostingUnitKey)
                    select item;
            try
            {
                if (v == null)
                    throw new OverflowException("לא ניתן למחוק יחידת אירוח כל עוד יש הצעה הקשורה אליה במצב פתוח");
                else dal.DeleteHostingUnit(H);
            }
            catch (OverflowException ex)
            {
                throw ex;
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
        }
        public void SendMail(Order O)
        {

            MailMessage mail = new MailMessage();
            // כתובת הנמען)ניתן להוסיף יותר מאחד(
            mail.To.Add("toEmailAddress");
            // הכתובת ממנה נשלח המייל
            mail.From = new MailAddress("fromEmailAddress");
            // נושא ההודע ה
            mail.Subject = "mailSubject";
            //( HTML תוכן ההודעה )נניח שתוכן ההודעה בפורמט
            mail.Body = "mailBody";
            // HTML הגדרה שתוכן ההודעה בפורמט
            mail.IsBodyHtml = true;
            // Smtp יצירת עצם מסוג
            SmtpClient smtp = new SmtpClient();
            // gmail הגדרת השרת של
            smtp.Host = "smtp.gmail.com";
            // gmail הגדרת פרטי הכניסה )שם משתמש וסיסמה( לחשבון ה
            smtp.Credentials = new System.Net.NetworkCredential("myGmailEmailAddress@gmail.com", "myGmailPassword");
            // SSL ע"פ דרישת השר, חובה לאפשר במקרה זה
            smtp.EnableSsl = true;
            //try
            //{
            //    שליחת ההודע ה
            //        smtp.Send(mail);
            //}
            //catch (KeyNotFoundException ex)
            //{
            //    טיפול בשגיאות ותפיסת חריגות
            //   txtMessage.Text = ex.ToString();
            //}
        }
        public bool AvailableUnit(DateTime Entry, int sumDay)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> hostingUnits = dal.Get_HostingUnitsList();
            DateTime Release = Entry.AddDays(sumDay);
            var v = from item in hostingUnits
                    where AvailableDate(item, new GuestRequest { EntryDate = Entry, ReleaseDate = Release })
                    select item;
            foreach (var item in v)
               if (v == null)
                    throw new OverflowException("כל יחידות האירוח תפוסות בתאריכים אילו");

            return true;
        }

        public int CalculateDate(DateTime firstDate, DateTime secondDate = default(DateTime))
        {
            if (secondDate == default(DateTime))
                return (DateTime.Now - firstDate).Days;
            return (secondDate - firstDate).Days;
        }
        public IEnumerable<Order> DateOfcreateOrder(int sumDay)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<Order> order = dal.Get_Orders();
            var v = from item in order
                    where (DateTime.Now - item.OrderDate).Days >= sumDay
                    select item.Clone();// מתי צריך לעשות clone
            return v;
        }
        public IEnumerable<GuestRequest> Condition_Guest_Request(Predicate<GuestRequest> conditions)//צריך לכתוב את הפונקציה טוב יותר
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<GuestRequest> match_condition = dal.Get_GuestRequestList();
            var applicable = from item in match_condition
                             where conditions(item)
                             select item;
            return applicable;
        }
        public int NumOfOrder(GuestRequest gu)//מספר ההזמנות שנשלחו ללקוח לפי דרישת לקוח
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<Order> order = dal.Get_Orders();
            var countOfOrder = (from item in order
                                where gu.GuestRequestKey == item.GuestRequestKey
                                select new { OKey = item.OrderKey }).Count();
            return countOfOrder;
        }
        public int NumOfOrderClosed(HostingUnit hu)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<Order> order = dal.Get_Orders();
            var countOfClosed = (from item in order
                                 let closed = item.Status == OrderStatus.Closes_with_customer_response
                                 where hu.HostingUnitKey == item.HostingUnitKey && closed == true
                                 select item).Count();
            return countOfClosed;
        }
        //grouping
        //-------------------------------------------------------------------------

        public IEnumerable<IGrouping<AreasInTheCountry, GuestRequest>> VacationArea()
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<GuestRequest> guestRequest = dal.Get_GuestRequestList();
            var Request_With_Same_Area = from item in guestRequest
                                         group item by item.Area into groupByArea
                                         select groupByArea;
            return Request_With_Same_Area;
        }
        public IEnumerable<IGrouping<int, GuestRequest>> SumOfVacationers()
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<GuestRequest> guestRequest = dal.Get_GuestRequestList();
            var sum_of_Vacationers = from item in guestRequest
                                     group item by (item.Adults + item.Children) into count_person
                                     select count_person;
            return sum_of_Vacationers;
        }
        public IEnumerable<IGrouping<int, Host>> SumOfHostingunit()//צריך לשנות את הפונקציה
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> hostingUnit = dal.Get_HostingUnitsList();
            var numUnit = from item in hostingUnit
                          group item by item.Owner into unit
                          group unit.Key by unit.Count() into count_unit
                          select count_unit;
            return numUnit;
        }
        public IEnumerable<IGrouping<AreasInTheCountry, HostingUnit>> RegionOfUnit()
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> hostingUnit = dal.Get_HostingUnitsList();
            var unit_area = from item in hostingUnit
                            group item by item.Area into Ua
                            select Ua;
            return unit_area;
        }
        public IEnumerable<string> NameOfUnit(Host HO)//מקבץ לי שמות יחדות אירוח לפי מספר זהות של המארח
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> hostingUnit = dal.Get_HostingUnitsList();
            var unit_Name = from item in hostingUnit
                            where item.Owner.HostKey == HO.HostKey
                            select item.HostingUnitName;
            foreach (var item in unit_Name)
            if (unit_Name.ToList() == null) 
            throw new KeyNotFoundException("אין יחידות אירוח למארח זה במערכת ");
                 return unit_Name.ToList();
        }
        public void CheackMail(string adress)
        {
            if ((adress.IndexOf("@") < 2) || (adress.IndexOf('.') < adress.IndexOf("@") + 3) || adress.IndexOf(" ") > 0)
                throw new KeyNotFoundException("המייל שהכנסת שגוי או שלא הכנסת מייל, נסה שוב!");
        }
        void CheackNumOfPeople(int sum)
        {
            try
            {
                if (sum < 1)
                    throw new FormatException("לא הזנת מספר תקין של אנשים");
            }
            catch (FormatException a)
            {
                throw a;
            }
        }


        //IDAL
        //-------------------------------------------------------------------------
        public void AddGuestRequestB(GuestRequest T)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            try
            {
                CheckSumDay(T);
                CheackMail(T.MailAddress);
                CheackNumOfPeople(T.Adults + T.Children);
                AvailableUnit(T.EntryDate, (T.ReleaseDate - T.EntryDate).Days);
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (FormatException a)
            {
                throw a;
            }
            catch (OverflowException e)
            {
                throw e;
            }
            dal.AddGuestRequest(T);
        }
        public void AddOrderB(Order O)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> hostingUnit = dal.Get_HostingUnitsList();
            var H = hostingUnit.FirstOrDefault(X => O.HostingUnitKey == X.HostingUnitKey);
            IEnumerable<GuestRequest> guestReques = dal.Get_GuestRequestList();
            var G = guestReques.First(Y => O.GuestRequestKey == Y.GuestRequestKey);
            if (H == null)
                throw new OverflowException("?????? לא תקינה");
            if (G == null)
                throw new OverflowException("דרישת האירוח לא תקינה");
            dal.AddOrder(O);
            if (!AvailableDate(H, G))
                throw new OverflowException("שלח בקשת אירוח");
        }

        public void AddHostingUnitB(HostingUnit T)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            dal.AddHostingUnit(T);
        }
        
        public void UpdateHostingUnitB(HostingUnit T)
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
        public void OrderChangedB(Order O)
        {
            try
            {
                CheckStatus(O);
            }
            catch (Exception)
            {

                throw;
            }

            if (O.Status == OrderStatus.Closes_with_customer_response)
            {
                try
                {
                    StatusChange(O);
                }
                catch (KeyNotFoundException ex)
                {
                    throw ex;
                }
                catch (Exception)
                {
                    throw;
                }
                StatusDone(O);
                BusyDate(O);
            }
            if (O.Status == OrderStatus.mail_has_been_sent)
                try
                {
                    SendMail(O);
                }
                catch (KeyNotFoundException ex)
                {
                    throw ex;
                }
        }
        public IEnumerable<HostingUnit> Get_HostingUnitsListB()
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            return dal.Get_HostingUnitsList();
        }
       
        public IEnumerable<GuestRequest> Get_GuestRequestListB()
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            return dal.Get_GuestRequestList();
        }
        public IEnumerable<Order> Get_OrdersB()
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            return dal.Get_Orders();
        }
        public IEnumerable<BankBranch> Brunch_bankB()
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            return dal.GetBrunch_bank();
        }
        //HostingUnit
        public Host CheckId(int id)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<Host> hostList = dal.Get_HostList();
            var H = hostList.FirstOrDefault(X => X.HostKey == id);
            if (H != null)
                return H;
            throw new KeyNotFoundException("הת.ז לא קיימת במערכת ");

        }
        public HostingUnit Host_ToHostingUnit(Host h,string name)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            IEnumerable<HostingUnit> HU = dal.Get_HostingUnitsList();
            var V = HU.FirstOrDefault(X=> X.HostingUnitName == name && X.Owner.HostKey == h.HostKey);
            return V;

        }
        //GuestRequest
        public void UpdateGuestRequestB(GuestRequest T)
        {
            DAL.IDAL dal = DAL.FactoryDal.GetDal();
            try
            {
                dal.UpdateGuestRequest(T);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }

        }
    }
}

