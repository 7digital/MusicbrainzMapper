using System.Threading.Tasks;
using Npgsql;

namespace MusicbrainzMapper
{
    public static class NpgsqlExtensions
    {
        public static Task<NpgsqlDataReader> ExecuteReaderActuallyAsync(this NpgsqlCommand command)
        {
            return Task.Factory.StartNew(() => command.ExecuteReader());
        }
    }
}