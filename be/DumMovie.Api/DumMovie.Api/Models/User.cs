using System;
using System.Collections.Generic;

namespace DumMovie.Api
{
    public partial class User
    {
        public User()
        {
            UserTypePreferences = new HashSet<UserTypePreference>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<UserTypePreference> UserTypePreferences { get; set; }
    }
}
