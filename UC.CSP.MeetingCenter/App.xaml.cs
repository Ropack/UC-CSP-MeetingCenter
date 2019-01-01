using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using UC.CSP.MeetingCenter.BL;
using UC.CSP.MeetingCenter.DAL;

namespace UC.CSP.MeetingCenter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AutoMapperConfiguration.Configure();
        }
    }
}
