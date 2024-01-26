// See https://github.com/manyeyes for more information
// Copyright (c)  2023 by manyeyes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLangid.Model
{
    public class SetEntity
    {
        public int Members { get; set; }
        public int[]? Sparse { get; set; }
        public int[]? Dense { get; set; }
        public int[]? Counts { get; set; }
    }
}
