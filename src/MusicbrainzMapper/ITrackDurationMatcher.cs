using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicbrainzMapper
{
    public interface ITrackDurationMatcher<TId>
    {
        Task<IList<TId>> FindMatchesAsync(IList<int> trackDuration);
    }
}