using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using ML.Activities.Activities;
using ML.Activities.Design.Designers;
using ML.Activities.Design.Properties;

namespace ML.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            var categoryAttribute =  new CategoryAttribute($"{Resources.Category}");


            builder.AddCustomAttributes(typeof(MLScope), categoryAttribute);
            builder.AddCustomAttributes(typeof(MLScope), new DesignerAttribute(typeof(MLScopeDesigner)));
            builder.AddCustomAttributes(typeof(MLScope), new HelpKeywordAttribute("https://go.uipath.com"));

            builder.AddCustomAttributes(typeof(LoadFromTextFile), categoryAttribute);
            builder.AddCustomAttributes(typeof(LoadFromTextFile), new DesignerAttribute(typeof(LoadFromTextFileDesigner)));
            builder.AddCustomAttributes(typeof(LoadFromTextFile), new HelpKeywordAttribute("https://go.uipath.com"));

            builder.AddCustomAttributes(typeof(SaveToTextFile), categoryAttribute);
            builder.AddCustomAttributes(typeof(SaveToTextFile), new DesignerAttribute(typeof(SaveToTextFileDesigner)));
            builder.AddCustomAttributes(typeof(SaveToTextFile), new HelpKeywordAttribute("https://go.uipath.com"));

            builder.AddCustomAttributes(typeof(SelectColumns), categoryAttribute);
            builder.AddCustomAttributes(typeof(SelectColumns), new DesignerAttribute(typeof(SelectColumnsDesigner)));
            builder.AddCustomAttributes(typeof(SelectColumns), new HelpKeywordAttribute("https://go.uipath.com"));

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
