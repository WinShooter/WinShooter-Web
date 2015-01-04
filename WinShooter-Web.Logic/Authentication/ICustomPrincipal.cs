namespace WinShooter.Logic.Authentication
{
    using System;
    using System.Security.Principal;

    interface ICustomPrincipal : IPrincipal
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        bool IsSystemAdmin { get; set; }
    }
}
