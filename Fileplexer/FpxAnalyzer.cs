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
		var items = Host.Client.GetListingEnumerable(dir, opt, c);

		IAsyncEnumerator<FtpListItem> enumerator = null;

		try {
			enumerator = items.GetAsyncEnumerator(c);

			while (await enumerator.MoveNextAsync()) {

				var item = enumerator.Current;

				var compRes = await Host.Client.CompareFile(comparand, item.FullName,
				                                            FtpCompareOption.Auto, c);
				var res = await h.HandleItem(item, compRes);
				// Console.WriteLine(res); //todo
				if (res is bool b && b) {
					Console.WriteLine($"{res} {item}");
				}
			}

		}
		finally {
			if (enumerator != null) {
				await enumerator.DisposeAsync();
			}
		}

	}

}