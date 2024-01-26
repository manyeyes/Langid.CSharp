// See https://github.com/manyeyes for more information
// Copyright (c)  2023 by manyeyes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLangid.Model
{
    public class LangidRecognizerResultEntity
    {
        private string _text;
        private string _language;
        private int _languageid;

        public string Text { get => _text; set => _text = value; }
        public string Language { get => _language; set => _language = value; }
        public int Languageid { get => _languageid; set => _languageid = value; }
    }
}
