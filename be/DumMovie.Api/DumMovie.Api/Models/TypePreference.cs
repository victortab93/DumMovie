using System;
using System.Collections.Generic;

namespace DumMovie.Api
{
    public partial class TypePreference
    {
        public TypePreference()
        {
            MovieTypePreferences = new HashSet<MovieTypePreference>();
            UserTypePreferences = new HashSet<UserTypePreference>();
        }

        public int TypePreferenceId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<MovieTypePreference> MovieTypePreferences { get; set; }
        public virtual ICollection<UserTypePreference> UserTypePreferences { get; set; }
    }
}
