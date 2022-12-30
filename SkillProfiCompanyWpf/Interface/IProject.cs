using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SkillProfiCompanyWpf.Interface
{
    public interface IProject
    {
        string ProjectTitle { get; set; }
        int ProjectId { get; set; }
        string ProjectHeader { get; set; }
        string ProjectDescription { get; set; }
        BitmapImage ProjectImage { get; set; }
        string ProjectFileName { get; set; }
        string ProjectVisibility { get; set; }
        bool ProjectIsReadOnly { get; set; }
        string ProjectVisibilityContextMenu { get; set; }
        string ProjectError { get; set; }
    }
}
