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
        // void Signed_bank_debit_authoization(GuestRequest x);
        bool AvailableDate(HostingUnit T, GuestRequest F);
        void BusyDate(Order O);
        void CheckStatus(Order O);
        int StatusDone(Order O);
        void StatusChange(Order o); 
        void SendMail(HostingUnit unit, GuestRequest guest);
        bool AvailableUnit(DateTime Entry, int sumDay);
        int CalculateDate(DateTime firstDate, DateTime secondDate);
        IEnumerable<IGrouping<AreasInTheCountry, GuestRequest>> VacationArea();
        IEnumerable<IGrouping<int, GuestRequest>> SumOfVacationers();
        IEnumerable<IGrouping<int, Host>> SumOfHostingunit();
        IEnumerable<IGrouping<AreasInTheCountry, HostingUnit>> RegionOfUnit();
        Host CheckId(int id);
        List<HostingUnit> FindHostingUnit(int id);
        IEnumerable< string> NameOfUnit(Host HO);
        IEnumerable<GuestRequest> Condition_Guest_Request1(Predicate<GuestRequest> conditions, IEnumerable<GuestRequest> g);
        IEnumerable<GuestRequest> Condition_Guest_Request2(Func< GuestRequest, bool> conditions, IEnumerable<GuestRequest> g);
       
       
        bool PoolT(GuestRequest g);
        bool PoolF(GuestRequest g);
        bool GardenT(GuestRequest g);
        bool GardenF(GuestRequest g);
        bool jakuzziT(GuestRequest g);
        bool jakuzziF(GuestRequest g);
        bool AttractionT(GuestRequest g);
        bool AttractionF(GuestRequest g);
        bool Breakfast(GuestRequest g);
        bool Lunch(GuestRequest g);
        bool Dinner(GuestRequest g);
        bool Type(GuestRequest g, TypesOfVacation a);
        bool SubArea(GuestRequest g, All a);

        //IDAL
        void AddGuestRequestB(GuestRequest T);
        void AddOrderB(Host H, HostingUnit unit, Order T);
        void AddHostingUnitB(HostingUnit T);
        void AddHostB(Host T);
        void RemoveHostingUnitB(HostingUnit H);
        void RemoveGuestRequestB(GuestRequest T);
        void UpdateGuestRequestB(GuestRequest T);
        void UpdateHostingUnitB(HostingUnit T);
        void OrderChangedB(Host H,Order T);
        IEnumerable<HostingUnit> Get_HostingUnitsListB();
        IEnumerable<GuestRequest> Get_GuestRequestListB();
        IEnumerable<Order> Get_OrdersB();
        IEnumerable<BankBranch> Brunch_bankB();

        // HostingUnit
        void AddhostingUnitImage(int id, string newImagePath,int i);
        List<string> AllMyImage( HostingUnit unit);
        HostingUnit Host_ToHostingUnit(Host h, string name);
        List<HostingUnit> Host_ToHostingUnits(Host h);
        IEnumerable<Order> UnitToOrder(HostingUnit H);
        string KeyToNameHu(int key);
        string KeyToNameGR(int key);
        void sumAdult(GuestRequest g, HostingUnit H);
        void sumChilds(GuestRequest g, HostingUnit H);

        //SiteOwner
        IEnumerable<GuestRequest> CloseGR();
        IEnumerable<GuestRequest> NoTreat();
        IEnumerable<GuestRequest> MailwasSent();
        IEnumerable<HostingUnit> AvailableUnitList(DateTime Entry, int sumDay);
        IEnumerable<HostingUnit> BusyUnitList(DateTime Entry, int sumDay);
        IEnumerable<Host> Get_Host();
        IEnumerable<Order> pagTokef();
        void Accumulation(int t);
        int Accumulation();
        int updatepasswords();
        IEnumerable<BankBranch> ListOfBanks(string searchString);

        //GuestRequest
        GuestRequest CheckGr(int id);
        Order GrToOrder(GuestRequest g);
        //order 
        IEnumerable<Order> HostToOrder(Host h);
        IEnumerable<GuestRequest> GetOrderWasNotTreated(IEnumerable<GuestRequest> g);
    }
}
