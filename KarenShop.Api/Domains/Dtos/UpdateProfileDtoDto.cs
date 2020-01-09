namespace KarenShop.Api.Domains.Dtos
{
    public class UpdateProfileDtoDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string ProfilePicture { get; set; }
    }
}
