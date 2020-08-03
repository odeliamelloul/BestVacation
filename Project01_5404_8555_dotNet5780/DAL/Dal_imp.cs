using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DS;


namespace DAL
{
    public class Dal_imp : IDAL
    {
        public void AddHostingUnit(HostingUnit T)
        {
            
            T.HostingUnitKey = Configuration.hostingUnitKey++;
            var v = from item in Get_HostingUnitsList()
                    where item.HostingUnitName == T.HostingUnitName && item.Owner.HostKey == T.Owner.HostKey
                    select item;
            foreach (var item in v)
            if (v != null)
                throw new KeyNotFoundException("שם יחידת האירוח כבר קיימת במערכת בחר שם אחר");

            T.Diary = new bool[32, 13];
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 13; j++)
                    T.Diary[i, j] =false;
            }
            DataSource.hostingUnitList.Add(T);
        }
        public void UpdateHostingUnit(HostingUnit T)
        {
            var v = DataSource.hostingUnitList.FirstOrDefault(X => X.HostingUnitKey == T.HostingUnitKey);
            if (v == null)
                throw new KeyNotFoundException(" יחידת האירוח שאותה  אנו רוצים לעדכן אינה קיימת");
            DataSource.hostingUnitList.Remove(v);
            DataSource.hostingUnitList.Add(T);
        }
        public void DeleteHostingUnit(HostingUnit T)
        {
                var v = from item in DataSource.hostingUnitList
                        where item.HostingUnitKey == T.HostingUnitKey
                        select item.Clone();
                if (v != null)
                    DataSource.hostingUnitList.Remove(T);
                else throw new KeyNotFoundException("יחידת האירוח למחיקה אינה קיימת");
        }
        public void AddOrder(Order T) 
        {
            T.OrderKey = Configuration.orderKey++;
            DataSource.ordersList.Add(T);
        }
        public void OrderChanged(Order T)
        {
            var v = DataSource.ordersList.FirstOrDefault(X => X.OrderKey == T.OrderKey);
            if (v == null)
                throw new KeyNotFoundException(" הזמנה שאותה  אנו רוצים לעדכן אינה קיימת");
            DataSource.ordersList.Remove(v);
            DataSource.ordersList.Add(T);
        }
        public void AddGuestRequest(GuestRequest T)
        {
            T.GuestRequestKey = Configuration.guestRequestKey++;
            DataSource.guestRequestsList.Add(T);
        }
        public void UpdateGuestRequest(GuestRequest T)
        {
            var v = DataSource.guestRequestsList.FirstOrDefault(X => X.GuestRequestKey == T.GuestRequestKey);
            if (v == null)
                throw new KeyNotFoundException(" יחידת האירוח שאותה  אנו רוצים לעדכן אינה קיימת");
            DataSource.guestRequestsList.Remove(v);
          DataSource.guestRequestsList.Add(T);
        }
        public IEnumerable<HostingUnit> Get_HostingUnitsList()
        {
            return from item in DataSource.hostingUnitList
                   select item.Clone();
        }
        public IEnumerable<GuestRequest> Get_GuestRequestList()
        {
            return from item in DataSource.guestRequestsList
                   select item.Clone();
        }
        public IEnumerable<Order> Get_Orders()
         {
            return from item in DataSource.ordersList
                   select item.Clone();
        }
        public IEnumerable<BankBranch> GetBrunch_bank()
        {
            return from item in DataSource.hostingUnitList
                   select item.Owner.BankAccount;
        }
        public IEnumerable<Host> Get_HostList()
        {
            return from item in DataSource.hostList
                   select item.Clone();
        }

    }
}
