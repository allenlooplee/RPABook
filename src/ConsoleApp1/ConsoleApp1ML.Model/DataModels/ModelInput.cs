//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using Microsoft.ML.Data;

namespace ConsoleApp1ML.Model.DataModels
{
    public class ModelInput
    {
        [ColumnName("age"), LoadColumn(0)]
        public float Age { get; set; }


        [ColumnName("sex"), LoadColumn(1)]
        public float Sex { get; set; }


        [ColumnName("cp"), LoadColumn(2)]
        public float Cp { get; set; }


        [ColumnName("trestbps"), LoadColumn(3)]
        public float Trestbps { get; set; }


        [ColumnName("chol"), LoadColumn(4)]
        public float Chol { get; set; }


        [ColumnName("fbs"), LoadColumn(5)]
        public float Fbs { get; set; }


        [ColumnName("restecg"), LoadColumn(6)]
        public float Restecg { get; set; }


        [ColumnName("thalach"), LoadColumn(7)]
        public float Thalach { get; set; }


        [ColumnName("exang"), LoadColumn(8)]
        public float Exang { get; set; }


        [ColumnName("oldpeak"), LoadColumn(9)]
        public float Oldpeak { get; set; }


        [ColumnName("slope"), LoadColumn(10)]
        public float Slope { get; set; }


        [ColumnName("ca"), LoadColumn(11)]
        public float Ca { get; set; }


        [ColumnName("thal"), LoadColumn(12)]
        public float Thal { get; set; }


        [ColumnName("num"), LoadColumn(13)]
        public bool Num { get; set; }


    }
}
