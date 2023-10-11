using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class DevTeam
{
    public int IdStaff { get; set; }

    public int IdProjects { get; set; }

    public virtual Project IdProjectsNavigation { get; set; } = null!;

    public virtual Staff IdStaffNavigation { get; set; } = null!;
}
