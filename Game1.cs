using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


internal class Game1 : ExtendedGame
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    GameTime gameTime;
    public static float DeltaTime(GameTime gameTime)
    {
        return (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public Game1()
    {
    }


    protected override void LoadContent()
    {
        base.LoadContent();
        gameTime = new GameTime();
        
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        DeltaTime(gameTime);

    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        base.Draw(gameTime);
    }
}