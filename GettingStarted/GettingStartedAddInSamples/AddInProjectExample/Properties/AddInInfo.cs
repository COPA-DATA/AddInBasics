using Mono.Addins;

// Declares that this assembly is an add-in
[assembly: Addin("AddInProject", "1.0")]

// Declares that this add-in depends on the scada v1.0 add-in root
[assembly: AddinDependency("::scada", "1.0")]

[assembly: AddinName("Project Add-In Example")]
[assembly: AddinDescription("Demonstrates an Add-In with a Wizard and Service extension for zenon Runtime.")]