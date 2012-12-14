using System.Collections.Generic;

namespace MusicbrainzMapper
{
    public interface ITrackDurationService
    {
        IList<int> Get(int releaseId);
    }
}