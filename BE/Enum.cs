using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum TypesOfVacation { צימר, מלון, קמפינג, דירת_אירוח }
    public enum AreasInTheCountry { הכל, צפון, דרום, מרכז, ירושלים }

    public enum Jerusalem { הכל, העיר_העתיקה, ממילה, גילו, תלפיות, מרכז_העיר, בית_וגן }
    public enum Center { הכל, תל_אביב,  רמת_גן, קיריית_אונו, נתניה, פתח_תקווה, אור_יהודה, גיבעתיים, ראשון_לציון }
    public enum South { הכל, אילת, באר_שבע, אשדוד, אשקלון, אופקים, ערד, קריית_גת, נתיבות }
    public enum North { הכל, נהריה, בית_שאן, עפולה, צפת, כרמיאל, גליל_עליון,טבריה,קיריית_שמונה }
    public enum All
    {
      הכל, נהריה, בית_שאן, עפולה, צפת, כרמיאל, גליל_עליון, קיריית_שמונה ,אילת, באר_שבע,
      טבריה,אשדוד, אשקלון, אופקים, ערד, קריית_גת, נתיבות , תל_אביב, רמת_גן, קיריית_אונו,
      ירושלים,מרכז,צפון,דרום ,נתניה ,העיר_העתיקה, ממילה, גילו, תלפיות, מרכז_העיר, בית_וגן, פתח_תקווה, אור_יהודה, גיבעתיים, ראשון_לציון
    }
    //public enum Meal {Breakfast,Dinner,Lunch}
    //public enum CustomersRequirement { Open, Closed_when_expired, The_deal_was_closed }
    public enum ChoosingAttraction{ הכרחי,אפשרי,לא_מעוניין}
    public enum OrderStatus { לא_טופל,נשלח_מייל, נסגר_מחוסר_הענות, נסגרה_עסקה }
    public enum Bank_Name {Mizrahi,Discont,Marcentil,BenLeoumi,Igood}
    public class Program
    {
    }
    



}
