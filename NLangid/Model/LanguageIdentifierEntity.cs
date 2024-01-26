// See https://github.com/manyeyes for more information
// Copyright (c)  2023 by manyeyes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;

namespace NLangid.Model
{
    public enum SysConfig
    {
        NUM_FEATS = 7480,
        NUM_LANGS = 97,
        NUM_STATES = 9118,
        NUM_STATE = 256,
        NUM_ACTIONS = 725560
    }

    public class LanguageIdentifierEntity
    {
        private const int _numFeats = 7480;
        private const int _numLangs = 97;
        private const int _numStates = 9118;
        private int[,]? _tkNextmove;
        private int[]? tkOutputC;
        private int[]? _tkOutputS;
        private int[]? _tkOutput;
        private double[]? _nbPc;
        private double[]? _nbPtc;
        private string[]? _nbClasses;

        public static int NumFeats => _numFeats;
        public static int NumLangs => _numLangs;
        public static int NumStates => _numStates;
        public int[,] TkNextmove { get => _tkNextmove; set => _tkNextmove = value; }
        public int[]? TkOutputC { get => tkOutputC; set => tkOutputC = value; }
        public int[]? TkOutputS { get => _tkOutputS; set => _tkOutputS = value; }
        public int[]? TkOutput { get => _tkOutput; set => _tkOutput = value; }
        public double[]? NbPc { get => _nbPc; set => _nbPc = value; }
        public double[]? NbPtc { get => _nbPtc; set => _nbPtc = value; }
        public string[]? NbClasses { get => _nbClasses; set => _nbClasses = value; }
    }
}
