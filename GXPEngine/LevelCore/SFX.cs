using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class SFX : GameObject
    {
        private SoundChannel menuMusic;
        private SoundChannel levelMusic;
        private Sound swordHitSFX;
        private Sound swordWoosh;

        public float musicVolume = 0;
        public float effectVolume = 0;

        public Vector2 musicVolumePoz = new Vector2(260, 77.5f);
        public Vector2 effectVolumePoz = new Vector2(260, 169.5f);
        public SFX()
        {
            menuMusic = new Sound("Music/menuMusic.mp3", true, false).Play();
            levelMusic = new Sound("Music/levelMusic.mp3", true, false).Play();

            swordHitSFX = new Sound("Music/Sword/SwordHitSFX.mp3");
            swordWoosh = new Sound("Music/Sword/SwordWoosh.mp3");

            this.SetVolume();

            menuMusic.IsPaused = true;
            levelMusic.IsPaused = true;
        }

        public void PlayMusic(bool isLevel)
        {
            if (!isLevel)
            {
                levelMusic.IsPaused = true;
                menuMusic.IsPaused = false;
            }
            else
            {
                menuMusic.IsPaused = true;
                levelMusic.IsPaused = false;
            }
        }

        public void SetVolume()
        {
            menuMusic.Volume = musicVolume * 0.1f;
            levelMusic.Volume = musicVolume * 0.1f;
            
        }

        public void PlaySwordHit()
        {
            swordHitSFX.Play(false, 0, effectVolume * 0.1f, 0);
        }
        public void PlaySwordWoosh()
        {
            swordWoosh.Play(false, 0, effectVolume * 0.1f, 0);
        }
    }
}
