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

namespace WPF样式收集.Pages.图片翻转切换特效
{
    /// <summary>
    /// MyImageButtom.xaml 的交互逻辑
    /// </summary>
    public partial class MyImageButton : UserControl
    {
        #region Property
        public static DependencyProperty NormalImageProperty = DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(MyImageButton), new PropertyMetadata(null));
        public ImageSource NormalImage
        {
            get { return (ImageSource)GetValue(NormalImageProperty); }
            set { SetValue(NormalImageProperty, value); }
        }

        public static readonly DependencyProperty HoverImageProperty = DependencyProperty.Register("HoverImage", typeof(ImageSource), typeof(MyImageButton), new PropertyMetadata(null));
        public ImageSource HoverImage
        {
            get { return (ImageSource)GetValue(HoverImageProperty); }
            set { SetValue(HoverImageProperty, value); }
        }
        #endregion

        private Rect LRect, TRect, RRect, BRect, DLRect, DTRect, DRRect, DBRect;//左、上、右、下四条边的矩形，用于判断进入进出图像
        private bool IsCanMove;//是否移动
        private Storyboard storyboard = new Storyboard();
        DependencyProperty[] propertyChain;//动作设置属性

        public MyImageButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        private void MyUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.imgHover.RenderTransform = new RotateTransform();
            propertyChain = new DependencyProperty[]
            {
                Image.RenderTransformProperty,
                RotateTransform.AngleProperty
            };

            GetLTRBRect();
            this.MouseEnter += MyImageButtom_MouseEnter;
            this.MouseMove += MyImageButtom_MouseMove;
            this.MouseLeave += MyImageButtom_MouseLeave;
        }

        /// <summary>
        /// 鼠标进入
        /// </summary>
        private void MyImageButtom_MouseEnter(object sender, MouseEventArgs e)
        {
            IsCanMove = true;
        }

        private void MyUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.MouseEnter -= MyImageButtom_MouseEnter;
            this.MouseMove -= MyImageButtom_MouseMove;
            this.MouseLeave -= MyImageButtom_MouseLeave;
        }

        /// <summary>
        /// 鼠标移动，判断从哪个方向进入
        /// </summary>
        private void MyImageButtom_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsCanMove) return;
            EntryDirection direction = EntryDirection.None;
            Point tempEndPoint = e.GetPosition(this);
            if (IsInRect(tempEndPoint.X, tempEndPoint.Y, LRect))
                direction = EntryDirection.Left;
            else if (IsInRect(tempEndPoint.X, tempEndPoint.Y, TRect))
                direction = EntryDirection.Top;
            else if (IsInRect(tempEndPoint.X, tempEndPoint.Y, RRect))
                direction = EntryDirection.Right;
            else if (IsInRect(tempEndPoint.X, tempEndPoint.Y, BRect))
                direction = EntryDirection.Bottom;
            AppearAnimate(direction);
            IsCanMove = false;
        }

        /// <summary>
        /// 鼠标离开，判断从哪个方向离开
        /// </summary>
        private void MyImageButtom_MouseLeave(object sender, MouseEventArgs e)
        {
            EntryDirection direction = EntryDirection.None;
            Point tempEndPoint = e.GetPosition(this);
            if (IsInRect(tempEndPoint.X, tempEndPoint.Y, DLRect))
                direction = EntryDirection.Left;
            else if (IsInRect(tempEndPoint.X, tempEndPoint.Y, DTRect))
                direction = EntryDirection.Top;
            else if (IsInRect(tempEndPoint.X, tempEndPoint.Y, DRRect))
                direction = EntryDirection.Right;
            else if (IsInRect(tempEndPoint.X, tempEndPoint.Y, DBRect))
                direction = EntryDirection.Bottom;
            DisappearAnimate(direction);
        }

        /// <summary>
        /// 获得4条边的矩形
        /// </summary>
        public void GetLTRBRect()
        {
            int localArea = 10;

            LRect = new Rect(0, 0, localArea, this.ActualHeight);
            TRect = new Rect(0, 0, this.ActualWidth, localArea);
            RRect = new Rect(this.ActualWidth - localArea, 0, localArea, this.ActualHeight);
            BRect = new Rect(0, this.ActualHeight - localArea, this.ActualWidth, localArea);
            DLRect = new Rect(-localArea, 0, localArea, this.ActualHeight);
            DTRect = new Rect(0, -localArea, this.ActualWidth, localArea);
            DRRect = new Rect(this.ActualWidth, 0, localArea, this.ActualHeight);
            DBRect = new Rect(0, this.ActualHeight, this.ActualWidth, localArea);
        }

        /// <summary>
        /// 指定的坐标是否在矩形里
        /// </summary>
        public bool IsInRect(double x, double y, Rect rect)
        {
            return x >= rect.X && x <= rect.X + rect.Width && y >= rect.Y && y <= rect.Y + rect.Height;
        }

        /// <summary>
        /// 鼠标进入出现动图
        /// </summary>
        private void AppearAnimate(EntryDirection direction)
        {
            storyboard.Children.Clear();

            DoubleAnimation RotateTransformAnimation;
            switch (direction)
            {
                case EntryDirection.Left:
                    this.imgHover.RenderTransformOrigin = new Point(0, 1);
                    RotateTransformAnimation = new DoubleAnimation(-90, 0, new Duration(TimeSpan.FromMilliseconds(500)));
                    break;
                case EntryDirection.Top:
                    this.imgHover.RenderTransformOrigin = new Point(0, 0);
                    RotateTransformAnimation = new DoubleAnimation(-90, 0, new Duration(TimeSpan.FromMilliseconds(500)));
                    break;
                case EntryDirection.Right:
                    this.imgHover.RenderTransformOrigin = new Point(1, 1);
                    RotateTransformAnimation = new DoubleAnimation(90, 0, new Duration(TimeSpan.FromMilliseconds(500)));
                    break;
                case EntryDirection.Bottom:
                    this.imgHover.RenderTransformOrigin = new Point(0, 1);
                    RotateTransformAnimation = new DoubleAnimation(90, 0, new Duration(TimeSpan.FromMilliseconds(500)));
                    break;
                default:
                    return;
            }
            Storyboard.SetTarget(RotateTransformAnimation, this.imgHover);
            Storyboard.SetTargetProperty(RotateTransformAnimation, new PropertyPath("(0).(1)", propertyChain));
            storyboard.Children.Add(RotateTransformAnimation);
            storyboard.Begin();
            Canvas.SetZIndex(this.imgHover, 0);
        }

        /// <summary>
        /// 鼠标移出消失动图
        /// </summary>
        private void DisappearAnimate(EntryDirection direction)
        {
            storyboard.Children.Clear();

            DoubleAnimation RotateTransformAnimation;
            switch (direction)
            {
                case EntryDirection.Left:
                    this.imgHover.RenderTransformOrigin = new Point(0, 1);
                    RotateTransformAnimation = new DoubleAnimation(0, -90, new Duration(TimeSpan.FromMilliseconds(500)));
                    break;
                case EntryDirection.Top:
                    this.imgHover.RenderTransformOrigin = new Point(0, 0);
                    RotateTransformAnimation = new DoubleAnimation(0, -90, new Duration(TimeSpan.FromMilliseconds(500)));
                    break;
                case EntryDirection.Right:
                    this.imgHover.RenderTransformOrigin = new Point(1, 1);
                    RotateTransformAnimation = new DoubleAnimation(0, 90, new Duration(TimeSpan.FromMilliseconds(500)));
                    break;
                case EntryDirection.Bottom:
                    this.imgHover.RenderTransformOrigin = new Point(0, 1);
                    RotateTransformAnimation = new DoubleAnimation(0, 90, new Duration(TimeSpan.FromMilliseconds(500)));
                    break;
                default:
                    return;
            }
            Storyboard.SetTarget(RotateTransformAnimation, this.imgHover);
            Storyboard.SetTargetProperty(RotateTransformAnimation, new PropertyPath("(0).(1)", propertyChain));
            storyboard.Children.Add(RotateTransformAnimation);
            storyboard.Begin();
            Canvas.SetZIndex(this.imgHover, 0);
        }

        /// <summary>
        /// 进入离开方向枚举
        /// </summary>
        public enum EntryDirection
        {
            None,
            Left,
            Top,
            Right,
            Bottom
        }
    }
}
