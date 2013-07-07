namespace WinShooter.Api
{
    using ServiceStack.ServiceHost;

    [Route("/api/hello")]
    public class Hello : IReturn<HelloResponse>
    {
        public string Name { get; set; }
    }
}