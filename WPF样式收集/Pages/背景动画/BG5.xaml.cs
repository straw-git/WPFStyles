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

namespace WPF样式收集.Pages.背景动画
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class BG5 : Page
    {
        private ParticleSystem ps;
        private Point pMouse = new Point(600, 700);
        bool unLoaded = false;

        public BG5()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 帧渲染事件
        /// </summary>
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (unLoaded) return;
            ps.ParticleRoamUpdate(pMouse);
            ps.AddOrRemoveParticleLine();
            ps.MoveParticleLine();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (unLoaded) return;
            pMouse = e.GetPosition(this.cvs_particleContainer);
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (unLoaded) return;
            pMouse = new Point(600, 700);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ps = new ParticleSystem(15, 100, 10, 100, 150, this.cvs_particleContainer, this.grid_lineContainer);
            //注册帧动画
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            unLoaded = true;
            ps.Clear();
            ps = null;
        }
    }
}
