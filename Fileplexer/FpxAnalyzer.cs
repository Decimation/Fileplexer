using FluentFTP;

#nullable disable
namespace Fileplexer;

public class FpxAnalyzer
{

	public FpxHost Host { get; }

	public FpxAnalyzer(FpxHost host)
	{
		Host = host;
	}

	public async Task Run(FpxHandler h, string comparand, string dir,
	                      CancellationToken c = default, FtpListOption opt = default)
	{
		var items = await Host.Client.GetListing(dir, opt, c);
		items = items.Where(x => x.Type != FtpObjectType.Directory).ToArray();
		var cnt = items.Length;

		var grp = items.GroupBy(x => Path.GetDirectoryName(x.FullName)/*,
		                        resultSelector: (_, enumerable) => enumerable*/).OrderBy(x=>x.Count());

		foreach (var g in grp)
		{
			Console.WriteLine(g.Key);
			Console.WriteLine();
			/*await Parallel.ForEachAsync(g, pl, async (item, token) =>
			{
				var cm = await Host.Client.CompareFile(comparand, item.FullName,
													   FtpCompareOption.Auto, token);
				var res = h.HandleItem(item, cm);
				Interlocked.Decrement(ref cnt);
				Console.WriteLine($"\r{cnt}");
			}).ConfigureAwait(false);*/

			foreach (FtpListItem item in g) {
				
				var cm = await Host.Client.CompareFile(comparand, item.FullName,
				                                       FtpCompareOption.Size, c);

				var res = h.HandleItem(item, cm);
				Interlocked.Decrement(ref cnt);
				Console.Write($"\r{cnt} {cm} | {item.FullName}");

			}
		}
	}

}