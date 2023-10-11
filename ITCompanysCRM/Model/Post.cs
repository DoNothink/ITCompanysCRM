using System;
using System.Collections.Generic;

namespace ITCompanysCRM.Model;

public partial class Post
{
    public int IdPost { get; set; }

    public string NamePost { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
