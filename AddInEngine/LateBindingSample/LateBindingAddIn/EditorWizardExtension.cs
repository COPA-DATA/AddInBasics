using System;
using Scada.AddIn.Contracts;

namespace LateBindingAddIn
{
    /// <summary>
    /// Description of Editor Wizard Extension.
    /// </summary>
    [AddInExtension("Late Bound Assembly", "Demonstratest the usage of late bound assembly in Add-Ins", "Samples")]
    public class EditorWizardExtension : IEditorWizardExtension
    {
        #region IEditorWizardExtension implementation

        public void Run(IEditorApplication context, IBehavior behavior)
        {
            try
            {
                // To load assemblies during runtime, Activor.CreateInstances is uses here (but there are more options available in .NET, e. g. IoC container)
                // by loading an assembly LateBoundAssembly.dll and instantiation of class LateBoundAssembly.SampleClass 
                // Properties\AddInInfo.cs contains attribute [assembly: AddinModule("LateBoundAssembly.dll")] to add the DLL zu the Add-In Package
                var result = Activator.CreateInstance("LateBoundAssembly", "LateBoundAssembly.SampleClass");
                context.DebugPrint(result.Unwrap().GetType().FullName, DebugPrintStyle.Standard);
            }
            catch (Exception e)
            {
                context.DebugPrint(e.Message, DebugPrintStyle.Error);
            }
        }

        #endregion
    }

}