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
        void AddHost(Host Ho);
        IEnumerable<HostingUnit> Get_HostingUnitsList();
        IEnumerable<GuestRequest> Get_GuestRequestList();
        IEnumerable<Order> Get_Orders();
        void DeleteGuestRequest(GuestRequest g);
        IEnumerable<Host> Get_HostList();
        void updateaccumulation(int co);
        int updatepasswords(int co);
        int Get_commission();

        int Get_passwords();

        int Get_accumulation();
        IEnumerable<BankBranch> GetBrunch_bank();
       
        IEnumerable<BankBranch> GetAllbanks(Func<BankBranch, bool> condition = null);
      
        // GuestRequest FindGR(int ID);///?????
        //T ReadXml<T>(string filename);

        //List<BankBranch> GetListBankAccount();
    }
}
