
using System.Diagnostics.CodeAnalysis;
using VideoGameAPI.Models;

namespace VideoGameAPI.Helper
{
    public class GenreEqualComparer : EqualityComparer<Genre>
    {
        public override bool Equals(Genre? x, Genre? y)
        {
            if (x != null && y == null || x != null && y == null)
                return false;
            return x.Name == y.Name;
        }

        public override int GetHashCode([DisallowNull] Genre obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
