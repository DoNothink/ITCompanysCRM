using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class Passport
{
    public int IdPassport { get; set; }

    public int SeriesPassport { get; set; }

    public int NumberPassport { get; set; }

    public string IssuedPassport { get; set; } = null!;

    public DateTime DateOfIssuedPassport { get; set; }

    public int DivisionCodePassport { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
