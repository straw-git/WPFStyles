using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF样式收集.Pages.水珠效果按钮组
{
    /// <summary>
    /// UCWaterDropsButtonGroup.xaml 的交互逻辑
    /// </summary>
    public partial class UCWaterDropsButtonGroup : UserControl
    {
        public UCWaterDropsButtonGroup()
        {
            InitializeComponent();
        }

        #region 自定义事件
        #region Click
        //声明和注册路由事件
        public static readonly RoutedEvent ClickRoutedEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(EventHandler<UCWaterDropsButtonGroupRoutedEventArgs>), typeof(UCWaterDropsButtonGroup));
        //CLR事件包装
        public event RoutedEventHandler Click
        {
            add { this.AddHandler(ClickRoutedEvent, value); }
            remove { this.RemoveHandler(ClickRoutedEvent, value); }
        }
        #endregion
        #endregion

        #region 自定依赖义属性
        #region ItemsSource
        public static readonly DependencyProperty ItemsSourceDependencyProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(IList<object>),
            typeof(UCWaterDropsButtonGroup),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(ItemsSourcePropertyChangedCallback)
                )
            );

        public IList<object> ItemsSource
        {
            get { return (IList<object>)GetValue(ItemsSourceDependencyProperty); }
            set { SetValue(ItemsSourceDependencyProperty, value); }
        }

        private static void ItemsSourcePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(UCWaterDropsButtonGroup))
            {
                UCWaterDropsButtonGroup uc = (UCWaterDropsButtonGroup)sender;
                uc.Init();
                uc.GenerateItem(e.NewValue as IList<object>);
            }
        }
        #endregion
        #endregion

        #region 成员变量
        /// <summary>
        /// 主体大小
        /// </summary>
        private double _size;
        /// <summary>
        /// 主体按钮圆半径
        /// </summary>
        private double _r;
        /// <summary>
        /// item大小
        /// </summary>
        private double _itemSize;
        /// <summary>
        /// item半径
        /// </summary>
        private double _itemR;
        /// <summary>
        /// item边缘到主体边缘的距离
        /// </summary>
        private double _itemDistance;
        /// <summary>
        /// item圆心到主体圆心的距离
        /// </summary>
        private double _itemCenterDistance;
        /// <summary>
        /// item移动时间
        /// </summary>
        private int _itemMoveTime;
        /// <summary>
        /// item延迟移动总时间
        /// </summary>
        private int _itemMoveDelay;
        /// <summary>
        /// 主体按钮选中执行动画
        /// </summary>
        private Storyboard _checkedItemMoveSB = new Storyboard();
        /// <summary>
        /// 主体按钮非选中执行动画
        /// </summary>
        private Storyboard _uncheckedItemMoveSB = new Storyboard();
        #endregion

        #region 事件
        /// <summary>
        /// 主体按钮选中
        /// </summary>
        private void tbtn_main_Checked(object sender, RoutedEventArgs e)
        {
            _checkedItemMoveSB.Begin(this);
        }

        /// <summary>
        /// 主体按钮非选中
        /// </summary>
        private void tbtn_main_Unchecked(object sender, RoutedEventArgs e)
        {
            _uncheckedItemMoveSB.Begin(this);
        }

        /// <summary>
        /// 主体按钮点击
        /// </summary>
        private void tbtn_main_Click(object sender, RoutedEventArgs e)
        {
            UCWaterDropsButtonGroupRoutedEventArgs args = new UCWaterDropsButtonGroupRoutedEventArgs(ClickRoutedEvent, this);
            args.Index = -1;
            RaiseEvent(args);
        }

        /// <summary>
        /// 按钮点击
        /// </summary>
        private void layout_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(Button))
            {
                Button btn = e.OriginalSource as Button;
                UCWaterDropsButtonGroupRoutedEventArgs args = new UCWaterDropsButtonGroupRoutedEventArgs(ClickRoutedEvent, this);
                args.Index = int.Parse(btn.Name.Substring(4, 1));
                RaiseEvent(args);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            _size = Width;
            _r = _size / 2;
            _itemSize = _size / 2;
            _itemR = _itemSize / 2;
            _itemDistance = _itemSize / 2;
            _itemCenterDistance = _r + _itemR + _itemDistance;
            _itemMoveTime = 500;
            _itemMoveDelay = 500;
        }

        /// <summary>
        /// 生成item
        /// </summary>
        private void GenerateItem(IList<object> itemsSource)
        {
            //清除
            itemLayout.Children.Clear();
            _checkedItemMoveSB.Children.Clear();
            _uncheckedItemMoveSB.Children.Clear();
            if (itemsSource == null)
            {
                return;
            }

            //每个item延迟移动时间
            int perDelay = _itemMoveDelay / (itemsSource.Count - 1);
            //直角三角形角A对边a,临边b,斜边c
            double a, b, c;//item位置三角形
            double a1, b1, c1;//join位置三角形
            double b2;//join高度三角形

            c = _itemCenterDistance;
            b2 = Math.Sqrt(_r * _r - _itemR * _itemR);
            double d = _r - b2;//图中蓝色的d
            double joinWidth = _itemSize;//join容器宽
            double joinHeight = _itemR + _itemDistance + d;//join容器高
            c1 = _itemCenterDistance - joinHeight / 2;

            for (int i = 0; i < itemsSource.Count; i++)
            {
                //连接
                //容器
                a1 = c1 * Math.Sin(2 * Math.PI / 8 * i - (itemsSource.Count - 1) * 2 * Math.PI / 8 / 2);
                b1 = c1 * Math.Cos(2 * Math.PI / 8 * i - (itemsSource.Count - 1) * 2 * Math.PI / 8 / 2);
                Grid joinContainer = GetJoinContainer(itemsSource, i, joinWidth, joinHeight, a1, b1);
                itemLayout.Children.Add(joinContainer);

                //连接动画
                int joinTime = (int)((c - b2) / c * _itemMoveTime);
                int joinDelay = (int)(b2 / c * _itemMoveTime);
                SetJoinAnimation(joinContainer, perDelay * i, joinTime, joinDelay);

                //item按钮
                Button btn = GetItemButton(itemsSource, i);
                itemLayout.Children.Add(btn);
                //item移动动画
                a = c * Math.Sin(2 * Math.PI / 8 * i - (itemsSource.Count - 1) * 2 * Math.PI / 8 / 2);
                b = c * Math.Cos(2 * Math.PI / 8 * i - (itemsSource.Count - 1) * 2 * Math.PI / 8 / 2);
                SetItemMoveAnimation(btn, a, b, perDelay * i);
                //item透明动画
                SetItemOpacityAnimation(btn, perDelay * i);
            }
        }

        /// <summary>
        /// 获取item按钮
        /// </summary>
        /// <param name="itemsSource">数据源</param>
        /// <param name="i">index</param>
        /// <returns>button</returns>
        private Button GetItemButton(IList<object> itemsSource, int i)
        {
            Button btn = new Button()
            {
                Name = $"btn_{i}",
                Width = _itemSize,
                Height = _itemSize,
                Content = itemsSource[i],
                Opacity = 0,//在圆心时,透明
                IsHitTestVisible = false,//在圆心时,按钮不能点击
                RenderTransform = new TranslateTransform() { X = 0, Y = 0 },
                Style = Resources["UCWaterDropsButtonGroupButtonStyle"] as Style
            };

            return btn;
        }

        /// <summary>
        /// 设置item移动动画
        /// </summary>
        /// <param name="item">容器</param>
        /// <param name="a">直角三角形角A对边</param>
        /// <param name="b">直角三角形角A邻边</param>
        /// <param name="delay">延迟执行时间</param>
        private void SetItemMoveAnimation(UIElement item, double a, double b, int delay)
        {
            //checked
            //X轴
            DoubleAnimation checkedDaMX = new DoubleAnimation() { To = a, Duration = new TimeSpan(0, 0, 0, 0, _itemMoveTime), BeginTime = new TimeSpan(0, 0, 0, 0, delay) };
            Storyboard.SetTarget(checkedDaMX, item);
            Storyboard.SetTargetProperty(checkedDaMX, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
            _checkedItemMoveSB.Children.Add(checkedDaMX);
            //Y轴
            DoubleAnimation checkedDaMY = new DoubleAnimation() { To = -b, Duration = new TimeSpan(0, 0, 0, 0, _itemMoveTime), BeginTime = new TimeSpan(0, 0, 0, 0, delay) };
            Storyboard.SetTarget(checkedDaMY, item);
            Storyboard.SetTargetProperty(checkedDaMY, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
            _checkedItemMoveSB.Children.Add(checkedDaMY);

            //unchecked
            //X轴
            DoubleAnimation uncheckedDaMX = new DoubleAnimation() { To = 0, Duration = new TimeSpan(0, 0, 0, 0, _itemMoveTime), BeginTime = new TimeSpan(0, 0, 0, 0, delay) };
            Storyboard.SetTarget(uncheckedDaMX, item);
            Storyboard.SetTargetProperty(uncheckedDaMX, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
            _uncheckedItemMoveSB.Children.Add(uncheckedDaMX);
            //Y轴
            DoubleAnimation uncheckedDaMY = new DoubleAnimation() { To = 0, Duration = new TimeSpan(0, 0, 0, 0, _itemMoveTime), BeginTime = new TimeSpan(0, 0, 0, 0, delay) };
            Storyboard.SetTarget(uncheckedDaMY, item);
            Storyboard.SetTargetProperty(uncheckedDaMY, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
            _uncheckedItemMoveSB.Children.Add(uncheckedDaMY);
        }

        /// <summary>
        /// 设置item透明动画
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="delay">延迟执行时间</param>
        private void SetItemOpacityAnimation(UIElement item, int delay)
        {
            //item圆心在主体圆内部时,透明渐变,在外部为1
            //item圆心在主体圆内部的时间
            int time = (int)(_r / _itemCenterDistance * _itemMoveTime);
            //item圆心在主体圆外部的时间
            int uncheckedDelay = (int)((_itemCenterDistance - _r) / _itemCenterDistance * _itemMoveTime);
            //checked
            DoubleAnimation checkedDaO = new DoubleAnimation() { To = 1, Duration = new TimeSpan(0, 0, 0, 0, time), BeginTime = new TimeSpan(0, 0, 0, 0, delay) };
            Storyboard.SetTarget(checkedDaO, item);
            Storyboard.SetTargetProperty(checkedDaO, new PropertyPath(OpacityProperty));
            _checkedItemMoveSB.Children.Add(checkedDaO);
            //在圆外时,按钮能点击
            checkedDaO.Completed += (sender, e) => { item.IsHitTestVisible = true; };
            //unchecked
            DoubleAnimation uncheckedDaO = new DoubleAnimation() { To = 0, Duration = new TimeSpan(0, 0, 0, 0, time), BeginTime = new TimeSpan(0, 0, 0, 0, delay + uncheckedDelay) };
            Storyboard.SetTarget(uncheckedDaO, item);
            Storyboard.SetTargetProperty(uncheckedDaO, new PropertyPath(OpacityProperty));
            _uncheckedItemMoveSB.Children.Add(uncheckedDaO);
            //在圆心时,按钮不能点击
            uncheckedDaO.Completed += (sender, e) => { item.IsHitTestVisible = false; };
        }

        /// <summary>
        /// 获取连接容器
        /// </summary>
        /// <param name="itemsSource">数据源</param>
        /// <param name="i">index</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="a">直角三角形角A对边</param>
        /// <param name="b">直角三角形角A邻边</param>
        /// <returns>Grid</returns>
        private Grid GetJoinContainer(IList<object> itemsSource, int i, double width, double height, double a, double b)
        {
            //容器
            Grid joinContainer = new Grid()
            {
                Width = width,
                Height = height,
                IsHitTestVisible = false,
                RenderTransformOrigin = new Point(0.5, 0.5)
            };
            TransformGroup tfg = new TransformGroup();
            tfg.Children.Add(new RotateTransform() { Angle = 45 * i - (itemsSource.Count - 1) * 22.5 });
            tfg.Children.Add(new TranslateTransform() { X = a, Y = -b });
            joinContainer.RenderTransform = tfg;
            //path
            Path join = new Path() { Fill = new SolidColorBrush(Color.FromRgb(27, 218, 238)), VerticalAlignment = VerticalAlignment.Bottom };
            PathFigure pf = new PathFigure() { StartPoint = new Point(joinContainer.Width, 0) };
            pf.Segments.Add(new QuadraticBezierSegment() { Point1 = new Point(joinContainer.Width, 0), Point2 = new Point(joinContainer.Width, 0) });
            pf.Segments.Add(new LineSegment() { Point = new Point(0, 0) });
            pf.Segments.Add(new QuadraticBezierSegment() { Point1 = new Point(0, 0), Point2 = new Point(0, 0) });
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            join.Data = pg;

            joinContainer.Children.Add(join);

            return joinContainer;
        }

        /// <summary>
        /// 设置连接动画
        /// </summary>
        /// <param name="joinContainer">容器</param>
        /// <param name="delay">item延迟时间</param>
        /// <param name="joinTime">连接时间</param>
        /// <param name="joinDelay">连接延迟时间</param>
        private void SetJoinAnimation(Grid joinContainer, int delay, int joinTime, int joinDelay)
        {
            //连接动画
            Path join = joinContainer.Children[0] as Path;
            //checked
            //位置0贝塞尔曲线
            //point1
            PointAnimation checkedPa01 = new PointAnimation() { To = new Point(0, joinContainer.Height * 5 / 6), Duration = new TimeSpan(0, 0, 0, 0, joinTime), BeginTime = new TimeSpan(0, 0, 0, 0, joinDelay + delay) };
            Storyboard.SetTarget(checkedPa01, join);
            Storyboard.SetTargetProperty(checkedPa01, new PropertyPath("(Path.Data).(PathGeometry.Figures)[0].(PathFigure.Segments)[0].(QuadraticBezierSegment.Point1)"));
            _checkedItemMoveSB.Children.Add(checkedPa01);
            //point2
            PointAnimation checkedPa02 = new PointAnimation() { To = new Point(joinContainer.Width, joinContainer.Height), Duration = new TimeSpan(0, 0, 0, 0, joinTime), BeginTime = new TimeSpan(0, 0, 0, 0, joinDelay + delay) };
            Storyboard.SetTarget(checkedPa02, join);
            Storyboard.SetTargetProperty(checkedPa02, new PropertyPath("(Path.Data).(PathGeometry.Figures)[0].(PathFigure.Segments)[0].(QuadraticBezierSegment.Point2)"));
            _checkedItemMoveSB.Children.Add(checkedPa02);
            //位置1直线
            //point
            PointAnimation checkedPa1 = new PointAnimation() { To = new Point(0, joinContainer.Height), Duration = new TimeSpan(0, 0, 0, 0, joinTime), BeginTime = new TimeSpan(0, 0, 0, 0, joinDelay + delay) };
            Storyboard.SetTarget(checkedPa1, join);
            Storyboard.SetTargetProperty(checkedPa1, new PropertyPath("(Path.Data).(PathGeometry.Figures)[0].(PathFigure.Segments)[1].(LineSegment.Point)"));
            _checkedItemMoveSB.Children.Add(checkedPa1);
            //位置2贝塞尔曲线
            //point1
            PointAnimation checkedPa21 = new PointAnimation() { To = new Point(joinContainer.Width, joinContainer.Height * 5 / 6), Duration = new TimeSpan(0, 0, 0, 0, joinTime), BeginTime = new TimeSpan(0, 0, 0, 0, joinDelay + delay) };
            Storyboard.SetTarget(checkedPa21, join);
            Storyboard.SetTargetProperty(checkedPa21, new PropertyPath("(Path.Data).(PathGeometry.Figures)[0].(PathFigure.Segments)[2].(QuadraticBezierSegment.Point1)"));
            _checkedItemMoveSB.Children.Add(checkedPa21);
            //动画完隐藏
            DoubleAnimation checkedDa = new DoubleAnimation() { To = 0, Duration = new TimeSpan(0), BeginTime = new TimeSpan(0, 0, 0, 0, joinTime + joinDelay + delay) };
            Storyboard.SetTarget(checkedDa, join);
            Storyboard.SetTargetProperty(checkedDa, new PropertyPath(OpacityProperty));
            _checkedItemMoveSB.Children.Add(checkedDa);
            //unchecked
            //动画前先展示
            DoubleAnimation uncheckedDa = new DoubleAnimation() { To = 1, Duration = new TimeSpan(0), BeginTime = new TimeSpan(0, 0, 0, 0, delay) };
            Storyboard.SetTarget(uncheckedDa, join);
            Storyboard.SetTargetProperty(uncheckedDa, new PropertyPath(OpacityProperty));
            _uncheckedItemMoveSB.Children.Add(uncheckedDa);
            //位置0贝塞尔曲线
            //point1
            PointAnimation uncheckedPa01 = new PointAnimation() { To = new Point(joinContainer.Width, 0), Duration = new TimeSpan(0, 0, 0, 0, joinTime), BeginTime = new TimeSpan(0, 0, 0, 0, delay) };
            Storyboard.SetTarget(uncheckedPa01, join);
            Storyboard.SetTargetProperty(uncheckedPa01, new PropertyPath("(Path.Data).(PathGeometry.Figures)[0].(PathFigure.Segments)[0].(QuadraticBezierSegment.Point1)"));
            _uncheckedItemMoveSB.Children.Add(uncheckedPa01);
            //point2
            PointAnimation uncheckedPa02 = new PointAnimation() { To = new Point(joinContainer.Width, 0), Duration = new TimeSpan(0, 0, 0, 0, joinTime), BeginTime = new TimeSpan(0, 0, 0, 0, delay) };
            Storyboard.SetTarget(uncheckedPa02, join);
            Storyboard.SetTargetProperty(uncheckedPa02, new PropertyPath("(Path.Data).(PathGeometry.Figures)[0].(PathFigure.Segments)[0].(QuadraticBezierSegment.Point2)"));
            _uncheckedItemMoveSB.Children.Add(uncheckedPa02);
            //位置1直线
            //point
            PointAnimation uncheckedPa1 = new PointAnimation() { To = new Point(0, 0), Duration = new TimeSpan(0, 0, 0, 0, joinTime), BeginTime = new TimeSpan(0, 0, 0, 0, delay) };
            Storyboard.SetTarget(uncheckedPa1, join);
            Storyboard.SetTargetProperty(uncheckedPa1, new PropertyPath("(Path.Data).(PathGeometry.Figures)[0].(PathFigure.Segments)[1].(LineSegment.Point)"));
            _uncheckedItemMoveSB.Children.Add(uncheckedPa1);
            //位置2贝塞尔曲线
            //point1
            PointAnimation uncheckedPa21 = new PointAnimation() { To = new Point(0, 0), Duration = new TimeSpan(0, 0, 0, 0, joinTime), BeginTime = new TimeSpan(0, 0, 0, 0, delay) };
            Storyboard.SetTarget(uncheckedPa21, join);
            Storyboard.SetTargetProperty(uncheckedPa21, new PropertyPath("(Path.Data).(PathGeometry.Figures)[0].(PathFigure.Segments)[2].(QuadraticBezierSegment.Point1)"));
            _uncheckedItemMoveSB.Children.Add(uncheckedPa21);
        }
        #endregion
    }

    /// <summary>
    /// 自定义事件参数类
    /// </summary>
    public class UCWaterDropsButtonGroupRoutedEventArgs : RoutedEventArgs
    {
        public UCWaterDropsButtonGroupRoutedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }

        public int Index { get; set; }
    }
}
