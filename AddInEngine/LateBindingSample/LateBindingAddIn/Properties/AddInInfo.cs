﻿using Mono.Addins;

// Declares that this assembly is an add-in
[assembly: Addin("LateBindingAddIn", "1.0")]

// Declares that this add-in depends on the scada v1.0 add-in root
[assembly: AddinDependency("::scada", "1.0")]

[assembly: AddinName("LateBindingAddIn")]
[assembly: AddinDescription("")]

[assembly: AddinModule("LateBoundAssembly.dll")]