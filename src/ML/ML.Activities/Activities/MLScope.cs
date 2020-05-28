using System;
using System.Activities;
using System.ComponentModel;
using System.Activities.Statements;
using ML.Activities.Properties;

namespace ML.Activities
{

    //[LocalizedDescription(nameof(Resources.ParentScopeDescription))]
    //[LocalizedDisplayName(nameof(Resources.ParentScope))]
    public class MLScope : NativeActivity
    {
        #region Properties

        [Browsable(false)]
        public ActivityAction<Application> Body { get; set; }

        internal static string MLContainerPropertyTag => "MLScope";

        #endregion


        #region Constructors

        public MLScope()
        {

            Body = new ActivityAction<Application>
            {
                Argument = new DelegateInArgument<Application>(MLContainerPropertyTag),
                Handler = new Sequence { DisplayName = "Do" }
            };
        }

        #endregion


        #region Private Methods

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
        }

        protected override void Execute(NativeActivityContext context)
        {
            var application = new Application();

            if (Body != null)
            {
                context.ScheduleAction<Application>(Body, application, OnCompleted, OnFaulted);
            }
        }

        private void OnFaulted(NativeActivityFaultContext faultContext, Exception propagatedException, ActivityInstance propagatedFrom)
        {
            //TODO
        }

        private void OnCompleted(NativeActivityContext context, ActivityInstance completedInstance)
        {
            //TODO
        }

        #endregion


        #region Helpers
        
        #endregion
    }
}
