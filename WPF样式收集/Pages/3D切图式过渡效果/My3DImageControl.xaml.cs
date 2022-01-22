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

namespace WPF样式收集.Pages._3D切图式过渡效果
{
    /// <summary>
    /// My3DImageControl.xaml 的交互逻辑
    /// </summary>
    public partial class My3DImageControl : UserControl
    {
        public BitmapImage FrontImage;//初始展示的图源
        public BitmapImage BackImage;//转动后展示的图源
        public bool isFinished = true;//动作是否结束
        private const int HorizontalCount = 4;//横向裁剪数量
        private const int VerticalCount = 4;//纵向裁剪数量
        private BitmapSource[,] FrontBitmap = new BitmapSource[HorizontalCount, VerticalCount];
        private BitmapSource[,] BackBitmap = new BitmapSource[HorizontalCount, VerticalCount];

        public My3DImageControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 裁剪图片生成小3D立方体
        /// </summary>
        public void GetFlipImage()
        {
            this.mainCanvas.Children.Clear();
            if (this.FrontImage == null || this.BackImage == null)
            {
                isFinished = true;
                return;
            }
            //裁剪图片
            int partImgWidth = (int)(this.FrontImage.PixelWidth / HorizontalCount);
            int partImgHeight = (int)(this.FrontImage.PixelHeight / VerticalCount);
            int count = 0;
            for (int i = 0; i < HorizontalCount; i++)
            {
                for (int j = 0; j < VerticalCount; j++)
                {
                    FrontBitmap[i, j] = GetPartImage(this.FrontImage, i * partImgWidth, j * partImgHeight, partImgWidth, partImgHeight);
                    BackBitmap[i, j] = GetPartImage(this.BackImage, i * partImgWidth, j * partImgHeight, partImgWidth, partImgHeight);
                    My3DCubeControl my3DCube = new My3DCubeControl();
                    my3DCube.Width = partImgWidth;
                    my3DCube.Height = partImgHeight;
                    my3DCube.ImageFront = FrontBitmap[i, j];
                    my3DCube.ImageBack = BackBitmap[i, j];
                    Canvas.SetLeft(my3DCube, i * partImgWidth);
                    Canvas.SetTop(my3DCube, j * partImgHeight);
                    this.mainCanvas.Children.Add(my3DCube);
                    count = count + i + j;
                }
            }
            isFinished = false;
        }

        /// <summary>
        /// 展示动画
        /// </summary>
        public void ShowAnimation()
        {
            List<My3DCubeControl> my3DCubesList = GetChildObjects<My3DCubeControl>(this.mainCanvas);
            foreach (var cube in my3DCubesList)
            {
                cube.BeginRandomRotationAnimation();
                Thread.Sleep(10);
            }
            isFinished = true;
        }

        /// <summary>
        /// 裁剪图片
        /// </summary>
        private BitmapSource GetPartImage(BitmapImage img, int XCoordinate, int YCoordinate, int Width, int Height)
        {
            return new CroppedBitmap(img, new Int32Rect(XCoordinate, YCoordinate, Width, Height));
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
