using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace MusicbrainzMapper
{
    public class TrackDurationMatcher: ITrackDurationMatcher, IDisposable
    {
        private readonly NpgsqlConnection _connection;
        private readonly NpgsqlCommand _command;

        private static readonly NpgsqlParameter TrackDurations = new NpgsqlParameter("track_durations", NpgsqlDbType.Array | NpgsqlDbType.Integer);
        private static readonly NpgsqlParameter NumberTracks = new NpgsqlParameter("number_tracks", NpgsqlDbType.Integer);
        private static readonly NpgsqlParameter Fuzziness = new NpgsqlParameter("fuzziness", NpgsqlDbType.Integer);
        private const int FuzinessFactor = 5000;

        private const string Query = @"SELECT r.gid as release_id
                FROM medium m
                    JOIN tracklist t ON t.id = m.tracklist
                    JOIN tracklist_index ti ON ti.tracklist = t.id
                    JOIN release r ON m.release = r.id
                WHERE toc <@ create_bounding_cube(:track_durations, :fuzziness)
                    AND track_count = :number_tracks;";

        //Replace this with your musicbrainz databse setup
        private const string ConnectionString = "Server=10.0.10.119;Port=5432;User Id=musicbrainz;Password=musicbrainz;Database=musicbrainz_db;";

        public TrackDurationMatcher()
        {
            _connection = new NpgsqlConnection(ConnectionString);
            _connection.Open();

            _command = new NpgsqlCommand(Query, _connection);
            _command.Parameters.Add(TrackDurations);
            _command.Parameters.Add(NumberTracks);
            _command.Parameters.Add(Fuzziness);
            _command.Parameters["fuzziness"].Value = FuzinessFactor;
        }

        public async Task<IList<Guid>> FindMatchesAsync(IList<int> trackDuration)
        {
            if (trackDuration.Count == 0)
            {
                throw new ArgumentException("Cannot match empty track duration list.");
            }

            var result = new List<Guid>();
            _command.Parameters["track_durations"].Value = trackDuration;
            _command.Parameters["number_tracks"].Value = trackDuration.Count;

            using (var reader = await _command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Add(reader.GetGuid(0));
                }
            }
            return result;
        }

        public void Dispose()
        {
            _command.Dispose();
            _connection.Dispose();
        }
    }
}