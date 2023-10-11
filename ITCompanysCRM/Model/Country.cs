using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class Country
{
    public int IdCountry { get; set; }

    public string NameCountry { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
