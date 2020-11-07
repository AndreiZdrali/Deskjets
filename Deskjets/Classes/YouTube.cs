using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using Deskjets.Settings;

namespace Deskjets.Classes
{
    static class YouTube
    {
        public static async void DownloadVideoAsync(string url, string path)
        {
            YoutubeClient youtube = new YoutubeClient();

            Video video = await youtube.Videos.GetAsync(url); //ca sa pot sa iau id-ul de la video fara sa folosesc regex
            StreamManifest streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
            IStreamInfo streamInfo = streamManifest.GetMuxed().WithHighestVideoQuality();

            if (streamInfo != null)
            {
                await youtube.Videos.Streams.DownloadAsync(streamInfo, Path.Combine(path, $"{video.Title}.mp4"));
            }
        }

        public static async void DownloadAudioAsync(string url, string path)
        {
            YoutubeClient youtube = new YoutubeClient();

            Video video = await youtube.Videos.GetAsync(url); //ca sa pot sa iau id-ul de la video fara sa folosesc regex

            StreamManifest streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
            IStreamInfo streamInfo = streamManifest.GetAudioOnly().WithHighestBitrate();

            if (streamInfo != null)
            {
                await youtube.Videos.Streams.DownloadAsync(streamInfo, Path.Combine(path, $"{video.Title}.mp3"));
            }
        }
    }
}
