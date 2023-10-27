using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class LogBook
{
    public int IdLogBook { get; set; }

    public int IdUser { get; set; }

    public int IdRole { get; set; }

    public string Description { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
