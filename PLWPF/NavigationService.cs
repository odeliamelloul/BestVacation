using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace PLWPF
{
    public class NavigationService
    {
        public  NavigationService()
        {
          // NavigationStack.Push(Application.Current.MainWindow);
        }

        public static Stack<Window> NavigationStack = new Stack<Window>();

 

        public static void NavigateBack()
        {
            NavigationStack.Pop().Close();
            NavigationStack.Peek().Show();
            
        }

        public static bool CanNavigateBack()
        {
            return NavigationStack.Count > 1;
        }
    }
}
