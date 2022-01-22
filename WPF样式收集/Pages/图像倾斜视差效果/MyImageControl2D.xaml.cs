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

namespace WPF样式收集.Pages.图像倾斜视差效果
{
    /// <summary>
    /// MyImageControl2D.xaml 的交互逻辑
    /// </summary>
    public partial class MyImageControl2D : UserControl
    {
        public static readonly DependencyProperty ShowImageProperty = DependencyProperty.Register("ShowImage", typeof(ImageSource), typeof(MyImageControl2D), new PropertyMetadata(null));
        public ImageSource ShowImage
        {
            get { return (ImageSource)GetValue(ShowImageProperty); }
            set { SetValue(ShowImageProperty, value); }
        }

        public int extraImgCount = 4;//图片个数
        public double ImgOpacity = 0.5;//除底图外图片透明度
        public int ImgTranslate = 15;//图片位移量

        public MyImageControl2D()
        {
            InitializeComponent();
            this.Loaded += MyImageControl2D_Loaded;
        }

        private void MyImageControl2D_Loaded(object sender, RoutedEventArgs e)
        {
            InitShowImage();
            this.mainGrid.MouseLeave += MainGrid_MouseLeave;
            this.mainGrid.MouseMove += MainGrid_MouseMove;
        }

        /// <summary>
        /// 初始化加载Image控件
        /// </summary>
        private void InitShowImage()
        {
            for (int i = 0; i < extraImgCount; i++)
            {
                Image img = new Image
                {
                    Source = ShowImage,
                    Stretch = Stretch.Fill,
                    Opacity = i > 0 ? ImgOpacity : 1,
                };
                var trans = new TranslateTransform();
                img.RenderTransform = trans;
                this.mainGrid.Children.Add(img);
            }
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            var moveX = e.GetPosition(this.mainGrid).X / this.mainGrid.ActualWidth - 0.5;
            var moveY = e.GetPosition(this.mainGrid).Y / this.mainGrid.ActualHeight - 0.5;

            List<Image> imgs = GetChildObjects<Image>(this.mainGrid);
            for (int i = 1; i < imgs.Count; i++)
            {
                TranslateTransform trans = imgs[i].RenderTransform as TranslateTransform;
                trans.X = moveX * ImgTranslate * i;
                trans.Y = moveY * ImgTranslate * i;
            }
        }

        private void MainGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            List<Image> imgs = GetChildObjects<Image>(this.mainGrid);
            for (int i = 1; i < imgs.Count; i++)
            {
                TranslateTransform trans = imgs[i].RenderTransform as TranslateTransform;
                trans.X = 0;
                trans.Y = 0;
            }
        }
        /// <summary>
        /// 获得所有子控件
        /// </summary>
        private List<T> GetChildObjects<T>(System.Windows.DependencyObject obj) where T : System.Windows.FrameworkElement
        {
            System.Windows.DependencyObject child = null;
            List<T> childList = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child));
            }
            return childList;
        }
    }
}
