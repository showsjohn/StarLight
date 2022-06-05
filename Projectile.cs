using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

//TODO turn this into projectile manager
internal class Projectile : SpriteGameObject
{
    private Texture2D debug;

    public Projectile(string spriteName) : base(spriteName)
    {
        velocity = new Vector2(0, 300);
        rectangle.Width = 8;
        rectangle.Height = 8;
        debug = ExtendedGame.AssetManager.LoadSprite("debug");
        currentSprite = new Rectangle(6, 7, 5, 5);
        //SetOriginToCenter();
    }

    public Vector2 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    public Rectangle CollisionBox
    {
        get
        {
            var spriteBounds = new Rectangle(0, 0, currentSprite.Width, currentSprite.Height);
            spriteBounds.Offset(GlobalPosition - origin);
            return spriteBounds;
        }
    }


    public override void Update(GameTime gameTime)
    {
        localPosition -= velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
        spriteBatch.Draw(sprite, GlobalPosition, currentSprite, Color.White, 0f, origin, 1f, SpriteEffects.None, 1);
        //spriteBatch.Draw(debug, CollisionBox, Color.Red);
    }
}