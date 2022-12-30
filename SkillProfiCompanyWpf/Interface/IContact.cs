using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiCompanyWpf.Interface
{
    public interface IContact
    {
        string ContactVisibilityContextMenu { get; set; }
        string ContactVisibilityCreateMenu { get; set; }
        string ContactCreateHeader { get; set; }
        string ContactId { get; set; }
        string ContactHeader { get; set; }
        string ContactValue { get; set; }
        int ContactTag { get; set; }
        string ContactError { get; set; }
        string ContactVisibilityButtonDelete { get; set; }
    }
}
