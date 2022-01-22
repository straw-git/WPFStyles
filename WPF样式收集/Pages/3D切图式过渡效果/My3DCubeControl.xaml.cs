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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF样式收集.Pages._3D切图式过渡效果
{
    /// <summary>
    /// My3DCubeControl.xaml 的交互逻辑
    /// </summary>
    public partial class My3DCubeControl : UserControl
    {
        public static readonly DependencyProperty ImageFrontProperty = DependencyProperty.Register("ImageFront", typeof(BitmapSource), typeof(My3DCubeControl), new PropertyMetadata(null));
        public BitmapSource ImageFront
        {
            get { return (BitmapSource)GetValue(ImageFrontProperty); }
            set { SetValue(ImageFrontProperty, value); }
        }

        public static readonly DependencyProperty ImageBackProperty = DependencyProperty.Register("ImageBack", typeof(BitmapSource), typeof(My3DCubeControl), new PropertyMetadata(null));
        public BitmapSource ImageBack
        {
            get { return (BitmapSource)GetValue(ImageBackProperty); }
            set { SetValue(ImageBackProperty, value); }
        }

        public My3DCubeControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开启随机角度转动
        /// </summary>
        public void BeginRandomRotationAnimation()
        {
            Random rand = new Random();
            int c = Enum.GetValues(typeof(RotationDirection)).Length;
            int n = rand.Next(0, c);
            switch (n)
            {
                case (int)RotationDirection.Left:
                    Storyboard leftStoryboard = this.FindResource("LeftStoryboard") as Storyboard;
                    leftStoryboard.Begin();
                    break;
                case (int)RotationDirection.Right:
                    Storyboard rightStoryboard = this.FindResource("RightStoryboard") as Storyboard;
                    rightStoryboard.Begin();
                    break;
                case (int)RotationDirection.Upward:
                    Storyboard upwardStoryboard = this.FindResource("UpwardStoryboard") as Storyboard;
                    upwardStoryboard.Begin();
                    break;
                case (int)RotationDirection.Downward:
                    Storyboard downwardStoryboard = this.FindResource("DownwardStoryboard") as Storyboard;
                    downwardStoryboard.Begin();
                    break;
            }
            //放大缩小
            Storyboard zoomXStoryboard = this.FindResource("ZoomXStoryboard") as Storyboard;
            zoomXStoryboard.Begin();
            Storyboard zoomYStoryboard = this.FindResource("ZoomYStoryboard") as Storyboard;
            zoomYStoryboard.Begin();
        }

        //旋转方向
        public enum RotationDirection
        {
            Left,
            Right,
            Upward,
            Downward
        }
    }
}
