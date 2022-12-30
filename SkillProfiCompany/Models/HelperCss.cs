using SkillProfiCompany.DatabaseContext;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace SkillProfiCompany.Models
{
    public class HelperCss
    {
        public async static Task CreateFileProjectCss()
        {
            using (StreamWriter writer = new StreamWriter(@"..\SkillProfiCompany\wwwroot\Styles\ProjectImageStyle.css", false))
            {
                using (DatabaseCont db = new DatabaseCont())
                {
                    List<string> images = new List<string>();
                    db.Projects.ToList().ForEach(e => { images.Add(e.ImageProject); });

                    var sb = new StringBuilder();

                    for (int i = 0; i < images.Count; i++)
                    {
                        sb.Append(" .image-list-" + i +
                            "{min-height: 100%;" +
                            "background-image: url(" + images[i] + ");" +
                            "background-position: center;" +
                            "background-size: cover;" +
                            "background-color: gray;}");
                    }

                    await writer.WriteLineAsync(sb);
                }
            }
        }

        public async static Task CreateFileBlogCss()
        {
            using (StreamWriter writer = new StreamWriter(@"..\SkillProfiCompany\wwwroot\Styles\BlogImageStyle.css", false))
            {
                using (DatabaseCont db = new DatabaseCont())
                {
                    List<string> images = new List<string>();
                    db.Blogs.ToList().ForEach(e => { images.Add(e.ImageBlog); });

                    var sb = new StringBuilder();

                    for (int i = 0; i < images.Count; i++)
                    {
                        sb.Append(" .image-list-" + i +
                            "{min-height: 100%;" +
                            "background-image: url(" + images[i] + ");" +
                            "background-position: center;" +
                            "background-size: cover;" +
                            "background-color: gray;}");
                    }

                    await writer.WriteLineAsync(sb);
                }
            }
        }
    }
}
