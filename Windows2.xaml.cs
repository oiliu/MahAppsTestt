using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace MahAppsTestt
{
    /// <summary>
    /// Interaction logic for Windows2.xaml
    /// 
    /// </summary>
    public partial class Windows2 : MetroWindow
    {
        public Windows2()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/ico.ico"));
            this.Closed += new EventHandler(Closeing);
        }

        private void Closeing(object sender, EventArgs e)
        {
            //是否允许关闭程序
            if ((bool)Ck_isShutDown.IsChecked)
            {
                Application.Current.Shutdown();
                //解析http://
                //解析json
            }
        }
    }
}
