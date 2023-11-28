using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class Project
{
    public int IdProjects { get; set; }

    public string NameProjects { get; set; } = null!;

    public string PurposeProjects { get; set; } = null!;

    public DateTime StartOfDev { get; set; }

    public DateTime EndOfDev { get; set; }

    public decimal DevBudget { get; set; }

    public int IdStatusOfProject { get; set; }

    public int IdClient { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual StatusOfProject IdStatusOfProjectNavigation { get; set; } = null!;
}
