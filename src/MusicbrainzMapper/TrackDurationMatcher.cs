using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace MusicbrainzMapper
{
    public class TrackDurationMatcher: ITrackDurationMatcher, IDisposable
    {
        private readonly NpgsqlConnection _connection;
        private const string Query = @"SELECT r.gid as release_id
                FROM medium m
                    JOIN tracklist t ON t.id = m.tracklist
                    JOIN tracklist_index ti ON ti.tracklist = t.id
                    JOIN release r ON m.release = r.id
                WHERE toc <@ create_bounding_cube(:track_durations, 5000)
                    AND track_count = :number_tracks;";

        public TrackDurationMatcher()
        {
            _connection = new NpgsqlConnection("Server=10.0.10.119;Port=5432;User Id=musicbrainz;Password=musicbrainz;Database=musicbrainz_db;");
            _connection.Open();
        }

        public async Task<IList<Guid>> FindMatchesAsync(IList<int> trackDuration)
        {
            if (trackDuration.Count == 0)
            {
                throw new ArgumentException("Cannot match empty track duration list.");
            }

            var result = new List<Guid>();
            using (var command = new NpgsqlCommand(Query, _connection))
            {
                var trackDurations = new NpgsqlParameter("track_durations",
                                                            NpgsqlDbType.Array | NpgsqlDbType.Integer);
                var numberTracks = new NpgsqlParameter("number_tracks", NpgsqlDbType.Integer);
                command.Parameters.Add(trackDurations);
                command.Parameters.Add(numberTracks);
                command.Parameters[0].Value = trackDuration;
                command.Parameters[1].Value = trackDuration.Count;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(reader.GetGuid(0));
                    }
                }
            }
            return result;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}