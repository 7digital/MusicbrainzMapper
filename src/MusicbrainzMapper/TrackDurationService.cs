using System.Collections.Generic;
using System.Threading.Tasks;

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
            return new List<int>();
        }
    }
}