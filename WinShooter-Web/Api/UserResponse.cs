namespace WinShooter.Api
{
    using System;

    using WinShooter.Database;

    public class UserResponse
    {
        public UserResponse(User user)
        {
            this.UserId = user.Id;
            this.CardNumber = user.CardNumber;
            this.ClubId = user.ClubId;
            this.DisplayName = user.DisplayName;
            this.Email = user.Email;
            this.Givenname = user.Givenname;
            this.HasAcceptedTerms = user.HasAcceptedTerms;
            this.IsSystemAdmin = user.IsSystemAdmin;
            this.LastUpdated = user.LastUpdated;
            this.Surname = user.Surname;
        }

        public Guid UserId { get; set; }
        public string CardNumber { get; set; }
        public Guid ClubId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Givenname { get; set; }
        public int HasAcceptedTerms { get; set; }
        public bool IsSystemAdmin { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Surname { get; set; }
    }
}