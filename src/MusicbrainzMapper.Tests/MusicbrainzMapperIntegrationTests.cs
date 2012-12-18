using System;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SevenDigital.Api.Wrapper.Exceptions;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class MusicbrainzMapperIntegrationTests
    {
        private SevenDigitalToMusicBrainzMapper _mapper;
        private MusicbrainzTrackDurationMatcher _trackDurationMatcher;
        private SevenDigitalTrackDurationService _trackDurationService;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _trackDurationService = new SevenDigitalTrackDurationService();
            _trackDurationMatcher = new MusicbrainzTrackDurationMatcher();
            _mapper = new SevenDigitalToMusicBrainzMapper(_trackDurationService, _trackDurationMatcher);
        }

        [TestCase(287887, "e956c901-acb7-48d6-9dc6-389a5f91f372", "Rage Against The Machine")]
        [TestCase(1076864, "c51eb64c-e4e2-3992-8f4e-49909853a663", "21")]
        [TestCase(499472, "3fed3ec7-96da-497e-989f-0b9e711378d2", "Emigrantski Raggamuffin")]
        [TestCase(28815, "497f9acf-a695-332e-bc20-1c8745248550", "Homework")]
        public void Maps7dReleaseIdtoMbId(int sevenDigitalId, string musicBrainzId, string releaseName)
        {
            var mbId = new Guid(musicBrainzId);
            var matches = _mapper.Map(sevenDigitalId);

            Assert.That(matches, Contains.Item(mbId), string.Format("Could not map {0}", releaseName));
        }

        [Test, Explicit]
        public void CheckLastFMData()
        {
            var result = File.Create(@"../../../../no_matches.tsv");
            using (var writer = new StreamWriter(result, Encoding.UTF8))
            {
                writer.Write("7digital release id");
                writer.Write("\t");
                writer.Write("Musicbrainz id");
                writer.Write("\t");
                writer.Write("matches found");
                writer.Write("\t");
                writer.Write("Ids match");
                writer.Write('\n');

                foreach (var line in File.ReadAllLines(@"../../../../albums.query.tsv").Take(100))
                {
                    var split = line.Split('\t');
                    var sdId = int.Parse(split[1]);
                    var mbId = new Guid(split[0]);

                    var country = split[2].Substring(7, 3);

                    _trackDurationService.Country = country.EndsWith(".")
                                                        ? country.Substring(0, 2)
                                                        : "GB";
                    writer.Write(sdId);
                    writer.Write('\t');
                    writer.Write(mbId);
                    writer.Write('\t');

                    try
                    {
                        var matches = _mapper.Map(sdId).Count;
                        writer.Write(matches);
                        writer.Write('\t');

                        if (_mapper.Map(sdId).Contains(mbId))
                        {
                            writer.Write("match");
                        }
                        else
                        {
                            writer.Write("no match");
                        }
                    }
                    catch (InvalidResourceException)
                    {
                        writer.Write(0);
                        writer.Write('\t');
                        writer.Write("no release found");
                    }
                    writer.Write('\n');
                }
            }
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _trackDurationMatcher.Dispose();
        }
    }
}