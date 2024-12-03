using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookJurnalLibrary
{
    [Flags]
    public enum genre //создание перечисления жанров 
    {
        Comedy,
        Thriller,
        Horror,
        Novel,
        ScienceFiction
    }
}
