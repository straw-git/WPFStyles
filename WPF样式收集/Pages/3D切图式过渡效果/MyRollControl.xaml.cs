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

namespace WPF样式收集.Pages._3D切图式过渡效果
{
    /// <summary>
    /// MyRollControl.xaml 的交互逻辑
    /// </summary>
    public partial class MyRollControl : UserControl
    {
        public MyRollControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 滚动数值
        /// </summary>
        public int n_index = 0;

        /// <summary>
        /// 图片列表
        /// </summary>
        public List<BitmapImage> ls_images;

        /// <summary>
        /// 启动
        /// </summary>
        public void Begin()
        {
            SetRollImageBtnState();
            this.ResetStory(DirectionType.right);
            this.Cube3D.GetFlipImage();
        }

        /// <summary>
        /// 初始化图片
        /// </summary>
        void ResetStory(DirectionType direction)
        {
            //向左滚动
            if (direction == DirectionType.left)
            {
                this.Cube3D.FrontImage = this.ls_images[this.n_index % this.ls_images.Count];

                if (this.n_index > 0)
                {
                    this.Cube3D.BackImage = this.ls_images[(this.n_index - 1) % this.ls_images.Count];
                }
            }
            //向右滚动
            else
            {
                this.Cube3D.FrontImage = this.ls_images[this.n_index % this.ls_images.Count];
                if (this.n_index < this.ls_images.Count - 1)
                {
                    this.Cube3D.BackImage = this.ls_images[(this.n_index + 1) % this.ls_images.Count];
                }
            }
        }

        private void ImgLeft_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetRoll(DirectionType.left);
        }

        private void ImgRight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetRoll(DirectionType.right);
        }

        /// <summary>
        /// 左右滚动逻辑
        /// </summary>
        private void SetRoll(DirectionType direction)
        {
            if (direction == DirectionType.left)
            {
                if (this.imgLeft.IsEnabled == true)
                {
                    if (!this.Cube3D.isFinished)
                    {
                        this.Cube3D.ShowAnimation();
                    }
                    else
                    {
                        this.ResetStory(direction);
                        this.Cube3D.GetFlipImage();
                        this.Cube3D.ShowAnimation();
                        this.n_index--;
                    }
                }
            }
            else
            {
                if (this.imgRight.IsEnabled == true)
                {
                    if (!this.Cube3D.isFinished)
                    {
                        this.n_index++;
                        this.Cube3D.ShowAnimation();
                    }
                    else
                    {
                        this.ResetStory(direction);
                        this.Cube3D.GetFlipImage();
                        this.Cube3D.ShowAnimation();
                        this.n_index++;
                    }
                }
            }
            SetRollImageBtnState();
        }

        /// <summary>
        /// 设置左右滚动按钮状态
        /// </summary>
        private void SetRollImageBtnState()
        {
            if (this.n_index <= 0)
            {
                this.imgLeft.IsEnabled = false;
            }
            else if (this.n_index > 0 && this.n_index < this.ls_images.Count - 1)
            {
                this.imgLeft.IsEnabled = true;
                this.imgRight.IsEnabled = true;
            }
            else if (this.n_index >= this.ls_images.Count - 1)
            {
                this.imgRight.IsEnabled = false;
            }
        }
    }

    public enum DirectionType
    {
        left,
        right
    }
}
