using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using AppTimer.Data;
using AppTimer.Entities;

namespace AppTimer;

public partial class MainWindow : Window
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    private DispatcherTimer _dispatcherTimer;
    private TimeSpan _timeElapsed;

    private List<string> _activeApps = new List<string>();
    private string _targetApp = string.Empty;

    private readonly DataContext _context = new DataContext();

    public MainWindow()
    {
        InitializeComponent();

        GetDesktopWindows(); // set the activeApps var
        
        LoadHistory();

        _timeElapsed = TimeSpan.Zero;

        ActiveAppsList.ItemsSource = _activeApps;

        IntPtr foregroundWindowHandle = GetForegroundWindow();

        if (foregroundWindowHandle == IntPtr.Zero)
        {
            Console.WriteLine("No window is currently focused.");
            return;
        }

        GetWindowThreadProcessId(foregroundWindowHandle, out int foregroundProcessId);

        if (ActiveAppsList.SelectedItem != null)
        {
            Process[] processes = Process.GetProcessesByName(ActiveAppsList.SelectedItem.ToString());

            foreach (var process in processes)
            {
                if (process.Id == foregroundProcessId)
                {
                    Console.WriteLine("The process is running.");
                }
            }
        }

        _dispatcherTimer = new DispatcherTimer();
        _dispatcherTimer.Interval = TimeSpan.FromSeconds(1); // intervalul dintre tick-uri
        _dispatcherTimer.Tick += DispatcherTimer_Tick;

        var monitorTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(500)
        };
        monitorTimer.Tick += MonitorActiveApp;
        monitorTimer.Start();
    }

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        _timeElapsed = _timeElapsed.Add(TimeSpan.FromSeconds(1));
        if (TimerText != null)
        {
            TimerText.Content = _timeElapsed.ToString("c");
        }
    }

    private void MonitorActiveApp(object sender, EventArgs e)
    {
        string acApp = GetActiveWindowTitle();
        if(!string.IsNullOrEmpty(acApp) && ActiveAppsList.SelectedItem != null && acApp.Contains(ActiveAppsList.SelectedItem.ToString().ToLower())) // if the active app is the same as the selected one
        {
            _dispatcherTimer.Start();
        }
        else
        {
            _dispatcherTimer.Stop();
        }
    }

    /// <summary>
    ///   Get all the active windows on the desktop
    /// </summary>
    public void GetDesktopWindows()
    {
        Process[] myProcesses = Process.GetProcesses();

        foreach (Process P in myProcesses)
        {
            if (P.MainWindowTitle.Length > 1)
            {
                _activeApps.Add(P.ProcessName);
            }
        }
    }

    /// <summary>
    /// Get the active window title
    /// </summary>
    /// <returns>
    ///  The active window title
    /// </returns>
    private string GetActiveWindowTitle()
    {
        IntPtr handle = GetForegroundWindow();
        GetWindowThreadProcessId(handle, out int pid);
        
        Process[] processes = Process.GetProcesses();
        foreach (Process process in processes)
        {
            if (process.Id == pid)
            {
                return process.ProcessName.ToLower(); // get the current active app
            }
        }

        return null;
    }

    /// <summary>
    ///   Reset the timer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ResetTimerButton_OnClick(object sender, RoutedEventArgs e)
    {
        _timeElapsed = TimeSpan.Zero;
        if (TimerText != null)
        {
            TimerText.Content = _timeElapsed.ToString("c");
        }
        
        System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\sandu\Documents\AppTimer\Assets\stop.wav");
        player.Play();
    }

    private void PauseButton_OnClick(object sender, RoutedEventArgs e)
    {
        _dispatcherTimer.Stop();
        
        PlaySound("pause");
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (ActiveAppsList.SelectedItem != null)
        {
            _targetApp = ActiveAppsList.SelectedItem.ToString()?.ToLower() ?? string.Empty;
            AppName.Content = ActiveAppsList.SelectedItem.ToString(); // change the title of the app

            _timeElapsed = TimeSpan.Zero; // reset the timer
        }
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (_timeElapsed.TotalSeconds > 5)
        {
            var registry = new Registry
            {
                Name = _targetApp,
                Duration = _timeElapsed.ToString("c"),
                DateAdded = DateTime.Now.ToString("dd/MM/yyyy")
            };

            _context.Registries.Add(registry);

            _context.SaveChanges();
            
            // reset the timer
            ResetTimer();
            
            PlaySound("save");
            
            LoadHistory();
        }
    }

    private void LoadHistory()
    {
        var history = _context.Registries.ToList();
        HistoryList.ItemsSource = history;
    }

    private void ResetTimer()
    {
        _dispatcherTimer.Stop();
        
        _timeElapsed = TimeSpan.Zero;
        //
        // System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\sandu\Documents\AppTimer\Assets\stop.wav");
        // player.Play();
        
        PlaySound("stop");
    }

    private void PlaySound(string path)
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer(@$"C:\Users\sandu\Documents\AppTimer\Assets\{path}.wav");
        player.Play();
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;

        if (button?.Tag is int id)
        {
            var registry = _context.Registries.FirstOrDefault(r => r.Id == id);
            
            if (registry != null)
            {
                _context.Registries.Remove(registry);
                _context.SaveChanges();
                
                LoadHistory();
            }
        }
    }
}