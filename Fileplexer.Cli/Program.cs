using System.Diagnostics;
using System.Net;
using System.Security;
using System.Security.Authentication;
using System.Text;
using FluentFTP;

namespace Fileplexer.Cli;

public static class Program
{

	static async Task Main(string[] args)
	{
		var s = "192.168.1.235";

		var f = new FpxHost(s, "Admin", "deci", 2121);

		var profile = await f.Client.AutoConnect();
		f.Client.Encoding = Encoding.UTF8;
		// await f.Client.Connect(profile);
		Console.WriteLine(profile);
		Console.WriteLine(await f.Client.GetWorkingDirectory());
		var a = new FpxAnalyzer(f);
		var h = new Hndl();
		
		await a.Run(h, @"C:\Users\Deci\Downloads\Kallen_FINAL_1-3.png", "DCIM/",
		            opt: FtpListOption.AllFiles | FtpListOption.Recursive);

		await f.Client.Disconnect();
	}

	public class Hndl : FpxHandler
	{

		public override async Task<object> HandleItem(FtpListItem item, FtpCompareResult result)
		{
			if (result == FtpCompareResult.Equal) {
				return true;
			}

			return false;
		}

	}

}