using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class Address
{
    public int IdAddress { get; set; }

    public int IdCountry { get; set; }

    public int IdCity { get; set; }

    public int IdStreet { get; set; }

    public string HomeAddress { get; set; } = null!;

    public int FlatAddress { get; set; }

    public virtual City IdCityNavigation { get; set; } = null!;

    public virtual Country IdCountryNavigation { get; set; } = null!;

    public virtual Street IdStreetNavigation { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
