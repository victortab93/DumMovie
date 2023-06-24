using System;
using System.Collections.Generic;

namespace DumMovie.Api
{
    public partial class UserTypePreference
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdTypePreference { get; set; }

        public virtual TypePreference IdTypePreferenceNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
