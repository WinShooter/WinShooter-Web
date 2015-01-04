namespace WinShooter.Logic.Authentication
{
    using System;

    public class CustomPrincipalSerializeModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSystemAdmin { get; set; }
    }
}
