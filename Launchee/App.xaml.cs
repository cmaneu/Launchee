using System;
using System.Linq;
using System.Windows;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Launchee
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorReportWindow.ShowErrorReport(e.ExceptionObject as Exception);
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ErrorReportWindow.ShowErrorReport(e.Exception);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            LinkManager mgr = new LinkManager();
            var links = mgr.LoadLinksFromFile("jsontest.json");

            var jumplist = Microsoft.WindowsAPICodePack.Taskbar.JumpList.CreateJumpList();
            jumplist.ClearAllUserTasks();


            // Handle categories
            var categories = from   link in links
                                    where link.IsDefault == false 
                                    group link by link.Category
                                    into g
                                    select new {CategoryTitle = g.Key, Links = g.ToList()};

            foreach (var category in categories)
            {
                if (string.IsNullOrWhiteSpace(category.CategoryTitle))
                {
                    // Default
                    foreach (var link in category.Links)
                    {
                        jumplist.AddUserTasks(link.ToJumpListLink());
                    }
                }
                else
                {
                    var cat = new JumpListCustomCategory(category.CategoryTitle);
                    foreach (var link in category.Links)
                    {
                        cat.AddJumpListItems(link.ToJumpListLink());
                    }
                    jumplist.AddCustomCategories(new JumpListCustomCategory[] { cat });
                }
            }

            // Handle Default lau ncj


            //var jumplist = Microsoft.WindowsAPICodePack.Taskbar.JumpList.CreateJumpList();
            //jumplist.ClearAllUserTasks();
            ////jumplist.AddUserTasks(new JumpListLink("calc.exe","Calculatrice"));
            //jumplist.AddUserTasks(new JumpListLink("http://google.fr","Bisounours"){ShowCommand = new WindowShowCommand(){});
            ////jumplist.AddUserTasks(new JumpListLink("http://google.fr","Google"));
            ////jumplist.AddUserTasks(new JumpListLink("http://google.fr","Google"));
            ////jumplist.AddUserTasks(new JumpListLink("http://google.fr","Google"));
            ////jumplist.AddUserTasks(new JumpListLink("http://google.fr","Google"));
            ////jumplist.AddUserTasks(new JumpListLink("http://google.fr","Google"));
            ////jumplist.AddUserTasks(new JumpListLink("http://google.fr","Google"));
            ////jumplist.AddUserTasks(new JumpListLink("http://google.fr","Google"));


            ////var cat = new JumpListCustomCategory("Exploitation");
            ////cat.AddJumpListItems(new JumpListLink("http://deezer.com", "Deezer"));
            ////jumplist.AddCustomCategories(new JumpListCustomCategory[] { cat });
            ////jumplist.AddUserTasks(new JumpListLink("http://google.fr","Google"));
            jumplist.Refresh();
        }
    }
}
