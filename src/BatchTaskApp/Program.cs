using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using System.Text;

namespace BatchTaskApp
{
	class Program
	{
		static Task<int> Main(string[] args) => CommandLineApplication.ExecuteAsync<Program>(args);

		[Option("-b|--base64", Description = "Base64 input to write to file")]
		public string Base64 { get; } = "SGVsbG8gV29ybGQh"; // "Hello World!"

		const string TargetFilenameByConvention = "target.txt";

		private async Task<int> OnExecuteAsync(CommandLineApplication app, CancellationToken cancellationToken = default)
		{
			byte[] data = Convert.FromBase64String(Base64);
			string decodedString = Encoding.UTF8.GetString(data);

			using (var fs = File.OpenWrite(TargetFilenameByConvention))
			using (var sw = new StreamWriter(fs))
			{
				await sw.WriteAsync(decodedString);
				await sw.FlushAsync();
			}

			return 0;
		}
	}
}
