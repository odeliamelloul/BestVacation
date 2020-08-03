using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;



namespace DAL
{
       public interface IDAL
       {
        void AddGuestRequest(GuestRequest T);
     
        void AddOrder(Order T);
        void AddHostingUnit(HostingUnit T);
        void DeleteHostingUnit(HostingUnit T);
        void UpdateGuestRequest(GuestRequest T);
        void UpdateHostingUnit(HostingUnit T);
        void OrderChanged(Order T);
        IEnumerable<HostingUnit> Get_HostingUnitsList();
        IEnumerable<GuestRequest> Get_GuestRequestList();
        IEnumerable<Order> Get_Orders();
        IEnumerable<BankBranch> GetBrunch_bank();
        IEnumerable<Host> Get_HostList();
    }
}
