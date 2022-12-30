using System.Collections.Generic;

namespace SkillProfiCompany.Interfaces
{
    public interface IFilter<T> where T : class
    {
        public List<T> Filter();
    }
}
