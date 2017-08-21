using System;
using Scada.AddIn.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace ApiPerformanceSample
{
    /// <summary>
    /// Editor Wizard Extension with WPF
    /// </summary>
    [AddInExtension("Performance API Call Sample", "This wizard demonstrates the access times using main thread vs. background threads.", "Samples")]
    public class EditorWizardExtension : IEditorWizardExtension
    {
        #region IEditorWizardExtension implementation
        /// <summary>
        /// Method which is executed on starting the SCADA Editor Wizard
        /// </summary>
        /// <param name="context">SCADA editor application object</param>
        /// <param name="behavior">For future use</param>
        public void Run(IEditorApplication context, IBehavior behavior)
        {
            try
            {
                if (context.Workspace.ActiveProject == null)
                {
                    MessageBox.Show(string.Format("There is no active project available." + Environment.NewLine +
                                                  "Please load a project into the workspace!")
                        , "Wizard with WPF GUI");
                    return;
                }

                // Check the time consumption call 10000 times a property called "Name" of IProject
                DateTime start = DateTime.Now;

                for (int i = 0; i < 10000; i++)
                {
                    var name = context.Name;
                }

                MessageBox.Show($"Calling 10000 times a property in main thread takes {(DateTime.Now - start).TotalMilliseconds}ms");

                // Check the time consumption using a background thread
                start = DateTime.Now;

                var task = Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        var name = context.Name;
                    }
                });

                task.Wait();
                
                MessageBox.Show($"Calling 10000 times a property in background thread takes {(DateTime.Now - start).TotalMilliseconds}ms");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion
    }

}