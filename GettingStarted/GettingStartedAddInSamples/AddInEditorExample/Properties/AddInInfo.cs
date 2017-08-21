using Mono.Addins;

// Declares that this assembly is an add-in
[assembly: Addin("AddInEditor", "1.0")]

// Declares that this add-in depends on the scada v1.0 add-in root
[assembly: AddinDependency("::scada", "1.0")]

[assembly: AddinName("Editor Add-In Demo")]
[assembly: AddinDescription("Demonstrates an Add-In with a Wizard and Service extension for zenon Editor.")]