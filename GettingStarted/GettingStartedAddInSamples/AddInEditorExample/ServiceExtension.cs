using System;
using Scada.AddIn.Contracts;

namespace AddInEditorExample
{
	/// <summary>
	/// Description of ServiceExtension.
	/// </summary>
	[AddInExtension("Service Demo Extension", "A simple Hello World Editor service", "Samples")] 
	public class ServiceExtension : IEditorServiceExtension
	{
		#region IServiceExtension implementation

		public void Start(IEditorApplication context, IBehavior behavior)
		{
			try 
			{
				frmDescription frm = new frmDescription("Editor", "Service", context.Name);
				frm.Show();
			} 
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
		}

		public void Stop()
		{
		}

		#endregion
	}
}
