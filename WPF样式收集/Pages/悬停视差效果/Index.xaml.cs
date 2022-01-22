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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF样式收集.Pages.悬停视差效果
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : Page
    {
        public Index()
        {
            InitializeComponent();
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            var moveX = (e.GetPosition(this.img).X / this.img.ActualWidth - 0.5) * (-25);
            var moveY = -(e.GetPosition(this.img).Y / this.img.ActualHeight - 0.5) * (-20);

            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.To = 10d;
            Vector3D axis = new Vector3D(moveX, moveY, 0);
            AxisAngleRotation3D aar = this.FindName("MyAxisAngleRotation3D") as AxisAngleRotation3D;
            if (aar != null)
            {
                aar.Axis = axis;
                aar.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.To = 0d;
            AxisAngleRotation3D aar = this.FindName("MyAxisAngleRotation3D") as AxisAngleRotation3D;
            if (aar != null)
            {
                aar.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
            }
        }
    }
}
