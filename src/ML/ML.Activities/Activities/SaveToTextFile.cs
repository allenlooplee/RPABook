using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Threading.Tasks;
using System.Threading;

namespace ML.Activities.Activities
{
    public sealed class SaveToTextFile : AsyncTaskCodeActivity
    {
        public InArgument<string> FilePath { get; set; }

        protected override Task ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken, Application client)
        {
            string filePath = context.GetValue<string>(FilePath);
            return Task.Run(() => client.SaveToTextFile(filePath));
        }
    }
}
