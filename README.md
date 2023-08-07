# [Vca](http://172.86.123.207:1987/) - ASP.NET Core Web App (Model-View-Controller) with ASP Classic
To start work with the project, it's recommended to check whether your system can [meet requirements](https://learn.microsoft.com/en-us/dotnet/core/install/windows?tabs=net70) for [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) and then update `appsettings.json` based on your server configurations (e.g. connection string). 

## What's the project's purpose?
The primary purpose of this task is to demonstrate one of ways of migrating from existing `ASP Classic` project to `ASP.NET Core Web App`. Moreover, the application needs to be deployed as a single `ASP.NET Core app`.

## How was done?
So, in order to achieve the target, all `ASP Classic` pages should be stored in a separate folder(in ASP folder) inside the project and rewrite the business logic using .NET page by page. In other words, ASP Classic pages must eventually become only the presentation layer, and middle tier must be migrated to .NET. It's worth to note that the biggest challenge during such migration is to pass/share data between the 2 tiers as they are totally different worlds(`ASP.NET Core` and `ASP Classic`). For sure, `Session` cannot be shared as they "don't know each other". One possible solution would be using `Cookies` with pre-defined keys for each action methods:

- perform necessary operation to fetch data
- serialize the data as a JSON object
- append the serialized data to `Cookies` with the custom key
- then redirect to the legacy `ASP` page 
- `ASP Classic` is able to fetch the data from `Cookies`
- in order to parse the `JSON` in `VB`, [aspJSON](https://github.com/rcdmk/aspJSON/tree/master) is being used.

This approach is being for almost all pages.
Alternative way would be making `.ajax` calls from `ASP Classic` to `ASP.NET Core`. This approach is also being used in deleting item for demonstration purposes.

## Walkthrough the application architecture
The project follows [N-Tier architecture](https://learn.microsoft.com/en-us/visualstudio/data-tools/walkthrough-creating-an-n-tier-data-application?view=vs-2022&tabs=csharp). I'm assumed that the reader's are already/well familiar with this architecture and I found it unnecessary to enlist all of the benefits. It includes a presentation tier, a middle-tier, and a data tier:
1. The presentation tier consists of :
- `Vca` which is `ASP.NET Core Web (Model-View-Controller)` with `ASP Classic pages`
2. Middle tier:
- a data access layer
  - `Vca.Data` - EF Core, no Unit of Work with repository pattern
- a business logic layer
  - `Vca.Abstractions` - also know app contracts define the strategy of the app
  - `Vca.Core` - default implementations of defined contracts
- and shared components which are avaliable across the project
  - `Vca.Shared`
3. The data tier:
- `MS SQL Server`

## Tests
The project is decorated with unit tests using [Microsoft.NET.Test.Sdk](https://github.com/microsoft/vstest/) and contains `4 positive test cases` for CRUD operations.
## Dos
The [Automapper](https://docs.automapper.org/en/stable/Getting-started.html) is introduced (inside `MappingProfile.cs`) to adapt from models to entities and vice versa. Moreover, models are extracted as `Vca.Models` project which is done in purpose to improve reusablity of each layers.

## Don'ts
1. The project doesn't contain advanced validation rules for request payloads because of limited time. Hence for a quick solution to resolve, the app sticked with .NET's default validation scheme with [ModelState](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-7.0) and [Data Annotations](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/mvc-music-store/mvc-music-store-part-6). I would always prefer to include [FluentValidation](https://docs.fluentvalidation.net/en/latest/aspnet.html) for all projects.
2. I know that some time ago, there was possibility to deploy any web app to `AWS`. Unfortunately, when it came to deployment of the project, I couldn't find this feature :(. Probably it's removed.

## Why the project is called [Vca](http://172.86.123.207:1987/)?
When I received an email containing test project, the email's subject is "VCA - Test - Project". It's clear that it's test project, so trimmed the end and left the word [VCA](http://172.86.123.207:1987/)

## How to deploy the app?
1. First things first - the Vca app needs to be published to a folder:
  - right-click on the [Vca] project(not solution) in **Solution Explorer** and select **Publish**
  - in the Pick a publish target dialog, select the **Folder** publish option.
2. Next step is [installing the ASP.NET Core Module/Hosting Bundle](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-7.0#iis-configuration). Also, it's recommended turn off **WebDAV Publishing** as shown below

<p align="center">
  <img src="HowToDeploy/WebDAVPublishingDisabled.png?raw=true" alt="WebDAV Publishing disabled"/>
</p>

### Now we are ready for deploying our app. Follow the below steps:
 - Create the IIS site
   - On the IIS server, create a folder to contain the app's published folders and files. In a following step, the folder's path is provided to IIS as the physical path to the app
   - In IIS Manager, open the server's node in the **Connections** panel. Right-click the **Sites** folder. Select **Add Website** from the contextual menu

<p align="center">
  <img src="HowToDeploy/AddWebSite.png?raw=true" alt="Add Website"/>
</p>
  
   - Provide a **Site** name and set the **Physical path** to the app's deployment folder that you created. Provide the **Binding** configuration(**_without https_**) and create the website by selecting **OK**.

<p align="center">
  <img src="HowToDeploy/SiteNameAndPhysicalPath.png?raw=true" alt="Add Website"/>
</p>

  - Right-click on the created website. Select **Add Application** from the contextual menu

<p align="center">
  <img src="HowToDeploy/AddApplication.png?raw=true" alt="Add Application"/>
</p>

  - In **Alias** write **asp** _which is very important_

<p align="center">
  <img src="HowToDeploy/AppAliasAndPhysicalPath.png?raw=true" alt="Add Application"/>
</p>

  - Set the **Physical path** to the app's ASP Classic pages which are located in ASP folder of deployment folder

<p align="center">
  <img src="HowToDeploy/ChooseAspPages.png?raw=true" alt="Add Application"/>
</p>

  - Complete the adding application by selecting **OK**

  <p align="center">
  <img src="HowToDeploy/ConfirmAppCreation.png?raw=true" alt="Add Application"/>
</p>

## Browse the website
The app is accessible in a browser after it receives the first request. Make a request to the app at the endpoint binding that you established in IIS Manager for the site - http://172.86.123.207:1987/ in my case.