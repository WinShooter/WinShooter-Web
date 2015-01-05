namespace WinShooter.Api
{
    using WinShooter.Database;

    public class UserResponse
    {
        public UserResponse()
        {
        }

        public UserResponse(User user)
        {
            this.UserId = user.Id.ToString();
            this.CardNumber = user.CardNumber;
            this.ClubId = user.ClubId.ToString();
            this.DisplayName = user.DisplayName;
            this.Email = user.Email;
            this.Givenname = user.Givenname;
            this.HasAcceptedTerms = user.HasAcceptedTerms;
            this.IsSystemAdmin = user.IsSystemAdmin;
            this.LastUpdated = user.LastUpdated.ToUniversalTime().ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z"); ;
            this.Surname = user.Surname;
        }

        public string UserId { get; set; }
        public string CardNumber { get; set; }
        public string ClubId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Givenname { get; set; }
        public int HasAcceptedTerms { get; set; }
        public bool IsSystemAdmin { get; set; }
        public string LastUpdated { get; set; }
        public string Surname { get; set; }
    }
}