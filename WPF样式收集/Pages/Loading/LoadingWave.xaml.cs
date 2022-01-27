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
using System.Windows.Threading;

namespace WPF样式收集.Pages.Loading
{
    /// <summary>
    /// LoadingWave.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingWave : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(LoadingWave), new PropertyMetadata(null));
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private DispatcherTimer frameTimer;
        private StreamGeometry myPathGeometry;

        public LoadingWave()
        {
            InitializeComponent();

            CompositionTarget.Rendering += UpdateRectangle;

            frameTimer = new DispatcherTimer();
            frameTimer.Tick += OnFrame;
            frameTimer.Interval = TimeSpan.FromSeconds(3.0 / 60.0);
            frameTimer.Start();
        }

        private void OnFrame(object sender, EventArgs e)
        {
            this.GeometryCanvas.Children.Clear();
            if (Value > 100)
            {
                if (frameTimer != null)
                    frameTimer.Stop();
            }
            Value++;
            myPathGeometry = GetSinGeometry(this.Width, -5, 1 / 30.0, -this.Width + this.Width * Value / 100, this.Height - this.Height * Value / 100);
            GeometryGroup group = new GeometryGroup();
            group.Children.Add(myPathGeometry);

            Path myPath = new Path();
            myPath.Fill = Brushes.Transparent;
            myPath.Data = group;

            this.GeometryCanvas.Children.Add(myPath);
        }

        private void UpdateRectangle(object sender, EventArgs e)
        {
            this.GeometryText.Clip = myPathGeometry;
        }
        /// <summary>
        /// 得到正弦曲线
        /// </summary>
        /// <param name="waveWidth">水纹宽度</param>
        /// <param name="waveA">水纹振幅</param>
        /// <param name="waveW">水纹周期</param>
        /// <param name="offsetX">位移</param>
        /// <param name="currentK">当前波浪高度</param>
        /// <returns></returns>
        public StreamGeometry GetSinGeometry(double waveWidth, double waveA, double waveW, double offsetX, double currentK)
        {
            StreamGeometry g = new StreamGeometry();
            using (StreamGeometryContext ctx = g.Open())
            {
                ctx.BeginFigure(new Point(0, 0), true, true);
                for (int x = 0; x < waveWidth; x += 1)
                {
                    double y = waveA * Math.Sin(x * waveW + offsetX) + currentK;
                    ctx.LineTo(new Point(x, y), true, true);
                }
                ctx.LineTo(new Point(waveWidth, 0), true, true);
            }
            return g;
        }
    }
}
