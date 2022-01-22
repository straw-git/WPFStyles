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

namespace WPF样式收集.Pages.菜单折叠效果
{
    /// <summary>
    /// RoundMenuControl.xaml 的交互逻辑
    /// </summary>
    public partial class RoundMenuControl : UserControl
    {
        private bool isCollapsed = true;//标识动画是否收起
        private double centerX, centerY;//中心点坐标
        private int angleNum = 7;//子菜单个数
        private double R = 50;//子菜单距中心点距离
        private readonly double angleAll = 360;

        public RoundMenuControl()
        {
            InitializeComponent();
            this.Loaded += RoundMenuControl_Loaded;
        }

        private void RoundMenuControl_Loaded(object sender, RoutedEventArgs e)
        {
            //计算中心点坐标
            centerX = Canvas.GetLeft(this.MenuBtn) + this.MenuBtn.Width / 2.0;
            centerY = Canvas.GetTop(this.MenuBtn) + this.MenuBtn.Height / 2.0;
        }

        /// <summary>
        /// 点击生成菜单按钮
        /// </summary>
        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isCollapsed)
            {
                isCollapsed = false;
                double stepAngle = angleAll / angleNum;
                Point beginPoint = new Point(centerX + R, centerY - 50.0 / 2.0);
                for (int i = 0; i < angleNum; i++)
                {
                    Matrix mtr = new Matrix();
                    mtr.RotateAt(stepAngle * i, centerX, centerY);
                    Point postion = Point.Multiply(beginPoint, mtr);
                    CreatButton(postion.X, postion.Y, stepAngle * i, new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images\\菜单折叠效果\\" + i + ".png")));
                }
            }
            else
            {
                isCollapsed = true;
                List<MyButton> btnList = GetChildObjects<MyButton>(this.mainCanvas);
                foreach (var item in btnList)
                {
                    if (item.Name != "MenuBtn")
                        HideAnimation(item);
                }
            }
        }

        /// <summary>
        /// 生成按钮
        /// </summary>
        private void CreatButton(double left, double top, double angle, ImageSource source)
        {
            MyButton btn = new MyButton();
            btn.Height = btn.Width = 32;
            btn.DisplayImage = source;
            Canvas.SetLeft(btn, centerX);
            Canvas.SetTop(btn, centerY);
            this.mainCanvas.Children.Add(btn);
            RotateTransform rot = new RotateTransform();
            rot.Angle = angle;
            btn.RenderTransform = rot;
            ShowAnimation(btn, left, top);
        }

        /// <summary>
        /// 子按钮展开动画
        /// </summary>
        private void ShowAnimation(MyButton btn, double left, double top)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation daLeft = new DoubleAnimation(left, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(daLeft, btn);
            Storyboard.SetTargetProperty(daLeft, new PropertyPath("(Canvas.Left)"));
            sb.Children.Add(daLeft);
            DoubleAnimation daTop = new DoubleAnimation(top, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(daTop, btn);
            Storyboard.SetTargetProperty(daTop, new PropertyPath("(Canvas.Top)"));
            sb.Children.Add(daTop);
            sb.Begin();
        }

        /// <summary>
        /// 子按钮折叠动画
        /// </summary>
        private void HideAnimation(MyButton btn)
        {
            Storyboard sb = new Storyboard();
            //动画结束删除该按钮
            sb.Completed += (S, E) =>
            {
                this.mainCanvas.Children.Remove(btn);
            };
            DoubleAnimation daLeft = new DoubleAnimation(Canvas.GetLeft(btn), centerX, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(daLeft, btn);
            Storyboard.SetTargetProperty(daLeft, new PropertyPath("(Canvas.Left)"));
            sb.Children.Add(daLeft);
            DoubleAnimation daTop = new DoubleAnimation(Canvas.GetTop(btn), centerY, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(daTop, btn);
            Storyboard.SetTargetProperty(daTop, new PropertyPath("(Canvas.Top)"));
            sb.Children.Add(daTop);
            sb.Begin();
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
