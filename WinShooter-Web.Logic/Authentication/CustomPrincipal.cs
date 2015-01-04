namespace WinShooter.Logic.Authentication
{
    using System;
    using System.Security.Principal;

    using WinShooter.Database;

    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }

        public CustomPrincipal(string identity)
        {
            this.Identity = new GenericIdentity(identity);
        }

        public CustomPrincipal(User user)
        {
            this.Identity = new GenericIdentity(user.Id.ToString());

            this.Id = user.Id;
            this.FirstName = user.Givenname;
            this.LastName = user.Surname;
            this.IsSystemAdmin = user.IsSystemAdmin;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSystemAdmin { get; set; }
    }
}
