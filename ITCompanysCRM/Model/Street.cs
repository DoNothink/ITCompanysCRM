using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class Street
{
    public int IdStreet { get; set; }

    public string NameStreet { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
