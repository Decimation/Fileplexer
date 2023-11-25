using System.Diagnostics;
using System.Net;
using System.Security;
using System.Security.Authentication;
using System.Text;
using FluentFTP;

namespace Fileplexer.Cli
{
	internal class Program
	{

		static async Task Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			var logger = new l();

			var s = "192.168.1.235";

			var f = new AsyncFtpClient(s, "Admin", "deci", 2121, new FtpConfig()
				                           { }, logger);

			var profile = await f.AutoConnect();
			await f.Connect(profile);
			Console.WriteLine(await f.GetWorkingDirectory());
			await f.Disconnect();
		}

		public class l : IFtpLogger
		{

			public void Log(FtpLogEntry entry)
			{
				Debug.WriteLine($"{entry.Message}");
			}

		}

	}
}