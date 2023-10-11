using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class City
{
    public int IdCity { get; set; }

    public string NameCity { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
