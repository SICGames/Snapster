using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using com.HellstormGames.ScreenCapture; //-- Snapture
using com.HellStormGames.Imaging;
using com.HellStormGames.Imaging.ScreenCapture;
using com.HellStormGames.Imaging.ScreenCapture.Interlop; //-- Snapster

namespace Sample04_TheShowDown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //-- Snapture declarations
        Snapture Snapture { get; set; }
        Stopwatch SnaptureBenchmark { get; set; }

        //-- Snapster Declarations
        DisplayMonitor DisplayMonitor { get; set; }
        Monitor CurrentMonitor { get; set; }

        Stopwatch SnapsterBenchmark { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int? _testIterations;
        public int? TestIterations
        {
            get { return _testIterations.GetValueOrDefault(0); }
            set
            {
                _testIterations = value;
                OnPropertyChanged(nameof(TestIterations));
            }
        }

        private int? _iterations;
        public int? Iterations
        {
            get
            {
                return _iterations.GetValueOrDefault(10000);
            }
            set
            {
                _iterations = value;
                OnPropertyChanged(nameof(Iterations));
            }
        }

        private int? _snaptureiterations;
        public int? SnaptureIterations
        {
            get
            {
                return _snaptureiterations.GetValueOrDefault(0);
            }
            set
            {
                _snaptureiterations = value;
                OnPropertyChanged(nameof(SnaptureIterations));
            }
        }


        private int? _snapsteriterations;
        public int? SnapsterIterations
        {
            get
            {
                return _snapsteriterations.GetValueOrDefault(0);
            }
            set
            {
                _snapsteriterations = value;
                OnPropertyChanged(nameof(SnapsterIterations));
            }
        }

        private string _snapstertime = string.Empty;
        public string SnapsterTime
        {
            get => _snapstertime;
            set
            {
                _snapstertime = value;
                OnPropertyChanged(nameof(SnapsterTime));
            }
        }
        private string _snapsteravgtime = string.Empty;
        public string SnapsterAvgTime
        {
            get => _snapsteravgtime;
            set
            {
                _snapsteravgtime = value;
                OnPropertyChanged(nameof(SnapsterAvgTime));
            }
        }
        private string _snapturetime = string.Empty;
        public string SnaptureTime
        {
            get => _snapturetime;
            set
            {
                _snapturetime = value;
                OnPropertyChanged(nameof(SnaptureTime));
            }
        }

        private string _snaptureavgtime = string.Empty;
        public string SnaptureAvgTime
        {
            get => _snaptureavgtime;
            set
            {
                _snaptureavgtime = value;
                OnPropertyChanged(nameof(SnaptureAvgTime));
            }
        }

        private long SnaptureElapsedTime = 0;
        private long SnapsterElapsedTime = 0;
        private TimeSpan SnapsterElapsed, SnaptureElapsed;
        private bool SnaptureCanContinue = false;
        private bool SnapsterCanContinue = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeSnapture();
            InitializeSnapster();
            SnaptureBenchmark = new Stopwatch();
            SnapsterBenchmark = new Stopwatch();
            this.DataContext = this;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ShutDownTests();
            SnaptureBenchmark = null;
            SnapsterBenchmark = null;
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            StartBenchmark();
        }

        private void InitializeSnapture()
        {
            Snapture = new Snapture();
            Snapture.onFrameCaptured += Snapture_onFrameCaptured;
            Snapture.isActive = true;
            Snapture.Start(FrameCapturingMethod.GDI);
        }
        bool bSnapsterInitialized = false;

        private void InitializeSnapster()
        {
            DisplayMonitor = new DisplayMonitor();
            MonitorCollection monitors = DisplayMonitor.GetMonitors();
            CurrentMonitor = monitors.Get(0); //-- main monitor.
            /*
            
            */
        }
     
        private void SnaptureCapture()
        {
            SnaptureCanContinue = false;
            SnaptureBenchmark = Stopwatch.StartNew();
            Snapture.CaptureDesktop();
        }
        private void SnapsterCapture()
        {
            SnapsterCanContinue = false;
            SnapsterBenchmark = Stopwatch.StartNew();

            if(bSnapsterInitialized)
            {
                IntPtr bitmap = IntPtr.Zero;
                SnaptureInvoke.DesktopDuplicationCaptureDesktop(out bitmap);
                if (bitmap != IntPtr.Zero)
                {
                    //Debug.WriteLine("Captured Bitmap");
                    var imagedata = ImageFactory.FromHBitmap(bitmap);
                    SnaptureInvoke.ReleaseCapturedBitmap(bitmap);
                    imagedata.Save($"Desktopduplicationtest_{SnapsterIterations}.jpg");
                    imagedata.Dispose();
                }

                SnapsterBenchmark.Stop();
                SnapsterElapsed += SnapsterBenchmark.Elapsed;
                SnapsterAvgTime = $"{SnapsterBenchmark.ElapsedMilliseconds} ms";
                SnapsterTime = SnapsterElapsed.ToString(@"mm\:ss\.ffff");
                SnapsterCanContinue = true;
            }
        }

        private Task RunSnapsterTest()
        {
            return Task.Run(() =>
            {
                SnapsterCanContinue = true;
                
                SnapsterIterations = 0;
                if (SnaptureInvoke.InitDesktopDuplicationCapture(CurrentMonitor) == 0)
                {
                    bSnapsterInitialized = true;
                }
                
                while (true)
                {
                    if (SnapsterIterations == Iterations)
                    {
                        break;       
                    }
                    if (SnapsterCanContinue)
                    {
                        SnapsterCapture();
                        SnapsterIterations++;
                    }
                }
                SnaptureInvoke.ReleaseDesktopDuplicationResources();

            });
        }
        private Task RunSnaptureTest()
        {
            return Task.Run(() =>
            {
                SnaptureIterations = 0;
                SnaptureCanContinue = true;
                while (true)
                {
                    if (SnaptureIterations == Iterations)
                    {
                        break;
                    }


                    if(SnaptureCanContinue)
                    {
                        SnaptureCapture();
                        SnaptureIterations++;
                    }
                }
            });
        }
        private void RunTasks()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Task[] tasks = { RunSnaptureTest(), RunSnapsterTest() };
                Task.WhenAll(tasks).ContinueWith(t =>
                {
                    TestButton.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        TestButton.IsEnabled = true;
                    }));
                });
            }));
        }

        private void StartBenchmark()
        {
            TestButton.Dispatcher.BeginInvoke(new Action(() =>
            {
                TestButton.IsEnabled = false;
            }));

            RunTasks();  
        }
        private void ShutDownTests()
        {
            
            Snapture.Release();
            Snapture = null;
            DisplayMonitor.Dispose();
            DisplayMonitor = null;
        }

        private void Snapture_onFrameCaptured(object sender, FrameCapturedEventArgs e)
        {
            e.ScreenCapturedBitmap.Dispose();
            SnaptureBenchmark.Stop();
            SnaptureElapsed += SnaptureBenchmark.Elapsed;
            SnaptureAvgTime = $"{SnaptureBenchmark.ElapsedMilliseconds} ms";
            SnaptureTime = SnaptureElapsed.ToString(@"mm\:ss\.ffff");
            SnaptureCanContinue = true;

        }
    }
}
