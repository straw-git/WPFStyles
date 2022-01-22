using System;
using System.Collections.Generic;
using System.IO;
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
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : Page
    {
        public Index()
        {
            InitializeComponent();

            List<BitmapImage> ls_adv_img = new List<BitmapImage>();
            List<string> listAdv = GetUserImages(AppDomain.CurrentDomain.BaseDirectory+ "Images/3D切图式过渡效果");
            foreach (string a in listAdv)
            {
                BitmapImage img = new BitmapImage(new Uri(a));
                ls_adv_img.Add(img);
            }
            this.rollImg.ls_images = ls_adv_img;
            this.rollImg.Begin();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 获取当前用户的图片文件夹中的图片路径列表(不包含子文件夹)
        /// </summary>
        private List<string> GetUserImages(string path)
        {
            List<string> images = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = GetPicFiles(path, "*.jpg,*.png,*.bmp,", SearchOption.TopDirectoryOnly);

            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    images.Add(file.FullName);
                }
            }
            return images;
        }

        private FileInfo[] GetPicFiles(string picPath, string searchPattern, SearchOption searchOption)
        {
            List<FileInfo> ltList = new List<FileInfo>();
            DirectoryInfo dir = new DirectoryInfo(picPath);
            string[] sPattern = searchPattern.Replace(';', ',').Split(',');
            for (int i = 0; i < sPattern.Length; i++)
            {
                FileInfo[] files = null;
                try
                {
                    files = dir.GetFiles(sPattern[i], searchOption);
                }
                catch
                {
                    files = new FileInfo[] { };
                }

                ltList.AddRange(files);
            }
            return ltList.ToArray();
        }
    }
}
