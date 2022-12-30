namespace SkillProfiCompany.Models
{
    public class CompanyImage
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public CompanyImage()
        {

        }

        public CompanyImage(string image)
        {
            Image = image;
        }
    }
}
