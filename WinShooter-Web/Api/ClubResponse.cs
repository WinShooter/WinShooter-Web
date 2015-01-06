namespace WinShooter.Api
{
    using System;

    using WinShooter.Database;

    public class ClubResponse
    {
        public ClubResponse()
        {
        }

        public ClubResponse(Club club, string adminLinkBase, string noAdminLink)
        {
            this.Id = club.Id.ToString();
            this.Bankgiro = club.Bankgiro;
            this.ClubId = club.ClubId;
            this.Country = club.Country;
            this.Email = club.Email;
            this.LastUpdated = club.LastUpdated;
            this.Name = club.Name;
            this.Plusgiro = club.Plusgiro;
            if (club.AdminUser != null)
            {
                this.AdminName = club.AdminUser.DisplayName;
                this.AdminLink = adminLinkBase + club.AdminUser.Id;
            }
            else
            {
                this.AdminName = "Administratör saknas";
                this.AdminLink = noAdminLink;
            }
        }

        public string Id { get; set; }

        public string Bankgiro { get; set; }

        public string ClubId { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Name { get; set; }

        public string Plusgiro { get; set; }

        public string AdminName { get; set; }

        public string AdminLink { get; set; }

        // TODO: Add ToString()
        public Club UpdateClub(Club club)
        {
            club.Bankgiro = this.Bankgiro;
            club.ClubId = this.ClubId;
            club.Country = this.Country;
            club.Email = this.Email;
            club.LastUpdated = DateTime.Now;
            club.Name = this.Name;
            club.Plusgiro = this.Plusgiro;
            return club;
        }
    }
}