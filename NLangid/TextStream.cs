// See https://github.com/manyeyes for more information
// Copyright (c)  2023 by manyeyes
using NLangid.Model;
using System.Text;

namespace NLangid
{
    public class TextStream
    {
        private string? _text;
        private SetEntity _sv;
        private SetEntity _fv;
        private int _pred;
        private IdentifierModel _identifierModel;
        public TextStream(IdentifierModel identifierModel)
        {
            _identifierModel = identifierModel;
        }

        public string Text { get => _text; set => _text = value; }
        public SetEntity Fv { get => _fv; set => _fv = value; }
        public int Pred { get => _pred; set => _pred = value; }

        public void AddText(string text)
        {
            Text=text;
            TextToFV(text);
        }
        public void TextToFV(string text)
        {
            _sv = AllocSet((int)SysConfig.NUM_STATES);
            _fv = AllocSet((int)SysConfig.NUM_FEATS);
            int m, s = 0;
            
            byte[] byteArray = Encoding.UTF8.GetBytes(text); 
            foreach (byte b in byteArray)
            {
                int index = (s << 8) + b;
                int x = (int)Math.Floor((double)index / 256);
                int y = index % 256;
                s = _identifierModel.LanguageIdentifierEntity.TkNextmove[x, y];
                _sv=AddSet(_sv, s, 1);
            }
            for (int i = 0; i < _sv.Members; i++)
            {
                m = _sv.Dense[i];
                for (int j = 0; j < _identifierModel.LanguageIdentifierEntity.TkOutputC[m]; j++)
                {
                    _fv=AddSet(_fv, _identifierModel.LanguageIdentifierEntity.TkOutput[_identifierModel.LanguageIdentifierEntity.TkOutputS[m] + j], _sv.Counts[i]);
                }
            }
        }

        private SetEntity AllocSet(int size)
        {
            SetEntity s = new SetEntity();
            s.Members = 0;
            s.Sparse = new int[size];
            s.Dense = new int[size];
            s.Counts = new int[size];
            return s;
        }

        public SetEntity AddSet(SetEntity s, int key, int val)
        {
            int index = s.Sparse[key];
            if (index < s.Members && s.Dense[index] == key)
            {
                s.Counts[index] += val;
            }
            else
            {
                //s.Members++;
                index = s.Members++;
                s.Sparse[key] = index;
                s.Dense[index] = key;
                s.Counts[index] = val;
            }
            return s;
        }
    }
}
