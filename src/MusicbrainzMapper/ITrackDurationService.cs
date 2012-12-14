using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicbrainzMapper
{
    public interface ITrackDurationService
    {
        Task<IList<int>> GetAsync(int releaseId);
    }
}