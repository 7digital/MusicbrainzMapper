using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicbrainzMapper
{
    public interface ITrackDurationMatcher
    {
        Task<IList<Guid>> FindMatchesAsync(IList<int> trackDuration);
    }
}