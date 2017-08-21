using System;
using System.Windows.Forms;

namespace AddInProjectExample
{
	/// <summary>
	/// Description of frmDescription.
	/// </summary>
	public partial class frmDescription : Form
	{
		public frmDescription(string environment, string typeExtension, string name)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.lblName.Text = string.Format("Hello! Type: '{0}', Environment '{1}', Name of Root-Object '{2}'", typeExtension, environment, name);
		}
		void Button1Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void frmDescription_Load(object sender, EventArgs e)
        {
            this.lblDateTime.Text = DateTime.Now.ToString();
        }
    }
}
