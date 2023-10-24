using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class Staff
{
    public int IdStaff { get; set; }

    public string SecondNameStaff { get; set; } = null!;

    public string FirstNameStaff { get; set; } = null!;

    public string? MiddleNameStaff { get; set; }

    public int? IdPost { get; set; }

    public DateTime? DateOfBirthStaff { get; set; }

    public int? IdAddress { get; set; }

    public string? PhoneNumberStaff { get; set; }

    public string? EmailStaff { get; set; }

    public string? OthersData { get; set; }

    public int? IdPassport { get; set; }

    public int? IdUser { get; set; }

    public virtual Address? IdAddressNavigation { get; set; }

    public virtual Passport? IdPassportNavigation { get; set; }

    public virtual Post? IdPostNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
