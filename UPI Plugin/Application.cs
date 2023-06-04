using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.IO;

namespace UPI_Plugin
{
    public class Application : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel panel = RibbonPanel(application);
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            if(panel.AddItem(new PushButtonData("Первый плагин","Первый плагин", thisAssemblyPath, "UPI_Plugin.Command")) is PushButton button)
            {
                button.ToolTip = "Мой первый плагин";

                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "UPI.ico"));
                BitmapImage bitmap = new BitmapImage(uri);
                button.LargeImage = bitmap;
            }
            return Result.Succeeded;
        }

        public RibbonPanel RibbonPanel(UIControlledApplication a)
        {
            string tabName = "Югорский Проектный Институт";
            RibbonPanel ribbonPanel = null;
            //Создание вкладки "Югорский проектный институт"
            try
            {
                a.CreateRibbonTab(tabName);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            //Создание группы панелей "Архитектура"
            try
            {
                a.CreateRibbonPanel(tabName, "Архитектура");
                a.CreateRibbonPanel(tabName, "Конструкции");
                a.CreateRibbonPanel(tabName, "О плагине");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            List<RibbonPanel> panels = a.GetRibbonPanels(tabName);
            foreach(RibbonPanel p in panels.Where(p => p.Name == "О плагине"))
            {
                ribbonPanel = p;
            }

            return ribbonPanel;
        }
    }
}
