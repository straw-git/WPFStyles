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

namespace WPF样式收集.Pages.Loading
{
    /// <summary>
    /// LoadingPendulum.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingPendulum : UserControl
    {

        private string showText = "loading";
        private Color[] ellipseColors = { (Color)ColorConverter.ConvertFromString("#DB2F00"), (Color)ColorConverter.ConvertFromString("#FF6D37"), (Color)ColorConverter.ConvertFromString("#FFA489"),
        (Color)ColorConverter.ConvertFromString("#FFFFFF"), (Color)ColorConverter.ConvertFromString("#99D3D4"), (Color)ColorConverter.ConvertFromString("#56BEBF"), (Color)ColorConverter.ConvertFromString("#13A3A5")};

        public LoadingPendulum()
        {
            InitializeComponent();
            InitEllipse(new Point(0, 0));
            BeginKeyFrameAnimation();
        }

        public void InitEllipse(Point beginPoint)
        {
            for (int i = 0; i < showText.Length; i++)
            {
                EllipsePendulum e = new EllipsePendulum()
                {
                    FillColor = new SolidColorBrush(ellipseColors[i]),
                    ShowText = showText.Substring(i, 1),
                    RenderTransform = new TranslateTransform(beginPoint.X, beginPoint.Y),
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    Margin = new Thickness(100 + i * 80, 0, 0, 0),
                };
                this.mainGrid.Children.Add(e);
            }
        }

        public void BeginKeyFrameAnimation()
        {
            Storyboard sb = new Storyboard();
            //动画完成事件 再次设置此动画
            sb.Completed += (S, E) =>
            {
                BeginKeyFrameAnimation();
            };
            DependencyProperty[] propertyChain = new DependencyProperty[]
            {
                EllipsePendulum.RenderTransformProperty,
                TranslateTransform.XProperty
            };
            for (int i = 0; i < this.mainGrid.Children.Count; i++)
            {
                DoubleKeyFrameCollection frameCollection = new DoubleKeyFrameCollection();
                DoubleAnimationUsingKeyFrames dakf = new DoubleAnimationUsingKeyFrames()
                {
                    Duration = new Duration(TimeSpan.FromSeconds(1)),
                    BeginTime = TimeSpan.FromMilliseconds(i * 500),
                    FillBehavior = FillBehavior.HoldEnd,
                    KeyFrames = frameCollection,
                    AutoReverse = true,
                };
                if (i == 0)
                {
                    //首球要向左摆动一下模仿开始撞击动作，它不需要自动反向
                    dakf.AutoReverse = false;
                    DiscreteDoubleKeyFrame dd = new DiscreteDoubleKeyFrame()
                    {
                        Value = -50,
                        KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)),
                    };
                    frameCollection.Add(dd);
                }
                //向右摆动
                SplineDoubleKeyFrame sd = new SplineDoubleKeyFrame()
                {
                    Value = 50,
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)),
                    KeySpline = new KeySpline(new Point(0, 0), new Point(0, 1)),
                };
                frameCollection.Add(sd);
                Storyboard.SetTarget(dakf, this.mainGrid.Children[i]);
                Storyboard.SetTargetProperty(dakf, new PropertyPath("(0).(1)", propertyChain));
                sb.Children.Add(dakf);
            }
            //首球返回原位
            DoubleAnimation da = new DoubleAnimation()
            {
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                BeginTime = TimeSpan.FromSeconds(1),
                FillBehavior = FillBehavior.HoldEnd,
                From = 50,
                To = 0,
            };
            Storyboard.SetTarget(da, this.mainGrid.Children[0]);
            Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)", propertyChain));
            sb.Children.Add(da);
            sb.Begin();
        }
    }
}
