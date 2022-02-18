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

namespace WPF样式收集.Pages.圆形警示效果
{
    /// <summary>
    /// RingControl.xaml 的交互逻辑
    /// </summary>
    public partial class RingControl : UserControl
    {
        /// <summary>
        /// 圆的颜色
        /// </summary>
        public Brush EllipseStroke { get; set; }
        /// <summary>
        /// 圆环的宽度
        /// </summary>
        public double EllipseStrokeThickness { get; set; }
        /// <summary>
        /// 圆的最小尺寸
        /// </summary>
        public double MinSize { get; set; }
        /// <summary>
        /// 圆的最大尺寸
        /// </summary>
        public double MaxSize { get; set; }
        /// <summary>
        /// 总圆环数量
        /// </summary>
        public int EllipseNum { get; set; }
        /// <summary>
        /// 不同圆之间的时间间隔
        /// </summary>
        public double EllipseInterval { get; set; }
        /// <summary>
        /// 动画启动等待时间
        /// </summary>
        private double StoryBeginTime { get; set; }
        /// <summary>
        /// 动画持续时间
        /// </summary>
        public double AnimationDuration { get; set; }

        public RingControl()
        {
            InitializeComponent();
        }

        private void MyUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Ellipse centerE = new Ellipse()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = MinSize,
                Height = MinSize,
                Stroke = EllipseStroke,
                StrokeThickness = EllipseStrokeThickness,
            };
            this.mainGrid.Children.Add(centerE);
            InitAnimation();
        }

        private void InitAnimation()
        {
            for (int i = 1; i <= EllipseNum; i++)
            {
                Storyboard story = new Storyboard()
                {
                    //是否循环
                    RepeatBehavior = RepeatBehavior.Forever,
                    //动画刷新速度
                    SpeedRatio = 2
                };
                //透明度调整动作
                DoubleAnimation myOpacityAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
                };
                story.Children.Add(myOpacityAnimation);
                //宽度调整动作
                DoubleAnimation myWidthSizeChangeAnimation = new DoubleAnimation
                {
                    From = MinSize,
                    To = MaxSize,
                    Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
                };
                story.Children.Add(myWidthSizeChangeAnimation);
                //高度调整动作
                DoubleAnimation myHeightSizeChangeAnimation = new DoubleAnimation
                {
                    From = MinSize,
                    To = MaxSize,
                    Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
                };
                story.Children.Add(myHeightSizeChangeAnimation);
                //生成形状
                Ellipse tempE = new Ellipse()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = MinSize,
                    Height = MinSize,
                    Stroke = EllipseStroke,
                    StrokeThickness = EllipseStrokeThickness,
                };
                //动画赋值
                Storyboard.SetTarget(myOpacityAnimation, tempE);
                Storyboard.SetTargetProperty(myOpacityAnimation, new PropertyPath(Ellipse.OpacityProperty));
                Storyboard.SetTarget(myWidthSizeChangeAnimation, tempE);
                Storyboard.SetTarget(myHeightSizeChangeAnimation, tempE);
                Storyboard.SetTargetProperty(myWidthSizeChangeAnimation, new PropertyPath(Ellipse.WidthProperty));
                Storyboard.SetTargetProperty(myHeightSizeChangeAnimation, new PropertyPath(Ellipse.HeightProperty));

                mainGrid.Children.Add(tempE);
                //时间间隔
                StoryBeginTime += EllipseInterval;
                story.BeginTime = TimeSpan.FromMilliseconds(StoryBeginTime);
                story.Begin();
            }
        }
    }
}
