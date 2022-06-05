using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;


internal class Explosion : SpriteGameObject, IExplosionHandler
{
    private int delay;
    private Vector2 position;
    private bool exploded = false;

    public Explosion(IExplodable obj) : base("explosion")
    {
        ExtendedGame.gameWorld.AddChild(this);
        currentSprite = new Rectangle(0, 0, 16, 16);
        delay = 0;
        SetOriginToCenter();
        obj.ShipExploded += OnShipExploded;
    }

    private void OnShipExploded(Vector2 localPosition)
    {
        position = localPosition;
        exploded = true;
    }

    public void HandleShipExploded()
    {
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (exploded)
        {
            if (delay > 5 && delay < 10)
                currentSprite.X = 16;
            else if (delay >= 10 && delay < 15)
                currentSprite.X = 32;
            else if (delay >= 15 && delay < 20)
                currentSprite.X = 48;
            else if (delay >= 20 && delay < 25)
                currentSprite.X = 64;
            else if (delay > 25) ExtendedGame.gameWorld.RemoveChild(this);
            delay++;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, position, currentSprite, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
    }
}