using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class StatusOfProject
{
    public int IdStatusOfProject { get; set; }

    public string NameStatusOfProject { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
