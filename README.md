zenon is a software system from COPA-DATA for industrial automation and the energy industry. Machines and equipment are controlled, monitored and optimized. zenon's particular strength is open and reliable communication throughout heterogeneous production facilities. Over 300 native communication protocols support the horizontal and vertical exchange of data. This allows for the continuous implementation of Industrial IoT and the Smart Factory.

zenon’s engineering environment is flexible and can be used in many ways. Complex functions for comprehensive applications such as HMI/SCADA and reporting are supplied out of the box to create intuitive and robust applications. Users can thus contribute to the increased flexibility and efficiency of HMI applications using zenon.

To open projects of this repository please install Visual Studio Developer Tools, they available at [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=vs-publisher-1463468.COPA-DATASCADAAdd-InDeveloperToolsforVS) 

This repository contains Add-Ins training samples. All projects of this folder are added at solution [Training.sln](Training.sln)


# AddInEngine

This folder contains samples how to influence the packaging and deployment of Add-In packages.

## LateBindingSample

All referenced assemblies are automatically included to the Add-In Package, that
* are not .NET Framework specific assemblies
* are not in installed by the zenon Setup

References are identified by using the public key token of .NET Assemblies
mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=<b>b77a5c561934e089</b>

Well-Known public key tokens of assemblies are ignored in the packaging process:
* b77a5c561934e089 (.NET Framework)
* 31bf3856ad364e35 (.NET Framework)
* B03f5f7f11d50a3a (.NET Framework)
* 0738eb9f132ed756 (Mono.AddIns)
* dbcf9a9c17b53bac (COPA-DATA zenon components)

Also, libraries loaded dynamically during runtime are not recognized in the packaging process. The sample in this folder demonstrates how to force that an assembly is added to the Add-In Package.

Late bound assemblies have to be declared manually (in AddInInfo.cs)
```cs
[assembly: AddinModule("LateBoundAssembly.dll")]
```

## DataAndResourceFilesSample

Data and resource files may be files with any type, like unmanaged libraries written in C++ or bitmaps or configuration files.

To add such a file to the Add-In package ensure that the file is copied to the output directory.
Add following attribute to Properties\AddInInfo.cs
```cs
[assembly: ImportAddinFile("Books.xml") ]
```

The sample in this folder demonstrates how to include such files to the Add-In Package.

# ApiPerformance

This folder gives hints how to work with the zenon API. Also, there are sample of how it shouldn't be.

## ApiPerformanceSample

This sample demonstrates why you shown reduce the calls to zenon API to an absolute minimum.

How to work with zenon API – best practices
* Do not iterate through whole lists like variables. There can be many thousands of variables defined!
* Use XML rather than access the API several thousand times.
* Cache data where applicable

## BadVariableSubscriptionSample

This sample demonstrates how you should **not work** with zenon events.

Look at class „BadVariableSubscription“, Method „Container_BulkChanged“ 
A long running operation is simulated by 
```cs
Thread.Sleep(5000)
```

Impact: Zenon process are blocked, e. g. write cyclic archives, because variable change distribution is blocked.

How to work with events – best practices
* Use Threads for long running operations
* No direct calls to Login or database operations (the Log-In or database server may not be available)
* Use bulk events where available


# CommonCoding

This folder demonstrates best-practices to create reliable code.

## ExceptionDemo
Using exceptions is state-of-the-art, but only for exceptional purposes. This sample demonstrates that exceptions may speed down the execution of code heavily.

## NLogSample
<a name="NLogSample"></a>
Uses [NLog](https://github.com/NLog/NLog) for logging in Add-Ins. 

There are two targets configured using NLog's configuration API:
* FileTarget for logging to C:\ProgramData\Company\zenon\ZENRT32_NLogAddIn.log
* NLogViewerTraget to send messages to Sentinal. [Sentinal](http://sentinel.codeplex.com/) is a Log Viewer 


## RedundancySample

This folder contains a project that demonstrates how to work with Add-Ins using zenon Network. When an extension starts, it has to be determined if the extension is running on server, standby or on a client, depending on the kind of implementation. There are two events for server-standby and standby-server switching to active or deactivate the processing of Add-In code.


# GettingStarted
This folder contains two projects. Each implemented extension opens a window that shows the context (Editor or Runtime project) and extension type.


## AddInEditorExample
An Add-In that contains two extensions for zenon Editor. 

The most important file of the project:
* Properties\AddInInfo.cs: The file defines the Add-In identification, name and description
* ServiceExtension.cs: An Editor Service Extension
* WizardExtension.cs: An Editor Wizard Extension


## AddInProjectExample
An Add-In that contains two extensions for zenon Runtime. 

The most important file of the project:
* Properties\AddInInfo.cs: The file defines the Add-In identification, name and description
* ServiceExtension.cs: A Runtime Project Service Extension
* WizardExtension.cs: A Runtime Project Wizard Extension













# Documentation 

This folder contains all zenon Help documentation samples


# Documentation and Support

Documentation is provided as integrated help. Just press F1 within the application to get context sensitive help. Further support is available on the COPA-DATA website:

Use our forum to get the latest news: 
https://forum.copadata.com/forumdisplay.php?45-Programming-Interface-API

For help contact our support: 
https://www.copadata.com/en/support-services/