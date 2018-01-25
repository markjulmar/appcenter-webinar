using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCenterFeatures
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        static int counter = 0;

        void OnReportProgress(object sender, EventArgs e)
        {
            Analytics.TrackEvent("ReportProgress",
                new Dictionary<string, string>() { { "counter", (counter++).ToString() } });
        }

        void OnCrash(object sender, EventArgs e)
        {
            int n = 0;
            var results = 1 / n;
        }

        void OnHandledException(object sender, EventArgs e)
        {
            try
            {
                string msg = null;
                msg.IndexOf(".");
            }
            catch (Exception ex)
            {
                ex.Report();
            }
        }
    }

    static class ExceptionHelpers
    {
        public static void Report(this Exception ex, [CallerMemberName] string caller = "")
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            Analytics.TrackEvent($"{ex.GetType().Name} ({caller})",
                    new Dictionary<string, string>()
                    {
                        { "message", ex.Message }
                    });
        }
    }
}
