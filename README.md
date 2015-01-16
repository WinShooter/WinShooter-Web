WinShooter-Web
==============

The web variant of WinShooter.

WinShooter is a software for handling competitions with pistols, 
especially "fältskytte". It's in Swedish, aimed at the Swedish 
market.

Source is available at https://github.com/WinShooter/WinShooter-Web.

The master branch is automaticly compiled to 
http://winshooter.azurewebsites.net/

For comments, contact john (at) winshooter.se.

Developer prerequisites
------------------------
To be able to run development, you need to create a config file
"SecretAuthParams.config" with the following content:
<appSettings>
  <add key="GoogleOauthClientId" value="<your-google-clientID>"/>
  <add key="GoogleOauthSecret" value="<your-google-secret>"/>
</appSettings>

This is because your Google secret is, well, secret and should 
not be committed to a public repository. :-)

Developer style
---------------
Development style adhears to StyleCop.
