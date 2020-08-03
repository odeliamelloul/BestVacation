using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using System.IO;
using System.Reflection;
using System.Net;

using System.ComponentModel;
using System.Xml;

namespace DAL
{
    public class Dal_XML_imp : IDAL
    {
        XElement GuestRequestRoot;
        string guestRequestPath = @"..\..\..\GuestRequestXml.xml";

        XElement HostingUnitRoot;
        string hostingUnitPath = @"..\..\..\HostingUnitXml.xml";

        XElement OrderRoot;
        string orderPath = @"..\..\..\OrderXml.xml";
        XElement HostRoot;
        string hostPath = @"..\..\..\HostXml.xml";

        XElement ConfigRoot;
        string configPath = @"..\..\..\ConfigXml.xml";

        //XElement BankRoot;

        public static volatile bool bankDownloaded = false;//flag if bank was downloaded
        BackgroundWorker worker;

        XElement SiteOwner1Root;
        string siteOwner1Path = @"..\..\..\SiteOwner1Xml.xml";
        public Dal_XML_imp()
        {
            if (!File.Exists(guestRequestPath))
            {
                List<GuestRequest> guestRequestList = new List<GuestRequest>();//empty list for start
                SaveToXML<List<GuestRequest>>(guestRequestList, guestRequestPath);
                //GuestRequestRoot = new XElement("GuestRequest");
                //GuestRequestRoot.Save(guestRequestPath);
            }
            else
                LoadGuestRequests();
            if (!File.Exists(hostingUnitPath))
            {
                List<HostingUnit> HostingUnitList = new List<HostingUnit>();//empty list for start
                SaveToXML<List<HostingUnit>>(HostingUnitList, hostingUnitPath);
                // HostingUnitRoot = new XElement("HostingUnit");
                //HostingUnitRoot.Save(hostingUnitPath);
            }
            else
                LoadHostingUnit();
            if (!File.Exists(orderPath))
            {
                List<Order> OrdertList = new List<Order>();//empty list for start
                SaveToXML<List<Order>>(OrdertList, orderPath);
                //OrderRoot = new XElement("Order");
                //  OrderRoot.Save(orderPath);
            }
            else
                LoadOrder();
            if (!File.Exists(hostPath))
            {
                List<Host> HostList = new List<Host>();//empty list for start
                SaveToXML<List<Host>>(HostList, hostPath);

            }
            else
                LoadHost();
            if (!File.Exists(configPath))
            {
                CreateConfigFile();
            }
            else
                Loadconfig();

            if (!File.Exists(siteOwner1Path))
            {
                CreateSiteOwner1();
            }
            else
                LoadSiteOwner();
            try
            {
                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerAsync();
            }
            catch(FileLoadException a)
            {

                throw a;
            }


        }
        public static void SaveToXML<T>(T source, string path)//save objects like elements from program to  file
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }

        public static T LoadFromXML<T>(string path)//save elements like objects from file to program 
        {

            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }
        public void LoadGuestRequests()
        {
            try
            {
                GuestRequestRoot = XElement.Load(guestRequestPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        public void LoadHostingUnit()
        {
            try
            {
                HostingUnitRoot = XElement.Load(hostingUnitPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        public void LoadOrder()
        {
            try
            {
                OrderRoot = XElement.Load(orderPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        public void LoadHost()
        {
            try
            {
                HostRoot = XElement.Load(hostPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        public void Loadconfig()
        {
            try
            {
                ConfigRoot = XElement.Load(configPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        public void LoadSiteOwner()
        {

            try
            {
                SiteOwner1Root = XElement.Load(siteOwner1Path);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        #region GuestRequest
        public void AddGuestRequest(GuestRequest g)
        {
            // if (CheckGR(g.GuestRequestKey)==null)//בודק אם המפתח כבר קיים
            //     throw new Exception("היחידת אירוח קיימת במערכת");
            int tmp = int.Parse(ConfigRoot.Element("guestRequestKey").Value) + 1;
            g.GuestRequestKey = tmp;
            ConfigRoot.Element("guestRequestKey").Value = tmp.ToString();
            List<GuestRequest> guestRequestList = LoadFromXML<List<GuestRequest>>(guestRequestPath);
            guestRequestList.Add(g);
            ConfigRoot.Save(configPath);
            SaveToXML<List<GuestRequest>>(guestRequestList, guestRequestPath);//שומר בקובץ
        }
        public void UpdateGuestRequest(GuestRequest g)
        { 
            List<GuestRequest> guestRequestList = LoadFromXML<List<GuestRequest>>(guestRequestPath);
            int index = guestRequestList.FindIndex(p => p.GuestRequestKey == g.GuestRequestKey);
            if (index == -1)//didnt find
                throw new Exception("לקוח לא נמצא במערכת");
            guestRequestList[index] = g;//update mother in her place 
            SaveToXML<List<GuestRequest>>(guestRequestList, guestRequestPath);//save in file
        }
        public IEnumerable<GuestRequest> Get_GuestRequestList()
        {
            return LoadFromXML<List<GuestRequest>>(guestRequestPath).AsEnumerable();
        }
        public GuestRequest CheckGR(int checkId)//An auxiliary function that brings and passes the Testers list
        {
            return LoadFromXML<List<GuestRequest>>(guestRequestPath).FirstOrDefault(p => p.GuestRequestKey == checkId);
        }
        #endregion

        #region HostingUnit

        public void AddHostingUnit(HostingUnit h)
        {
            if (Checkhu(h.HostingUnitKey) != null)//בודק אם המפתח כבר קיים
                throw new Exception("יחידת האירוח קיימת במערכת");
            for (int j = 0; j < 12; j++)
            {
                {
                    for (int i = 0; i < 31; i++)
                        h.Diary[j, i] = false;
                }
            }

            int tmp = int.Parse(ConfigRoot.Element("hostingUnitKey").Value) + 1;
            h.HostingUnitKey = tmp;
            ConfigRoot.Element("hostingUnitKey").Value = tmp.ToString();
            List<HostingUnit> hostingUnittList = LoadFromXML<List<HostingUnit>>(hostingUnitPath);
            hostingUnittList.Add(h);
            ConfigRoot.Save(configPath);
            SaveToXML<List<HostingUnit>>(hostingUnittList, hostingUnitPath);//שומר בקובץ
        }

        public void UpdateHostingUnit(HostingUnit h)//פונקציה שמעדכנת את יחידות האירוח
        {
            List<HostingUnit> hostingUnitList = LoadFromXML<List<HostingUnit>>(hostingUnitPath);
            int index = hostingUnitList.FindIndex(p => p.HostingUnitKey == h.HostingUnitKey);
            if (index == -1)// didnt find
                       throw new Exception("יחידת האירוח לא נמצאת במערכת");

            hostingUnitList[index] = h;//update mother in her place 
           
            SaveToXML<List<HostingUnit>>(hostingUnitList, hostingUnitPath);//save in file
        }
        public IEnumerable<HostingUnit> Get_HostingUnitsList()
        {
            return LoadFromXML<List<HostingUnit>>(hostingUnitPath).AsEnumerable();
        }
        public HostingUnit Checkhu(int checkId)
        {
            return LoadFromXML<List<HostingUnit>>(hostingUnitPath).FirstOrDefault(p => p.HostingUnitKey == checkId);
        }

        public void DeleteHostingUnit(HostingUnit T)
        {
            XElement hostingUnitElement;
            try
            {
                hostingUnitElement = (from item in HostingUnitRoot.Elements()
                                      where int.Parse(item.Element("HostingUnitKey").Value) == T.HostingUnitKey
                                      select item).FirstOrDefault();
                hostingUnitElement.Remove();
                HostingUnitRoot.Save(hostingUnitPath);
            }
            catch
            {
                throw new KeyNotFoundException("היחידת אירוח אינה קיימת במערכת");
            }

        }

        public void DeleteGuestRequest(GuestRequest g)
        {
            XElement GuestRequestElement;
            try
            {
                GuestRequestElement = (from item in GuestRequestRoot.Elements()
                                      where int.Parse(item.Element("GuestRequestKey").Value) == g.GuestRequestKey
                                      select item).First();
                
                GuestRequestElement.Remove();
                GuestRequestRoot.Save(guestRequestPath);
            }
            catch
            {
                throw new KeyNotFoundException(" הלקוח אינו קיים במערכת");
            }


        }

        #endregion

        #region Host
        public void AddHost(Host Ho)
        {
            if (CheckHost(Ho.HostKey))//בודק אם המפתח כבר קיים
                throw new Exception("המארח כבר קיים במערכת");
            List<Host> hostList = LoadFromXML<List<Host>>(hostPath);
            var v = hostList.FirstOrDefault(X => X.HostKey == Ho.HostKey);

            hostList.Add(Ho);
            SaveToXML<List<Host>>(hostList, hostPath);//שומר בקובץ
        }
        public bool CheckHost(int checkId)//An auxiliary function that brings and passes the host list
        {
            return LoadFromXML<List<Host>>(hostPath).FirstOrDefault(p => p.HostKey == checkId) == null ? false : true;
        }
        #endregion

        #region Order
        public void AddOrder(Order O)
        {
            if (CheckO(O.OrderKey) != null)//בודק אם המפתח כבר קיים
                throw new Exception("ההזמנה קיימת במערכת");
            int tmp = int.Parse(ConfigRoot.Element("orderKey").Value) + 1;
            O.OrderKey = tmp;
            ConfigRoot.Element("orderKey").Value = tmp.ToString();
            List<Order> orderList = LoadFromXML<List<Order>>(orderPath);
            orderList.Add(O);
            ConfigRoot.Save(configPath);
            SaveToXML<List<Order>>(orderList, orderPath);//שומר בקובץ
        }
        public void OrderChanged(Order o)//פונקציה שמעדכנת את ההזמנות
        {
            List<Order> orderList = LoadFromXML<List<Order>>(orderPath);
            int index = orderList.FindIndex(p => p.OrderKey == o.OrderKey);
            if (index == -1)//didnt find
                throw new Exception("ההזמנה לא נמצאת במערכת");
            orderList[index] = o;//update mother in her place 
            SaveToXML<List<Order>>(orderList, orderPath);//save in file
        }


        public IEnumerable<Order> Get_Orders()
        {
            return LoadFromXML<List<Order>>(orderPath).AsEnumerable();
        }
        public Order CheckO(int checkId)//An auxiliary function that brings and passes the ordedr list
        {
            return LoadFromXML<List<Order>>(orderPath).FirstOrDefault(p => p.OrderKey == checkId);
        }
        public IEnumerable<Host> Get_HostList()
        {
            return LoadFromXML<List<Host>>(hostPath).AsEnumerable();
        }

        public void DeleteOrder(Order T)
        {
            XElement orderElrment;
            try
            {
                orderElrment = (from item in OrderRoot.Elements()
                                where int.Parse(item.Element("orderKey").Value) == T.OrderKey
                                select item).FirstOrDefault();
                orderElrment.Remove();
                OrderRoot.Save(orderPath);
            }
            catch
            {
                throw new KeyNotFoundException("היחידת אירוח אינה קיימת במערכת");
            }
        }
        #endregion
        #region config
        void CreateConfigFile()
        {
            try
            {
                ConfigRoot = new XElement("Configuration");


                //XElement passwords = new XElement("passwords", 100000000);
                XElement hostingUnitKey = new XElement("hostingUnitKey", 100000000);
                XElement guestRequestKey = new XElement("guestRequestKey", 100000000);
                XElement orderKey = new XElement("orderKey", 100000000);
                ConfigRoot.Add(hostingUnitKey, guestRequestKey, orderKey);

                ConfigRoot.Save(configPath);
            }
            catch
            {
                throw new FileLoadException("Cannot start the project check your Config Xml files");
            }
        }

        #endregion

      
        #region SiteOwner1
        void CreateSiteOwner1()
        {
            try
            {
                SiteOwner1Root = new XElement("SiteOwner1");
                XElement passwords = new XElement("passwords", 100000000);
                XElement commission = new XElement("commission", 10);
                XElement accumulation = new XElement("accumulation", 0);
                //XElement cntImg = new XElement("cntImg", 0);
                SiteOwner1Root.Add(passwords, commission, accumulation);
                SiteOwner1Root.Save(siteOwner1Path);
            }
            catch
            {
                throw new FileLoadException("Cannot start the project check your Config Xml files");
            }

        }
        public void updateaccumulation(int co)
        {
            SiteOwner1Root.Element("accumulation").Value = co.ToString();
            SiteOwner1Root.Save(siteOwner1Path);
        }
        public int updatepasswords(int co)
        {
            SiteOwner1Root.Element("passwords").Value = co.ToString();
            SiteOwner1Root.Save(siteOwner1Path);
            return int.Parse(SiteOwner1Root.Element("passwords").Value);
        }
        public int Get_commission()
        {
            return int.Parse(SiteOwner1Root.Element("commission").Value);
        }
        public int Get_passwords()
        {
            return int.Parse(SiteOwner1Root.Element("passwords").Value);
        }
        public int Get_accumulation()
        {
            return int.Parse(SiteOwner1Root.Element("accumulation").Value);
        }
        //public int Get_CntImg()
        //{
        //    return int.Parse(SiteOwner1Root.Element("cntImg").Value);
        //}
        #endregion


        public IEnumerable<BankBranch> GetAllbanks(Func<BankBranch, bool> condition = null)
        {
            try
            {
                List<BankBranch> banks = GetBrunch_bank().ToList();
                List<BankBranch> tmp = (from bank in banks
                                        where condition(bank)
                                        select bank).ToList();
                return tmp;
            }
            catch (FileLoadException a)
            {
                throw a;
            }
        }

        public IEnumerable<BankBranch> GetBrunch_bank()
        {


            List<BankBranch> banks = new List<BankBranch>();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"atm.xml");
            XmlNode rootNode = doc.DocumentElement;

            XmlNodeList children = rootNode.ChildNodes;
            foreach (XmlNode child in children)
            {
                BankBranch b = GetBranchByXmlNode(child);
                if (b != null)
                {
                    banks.Add(b);
                }
            }

            return banks;
        }


        private static BankBranch GetBranchByXmlNode(XmlNode node)
        {
            if (node.Name != "BRANCH") return null;
            BankBranch branch = new BankBranch();


            XmlNodeList children = node.ChildNodes;

            foreach (XmlNode child in children)
            {
                switch (child.Name)
                {
                    case "Bank_Code":
                        branch.BankNumber = int.Parse(child.InnerText);
                        break;
                    case "Bank_Name":
                        branch.BankName = child.InnerText;
                        break;
                    case "Branch_Code":
                        branch.BranchNumber = int.Parse(child.InnerText);
                        break;
                    case "Address":
                        branch.BranchAddress = child.InnerText;
                        break;
                    case "City":
                        branch.BranchCity = child.InnerText;
                        break;

                }

            }

            if (branch.BranchNumber > 0)
                return branch;

            return null;

        }


        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

            object ob = e.Argument;
            while (bankDownloaded == false)//continues until it downloads
            {
                try
                {
                    DownloadBank();
                    Thread.Sleep(2000);//sleeps before trying
                }
                catch
                {

                }
            }

            GetBrunch_bank();//saves branches to ds
        }
        void DownloadBank()
        {
            
            string xmlLocalPath = @"atm.xml";
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath =
               @"https://www.boi.org.il/en/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/snifim_en.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
                bankDownloaded = true;
            }
            catch
            {

                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
                bankDownloaded = true;

            }
            finally
            {
                wc.Dispose();
            }
          }
            //#region GuestRequest
            // //ADD
            // public void AddGuestRequest(GuestRequest g)
            // {
            //     List<GuestRequest> GuestRequestList = new List<GuestRequest>();
            //     if (!CheckGR(g.GuestRequestKey))
            //     {
            //         g.GuestRequestKey = Configuration.guestRequestKey++;
            //         SavaGRToXElement(g);
            //     }
            // }

            // public void SavaGRToXElement( GuestRequest p)
            // {

            //     XElement GuestRequestKey = new XElement("GuestRequestKey", p.GuestRequestKey);
            //     XElement PrivateName = new XElement("PrivateName", p.PrivateName);
            //     XElement FamilyName = new XElement("FamilyName", p.FamilyName);
            //     XElement MailAddress = new XElement("MailAddress", p.MailAddress);
            //     XElement ID = new XElement("ID", p.ID);
            //     XElement Status = new XElement("Status", p.Status.ToString());
            //     XElement RegistrationDate = new XElement("RegistrationDate", p.RegistrationDate.ToString());
            //     XElement EntryDate = new XElement("EntryDate", p.EntryDate.ToString());
            //     XElement ReleaseDate = new XElement("ReleaseDate", p.ReleaseDate.ToString());
            //     XElement Area = new XElement("Area", p.Area.ToString());
            //     XElement SubArea = new XElement("SubArea", p.SubArea.ToString());
            //     XElement Type = new XElement("Type", p.Type.ToString());
            //     XElement Adults = new XElement("Adults", p.Adults);
            //     XElement Children = new XElement("Children", p.Children);
            //     XElement ChildrensAttractions = new XElement("ChildrensAttractions", p.ChildrensAttractions.ToString());
            //     XElement Room = new XElement("Room", p.Room);
            //     XElement Pool = new XElement("Pool", p.Pool.ToString());
            //     XElement Jacuzzi = new XElement("Jacuzzi", p.Jacuzzi.ToString());
            //     XElement Garden = new XElement("Garden", p.Garden.ToString());
            //     XElement Breakfast = new XElement("Breakfast", p.Breakfast);
            //     XElement Lunch = new XElement("Lunch", p.Lunch);
            //     XElement Dinner = new XElement("Dinner", p.Dinner);
            //     XElement Its_Signed = new XElement("Its_Signed", p.Its_Signed);
            //     XElement guestRequest = new XElement("guestRequest", GuestRequestKey, PrivateName, FamilyName, MailAddress, ID,
            //     Status, RegistrationDate, EntryDate, Area, SubArea, Type, Adults, Children, ChildrensAttractions,
            //     Room, Pool, Jacuzzi, Garden, Breakfast, Lunch, Dinner, Its_Signed);
            //     GuestRequestRoot.Add(guestRequest);
            //     GuestRequestRoot.Save(guestRequestPath);
            // }
            // public void LoadGuestRequests()
            // {
            //     try {
            //         GuestRequestRoot = XElement.Load(guestRequestPath);
            //         }
            //     catch
            //        {
            //         throw new Exception("File upload problem");
            //        }
            // }
            // //Update
            // public GuestRequest GetGRFromXElementT(int id)
            // {
            //     LoadGuestRequests();
            //     GuestRequest guestRequest;
            //     try
            //     {
            //         guestRequest = (from item in GuestRequestRoot.Elements()
            //                         where int.Parse(item.Element("GuestRequestKey").Value) == id
            //                         select new GuestRequest()
            //                         {
            //                             GuestRequestKey = Convert.ToInt32(item.Element("GuestRequestKey").Value),
            //                             PrivateName = item.Element("Name").Element("PrivateName").Value,
            //                             FamilyName = item.Element("Name").Element("FamilyName").Value,
            //                             MailAddress = item.Element("MailAddress").Value,
            //                             Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), item.Element("Status").Value),
            //                             RegistrationDate = new DateTime(int.Parse(item.Element("RegistrationDate").Element("YearReg").Value),
            //                                    int.Parse(item.Element("RegistrationDate").Element("MonthReg").Value),
            //                                    int.Parse(item.Element("RegistrationDate").Element("DayReg").Value)),
            //                             EntryDate = new DateTime(int.Parse(item.Element("EntryDate").Element("YearEntry").Value),
            //                                    int.Parse(item.Element("EntryDate").Element("MonthEntry").Value),
            //                                    int.Parse(item.Element("EntryDate").Element("DayEntry").Value)),
            //                             ReleaseDate = new DateTime(int.Parse(item.Element("ReleaseDate").Element("YearRelease").Value),
            //                                    int.Parse(item.Element("ReleaseDate").Element("MonthRelease").Value),
            //                                    int.Parse(item.Element("ReleaseDate").Element("DayRelease").Value)),
            //                             Area = (AreasInTheCountry)Enum.Parse(typeof(AreasInTheCountry), item.Element("Area").Value),
            //                             Type = (TypesOfVacation)Enum.Parse(typeof(TypesOfVacation), item.Element("Type").Value),
            //                             Adults = Convert.ToInt32(item.Element("Adults").Value),
            //                             Children = Convert.ToInt32(item.Element("Children").Value),
            //                             Pool = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("Pool").Value),
            //                             Jacuzzi = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("Jacuzzi").Value),
            //                             Garden = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("Garden").Value),
            //                             ChildrensAttractions = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("ChildrensAttractions").Value),
            //                             SubArea = (All)Enum.Parse(typeof(All), item.Element("Area").Value),
            //                             Room = Convert.ToInt32(item.Element("Room").Value),
            //                             ID = Convert.ToInt32(item.Element("ID").Value),
            //                             Breakfast = item.Element("Breakfast").Value == "true",
            //                             Lunch = item.Element("Lunch").Value == "true",
            //                             Dinner = item.Element("Dinner").Value == "true",
            //                             Its_Signed = item.Element("Its_Signed").Value == "true"
            //                         }).FirstOrDefault();
            //     }
            //     catch
            //     {
            //         guestRequest = null;
            //     }
            //     return guestRequest;
            // }
            // public void UpdateGuestRequest(GuestRequest G)
            // {
            //     LoadGuestRequests();
            //     try { 
            //     if (CheckGR(G.GuestRequestKey))
            //     {
            //         (from item in GuestRequestRoot.Elements()
            //          where item.Element("GuestRequestKey").Value == G.GuestRequestKey.ToString()
            //          select item
            //          ).FirstOrDefault().Remove();
            //         SavaGRToXElement(G);

            //     }
            //     else
            //      throw new KeyNotFoundException("GuestRequest to update doesn't exist");
            //      }
            //     catch (Exception a)
            //     {
            //         throw a;
            //     }

            // }
            ////get
            //public IEnumerable<GuestRequest> Get_GuestRequestList()
            // {
            //     LoadGuestRequests();
            //     List<GuestRequest> guestRequest;
            //     try
            //     {
            //         guestRequest = (from item in GuestRequestRoot.Elements()
            //                         select new GuestRequest()
            //                         {
            //                             GuestRequestKey = Convert.ToInt32(item.Element("GuestRequestKey").Value),
            //                             PrivateName = item.Element("Name").Element("PrivateName").Value,
            //                             FamilyName = item.Element("Name").Element("FamilyName").Value,
            //                             MailAddress = item.Element("MailAddress").Value,
            //                             Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), item.Element("Status").Value),
            //                             RegistrationDate = new DateTime(int.Parse(item.Element("RegistrationDate").Element("YearReg").Value),
            //                                    int.Parse(item.Element("RegistrationDate").Element("MonthReg").Value),
            //                                    int.Parse(item.Element("RegistrationDate").Element("DayReg").Value)),
            //                             EntryDate = new DateTime(int.Parse(item.Element("EntryDate").Element("YearEntry").Value),
            //                                    int.Parse(item.Element("EntryDate").Element("MonthEntry").Value),
            //                                    int.Parse(item.Element("EntryDate").Element("DayEntry").Value)),
            //                             ReleaseDate = new DateTime(int.Parse(item.Element("ReleaseDate").Element("YearRelease").Value),
            //                                    int.Parse(item.Element("ReleaseDate").Element("MonthRelease").Value),
            //                                    int.Parse(item.Element("ReleaseDate").Element("DayRelease").Value)),
            //                             Area = (AreasInTheCountry)Enum.Parse(typeof(AreasInTheCountry), item.Element("Area").Value),
            //                             Type = (TypesOfVacation)Enum.Parse(typeof(TypesOfVacation), item.Element("Type").Value),
            //                             Adults = Convert.ToInt32(item.Element("Adults").Value),
            //                             Children = Convert.ToInt32(item.Element("Children").Value),
            //                             Pool = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("Pool").Value),
            //                             Jacuzzi = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("Jacuzzi").Value),
            //                             Garden = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("Garden").Value),
            //                             ChildrensAttractions = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("ChildrensAttractions").Value),
            //                             SubArea = (All)Enum.Parse(typeof(All), item.Element("Area").Value),
            //                             Room = Convert.ToInt32(item.Element("Room").Value),
            //                             ID = Convert.ToInt32(item.Element("ID").Value),
            //                             Breakfast = item.Element("Breakfast").Value == "true",
            //                             Lunch = item.Element("Lunch").Value == "true",
            //                             Dinner = item.Element("Dinner").Value == "true",
            //                             Its_Signed = item.Element("Its_Signed").Value == "true"
            //                         }).ToList();
            //     }
            //     catch (Exception)
            //     {
            //         guestRequest = null;
            //     }
            //     return guestRequest;
            // }



            ////ADD
            //public void AddHostingUnit(HostingUnit h)
            //{
            //    List<HostingUnit> GuestRequestList = new List<HostingUnit>();
            //    if (!CheckHU(h.HostingUnitKey))
            //    {
            //        h.HostingUnitKey = Configuration.hostingUnitKey++;
            //        SavaHUToXElement(h);
            //    }
            //}
            //public bool CheckHU(int key)
            //{
            //    try
            //    {
            //        LoadHostingUnit();
            //        var tmp = from item in HostingUnitRoot.Elements()
            //                  where item.Element("HostingUnitKey").Value == key.ToString()
            //                  select item;
            //        if (tmp.ToList() == null)
            //            return false;
            //        else
            //            return true;
            //    }
            //    catch (FileLoadException a)
            //    {
            //        throw a;
            //    }
            //}
            //public void SavaHUToXElement(HostingUnit p)
            //{

            //    XElement HostingUnitKey = new XElement("GuestRequestKey", p.HostingUnitKey);

            //    XElement HostingUnitName = new XElement("HostingUnitName", p.HostingUnitName);
            //    XElement Diary = new XElement("Diary");
            //    for (int i = 0; i < 12; i++)
            //    {
            //        for (int j = 0; j < 31; j++)
            //        {
            //           Diary.Add(p.Diary[i, j].ToString() + " ");
            //        }
            //    }

            //    XElement BankNumber = new XElement("BankNumber", p.Owner.BankAccount.BankNumber);
            //    XElement BankName = new XElement("BankName", p.Owner.BankAccount.BankName);
            //    XElement BranchNumber = new XElement("BranchNumber", p.Owner.BankAccount.BranchNumber);
            //    XElement BranchAddress = new XElement("BranchAddress", p.Owner.BankAccount.BranchAddress);
            //    XElement BranchCity = new XElement("BranchCity", p.Owner.BankAccount.BranchCity);
            //    XElement BankAccountNumber = new XElement("BranchCity", p.Owner.BankAccount.BankAccountNumber);
            //    XElement BankAccount = new XElement("BankBranchDetails", BankNumber, BankName, BranchNumber,
            //        BranchAddress, BranchCity, BankAccountNumber);

            //    XElement HostKey = new XElement("HostKey", p.Owner.HostKey);
            //    XElement PrivateName = new XElement("PrivateName", p.Owner.PrivateName);
            //    XElement FamilyName = new XElement("FamilyName", p.Owner.FamilyName);
            //    XElement PhoneNumber = new XElement("PhoneNumber", p.Owner.PhoneNumber);
            //    XElement MailAddress = new XElement("MailAddress", p.Owner.MailAddress);
            //    XElement CollectionClearance = new XElement("CollectionClearance", p.Owner.CollectionClearance);
            //    XElement Name = new XElement("Name", PrivateName, FamilyName);
            //    XElement Owner = new XElement("Owner", HostKey, Name, PhoneNumber, MailAddress, CollectionClearance,
            //        BankAccount, BankAccountNumber);

            //    XElement Area = new XElement("Area", p.Area.ToString());
            //    XElement SubArea = new XElement("SubArea", p.SubArea.ToString());
            //    XElement Type = new XElement("Type", p.Type.ToString());
            //    XElement Adults = new XElement("Adults", p.Adults);
            //    XElement Children = new XElement("Children", p.Children);
            //    XElement ChildrensAttractions = new XElement("ChildrensAttractions", p.ChildrensAttractions.ToString());
            //    XElement Room = new XElement("Room", p.Room);
            //    XElement Pool = new XElement("Pool", p.Pool.ToString());
            //    XElement Jacuzzi = new XElement("Jacuzzi", p.Jacuzzi.ToString());
            //    XElement Garden = new XElement("Garden", p.Garden.ToString());
            //    XElement Breakfast = new XElement("Breakfast", p.Breakfast);
            //    XElement Lunch = new XElement("Lunch", p.Lunch);
            //    XElement Dinner = new XElement("Dinner", p.Dinner);

            //    XElement hostingUnit = new XElement("HostingUnit", HostingUnitKey, HostingUnitName, Diary, Owner, Area, Type, SubArea, Type
            //        , Adults, Children, ChildrensAttractions, Room, Pool, Jacuzzi, Garden, Breakfast, Lunch, Dinner);
            //    HostingUnitRoot.Add(hostingUnit);
            //    HostingUnitRoot.Save(hostingUnitPath);
            //}

            //public void LoadHostingUnit()
            //{
            //    try
            //    {
            //      HostingUnitRoot = XElement.Load(hostingUnitPath);
            //    }
            //    catch
            //    {
            //        throw new Exception("File upload problem");
            //    }
            //}
            ////Update
            //public HostingUnit GetHUFromXElementT(int id)
            //{
            //LoadHostingUnit();
            //    HostingUnit hostingUnit;
            //    try
            //    {
            //        hostingUnit = (from item in HostingUnitRoot.Elements()
            //                       where int.Parse(item.Element("HostingUnitKey").Value) == id
            //                       select new HostingUnit()
            //                       {
            //                           HostingUnitKey = Convert.ToInt32(item.Element("HostingUnitKey").Value),
            //                           HostingUnitName = item.Element("HostingUnitName").Value,
            //                           Owner = new Host()
            //                           {
            //                               HostKey = Convert.ToInt32(item.Element("Owner").Element("HostKey").Value),
            //                               PrivateName = item.Element("Owner").Element("Name").Element("PrivateName").Value,
            //                               FamilyName = item.Element("Owner").Element("Name").Element("FamilyName").Value,
            //                               PhoneNumber = Convert.ToInt32(item.Element("Owner").Element("PhoneNumber").Value),
            //                               MailAddress = item.Element("Owner").Element("MailAddress").Value,
            //                               CollectionClearance = item.Element("Owner").Element("CollectionClearance").Value == "true",
            //                               BankAccount = new BankBranch()
            //                               {
            //                                   BankNumber = Convert.ToInt32(item.Element("Owner").Element("BankAccount").Element("BankNumber").Value),
            //                                   BankName = item.Element("Owner").Element("BankAccount").Element("BankName").Value,
            //                                   BranchNumber = Convert.ToInt32(item.Element("Owner").Element("BankAccount").Element("BranchNumber").Value),
            //                                   BranchAddress = item.Element("Owner").Element("BankAccount").Element("BranchAddress").Value,
            //                                   BranchCity = item.Element("Owner").Element("BankAccount").Element("BranchCity").Value,
            //                                   BankAccountNumber = Convert.ToInt32(item.Element("Owner").Element("BankAccount").Value),
            //                               },
            //                           },
            //                           Area = (AreasInTheCountry)Enum.Parse(typeof(AreasInTheCountry), item.Element("Area").Value),
            //                           Type = (TypesOfVacation)Enum.Parse(typeof(TypesOfVacation), item.Element("Type").Value),
            //                           Adults = Convert.ToInt32(item.Element("Adults").Value),
            //                           Children = Convert.ToInt32(item.Element("Children").Value),
            //                           Pool = item.Element("Pool").Value == "true",
            //                           Jacuzzi = item.Element("Jacuzzi").Value == "true",
            //                           Garden = item.Element("Garden").Value == "true",
            //                           ChildrensAttractions = item.Element("ChildrensAttractions").Value == "true",
            //                           SubArea = (All)Enum.Parse(typeof(All), item.Element("SubArea").Value),
            //                           Room = Convert.ToInt32(item.Element("Room").Value),
            //                           Breakfast = item.Element("Breakfast").Value == "true",
            //                           Lunch = item.Element("Lunch").Value == "true",
            //                           Dinner = item.Element("Dinner").Value == "true",

            //                       }).FirstOrDefault();

            //    }

            //    catch
            //    {
            //        hostingUnit = null;
            //    }
            //    return hostingUnit;
            //}
            //public void UpdateGuestRequest(GuestRequest G)
            //{
            //LoadHostingUnit();
            //    try
            //    {
            //        if (CheckGR(G.GuestRequestKey))
            //        {
            //            (from item in GuestRequestRoot.Elements()
            //             where item.Element("GuestRequestKey").Value == G.GuestRequestKey.ToString()
            //             select item
            //             ).FirstOrDefault().Remove();
            //            SavaGRToXElement(G);

            //        }
            //        else
            //            throw new KeyNotFoundException("GuestRequest to update doesn't exist");
            //    }
            //    catch (Exception a)
            //    {
            //        throw a;
            //    }

            //}
            ////get
            //public IEnumerable<GuestRequest> Get_GuestRequestList()
            //{
            //LoadHostingUnit();
            //    List<GuestRequest> guestRequest;
            //    try
            //    {
            //        guestRequest = (from item in GuestRequestRoot.Elements()
            //                        select new GuestRequest()
            //                        {
            //                            GuestRequestKey = Convert.ToInt32(item.Element("GuestRequestKey").Value),
            //                            PrivateName = item.Element("Name").Element("PrivateName").Value,
            //                            FamilyName = item.Element("Name").Element("FamilyName").Value,
            //                            MailAddress = item.Element("MailAddress").Value,
            //                            Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), item.Element("Status").Value),
            //                            RegistrationDate = new DateTime(int.Parse(item.Element("RegistrationDate").Element("YearReg").Value),
            //                                   int.Parse(item.Element("RegistrationDate").Element("MonthReg").Value),
            //                                   int.Parse(item.Element("RegistrationDate").Element("DayReg").Value)),
            //                            EntryDate = new DateTime(int.Parse(item.Element("EntryDate").Element("YearEntry").Value),
            //                                   int.Parse(item.Element("EntryDate").Element("MonthEntry").Value),
            //                                   int.Parse(item.Element("EntryDate").Element("DayEntry").Value)),
            //                            ReleaseDate = new DateTime(int.Parse(item.Element("ReleaseDate").Element("YearRelease").Value),
            //                                   int.Parse(item.Element("ReleaseDate").Element("MonthRelease").Value),
            //                                   int.Parse(item.Element("ReleaseDate").Element("DayRelease").Value)),
            //                            Area = (AreasInTheCountry)Enum.Parse(typeof(AreasInTheCountry), item.Element("Area").Value),
            //                            Type = (TypesOfVacation)Enum.Parse(typeof(TypesOfVacation), item.Element("Type").Value),
            //                            Adults = Convert.ToInt32(item.Element("Adults").Value),
            //                            Children = Convert.ToInt32(item.Element("Children").Value),
            //                            Pool = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("Pool").Value),
            //                            Jacuzzi = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("Jacuzzi").Value),
            //                            Garden = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("Garden").Value),
            //                            ChildrensAttractions = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), item.Element("ChildrensAttractions").Value),
            //                            SubArea = (All)Enum.Parse(typeof(All), item.Element("Area").Value),
            //                            Room = Convert.ToInt32(item.Element("Room").Value),
            //                            ID = Convert.ToInt32(item.Element("ID").Value),
            //                            Breakfast = item.Element("Breakfast").Value == "true",
            //                            Lunch = item.Element("Lunch").Value == "true",
            //                            Dinner = item.Element("Dinner").Value == "true",
            //                            Its_Signed = item.Element("Its_Signed").Value == "true"
            //                        }).ToList();
            //    }
            //    catch (Exception)
            //    {
            //        guestRequest = null;
            //    }
            //    return guestRequest;
            //}

            //                    if (!File.Exists(BankPath))

            //            new Thread(() =>
            //            {
            //            do
            //            {
            //                try
            //                {
            //                    LoadBank();
            //                    BankFlag = true;
            //                }
            //                catch (Exception)
            //                {
            //                }

            //                Thread.Sleep(2000);
            //            } while (!BankFlag);
            //        }).Start();

            //            if (!File.Exists(siteOwner1Path))
            //            {
            //                CreateSiteOwner1();
            //    }
            //            else
            //                LoadSiteOwner();
            //}


        

    }
}