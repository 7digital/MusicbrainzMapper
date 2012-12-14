using System.Collections.Generic;

namespace MusicbrainzMapper
{
    public interface ITrackDurationService<in TId>
    {
        IList<int> Get(TId releaseId);
    }
}