using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BL
{
   public interface IBL
    {
        void CheckSumDay(GuestRequest guest);
        void Signed_bank_debit_authoization(GuestRequest x);
        bool AvailableDate(HostingUnit T, GuestRequest F);
        void BusyDate(Order O);
        void CheckStatus(Order O);
        int StatusDone(Order O);
        void StatusChange(Order o); 
        void SendMail(Order O);
        bool AvailableUnit(DateTime Entry, int sumDay);
        int CalculateDate(DateTime firstDate, DateTime secondDate);
        IEnumerable<IGrouping<AreasInTheCountry, GuestRequest>> VacationArea();
        IEnumerable<IGrouping<int, GuestRequest>> SumOfVacationers();
        IEnumerable<IGrouping<int, Host>> SumOfHostingunit();
        IEnumerable<IGrouping<AreasInTheCountry, HostingUnit>> RegionOfUnit();
        Host CheckId(int id);
        List<HostingUnit> FindHostingUnit(int id);
       IEnumerable< string> NameOfUnit(Host HO);

        //IDAL
        void AddGuestRequestB(GuestRequest T);
        void AddOrderB(Order T);
        void AddHostingUnitB(HostingUnit T);
        void RemoveHostingUnitB(HostingUnit H);
        void UpdateGuestRequestB(GuestRequest T);
        void UpdateHostingUnitB(HostingUnit T);
        void OrderChangedB(Order T);
        IEnumerable<HostingUnit> Get_HostingUnitsListB();
        IEnumerable<GuestRequest> Get_GuestRequestListB();
        IEnumerable<Order> Get_OrdersB();
        IEnumerable<BankBranch> Brunch_bankB();
       // HostingUnit
        HostingUnit Host_ToHostingUnit(Host h, string name);
    }
}
