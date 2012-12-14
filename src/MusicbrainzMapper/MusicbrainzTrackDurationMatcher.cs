using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

namespace MusicbrainzMapper
{
    public class MusicbrainzTrackDurationMatcher: ITrackDurationMatcher<Guid>, IDisposable
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

        public MusicbrainzTrackDurationMatcher(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();

            _command = _connection.CreateCommand();

            _command.CommandText = Query;
            _command.Parameters.Add(TrackDurations);
            _command.Parameters.Add(NumberTracks);
            _command.Parameters.Add(Fuzziness);
            _command.Parameters["fuzziness"].Value = FuzinessFactor;
        }

        public IList<Guid> FindMatches(IList<int> trackDuration)
        {
            if (trackDuration.Count == 0)
            {
                throw new ArgumentException("Cannot match empty track duration list.");
            }

            var result = new List<Guid>();
            _command.Parameters["track_durations"].Value = trackDuration;
            _command.Parameters["number_tracks"].Value = trackDuration.Count;

            using (var reader = _command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add((Guid) reader["release_id"]);
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