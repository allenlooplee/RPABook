using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ML.Enums;
using ML.Properties;
using ML.Tools;
using Microsoft.ML;
using Microsoft.ML.AutoML;

namespace ML
{
    public class Application
    {
        private MLContext _mlContext;
        private IDataView _trainDataView;

        public Application()
        {
            _mlContext = new MLContext();
        }

        public void LoadFromTextFile(string filePath, string labelColumnName)
        {
            var columnInference = _mlContext.Auto().InferColumns(filePath, labelColumnName, separatorChar: ',', groupColumns: false);
            var textLoader = _mlContext.Data.CreateTextLoader(columnInference.TextLoaderOptions);
            _trainDataView = textLoader.Load(filePath);
        }

        public void SelectColumns(params string[] columnNames)
        {
            IEstimator<ITransformer> estimator = _mlContext.Transforms.SelectColumns(columnNames);
            ITransformer transformer = estimator.Fit(_trainDataView);
            _trainDataView = transformer.Transform(_trainDataView);
        }

        public void SaveToTextFile(string filePath)
        {
            using (var fileStream = System.IO.File.OpenWrite(filePath))
            {
                _mlContext.Data.SaveAsText(_trainDataView, fileStream, separatorChar: ',', schema: false);
            }
        }
    }
}
