using System;
using System.Collections.Generic;

namespace DumMovie.Api
{
    public partial class MovieTypePreference
    {
        public int Id { get; set; }
        public int IdMovie { get; set; }
        public int IdTypePreference { get; set; }

        public virtual Movie IdMovieNavigation { get; set; } = null!;
        public virtual TypePreference IdTypePreferenceNavigation { get; set; } = null!;
    }
}
