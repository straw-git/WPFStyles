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

namespace WPF样式收集.Pages.字体动画效果
{
    /// <summary>
    /// MyImageControl.xaml 的交互逻辑
    /// </summary>
    public partial class MyImageControl : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String), typeof(MyImageControl), new PropertyMetadata(null));
        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register("TextColor", typeof(Brush), typeof(MyImageControl), new PropertyMetadata(null));
        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register("Direction", typeof(String), typeof(MyImageControl), new PropertyMetadata(null));
        public String Direction
        {
            get { return (String)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        private AxisAngleRotation3D aar;
        private RotateTransform3D rt;

        public MyImageControl()
        {
            InitializeComponent();
            this.Loaded += MyImageControl_Loaded;
        }

        private void MyImageControl_Loaded(object sender, RoutedEventArgs e)
        {
            rt = this.FindName("MyRotateTransform3D") as RotateTransform3D;
            aar = this.FindName("MyAxisAngleRotation3D") as AxisAngleRotation3D;
            switch (Direction)
            {
                case "Top":
                    aar.Axis = new Vector3D(1, 0, 0);
                    rt.CenterY = -10;
                    break;
                case "Bottom":
                    aar.Axis = new Vector3D(-1, 0, 0);
                    rt.CenterY = 10;
                    break;
                case "Left":
                    aar.Axis = new Vector3D(0, 1, 0);
                    rt.CenterX = 10;
                    break;
                case "Right":
                    aar.Axis = new Vector3D(0, -1, 0);
                    rt.CenterX = -10;
                    break;
                default:
                    aar.Axis = new Vector3D(1, 0, 0);
                    rt.CenterY = -10;
                    break;
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            aar.BeginAnimation(AxisAngleRotation3D.AngleProperty, new DoubleAnimation(0, 20, new Duration(TimeSpan.FromMilliseconds(500))));
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            aar.BeginAnimation(AxisAngleRotation3D.AngleProperty, new DoubleAnimation(20, 0, new Duration(TimeSpan.FromMilliseconds(500))));
        }
    }
}
