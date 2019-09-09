# Stateless services + Azure service fabric + Azure storage account + Twilio
###### SMS gateway prototype

Prototype project to showcase capabilities of Azure Service fabric. Project integrates .NET Core web API and stateless service, Azure storage account, Twilio API and Azure service fabric. Built on top of Microservice queue architecture. This project is capable of running fully on your local DEV environment.

<img src="https://miro.medium.com/max/3968/0*2ygciPbeUuMBhbP9.png" alt="service fabric logo"/>

What's included:

- Live prototype of a SMS gateway
- Implantation of a Azure service fabric queue architecture
- Sateless ASP.NET Core web API and service
- Uses [`Swagger`](https://swagger.io/) to simplify API development by adding documentations and etc...
- Uses [`Twilio`](https://twilio.com) to send out SMS
- Uses [`Queue Storage`](https://azure.microsoft.com/en-in/services/storage/queues/) to implement microservice queue architecture

## Table of Content

- [Quick Start](#quick-start)
  - [Prerequisites](#prerequisites)
  - [Development Environment Setup](#development-environment-setup)
  - [Build and run](#build-and-run-from-source)
  - [Available scripts](#available-scripts)
- [Related Projects](#related-projects)
- [License](#license)

## Quick Start

After setting up your local DEV environment, you can clone this repository and run the solution. Make sure to wire up your own Twilio account.

### Prerequisites

You'll need the following tools:

- [Azure Service Fabric SDK](https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-get-started)
- [Azure Storage Emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator)
- [Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/)
- [Git](https://git-scm.com/downloads)
- [.NET Core](https://dotnet.microsoft.com/download), version `>=2.2`
- [Visual Studio](https://visualstudio.microsoft.com/), version `>=2017`

### Development Environment Setup

First clone this repository locally.

- Install all of the the prerequisite tools mentioned above.
- Run below mentioned scripts.
- Setup your local Service fabric cluster using "Cluster manager".
- Connect your Azure storage account into Azure storage explore [`link`](https://docs.microsoft.com/en-us/azure-stack/user/azure-stack-storage-connect-se?view=azs-1908) Or use default emulator storage account.

### Build and run from source

With Visual studio:

Open up the two solutions using Visual studio.

- Add your Twilio Account SID and Auth KEY into config files in processor solution `Hint: Local.5Node.xml.. etc`.
- Add your cloud storage connection string if any.
- Restore solution `nuget` packages.
- Rebuild solution once.
- Publish solution using your desired publish profile.
- Open the local [`swagger`](http://localhost:8493/)
- Post a proper message contract using swagger

### Available scripts

Run below scripts on windows powershell

- `Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Force -Scope CurrentUser` - to enable service fabric script execution.
- `AzureStorageEmulator.exe init /server <SQLServerInstance>` - setup storage emulator locally for the first time.
- `AzureStorageEmulator.exe start` - to start storage emulator locally.

## Related Projects

[`Azure Service Fabric Repo`](https://github.com/Microsoft/service-fabric)
[`Azure Storage Repo`](https://github.com/Azure/azure-storage-net)

## License

Licensed under the [MIT](LICENSE) license.