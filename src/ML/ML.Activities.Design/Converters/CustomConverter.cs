using Microsoft.VisualBasic.Activities;
using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Presentation.Model;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace ML.Activities.Design
{
    public class CustomConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ModelItem modelItem = value as ModelItem;
            if (value != null)
            {
                InArgument<string> inArgument = modelItem.GetCurrentValue() as InArgument<string>;

                if (inArgument != null)
                {
                    Activity<string> expression = inArgument.Expression;
                    VisualBasicValue<string> vbexpression = expression as VisualBasicValue<string>;
                    Literal<string> literal = expression as Literal<string>;

                    if (literal != null)
                    {
                        return "\"" + literal.Value + "\"";
                    }

                    if (vbexpression != null)
                    {
                        return vbexpression.ExpressionText;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert combo box value to InArgument<string>  
            string itemContent = (string)((ComboBoxItem)value).Content;
            VisualBasicValue<string> vbArgument = new VisualBasicValue<string>(itemContent);
            InArgument<string> inArgument = new InArgument<string>(vbArgument);
            return inArgument;
        }
    }
}
