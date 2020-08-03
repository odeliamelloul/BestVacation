using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BE;

namespace DAL
{
    public static class Cloning
    {
        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit target = new HostingUnit();
            target.Adults = original.Adults;
            target.Area = original.Area;
            target.Children = original.Children;
            target.ChildrensAttractions = original.ChildrensAttractions;
            target.Diary= original.Diary;
            target.Garden = original.Garden;
            target.HostingUnitKey = original.HostingUnitKey;
            target.HostingUnitName = original.HostingUnitName;
            target.Jacuzzi = original.Jacuzzi;
            target.Owner= original.Owner;
            target.Pool= original.Pool;
            target.Breakfast = original.Breakfast;
            target.Dinner = original.Dinner;
            target.Lunch = original.Lunch;
            target.Room = original.Room;
            target.Type = original.Type;
            target.Adults = original.Adults;
            return target;
        }
        public static GuestRequest Clone(this GuestRequest original)
        {
            GuestRequest target = new GuestRequest();
            target.Adults = original.Adults;
            target.Area = original.Area;
            target.Children = original.Children;
            target.ID = original.ID;
            target.Room = original.Room;
            target.ChildrensAttractions = original.ChildrensAttractions;  
            target.Garden = original.Garden;
            target.Jacuzzi = original.Jacuzzi;
            target.Pool = original.Pool;
            target.FamilyName= original.FamilyName;
            target.GuestRequestKey = original.GuestRequestKey;
            target.Its_Signed= original.Its_Signed;
            target.MailAddress = original.MailAddress;
            target.PrivateName = original.PrivateName;
            target.RegistrationDate = original.RegistrationDate;
            target.ReleaseDate= original.ReleaseDate;
            target.Status= original.Status;
            target.SubArea = original.SubArea;
            target.Type = original.Type;
            target.Breakfast = original.Breakfast;
            target.Dinner = original.Dinner;
            target.Lunch = original.Lunch;

            return target;
        }
        public static Host Clone(this Host original)
        {
            Host target = new Host();
            target.BankAccount = original.BankAccount;
            target.CollectionClearance = original.CollectionClearance;
            target.FamilyName = original.FamilyName;
            target.FhoneNumber = original.FhoneNumber;
            target.HostKey= original.HostKey;
            target.MailAddress = original.MailAddress;
            target.PrivateName= original.PrivateName;
            return target;
        }
        //public static SiteOwner Clone(this SiteOwner original)
        //{
        //    SiteOwner target = new SiteOwner();
        //    target.Commission = original.Commission;

        //    return target;
        //}
        public static BankBranch Clone(this BankBranch original)
        {
            BankBranch target = new BankBranch();
            target.BankAccountNumber = original.BankAccountNumber;
            target.BankName = original.BankName;
            target.BankNumber= original.BankNumber;
            target.BranchAddress = original.BranchAddress;
            target.BranchCity = original.BranchCity;
            target.BranchNumber= original.BranchNumber;
            return target;
        }
        public static Order Clone(this Order original)
        {
            Order target = new Order();
            target.CreateDate= original.CreateDate;
            target.GuestRequestKey = original.GuestRequestKey;
            target.HostingUnitKey = original.HostingUnitKey;
            target.OrderDate= original.OrderDate;
            target.OrderKey = original.OrderKey;
            target.Status = original.Status;
            return target;
        }
    }
}
