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
    /// Логика взаимодействия для ProjectView.xaml
    /// </summary>
    public partial class ProjectView : Window
    {
        private readonly ViewModel viewModel;
        public ProjectView()
        {
            InitializeComponent();

            viewModel = MainWindow.viewModel;
            DataContext = viewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ProjectHeader.Text != String.Empty && ProjectDescription.Text != String.Empty && ProjectImage.Source != null)
            {
                try
                {
                    if (IdProject.Content.ToString() != "-1")
                    {
                        if (ProjectImage.Source != null)
                        {
                            using (System.Drawing.Image image = System.Drawing.Image.FromFile(ProjectImage.Source.ToString().Remove(0, 8)))
                            {
                                using (MemoryStream m = new MemoryStream())
                                {
                                    image.Save(m, image.RawFormat);
                                    byte[] imageBytes = m.ToArray();
                                    string img = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";

                                    ProjectClient project = new ProjectClient(IdProject.Content.ToString(), ProjectHeader.Text, ProjectDescription.Text, img);
                                    viewModel.SaveProject.Execute(project);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    ProjectClient project = new ProjectClient(IdProject.Content.ToString(), ProjectHeader.Text, ProjectDescription.Text, null);
                    viewModel.SaveProject.Execute(project);
                }
            }
            else
            {
                e.Cancel = true;
                viewModel.ProjectError.Execute("Заполнить все поля!");
            }
        }
    }
}
