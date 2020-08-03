using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
  public  class Order : IClonable
    {
        private int hostingUnitKey;
        public int HostingUnitKey { get => hostingUnitKey; set => hostingUnitKey = value; }
        private int guestRequestKey;
        public int GuestRequestKey { get => guestRequestKey; set => guestRequestKey = value; }
        private int orderKey;
        public int OrderKey { get => orderKey; set => orderKey = Configuration.orderKey++; }
        private OrderStatus status;
        public OrderStatus Status { get => status; set => status = value; }
        private DateTime createDate;
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        private DateTime orderDate;
        public  DateTime OrderDate { get => orderDate; set => orderDate = value; }


      
        public override string ToString()
        {
            string order = " ";
            return order += string.Format("KeHosting Unit Key: {0}\n" + "KeHosting Unit Key: {1}\n" + "Order Key: {2}\n" + "Status: {3}\n" + "Create Date: {4}\n" + "Order Date:{5}\n",
                HostingUnitKey, GuestRequestKey, OrderKey, Status, CreateDate, OrderDate);
        }

    }
}
