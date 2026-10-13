using System.Windows;

namespace WindowsDeveloperInterview;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        IInterviewService service = new InterviewService();
        new MainWindow(service).Show();
    }
}
