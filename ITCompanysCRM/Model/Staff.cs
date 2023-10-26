using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class Staff
{
    public int IdStaff { get; set; }

    public string SecondNameStaff { get; set; } = null!;

    public string FirstNameStaff { get; set; } = null!;

    public string? MiddleNameStaff { get; set; }

    public int IdPost { get; set; }

    public DateTime DateOfBirthStaff { get; set; }

    public int IdAddress { get; set; }

    public string PhoneNumberStaff { get; set; } = null!;

    public string? EmailStaff { get; set; }

    public string? OthersData { get; set; }

    public int SeriesPassport { get; set; }

    public int IdUser { get; set; }

    public int NumberPassport { get; set; }

    public DateTime DateOfIssuedPassport { get; set; }

    public int IdIssuedPassport { get; set; }

    public virtual Address IdAddressNavigation { get; set; } = null!;

    public virtual IssuedPassport IdIssuedPassportNavigation { get; set; } = null!;

    public virtual Post IdPostNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
