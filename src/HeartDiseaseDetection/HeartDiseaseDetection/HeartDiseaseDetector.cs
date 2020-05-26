using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Data;
using HeartDiseaseDetection.DataModels;
using Microsoft.ML;

namespace HeartDiseaseDetection
{
    [Designer(typeof(HeartDiseaseDetectorActivityDesigner))]
    public sealed class HeartDiseaseDetector : CodeActivity
    {
        // Define an activity input argument of type string
        [Category("Input")]
        public InArgument<string> ModelFilePath { get; set; }

        [Category("Input")]
        public InArgument<DataTable> Data { get; set; }

        [Category("Output")]
        public OutArgument<bool[]> Predictions { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            MLContext mlContext = new MLContext();

            var modelFilePath = context.GetValue(this.ModelFilePath);
            ITransformer mlModel = mlContext.Model.Load(modelFilePath, out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            var data = context.GetValue(this.Data);
            var modelInputs = ConvertFrom(data);

            var modalOutputs =
                from modelInput in modelInputs
                select predEngine.Predict(modelInput).Prediction;
            Predictions.Set(context, modalOutputs.ToArray());
        }

        private IEnumerable<ModelInput> ConvertFrom(DataTable data)
        {
            return
                from row in data.AsEnumerable()
                select new ModelInput
                {
                    Age = Convert.ToSingle(row["age"].ToString()),
                    Sex = Convert.ToSingle(row["sex"].ToString()),
                    Cp = Convert.ToSingle(row["cp"].ToString()),
                    Trestbps = Convert.ToSingle(row["trestbps"].ToString()),
                    Chol = Convert.ToSingle(row["chol"].ToString()),
                    Fbs = Convert.ToSingle(row["fbs"].ToString()),
                    Restecg = Convert.ToSingle(row["restecg"].ToString()),
                    Thalach = Convert.ToSingle(row["thalach"].ToString()),
                    Exang = Convert.ToSingle(row["exang"].ToString()),
                    Oldpeak = Convert.ToSingle(row["oldpeak"].ToString()),
                    Slope = Convert.ToSingle(row["slope"].ToString()),
                    Ca = Convert.ToSingle(row["ca"].ToString()),
                    Thal = Convert.ToSingle(row["thal"].ToString())
                };
        }
    }
}
