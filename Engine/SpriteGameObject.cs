using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;


public class SpriteGameObject : GameObject
{
    protected Texture2D sprite;
    protected Vector2 origin;
    private bool isMoving;
    protected Rectangle rectangle, spriteBounds;
    protected Rectangle currentSprite;
    
    public SpriteGameObject(string spriteName)
    {
        sprite = ExtendedGame.AssetManager.LoadSprite(spriteName);
        origin = Vector2.Zero;
    }

    public Rectangle CollisionBox
    {
        get
        {
            spriteBounds = new Rectangle(0, 0, currentSprite.Width, currentSprite.Height);
            spriteBounds.Offset(GlobalPosition - origin);
            return spriteBounds;
        }
    }



    public void SetOriginToCenter()
    {
        origin = new Vector2(currentSprite.Width, currentSprite.Height) / 2;
    }

    public void MoveUp()
    {
        isMoving = true;
        localPosition.Y -= velocity.Y * deltaTime;
    }

    public void MoveDown()
    {
        localPosition.Y += velocity.Y * deltaTime;
        isMoving = true;
    }

    public void MoveLeft()
    {
        localPosition.X -= velocity.X * deltaTime;
        isMoving = true;
    }

    public void MoveRight()
    {
        localPosition.X += velocity.X * deltaTime;
        isMoving = true;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (isVisible)
            spriteBatch.Draw(sprite, GlobalPosition, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
    }
}