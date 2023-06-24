using System;
using System.Collections.Generic;

namespace DumMovie.Api
{
    public partial class Movie
    {
        public Movie()
        {
            MovieTypePreferences = new HashSet<MovieTypePreference>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<MovieTypePreference> MovieTypePreferences { get; set; }
    }
}
