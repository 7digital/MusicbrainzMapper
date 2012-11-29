using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicbrainzMapper
{
    public interface ITrackDurationMatcher
    {
        Task<IList<string>> FindMatchesAsync(IList<int> trackDuration);
    }
}