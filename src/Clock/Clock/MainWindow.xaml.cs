using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Focus();
        }


        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var now = DateTime.Now;
                    var st = string.Format("{0} : {1} : {2}", now.Hour, now.Minute, now.Second);
                    this.Dispatcher.Invoke(() => 
                    {
                        this.dateTime.Content = st;
                    });

                    System.Threading.Thread.Sleep(100);
                }
            });
        }



    }
}
