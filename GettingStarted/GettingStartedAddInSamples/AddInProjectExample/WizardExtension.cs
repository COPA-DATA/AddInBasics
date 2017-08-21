using Scada.AddIn.Contracts;

namespace AddInProjectExample
{
		[AddInExtension("Wizard Extension Demo", "A simple Hello World Project Wizard", "Samples")] 
		public class WizardExtension: IProjectWizardExtension
	{
		public void Run(IProject context, IBehavior behavior)
		{
			frmDescription frm = new frmDescription("Project", "Wizard", context.Name);
			frm.ShowDialog();
		}
	}

}