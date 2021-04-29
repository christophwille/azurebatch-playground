using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Azure.Batch;
using System.Collections.Generic;
using Microsoft.Azure.Batch.Auth;

namespace Dispatch
{
	class Program
	{
		static Task<int> Main(string[] args) => CommandLineApplication.ExecuteAsync<Program>(args);

		[Option("-s|--serviceurl", Description = "Service URL")]
		[Required]
		public string BatchServiceUrl { get; } = "https://cwiazbatchtest.westeurope.batch.azure.com";

		[Option("-a|--accountname", Description = "Account name")]
		[Required]
		public string BatchAccountName { get; } = "cwiazbatchtest";

		[Option("-k|--accountkey", Description = "Account key")]
		[Required]
		public string BatchAccountKey { get; }

		const string BatchAppNameByConvention = "BatchTaskApp";
		const string BatchAppExecutableByConvention = "batchtaskapp.exe";
		const string BatchJobNameByConvention = "singlejob";

		private async Task<int> OnExecuteAsync(CommandLineApplication app, CancellationToken cancellationToken = default)
		{
			var _batchCredentials = new BatchSharedKeyCredentials(BatchServiceUrl, BatchAccountName, BatchAccountKey);
			using var batchClient = BatchClient.Open(_batchCredentials);

			string taskId = Guid.NewGuid().ToString();
			string cmd = $"cmd /c %AZ_BATCH_APP_PACKAGE_{BatchAppNameByConvention}%\\{BatchAppExecutableByConvention} -b SGkgQ2hyaXM=";

			// Intentionally not collecting any outputs (simplicity)
			CloudTask taskToCreate = new CloudTask(taskId, cmd)
			{
				ApplicationPackageReferences = new List<ApplicationPackageReference>
				{
					new ApplicationPackageReference()
					{
						ApplicationId = BatchAppNameByConvention
					}
				}
			};

			await batchClient.JobOperations.AddTaskAsync(BatchJobNameByConvention, taskToCreate);
			return 0;
		}
	}
}
