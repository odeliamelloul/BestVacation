using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BE
{
        public class GuestRequest :IClonable
        {

        private int guestRequestKey;
        public int GuestRequestKey { get => guestRequestKey; set => guestRequestKey = value; }
       

        private String privateName;
        public String PrivateName { get => privateName; set => privateName = value; }
        private String familyName;
        public String FamilyName { get => familyName; set => familyName = value; }

        private String mailAddress;
        public String MailAddress { get => mailAddress; set => mailAddress = value; }
        private int id;
        public int ID { get => id; set => id = value; }
        private OrderStatus status;
        public OrderStatus Status { get => status; set => status = value; }

        private DateTime registrationDate;
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        private DateTime entryDate;
        public DateTime EntryDate { get => entryDate; set => entryDate = value; }
        private DateTime releaseDate;
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        private AreasInTheCountry area;
        public AreasInTheCountry Area { get => area; set => area = value; }
        private All subArea;
        public All SubArea { get => subArea; set => subArea = value; }
        private TypesOfVacation type;
        public TypesOfVacation Type { get => type; set => type = value; }
        private int adults;
        public  int Adults { get => adults; set => adults = value; }
        private int children;
        public int Children { get => children; set => children = value; }
        private ChoosingAttraction childrensAttractions;
        public ChoosingAttraction ChildrensAttractions { get => childrensAttractions; set => childrensAttractions = value; }
        private int room;
        public int Room { get => room; set => room = value; }
        private ChoosingAttraction pool;
        public ChoosingAttraction Pool { get => pool; set => pool = value; }
        private ChoosingAttraction jacuzzi;
        public ChoosingAttraction Jacuzzi { get => jacuzzi; set => jacuzzi = value; }
        private ChoosingAttraction garden;
        public ChoosingAttraction Garden { get => garden; set => garden = value; }
        private bool breakfast;
    

        public bool Breakfast { get => breakfast; set => breakfast = value; }
        private bool lunch;

        public bool Lunch { get => lunch; set => lunch = value; }

        private bool dinner;

        public bool Dinner { get => dinner; set => dinner = value; }

        public override string ToString()
        {
            string GuestRequest = " ";
            return GuestRequest += string.Format("Guest Request Key: {0}\n"+ "Name: {1} {2}\n"+ "Mail:{3}\n" + "ID:{4}\n" + "status: {5}\n" 
                + "Registration Date:{6}\n"+ "Entry Date: {7}\n" + "Release Date:{8}\n" + "Area:{9}\n" + "Sub Area:{10}\n"+
                "Type:{11}\n" + "Adults:{12}\n" + "Children: {13}\n" + "Room: {14}\n" + "Pool:{15}\n" + "Jacuzzi: {16}\n" + "Garden{17}\n" + "Childrens Attractions: {18}\n"+  "Breakfast:{19}\n" + " Lunch:{20}\n" + " Dinner:{21}\n",  guestRequestKey,PrivateName,FamilyName, MailAddress,ID,Status,
                RegistrationDate, EntryDate, ReleaseDate, Area, SubArea , Type, Adults,Children,Room,Pool,Jacuzzi,Garden, ChildrensAttractions , Breakfast, Lunch,Dinner);

        }
    }


    
}
