using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiCompanyWpf.Interface
{
    public interface IService
    {
        int ServiceId { get; set; }
        string ServiceHeader { get; set; }
        string ServiceDescription { get; set; }
        string ServiceVisibilityButtonCancel { get; set; }
        string ServiceVisibilityContextMenu { get; set; }
        string ServiceError { get; set; }
    }
}
