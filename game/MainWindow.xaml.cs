using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int count = 1;
        int sec = 0;
        const int finish = 26;
        Random rand = new Random();
        DispatcherTimer timer = null;
        public MainWindow()
        {
            InitializeComponent();

            StartGame();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (sender as Button);

            if (Convert.ToInt32(btn.Content) == count)
            {
                txt.Foreground = Brushes.Black;
                count++;
            }
            else
            {
                txt.Foreground = Brushes.Red;
            }

            txt.Text = count.ToString();

            if (count > finish - 1)
            {
                txt.Text = "WIN";
                timer.Stop();
            }
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
            sec = 0;
            wrap.Children.Clear();
            count = 1;
            StartTimer();
            int[] masiv = new int[finish];

            while (count < finish)
            {
                int temp = rand.Next(1, finish);
                if (masiv[temp] == 0)
                {
                    masiv[temp] = count;
                    ++count;
                }
            }

            count = 1;

            for (int i = count; i < finish; i++)
            {
                Button button = new Button()
                {
                    Content = masiv[i].ToString(),
                    Width = 50,
                    Height = 50,
                    Background = Brushes.White,
                    Margin = new Thickness(0.5),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                button.Click += button_Click;
                wrap.Children.Add(button);
            }

            txt.Text = count.ToString();
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            sec++;
            txtTime.Text = $"Sec: {sec}";
        }
    }
}
