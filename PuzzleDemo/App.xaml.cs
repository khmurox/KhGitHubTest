using System.Windows;

namespace PuzzleDemo
{

	public partial class app : Application
	{
		void AppStartup(object sender, StartupEventArgs args)
		{
			Window mainWindow = new Puzzle();
			mainWindow.Show();
		}
	}
}