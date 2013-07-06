namespace WinShooter.Api.Configuration
{
    using Funq;

    using ServiceStack.WebHost.Endpoints;

    public class HelloAppHost : AppHostBase
    {
        //Tell Service Stack the name of your application and where to find your web services
        public HelloAppHost() : base("Hello Web Services", typeof(HelloService).Assembly) { }

        /// <summary>
        /// Configure the given container with the 
        ///             registrations provided by the funqlet.
        /// </summary>
        /// <param name="container">Container to register.</param>
        public override void Configure(Container container)
        {
            // register user-defined REST-ful urls
            Routes
              .Add<Hello>("/hello")
              .Add<Hello>("/hello/{Name}");
        }
    }
}