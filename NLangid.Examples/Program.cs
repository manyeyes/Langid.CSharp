// See https://github.com/manyeyes for more information
// Copyright (c)  2023 by manyeyes
using NLangid;

namespace ManyVitsTts.Examples
{
    internal static partial class Program
    {
        public static string applicationBase = AppDomain.CurrentDomain.BaseDirectory;
        [STAThread]
        private static void Main()
        {
            TestLangid();
        }
        public static void TestLangid()
        {
            Console.WriteLine("Please enter text:");
            while (true)
            {
                string text = Console.ReadLine();
                string ldpyFilePath = "";//applicationBase + "/" + "ldpy.model";
                NLangid.LangidRecognizer langidRecognizer = new NLangid.LangidRecognizer(ldpyFilePath: ldpyFilePath);
                NLangid.TextStream stream = langidRecognizer.CreateTextStream();
                stream.AddText(text);
                NLangid.Model.LangidRecognizerResultEntity langidRecognizerResultEntity = new NLangid.Model.LangidRecognizerResultEntity();
                langidRecognizerResultEntity = langidRecognizer.GetResult(stream);
                Console.WriteLine(langidRecognizerResultEntity.Language);
            }
        }
    }
}


