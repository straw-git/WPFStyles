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

namespace WPF样式收集.Pages.图片变换动画
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

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            image.Source = loadBitmap(WPF样式收集.Properties.Resources.images1);
        }
        public static BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnAnimate.IsEnabled = false;
            if (comboBox1.Text == "Circle")
            {
                CircleAnimation circleAnimationHelper = new CircleAnimation();
                circleAnimationHelper.AnimationCompleted += new Action(circleAnimationHelper_AnimationCompleted);
                circleAnimationHelper.MakeCircleAnimation((FrameworkElement)image, image.Width, image.Height, new TimeSpan(0, 0, 5));
            }
            else if (comboBox1.Text == "Radial")
            {
                RadialAnimation radialAnimationHelper = new RadialAnimation();
                radialAnimationHelper.AnimationCompleted += new Action(circleAnimationHelper_AnimationCompleted);
                radialAnimationHelper.MakeRadiaAnimation((FrameworkElement)image, image.Width, image.Height, new TimeSpan(0, 0, 5));
            }
            else if (comboBox1.Text == "Block")
            {
                BlockAnimation blockAnimationHelper = new BlockAnimation();
                blockAnimationHelper.AnimationCompleted += new Action(circleAnimationHelper_AnimationCompleted);
                blockAnimationHelper.MakeBlockAnimation((FrameworkElement)image, image.Width, image.Height, new TimeSpan(0, 0, 5));
            }
            else if (comboBox1.Text == "Interlace")
            {

                InterlacedAnimation interlacedAnimation = new InterlacedAnimation();
                interlacedAnimation.AnimationCompleted += new Action(circleAnimationHelper_AnimationCompleted);
                interlacedAnimation.MakeInterlacedAnimation((FrameworkElement)image, image.Width, image.Height, new TimeSpan(0, 0, 5));
            }

            else if (comboBox1.Text == "WaterFall")
            {

                WaterFallAnimation WaterFall = new WaterFallAnimation();
                WaterFall.AnimationCompleted += new Action(circleAnimationHelper_AnimationCompleted);
                WaterFall.MakeWaterFallAnimation((FrameworkElement)image, image.Width, image.Height, new TimeSpan(0, 0, 5));
            }

        }

        void circleAnimationHelper_AnimationCompleted()
        {
            btnAnimate.IsEnabled = true;
        }
    }
}
