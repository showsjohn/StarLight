using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


internal class Player : SpriteGameObject, IExplodable
{
    private Rectangle currentSprite;
    private int delay = 20;
    private int flickerTimer = 0;
    private int timesFlicked = 0;
    public int playerHP;
    private Texture2D debug;
    private bool isMoving;
    public event Action<Vector2> ShipExploded;
    Vector2 previousPosition;
    public Player() : base("ship")
    {
        currentSprite = new Rectangle(32, 0, 16, 24);
        velocity = new Vector2(300, 300);
        localPosition = new Vector2(100, 200);
        rectangle = new Rectangle((int)localPosition.X, (int)localPosition.Y, sprite.Width, sprite.Height);
        debug = ExtendedGame.AssetManager.LoadSprite("debug");
        SetOriginToCenter();
        playerHP = 10;
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
        deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        isMoving = false;

        if (ExtendedGame.inputHandler.IsKeyDown(Keys.W) && localPosition.Y > 0) MoveUp();
        if (ExtendedGame.inputHandler.IsKeyDown(Keys.S) &&
            localPosition.Y + currentSprite.Height < ExtendedGame.worldSize.Y) MoveDown();
        if (ExtendedGame.inputHandler.IsKeyDown(Keys.A) && localPosition.X > 0) MoveLeft();
        if (ExtendedGame.inputHandler.IsKeyDown(Keys.D) &&
            localPosition.X + currentSprite.Width < ExtendedGame.worldSize.X) MoveRight();

        if (!isMoving) currentSprite = new Rectangle(32, 0, 16, 24);
        if (ExtendedGame.inputHandler.IsKeyDown(Keys.Space) && delay > 5) Fire();
        delay++;

        if (playerHP <= 0)
        {
            Respawn();
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Draw(debug, CollisionBox, Color.White);
        if (isVisible)
            spriteBatch.Draw(sprite, new Vector2((int)GlobalPosition.X, (int)GlobalPosition.Y), currentSprite,
                Color.White, 0f, origin, 1, SpriteEffects.None, 0);
    }

    public void TakeDamage(int amount)
    {
       
            playerHP -= amount;
        
        if (playerHP <= 0 && flickerTimer == 0)
        {
            var explosion = new Explosion(this);
            ShipExploded(localPosition);
            ExtendedGame.score -= 500;
        }
    }
    
    public void Respawn()
    {

        
        if (flickerTimer >= 0 && flickerTimer < 16)
        {
            isVisible = false;
        }
        else if(flickerTimer >= 16 && flickerTimer < 31) 
        { 
            isVisible = true;
            
        }
        else if(flickerTimer > 31)
        {
            timesFlicked++;
            flickerTimer = 0;
        }

        if(timesFlicked == 0)
        {
            Reset();
        }
        
        else if ( timesFlicked >= 6)
        {
            isVisible = true;
            flickerTimer = 0;
            timesFlicked = 0;
            playerHP = 10;
            return;
        }
        flickerTimer++;
    }

    public override void Reset()
    {
            
            localPosition = new Vector2(140, 200);
            velocity = new Vector2(300, 300);
    }

    public void MoveUp()
    {
        currentSprite = new Rectangle(32, 0, 16, 24);
        isMoving = true;
        localPosition.Y -= velocity.Y * deltaTime;
    }

    public void MoveDown()
    {
        currentSprite = new Rectangle(32, 0, 16, 24);
        localPosition.Y += velocity.Y * deltaTime;
        isMoving = true;
    }

    public void MoveLeft()
    {
        currentSprite = new Rectangle(0, 0, 16, 24);
        localPosition.X -= velocity.X * deltaTime;
        isMoving = true;
    }

    public void MoveRight()
    {
        currentSprite = new Rectangle(64, 0, 16, 24);
        localPosition.X += velocity.X * deltaTime;
        isMoving = true;
    }

    public void Fire()
    {
        {
            var projectile = new Projectile("bullet");
            projectile.localPosition = new Vector2((int)GlobalPosition.X + currentSprite.Width / 3, (int)GlobalPosition.Y);
            ExtendedGame.gameManager.playerBullets.Add(projectile);
            delay = 0;
        }
    }
}