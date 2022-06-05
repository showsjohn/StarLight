using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

internal class AssetManager
{
    private ContentManager Content;

    public AssetManager(ContentManager Content)
    {
        this.Content = Content;
    }

    ///<summary>Loads a Texture2D sprite object</summary>
    public Texture2D LoadSprite(string spriteName)
    {
        return Content.Load<Texture2D>(spriteName);
    }

    ///<summary>Plays a sound effect</summary>
    public void PlaySoundEffect(string soundEffectName)
    {
        SoundEffect soundEffect;
        soundEffect = Content.Load<SoundEffect>(soundEffectName);
        soundEffect.Play();
    }

    ///<summary>Plays a song. Boolean "isLooped" will loop the song</summary>
    public void PlaySong(string songName, bool isLooped)
    {
        Song song;
        song = Content.Load<Song>(songName);
        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = isLooped;
    }
}