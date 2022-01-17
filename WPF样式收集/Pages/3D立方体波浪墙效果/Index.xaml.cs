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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF样式收集.Pages._3D立方体波浪墙效果
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : Page
    {
        private ParticleSystem _ps;
        private DispatcherTimer _frameTimer;
        private Point pMouse = new Point(9999, 9999);

        public Index()
        {
            InitializeComponent();

            _frameTimer = new DispatcherTimer();
            _frameTimer.Tick += OnFrame;
            _frameTimer.Interval = TimeSpan.FromMilliseconds(100);
            _frameTimer.Start();

            _ps = new ParticleSystem(30, 30, Colors.White);
            WorldModels.Children.Add(_ps.ParticleModel);
            _ps.SpawnParticle(50);
        }

        private void OnFrame(object sender, EventArgs e)
        {
            _ps.Update(pMouse);
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseposition = e.GetPosition(myViewport);
            PointHitTestParameters pointparams = new PointHitTestParameters(mouseposition);
            VisualTreeHelper.HitTest(myViewport, null, HTResult, pointparams);
        }

        /// <summary>
        /// 获取鼠标在场景中的3D坐标
        /// </summary>
        public HitTestResultBehavior HTResult(System.Windows.Media.HitTestResult rawresult)
        {
            RayHitTestResult rayResult = rawresult as RayHitTestResult;
            if (rayResult != null)
            {
                RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;
                if (rayMeshResult != null)
                {
                    GeometryModel3D hitgeo = rayMeshResult.ModelHit as GeometryModel3D;
                    MeshGeometry3D hitmesh = hitgeo.Geometry as MeshGeometry3D;
                    Point3D p1 = hitmesh.Positions.ElementAt(rayMeshResult.VertexIndex1);
                    double weight1 = rayMeshResult.VertexWeight1;
                    Point3D p2 = hitmesh.Positions.ElementAt(rayMeshResult.VertexIndex2);
                    double weight2 = rayMeshResult.VertexWeight2;
                    Point3D p3 = hitmesh.Positions.ElementAt(rayMeshResult.VertexIndex3);
                    double weight3 = rayMeshResult.VertexWeight3;
                    Point3D prePoint = new Point3D(p1.X * weight1 + p2.X * weight2 + p3.X * weight3, p1.Y * weight1 + p2.Y * weight2 + p3.Y * weight3, p1.Z * weight1 + p2.Z * weight2 + p3.Z * weight3);
                    pMouse = new Point(prePoint.X, prePoint.Y);
                }
            }
            return HitTestResultBehavior.Continue;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            pMouse = new Point(9999, 9999);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            //避免页面关闭后资源占用
            _frameTimer.Stop();
            _frameTimer.Tick -= OnFrame;
            _frameTimer = null;
            _ps = null;
        }
    }
}
