using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Levels
{
    internal class MusicPlayer
    {
        public static void Initialize()
        {
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
        }

        public static void PlaySong(ContentManager content, int songNumber)
        {
            Song song;
            switch (songNumber)
            {
                case 1:
                    song = content.Load<Song>("Music/Funky Forest");
                    break;
                case 2:
                    song = content.Load<Song>("Music/Sen's Fortress");
                    break;
                default:
                    song = content.Load<Song>("Music/Sen's Fortress");
                    break;

            }
            MediaPlayer.Stop();
            MediaPlayer.Play(song);

        }
        public static void Paused(bool toggle)
        {
            if (toggle)
                MediaPlayer.Pause();
            else
                MediaPlayer.Resume();
        }

    }
}
