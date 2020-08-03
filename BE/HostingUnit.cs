using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class HostingUnit : IClonable
    {
        private int hostingUnitKey;
        public int HostingUnitKey { get => hostingUnitKey; set => hostingUnitKey = value; }
        private int phoneNumber;
        public int PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        private int cntImg;
        public int CntImg { get => cntImg; set => cntImg = value; }
        private int numOfStars;
        public int NumOfStars { get => numOfStars; set => numOfStars = value; }
        private Host owner;
        public Host Owner { get => owner; set => owner = value; }
        private string hostingUnitName;
        public string HostingUnitName { get => hostingUnitName; set => hostingUnitName = value; }
        private All subArea;
        public All SubArea { get => subArea; set => subArea = value; }
        private AreasInTheCountry area;
        public AreasInTheCountry Area { get => area; set => area = value; }
      

        // tell the XmlSerializer to ignore this Property.
        [XmlIgnore]
        public bool[,] Diary =  new bool[12, 31];

        //optional. tell the XmlSerializer to name the Array Element as'Board'
        // instead of 'BoaredDto'
        [XmlArray("Diary")]
        public bool[] DiaryDto
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(12); } //5 is the number of roes in the matrix
        }
        //  private bool[,] diary = new bool[32, 13];
        //   public bool[,] Diary { get => diary;set => diary= value; }
        private int adults;
        public int Adults { get => adults; set => adults = value; }
        private int children;
        public int Children { get => children; set => children = value; }
        private bool pool;
        public bool Pool { get => pool; set => pool = value; }
        private bool jacuzzi;
        public bool Jacuzzi { get => jacuzzi; set => jacuzzi = value; }
        private bool garden;
        public bool Garden { get => garden; set => garden = value; }
        private bool childrensAttractions;
        public bool ChildrensAttractions { get => childrensAttractions; set => childrensAttractions = value; }
        private int room;
        public int Room { get => room; set => room = value; }
        private TypesOfVacation type;
        public TypesOfVacation Type { get => type; set => type = value; }
        private bool breakfast;
        public bool Breakfast { get => breakfast; set => breakfast = value; }
        private bool lunch;
        public bool Lunch { get => lunch; set => lunch = value; }
        private bool dinner;
        public bool Dinner { get => dinner; set => dinner = value; }

        public override string ToString()
        {
            string HostingUnit = " ";
            return HostingUnit += string.Format("Hosting Unit Key:{0}\n" + " -Owner:\n" + "{1}\n" + "Hosting Unit Name:{2}\n" + "Hosting Unit Area:{3}\n" + "Hosting Unit Adults:{4}\n" + "Hosting Unit Children:{5}\n" + "Hosting Unit Pool:{6}\n" +
                "Hosting Unit Jacuzzi:{7}\n" + "Hosting UnitGarden:{8}\n" + "Hosting Unit Area:{9}\n", HostingUnitKey, Owner, HostingUnitName, Area, Adults, children, pool, jacuzzi, Garden, ChildrensAttractions);
        }

    }
}


