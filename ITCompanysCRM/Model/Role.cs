using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class Role
{
    public int IdRole { get; set; }

    public string NameRole { get; set; } = null!;

    public virtual ICollection<LogBook> LogBooks { get; set; } = new List<LogBook>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
