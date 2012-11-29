using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicbrainzMapper
{
    public class TrackDurationMatcher: ITrackDurationMatcher
    {
        public async Task<IList<string>> FindMatchesAsync(IList<int> trackDuration)
        {
            return new List<string>();
        }
    }
}