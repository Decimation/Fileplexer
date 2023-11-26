using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentFTP;

namespace Fileplexer;

public class FpxHost
{

	public AsyncFtpClient Client { get; set; }

	public string Username { get; set; }

	public string Password { get; set; }

	public string Host { get; set; }

	public int Port { get; set; }

	public FpxHost(AsyncFtpClient client)
	{
		Client        = client;
		// Client.Logger = new FtpLogger();
	}

	public FpxHost(string username, string password, string host, int port) : this(
		new AsyncFtpClient(username, password, host, port))
	{
		Username = username;
		Password = password;
		Host     = host;
		Port     = port;
	}

}