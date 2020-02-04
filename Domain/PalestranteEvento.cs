using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class PalestranteEvento
    {
        public int FkEvento { get; set; }
        public int FkPalestrante { get; set; }
        public Palestrante Palestrante { get; }
        public Evento Evento { get;  }
    }
}
