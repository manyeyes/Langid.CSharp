// See https://github.com/manyeyes for more information
// Copyright (c)  2023 by manyeyes
using NLangid.Model;

namespace NLangid
{
    public class LangidRecognizer
    {
        private IdentifierModel _identifierModel;
        public LangidRecognizer(string ldpyFilePath = "", string acquisFilePath = "")
        {
            _identifierModel = new IdentifierModel(ldpyFilePath, acquisFilePath);
        }
        public TextStream CreateTextStream()
        {
            TextStream textStream = new TextStream(_identifierModel);
            return textStream;
        }
        public LangidRecognizerResultEntity GetResult(TextStream stream)
        {
            LangidRecognizerResultEntity langidRecognizerResultEntity = new LangidRecognizerResultEntity();
            Forward(stream);
            string language = DecodeOne(stream);
            langidRecognizerResultEntity.Text = stream.Text;
            langidRecognizerResultEntity.Language = language;
            langidRecognizerResultEntity.Languageid = stream.Pred;
            return langidRecognizerResultEntity;
        }
        public void Forward(TextStream stream)
        {
            double[] logprob = new double[LangidModel.NUM_LANGS];
            FVToLogprob(stream, ref logprob);
            int pred = LogprobToPred(logprob);
            stream.Pred = pred;
        }

        public void FVToLogprob(TextStream stream, ref double[] logprob)
        {
            /* Initialize using prior */
            for (int i = 0; i < LangidModel.NUM_LANGS; i++)
            {
                logprob[i] = _identifierModel.LanguageIdentifierEntity.NbPc[i];
            }
            /* Compute posterior for each class */
            for (int i = 0; i < stream.Fv.Members; i++)
            {
                int m = stream.Fv.Dense[i];
                /* NUM_FEATS * NUM_LANGS */
                double nbPtcP = _identifierModel.LanguageIdentifierEntity.NbPtc[m * LangidModel.NUM_LANGS];
                for (int j = 0; j < LangidModel.NUM_LANGS; j++)
                {
                    logprob[j] += stream.Fv.Counts[i] * nbPtcP;
                    nbPtcP = _identifierModel.LanguageIdentifierEntity.NbPtc[m * LangidModel.NUM_LANGS+j+1];
                }
            }
        }

        public int LogprobToPred(double[] logprob)
        {
            int m = 0;

            for (int i = 1; i < LangidModel.NUM_LANGS; i++)
            {
                if (logprob[m] < logprob[i])
                {
                    m = i;
                }
            }

            return m;
        }

        public string DecodeOne(TextStream stream)
        {
            return _identifierModel.LanguageIdentifierEntity.NbClasses[stream.Pred];
        }
    }
}
