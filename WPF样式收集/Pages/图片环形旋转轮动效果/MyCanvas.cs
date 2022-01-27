using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPF样式收集.Pages.图片环形旋转轮动效果
{
    /// <summary>
    /// MyCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class MyCanvas : Canvas
    {
        /// <summary>
        /// 方向 0：横向 1：纵向
        /// </summary>
        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register("Direction", typeof(int), typeof(MyCanvas), new PropertyMetadata(null));
        public int Direction
        {
            get { return (int)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }
        /// <summary>
        /// 展示图片数量
        /// </summary>
        public static readonly DependencyProperty ImageCountProperty = DependencyProperty.Register("ImageCount", typeof(int), typeof(MyCanvas), new PropertyMetadata(null));
        public int ImageCount
        {
            get { return (int)GetValue(ImageCountProperty); }
            set { SetValue(ImageCountProperty, value); }
        }

        private List<MyImageBox> imageList = null;//图片列表
        private bool isMouseDown = false;//鼠标是否按下
        private Point startPointCanvas;//鼠标按下起始坐标
        private Point startPointBox;//鼠标按下起始坐标
        private double ccR = 0;
        private double cR = 0;
        private double r = 0;
        private double size = 90;


        public MyCanvas()
        {
            this.Loaded += MyCanvas_Loaded;
        }

        private void MyCanvas_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.MouseDown += MyCanvas_MouseDown;
            this.MouseUp += MyCanvas_MouseUp;
            this.MouseMove += MyCanvas_MouseMove;

            r = Math.PI / 180 * 360 / ImageCount;

            imageList = new List<MyImageBox>();
            for (int i = 0; i < ImageCount; i++)
            {
                MyImageBox box = new MyImageBox();
                box.DisplayImage = loadBitmap(WPF样式收集.Properties.Resources.images1);
                box.ImageTitle = "图片" + i;
                box.Rotate = (r * i + 2 * Math.PI) % (2 * Math.PI);
                box.MouseDown += MyImageBox_MouseDown;
                box.MouseUp += MyImageBox_MouseUp;
                imageList.Add(box);
                this.Children.Add(box);
            }

            Rotate();
        }

        public static BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
        }

        /// <summary>
        /// 鼠标按下得到Box起始坐标
        /// </summary>
        private void MyImageBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startPointBox = e.GetPosition(this);
        }

        /// <summary>
        /// 鼠标抬起根据Box起始坐标判断是否是点击操作
        /// </summary>
        private void MyImageBox_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point stopPointBox = e.GetPosition(this);
            if (Direction == 0)
            {
                if ((stopPointBox.X - startPointBox.X) == 0)
                {
                    //点击了
                    MyImageBox box = sender as MyImageBox;
                    cR = Math.PI / 2 - box.Rotate;
                }
            }
            else
            {
                if ((stopPointBox.Y - startPointBox.Y) == 0)
                {
                    //点击了
                    MyImageBox box = sender as MyImageBox;
                    cR = Math.PI / 2 - box.Rotate;
                }
            }
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        private void MyCanvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isMouseDown = true;
            startPointCanvas = e.GetPosition(this);
        }

        /// <summary>
        /// 根据鼠标移动方向进行滚动
        /// </summary>
        private void MyCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point stopPointCanvas = e.GetPosition(this);
                if (Direction == 0)
                {
                    if (stopPointCanvas.X > startPointCanvas.X)
                    {
                        double ds = (stopPointCanvas.X - startPointCanvas.X) * 0.01;
                        cR = (cR - ds + 2 * Math.PI) % (2 * Math.PI);
                    }
                    else if (stopPointCanvas.X == startPointCanvas.X)
                    {

                    }
                    else
                    {
                        double ds = (startPointCanvas.X - stopPointCanvas.X) * 0.01;
                        cR = (cR + ds + 2 * Math.PI) % (2 * Math.PI);
                    }
                }
                else
                {
                    if (stopPointCanvas.Y > startPointCanvas.Y)
                    {
                        double ds = (stopPointCanvas.Y - startPointCanvas.Y) * 0.01;
                        cR = (cR - ds + 2 * Math.PI) % (2 * Math.PI);
                    }
                    else if (stopPointCanvas.Y == startPointCanvas.Y)
                    {

                    }
                    else
                    {
                        double ds = (startPointCanvas.Y - stopPointCanvas.Y) * 0.01;
                        cR = (cR + ds + 2 * Math.PI) % (2 * Math.PI);
                    }
                }

                startPointCanvas = stopPointCanvas;
                Rotate();
            }
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        private void MyCanvas_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        /// <summary>
        /// 转动
        /// </summary>
        public void Rotate()
        {
            ccR = (ccR + 2 * Math.PI) % (2 * Math.PI);

            if (cR - ccR < 0) cR = cR + 2 * Math.PI;
            if (cR - ccR < Math.PI)
            {
                ccR = ccR + (cR - ccR) / 19;
            }
            else
            {
                ccR = ccR - (2 * Math.PI + ccR - cR) / 19;
            }
            if (cR != 0 && Math.Abs((cR + 2 * Math.PI) % (2 * Math.PI) - (ccR + 2 * Math.PI) % (2 * Math.PI)) < Math.PI / 720)
            {
                ccR = cR;
            }
            for (var i = 0; i < imageList.Count; i++)
            {
                MyImageBox item = imageList[i];
                double w, h;
                var sinR = Math.Sin(r * i + ccR);
                var cosR = Math.Cos(r * i + ccR);
                w = size + 0.6 * size * sinR;
                h = size + 0.6 * size * sinR;
                item.Width = w;
                item.Height = h;

                double top, left, zIndex;
                if (Direction == 0)
                {
                    top = this.ActualHeight / 2 - item.Height / 2;
                    left = this.ActualWidth / 2 + cosR * this.ActualWidth / 2 - h / 2;
                    zIndex = this.ActualHeight / 2 + sinR * this.ActualWidth / 2 / 3 - w / 2;
                }
                else
                {
                    top = this.ActualHeight / 2 + cosR * this.ActualHeight / 2 - h / 2;
                    left = this.ActualWidth / 2 - item.Width / 2;
                    zIndex = this.ActualHeight / 2 + sinR * this.ActualWidth / 2 / 3 - w / 2;
                }
                item.SetValue(Canvas.TopProperty, top);
                item.SetValue(Canvas.LeftProperty, left);
                item.SetValue(Canvas.ZIndexProperty, (int)zIndex);
                if (item.Width < 60)
                {
                    item.Visibility = Visibility.Hidden;
                }
                else
                {
                    item.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
