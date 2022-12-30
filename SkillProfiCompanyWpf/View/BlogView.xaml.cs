using Microsoft.Win32;
using Prism.Commands;
using SkillProfiCompanyWpf.Interface;
using SkillProfiCompanyWpf.Models;
using SkillProfiCompanyWpf.ViewModels;
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
using System.Windows.Shapes;

namespace SkillProfiCompanyWpf.View
{
    /// <summary>
    /// Логика взаимодействия для BlogView.xaml
    /// </summary>
    public partial class BlogView : Window
    {
        private readonly ViewModel viewModel;
        public BlogView()
        {
            InitializeComponent();

            viewModel = MainWindow.viewModel;
            DataContext = viewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (BlogHeader.Text != String.Empty && BlogDescription.Text != String.Empty && BlogImage.Source != null)
            {
                try
                {
                    if (IdBlog.Content.ToString() != "-1")
                    {
                        if (BlogImage.Source != null)
                        {
                            using (System.Drawing.Image image = System.Drawing.Image.FromFile(BlogImage.Source.ToString().Remove(0, 8)))
                            {
                                using (MemoryStream m = new MemoryStream())
                                {
                                    image.Save(m, image.RawFormat);
                                    byte[] imageBytes = m.ToArray();
                                    string img = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";

                                    BlogClient blog = new BlogClient(IdBlog.Content.ToString(), BlogHeader.Text, BlogDescription.Text, DateTime.Now.ToShortDateString(), img);
                                    viewModel.SaveBlog.Execute(blog);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    BlogClient blog = new BlogClient(IdBlog.Content.ToString(), BlogHeader.Text, BlogDescription.Text, DateTime.Now.ToShortDateString(), null);
                    viewModel.SaveBlog.Execute(blog);
                }
            }
            else
            {
                e.Cancel = true;
                viewModel.BlogError.Execute("Заполнить все поля!");
            }
        }
    }
}
