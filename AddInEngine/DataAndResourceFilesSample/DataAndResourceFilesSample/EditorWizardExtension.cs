using System;
using System.IO;
using System.Xml.Linq;
using Scada.AddIn.Contracts;

namespace DataAndResourceFilesSample
{
    /// <summary>
    /// Description of Editor Wizard Extension.
    /// </summary>
    [AddInExtension("Data File Sample", "Shows an example how to handle data files", "Samples")]
    public class EditorWizardExtension : IEditorWizardExtension
    {
        #region IEditorWizardExtension implementation

        public void Run(IEditorApplication context, IBehavior behavior)
        {
            try
            {
                // Books.xml is copied to the output directory during compilation. In Properties\AddInInfo.cs there is an attribute defined to include the file to the package:
                // [assembly: ImportAddinFile("Books.xml") ]

                // The file gets extracted to the Add-In's directory. Property AssemblyDirectory determines the Add-Ins directory.
                var xmlDocument = XDocument.Load(Path.Combine(AssemblyDirectory, "Books.xml"));

                foreach (var bookElement in xmlDocument.Elements("catalog").Elements("book"))
                {
                    context.DebugPrint(bookElement.Element("title").Value, DebugPrintStyle.Standard);
                }
            }
            catch (Exception e) 
            {
                context.DebugPrint(e.Message, DebugPrintStyle.Error);
            }
        }

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = typeof(EditorWizardExtension).Assembly.CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        #endregion
    }

}