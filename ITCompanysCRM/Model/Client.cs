using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class Client
{
    public int IdClient { get; set; }

    public int IdTypeOfClient { get; set; }

    public string NameClient { get; set; } = null!;

    public string? EmailClient { get; set; }

    public string PhoneNumberClient { get; set; } = null!;

    public string ServicesClient { get; set; } = null!;

    public string? OthersData { get; set; }

    public virtual TypeOfClient IdTypeOfClientNavigation { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
