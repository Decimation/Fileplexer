// $User.Name $File.ProjectName $File.FileName
// $File.CreatedYear-$File.CreatedMonth-$File.CreatedDay @ $File.CreatedHour:$File.CreatedMinute

using FluentFTP;
using System.Diagnostics;

namespace Fileplexer;

public abstract class FpxHandler
{

	public abstract Task<object> HandleItem(FtpListItem item, FtpCompareResult result);

}
public class FtpLogger : IFtpLogger
{

	public void Log(FtpLogEntry entry)
	{
		Debug.WriteLine($"{entry.Message}");
	}

}
