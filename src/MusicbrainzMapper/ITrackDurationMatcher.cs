using System;
using System.Collections.Generic;

namespace MusicbrainzMapper
{
    public interface ITrackDurationMatcher
    {
        IList<Guid> FindMatches(IList<int> trackDuration);
    }
}