using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicbrainzMapper
{
    public interface ITrackDurationMatcher
    {
        IList<Guid> FindMatches(IList<int> trackDuration);
    }
}