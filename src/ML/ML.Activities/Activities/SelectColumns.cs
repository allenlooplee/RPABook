using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Threading.Tasks;
using System.Threading;

namespace ML.Activities.Activities
{
    public sealed class SelectColumns : AsyncTaskCodeActivity
    {
        public InArgument<string> ColumnNames { get; set; }

        protected override Task ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken, Application client)
        {
            var columnNames = context.GetValue<string>(ColumnNames);
            return Task.Run(() => client.SelectColumns(columnNames.Split(',')));
        }
    }
}
