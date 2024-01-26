// See https://github.com/manyeyes for more information
// Copyright (c)  2023 by manyeyes
using NLangid.Model;
using Google.Protobuf;
using System.Security.Cryptography;
using Langid;
using System.Linq;
using System;
using System.IO;

namespace NLangid
{
    public class IdentifierModel
    {
        private LanguageIdentifierEntity _languageIdentifierEntity;

        public LanguageIdentifierEntity LanguageIdentifierEntity { get => _languageIdentifierEntity; set => _languageIdentifierEntity = value; }

        public IdentifierModel(string ldpyFilePath = "", string acquisFilePath = "")
        {
            if (string.IsNullOrEmpty(ldpyFilePath))
            {

                _languageIdentifierEntity = InitDefaultModel();
            }
            else
            {
                _languageIdentifierEntity = InitProtobufModel(ldpyFilePath, acquisFilePath);
            }
        }

        public LanguageIdentifierEntity InitProtobufModel(string ldpyFilePath, string acquisFilePath = "")
        {
            LanguageIdentifierEntity languageIdentifierEntity = new LanguageIdentifierEntity();
            LanguageIdentifier languageIdentifier;
            using (var input = new FileStream(ldpyFilePath,FileMode.Open))
            {

                byte[] bts=new byte[input.Length];
                input.Read(bts);
                languageIdentifier = LanguageIdentifier.Parser.ParseFrom(bts);
            }
            //mmap
            if (languageIdentifier != null)
            {
                for (int i = 0; i < languageIdentifier.TkNextmove.Count / 256; i++)
                {
                    int[] item = new int[256];
                    for (int j = 0; i < 256; j++)
                    {
                        languageIdentifierEntity.TkNextmove[i,j] = languageIdentifier.TkNextmove[i * j]; 
                    }                    
                }
                Console.Write("num_feats: {0} num_langs: {1} num_states: {2}\n", languageIdentifier.NumFeats, languageIdentifier.NumLangs, languageIdentifier.NumStates);
                for (int i = 0; i < languageIdentifier.NumLangs; i++)
                {
                    Console.Write("  lang:{0}\n", languageIdentifier.NbClasses[i]);
                    Console.Write("  lid->nb_pc:%lf\n", languageIdentifier.NbPc[i]);
                    Console.Write("  msg->nb_pc:%lf\n", languageIdentifier.NbPc[i]);
                }
            }
            return languageIdentifierEntity;

        }
        public LanguageIdentifierEntity InitDefaultModel()
        {
            LanguageIdentifierEntity languageIdentifier = new LanguageIdentifierEntity();
            languageIdentifier.TkNextmove = LangidModel.tk_nextmove;
            languageIdentifier.TkOutputC = LangidModel.tk_output_c;
            languageIdentifier.TkOutputS = LangidModel.tk_output_s;
            languageIdentifier.TkOutput = LangidModel.tk_output;
            languageIdentifier.NbPc = LangidModel.nb_pc;
            languageIdentifier.NbPtc = LangidModel.nb_ptc;
            languageIdentifier.NbClasses = LangidModel.nb_classes;
            return languageIdentifier;
        }
    }
}