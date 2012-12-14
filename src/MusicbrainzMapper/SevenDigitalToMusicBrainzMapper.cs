using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicbrainzMapper
{
    public class SevenDigitalToMusicBrainzMapper
    {
        private readonly ITrackDurationService _trackDurationService;
        private readonly ITrackDurationMatcher<Guid> _trackDurationMatcher;

        public SevenDigitalToMusicBrainzMapper(ITrackDurationService trackDurationService, ITrackDurationMatcher<Guid> trackDurationMatcher)
        {
            _trackDurationService = trackDurationService;
            _trackDurationMatcher = trackDurationMatcher;
        }

        public async Task<IList<Guid>> MapAsync(int idToMap)
        {
            var tracksDuration = await _trackDurationService.GetAsync(idToMap);
            var matches = await _trackDurationMatcher.FindMatchesAsync(tracksDuration);
            return matches;
        }
    }
}