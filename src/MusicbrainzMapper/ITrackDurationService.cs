using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicbrainzMapper
{
    public interface ITrackDurationService<in TId>
    {
        Task<IList<int>> GetAsync(TId releaseId);
    }
}