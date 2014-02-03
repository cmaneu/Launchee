using System;
using System.Windows;

namespace Launchee
{
    public partial class ErrorReportWindow : Window
    {
        public ErrorReportWindow()
        {
            InitializeComponent();
        }

        internal static void ShowErrorReport(Exception ex)
        {
            ErrorReportWindow window = new ErrorReportWindow();

            window.ErrorTextBox.Text = "An unexpected error occured. Please sent the following content to the GitHub issue." + Environment.NewLine;
            window.ErrorTextBox.Text += "https://github.com/cmaneu/Launchee/issues" + Environment.NewLine;
            window.ErrorTextBox.Text += "-----------------------------------------" + Environment.NewLine;
            window.ErrorTextBox.Text += "" + Environment.NewLine;

            ErrorReport report = new ErrorReport(ex);
            window.ErrorTextBox.Text += report.ToJsonString()+ Environment.NewLine;
            window.ShowDialog();
            Environment.Exit(-1);
        }
    }
}
