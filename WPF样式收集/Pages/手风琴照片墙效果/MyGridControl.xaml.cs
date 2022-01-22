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

namespace WPF样式收集.Pages.手风琴照片墙效果
{
    /// <summary>
    /// MyGridControl.xaml 的交互逻辑
    /// </summary>
    public partial class MyGridControl : UserControl
    {
        private Storyboard storyboard = new Storyboard();

        public MyGridControl()
        {
            InitializeComponent();
        }

        private void Grid00_MouseEnter(object sender, MouseEventArgs e)
        {
            storyboard.Children.Clear();

            //将上侧行高扩大为2*，同时将下侧行高缩小至1*
            if (this.cellTop.Height != new GridLength(2, GridUnitType.Star))
            {
                GridLengthAnimation cellTopAnimation = this.FindResource("cellTopAnimation") as GridLengthAnimation;
                if (cellTopAnimation != null)
                {
                    cellTopAnimation.To = new GridLength(2, GridUnitType.Star);
                    storyboard.Children.Add(cellTopAnimation);
                    GridLengthAnimation cellBotAnimation = this.FindResource("cellBotAnimation") as GridLengthAnimation;
                    if (cellBotAnimation != null)
                    {
                        cellBotAnimation.To = new GridLength(1, GridUnitType.Star);
                        storyboard.Children.Add(cellBotAnimation);
                    }
                }
            }
            //将左侧列宽扩大为2*，同时将右侧列宽缩小至1*
            if (this.cellLeft.Width != new GridLength(2, GridUnitType.Star))
            {
                GridLengthAnimation cellLeftAnimation = this.FindResource("cellLeftAnimation") as GridLengthAnimation;
                if (cellLeftAnimation != null)
                {
                    cellLeftAnimation.To = new GridLength(2, GridUnitType.Star);
                    storyboard.Children.Add(cellLeftAnimation);
                    GridLengthAnimation cellRightAnimation = this.FindResource("cellRightAnimation") as GridLengthAnimation;
                    if (cellRightAnimation != null)
                    {
                        cellRightAnimation.To = new GridLength(1, GridUnitType.Star);
                        storyboard.Children.Add(cellRightAnimation);
                    }
                }
            }

            storyboard.Begin(this);
        }

        private void Grid01_MouseEnter(object sender, MouseEventArgs e)
        {
            storyboard.Children.Clear();

            //将上侧行高扩大为2*，同时将下侧行高缩小至1*
            if (this.cellTop.Height != new GridLength(2, GridUnitType.Star))
            {
                GridLengthAnimation cellTopAnimation = this.FindResource("cellTopAnimation") as GridLengthAnimation;
                if (cellTopAnimation != null)
                {
                    cellTopAnimation.To = new GridLength(2, GridUnitType.Star);
                    storyboard.Children.Add(cellTopAnimation);
                    GridLengthAnimation cellBotAnimation = this.FindResource("cellBotAnimation") as GridLengthAnimation;
                    if (cellBotAnimation != null)
                    {
                        cellBotAnimation.To = new GridLength(1, GridUnitType.Star);
                        storyboard.Children.Add(cellBotAnimation);
                    }
                }
            }
            //将右侧列宽扩大为2*，同时将左侧列宽缩小至1*
            if (this.cellRight.Width != new GridLength(2, GridUnitType.Star))
            {
                GridLengthAnimation cellRightAnimation = this.FindResource("cellRightAnimation") as GridLengthAnimation;
                if (cellRightAnimation != null)
                {
                    cellRightAnimation.To = new GridLength(2, GridUnitType.Star);
                    storyboard.Children.Add(cellRightAnimation);
                    GridLengthAnimation cellLeftAnimation = this.FindResource("cellLeftAnimation") as GridLengthAnimation;
                    if (cellLeftAnimation != null)
                    {
                        cellLeftAnimation.To = new GridLength(1, GridUnitType.Star);
                        storyboard.Children.Add(cellLeftAnimation);
                    }
                }
            }

            storyboard.Begin(this);
        }

        private void Grid10_MouseEnter(object sender, MouseEventArgs e)
        {
            storyboard.Children.Clear();

            //将下侧行高扩大为2*，同时将上侧行高缩小至1*
            if (this.cellBot.Height != new GridLength(2, GridUnitType.Star))
            {
                GridLengthAnimation cellBotAnimation = this.FindResource("cellBotAnimation") as GridLengthAnimation;
                if (cellBotAnimation != null)
                {
                    cellBotAnimation.To = new GridLength(2, GridUnitType.Star);
                    storyboard.Children.Add(cellBotAnimation);
                    GridLengthAnimation cellTopAnimation = this.FindResource("cellTopAnimation") as GridLengthAnimation;
                    if (cellTopAnimation != null)
                    {
                        cellTopAnimation.To = new GridLength(1, GridUnitType.Star);
                        storyboard.Children.Add(cellTopAnimation);
                    }
                }
            }
            //将左侧列宽扩大为2*，同时将右侧列宽缩小至1*
            if (this.cellLeft.Width != new GridLength(2, GridUnitType.Star))
            {
                GridLengthAnimation cellLeftAnimation = this.FindResource("cellLeftAnimation") as GridLengthAnimation;
                if (cellLeftAnimation != null)
                {
                    cellLeftAnimation.To = new GridLength(2, GridUnitType.Star);
                    storyboard.Children.Add(cellLeftAnimation);
                    GridLengthAnimation cellRightAnimation = this.FindResource("cellRightAnimation") as GridLengthAnimation;
                    if (cellRightAnimation != null)
                    {
                        cellRightAnimation.To = new GridLength(1, GridUnitType.Star);
                        storyboard.Children.Add(cellRightAnimation);
                    }
                }
            }

            storyboard.Begin(this);
        }

        private void Grid11_MouseEnter(object sender, MouseEventArgs e)
        {
            storyboard.Children.Clear();

            //将下侧行高扩大为2*，同时将上侧行高缩小至1*
            if (this.cellBot.Height != new GridLength(2, GridUnitType.Star))
            {
                GridLengthAnimation cellBotAnimation = this.FindResource("cellBotAnimation") as GridLengthAnimation;
                if (cellBotAnimation != null)
                {
                    cellBotAnimation.To = new GridLength(2, GridUnitType.Star);
                    storyboard.Children.Add(cellBotAnimation);
                    GridLengthAnimation cellTopAnimation = this.FindResource("cellTopAnimation") as GridLengthAnimation;
                    if (cellTopAnimation != null)
                    {
                        cellTopAnimation.To = new GridLength(1, GridUnitType.Star);
                        storyboard.Children.Add(cellTopAnimation);
                    }
                }
            }
            //将右侧列宽扩大为2*，同时将左侧列宽缩小至1*
            if (this.cellRight.Width != new GridLength(2, GridUnitType.Star))
            {
                GridLengthAnimation cellRightAnimation = this.FindResource("cellRightAnimation") as GridLengthAnimation;
                if (cellRightAnimation != null)
                {
                    cellRightAnimation.To = new GridLength(2, GridUnitType.Star);
                    storyboard.Children.Add(cellRightAnimation);
                    GridLengthAnimation cellLeftAnimation = this.FindResource("cellLeftAnimation") as GridLengthAnimation;
                    if (cellLeftAnimation != null)
                    {
                        cellLeftAnimation.To = new GridLength(1, GridUnitType.Star);
                        storyboard.Children.Add(cellLeftAnimation);
                    }
                }
            }

            storyboard.Begin(this);
        }

        private void LayoutGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            storyboard.Children.Clear();

            //行高判断
            if (this.cellTop.Height != new GridLength(1, GridUnitType.Star))
            {
                GridLengthAnimation cellTopAnimation = this.FindResource("cellTopAnimation") as GridLengthAnimation;
                if (cellTopAnimation != null)
                {
                    cellTopAnimation.From = new GridLength(2, GridUnitType.Star);
                    cellTopAnimation.To = new GridLength(1, GridUnitType.Star);
                    storyboard.Children.Add(cellTopAnimation);
                }
            }
            if (this.cellBot.Height != new GridLength(1, GridUnitType.Star))
            {
                GridLengthAnimation cellBotAnimation = this.FindResource("cellBotAnimation") as GridLengthAnimation;
                if (cellBotAnimation != null)
                {
                    cellBotAnimation.From = new GridLength(2, GridUnitType.Star);
                    cellBotAnimation.To = new GridLength(1, GridUnitType.Star);
                    storyboard.Children.Add(cellBotAnimation);
                }
            }
            //列宽判断
            if (this.cellLeft.Width != new GridLength(1, GridUnitType.Star))
            {
                GridLengthAnimation cellLeftAnimation = this.FindResource("cellLeftAnimation") as GridLengthAnimation;
                if (cellLeftAnimation != null)
                {
                    cellLeftAnimation.From = new GridLength(2, GridUnitType.Star);
                    cellLeftAnimation.To = new GridLength(1, GridUnitType.Star);
                    storyboard.Children.Add(cellLeftAnimation);
                }
            }
            if (this.cellRight.Width != new GridLength(1, GridUnitType.Star))
            {
                GridLengthAnimation cellRightAnimation = this.FindResource("cellRightAnimation") as GridLengthAnimation;
                if (cellRightAnimation != null)
                {
                    cellRightAnimation.From = new GridLength(2, GridUnitType.Star);
                    cellRightAnimation.To = new GridLength(1, GridUnitType.Star);
                    storyboard.Children.Add(cellRightAnimation);
                }
            }

            storyboard.Begin(this);
        }
    }
}
