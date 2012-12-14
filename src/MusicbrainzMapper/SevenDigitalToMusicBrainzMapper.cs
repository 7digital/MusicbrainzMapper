using System;
using System.Collections.Generic;

namespace MusicbrainzMapper
{
    public class SevenDigitalToMusicBrainzMapper
    {
        private readonly ITrackDurationService<int> _trackDurationService;
        private readonly ITrackDurationMatcher<Guid> _trackDurationMatcher;

        public SevenDigitalToMusicBrainzMapper(ITrackDurationService<int> trackDurationService, ITrackDurationMatcher<Guid> trackDurationMatcher)
        {
            _trackDurationService = trackDurationService;
            _trackDurationMatcher = trackDurationMatcher;
        }

        public IList<Guid> Map(int idToMap)
        {
            var tracksDuration = _trackDurationService.Get(idToMap);
            return _trackDurationMatcher.FindMatches(tracksDuration);
        }
    }
}