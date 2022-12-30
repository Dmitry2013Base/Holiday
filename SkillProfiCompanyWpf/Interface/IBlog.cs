using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SkillProfiCompanyWpf.Interface
{
    public interface IBlog
    {
        string BlogTitle { get; set; }
        int BlogId { get; set; }
        string BlogHeader { get; set; }
        string BlogDescription { get; set; }
        string BlogShortDescription { get; set; }
        BitmapImage BlogImage { get; set; }
        string BlogFileName { get; set; }
        string BlogVisibility { get; set; }
        bool BlogIsReadOnly { get; set; }
        string BlogVisibilityContextMenu { get; set; }
        string BlogError { get; set; }
    }
}
