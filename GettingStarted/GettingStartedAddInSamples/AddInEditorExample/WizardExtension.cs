using System;
using Scada.AddIn.Contracts;

namespace AddInEditorExample
{
  [AddInExtension("Wizard Extension Demo", "A simple Hello World Editor Wizard", "Samples")]
  public class WizardExtension : IEditorWizardExtension
  {
    public void Run(IEditorApplication context, IBehavior behavior)
    {
      try
      {
        frmDescription frm = new frmDescription("Editor", "Wizard", context.Name);
        frm.ShowDialog();
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.Message);
      }
    }
  }

}