using Scada.AddIn.Contracts;

namespace AddInProjectExample
{
	/// <summary>
	/// Description of ServiceExtension.
	/// </summary>
	[AddInExtension("Service Demo Extension", "A simple Hello World Project service", "Samples", DefaultStartMode = DefaultStartupModes.Auto)] 
	public class ServiceExtension : IProjectServiceExtension
	{
		#region IServiceExtension implementation

		public void Start(IProject context, IBehavior behavior)
		{
			frmDescription frm = new frmDescription("Project", "Service", context.Name);
			frm.Show();
		}

		public void Stop()
		{
		}

		#endregion
	}
}
