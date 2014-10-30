using BackupperNew.Screens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BackupperNew
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnActivated(EventArgs e)
        {
            Console.WriteLine("started!");
            MainWindow.DataContext = new MainScreen();
            foreach (var i in Resources.Keys)
            {
                Console.WriteLine(i.ToString());
            }
        }
    }
}
