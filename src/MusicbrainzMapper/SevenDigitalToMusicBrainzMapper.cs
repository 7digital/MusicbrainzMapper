using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicbrainzMapper
{
    public class SevenDigitalToMusicBrainzMapper
    {
        private readonly ITrackDurationService _trackDurationService;

        public SevenDigitalToMusicBrainzMapper(ITrackDurationService trackDurationService)
        {
            _trackDurationService = trackDurationService;
        }

        public async Task<IList<string>> MapAsync(int idToMap)
        {
            var trackDurations = await _trackDurationService.GetAsync(idToMap);
            return new List<string>();
        }
    }
}