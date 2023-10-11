using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class TypeOfClient
{
    public int IdTypeOfClient { get; set; }

    public string NameTypeOfClient { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
