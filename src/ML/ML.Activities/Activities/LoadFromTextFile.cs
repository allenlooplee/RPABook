using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using ML.Activities.Properties;

namespace ML.Activities
{
    //[LocalizedDisplayName(nameof(Resources.ChildActivityDisplayName))]
    //[LocalizedDescription(nameof(Resources.ChildActivityDescription))]
    public class LoadFromTextFile : AsyncTaskCodeActivity
    {
        public InArgument<string> FilePath { get; set; }
        public InArgument<string> LabelColumnName { get; set; }

        /// <inheritdoc />
        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
        }

        protected override Task ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken, Application client)
        {
            var filePath = FilePath.Get(context);
            var labelColumnName = LabelColumnName.Get(context);
            
            return Task.Run(() => client.LoadFromTextFile(filePath, labelColumnName));
        }
    }
}
