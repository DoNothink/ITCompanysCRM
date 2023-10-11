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

    public int IdPassport { get; set; }

    public virtual Address IdAddressNavigation { get; set; } = null!;

    public virtual Passport IdPassportNavigation { get; set; } = null!;

    public virtual Post IdPostNavigation { get; set; } = null!;
}
