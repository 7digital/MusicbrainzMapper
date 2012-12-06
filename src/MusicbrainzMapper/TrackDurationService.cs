using System.Collections.Generic;
using SevenDigital.Api.Schema.ReleaseEndpoint;
using SevenDigital.Api.Wrapper;
using System.Linq;

namespace MusicbrainzMapper
{
    public interface ITrackDurationService
    {
        IList<int> Get(int releaseId);
    }

    public class TrackDurationService : ITrackDurationService
    {
        public IList<int> Get(int releaseId)
        {
            return Api<ReleaseTracks>.Create
                                     .ForReleaseId(releaseId)
                                     .Please()
                                     .Tracks
                                     .Select(track => ConvertSecondsToMilliseconds(track.Duration))
                                     .ToArray();
        }

        private static int ConvertSecondsToMilliseconds(int duration)
        {
            return duration * 1000;
        }
    }
}