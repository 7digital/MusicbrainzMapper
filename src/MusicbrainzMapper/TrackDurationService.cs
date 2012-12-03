using System.Collections.Generic;
using System.Threading.Tasks;
using SevenDigital.Api.Schema.ReleaseEndpoint;
using SevenDigital.Api.Wrapper;
using System.Linq;

namespace MusicbrainzMapper
{
    public interface ITrackDurationService
    {
        Task<IList<int>> GetAsync(int releaseId);
    }

    public class TrackDurationService : ITrackDurationService
    {
        public async Task<IList<int>> GetAsync(int releaseId)
        {
            var releaseTracks = await Api<ReleaseTracks>.Create
                                                 .ForReleaseId(releaseId)
                                                 .PleaseAsync();

            return releaseTracks.Tracks
                .Select(track => ConvertSecondsToMilliseconds(track.Duration))
                .ToArray();
        }

        private static int ConvertSecondsToMilliseconds(int duration)
        {
            return duration * 1000;
        }
    }
}