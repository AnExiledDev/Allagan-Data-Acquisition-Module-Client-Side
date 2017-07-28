using MahApps.Metro.Controls;

namespace ADAM
{
	public partial class SettingsWindow : MetroWindow
	{
		public string CoreAuthKey { get; set; }

		public SettingsWindow()
		{
			InitializeComponent();

			CoreAuthKeyInput.Text = Settings.Instance.CoreAuthKey;

			CoreAuthKeyTestButton.Click += (s, e) => { CoreAuthKeyTestButton_Click(); };
		}

		private void CoreAuthKeyTestButton_Click()
		{
			string status = Settings.Instance.SetCoreAuthKey(CoreAuthKeyInput.Text);
			CoreAuthKeyTestResult.Text = status;
		}
	}
}
