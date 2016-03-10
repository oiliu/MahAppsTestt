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
using MahApps.Metro.Controls;
using WpfAnimatedGif;
using System.Windows.Media.Animation;
using System.Threading;

namespace MahAppsTestt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/ico.ico"));
            this.ResizeMode = ResizeMode.NoResize;
            this.btn_GetLocationWeather.Click += new RoutedEventHandler(btn_GetLocationWeather_Click);
        }

        private void btn_GetLocationWeather_Click(object sender, EventArgs e)
        {
            string LocationJson = GetLocation();
            if (LocationJson == "error")
                return;
            string city = Common.JieXiLocationJson(LocationJson);
            if (city == "error")
                return;
            //返回城市列表
            string WeatherJson = Common.RequestWeather(city);
            string W = Common.JieXiCodeJson(WeatherJson);
            if (W == "error")
                return;
            var image = new BitmapImage();
            image.BeginInit();
            if (W.IndexOf("晴") >= 0)
            {
                image.UriSource = new Uri("pack://application:,,,/Images/1.gif");
            }
            else if (W.IndexOf("雨") >= 0)
            {
                image.UriSource = new Uri("pack://application:,,,/Images/2.gif");
            }
            else
            {
                image.UriSource = new Uri("pack://application:,,,/Images/3.gif");
            }
            image.EndInit();
            ImageBehavior.SetAnimatedSource(this.ImageGif, image);
                        
            #region  动画效果
            DoubleAnimation toXiao = new DoubleAnimation()
            {
                To = 40,
                Duration = new Duration(TimeSpan.FromSeconds(0.9))
            };
            toXiao.Completed += new EventHandler(C);
            //开始动画
            //变化不是阻塞的,而是异步,所以看上去长度与高度几乎是同时变化
            ImageGif.BeginAnimation(Image.WidthProperty, toXiao);
            #endregion
            //本机外网ip,city,天气,
            //http解析，json解析，wpf皮肤控件，
            //DoubleAnimation 动画效果一个执行完执行另一个{Completed事件}
        }

        private void C(object sender, EventArgs e)
        {
            DoubleAnimation toDa = new DoubleAnimation()
            {
                To = 110,
                Duration = new Duration(TimeSpan.FromSeconds(0.7))
            };

            ImageGif.BeginAnimation(Image.WidthProperty, toDa);
        }

        #region 获取本地地址
        public string GetLocation()
        {
            string strIP = Common.GetIP();
            if (string.IsNullOrEmpty(strIP))
            {
                return "error";
            }
            string url = "http://apis.baidu.com/apistore/iplookupservice/iplookup";
            string param = "ip=" + strIP;
            string result = Common.RequestLocation(url, param);
            return result;
        }
        #endregion
    }
}
