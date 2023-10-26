using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class IssuedPassport
{
    public int IdIssuedPassport { get; set; }

    public string IssuedPassport1 { get; set; } = null!;

    public int DivisionCodePassport { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
