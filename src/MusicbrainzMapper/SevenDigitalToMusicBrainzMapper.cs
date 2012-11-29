using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicbrainzMapper
{
    public class SevenDigitalToMusicBrainzMapper
    {
        private readonly ITrackDurationService _trackDurationService;
        private readonly ITrackDurationMatcher _trackDurationMatcher;

        public SevenDigitalToMusicBrainzMapper(ITrackDurationService trackDurationService, ITrackDurationMatcher trackDurationMatcher)
        {
            _trackDurationService = trackDurationService;
            _trackDurationMatcher = trackDurationMatcher;
        }

        public async Task<IList<string>> MapAsync(int idToMap)
        {
            var tracksDuration = await _trackDurationService.GetAsync(idToMap);
            var matches = await _trackDurationMatcher.FindMatchesAsync(tracksDuration);
            return matches;
        }
    }
}