using System.Collections.Generic;

namespace MusicbrainzMapper
{
    public interface ITrackDurationMatcher<TId>
    {
        IList<TId> FindMatches(IList<int> trackDuration);
    }
}