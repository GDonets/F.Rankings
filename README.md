# Agent Rankings

For the application to work, you would first set "PartnerApi.ApiKey" User Secret.

UseSPA - related parts of the Startup logic of ASP.NET Core app were commented out due to Visual Studio
Angular template not working correctly with Angular 9+. 
The host simply won't start (https://github.com/dotnet/aspnetcore/issues/17277#issuecomment-562433864).

The Get request implying such a long completion time should instead work on as a background task. 
The web app should implement HTTP polling, where controller action should return 202 Accepted with the URL to the result. However due to background tasks 
implementation being tricky in ASP.NET Core and require additional ceremonies, I decided to skip this step.

Routing in the Angular app is messy. 
The api url is hardcoded in environment.ts. If you want to change it, be aware of CORS policies.
On dev server you'd have to update proxy.conf.json to avoid CORS errors.
