using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



internal class EnemySmall: Enemy
    {
    private int delay;
    private Texture2D debug;

    public EnemySmall(Vector2 startPosition) : base("enemy-small", startPosition)
    {
        debug = ExtendedGame.AssetManager.LoadSprite("debug");
        currentSprite = new Rectangle(0, 0, sprite.Width/2, sprite.Height);
        SetOriginToCenter();
        moveBehavior = new MoveDownWavy();
        health = 10;
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

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Move();
        CheckOffScreen();

        if (delay > 60)
        {
            Fire();
            delay = 0;
        }

        delay++;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, new Vector2((int)GlobalPosition.X, (int)GlobalPosition.Y), currentSprite,
            Color.White, 0f, origin, 1, SpriteEffects.None, 0);
    }


    private void Fire()
    {
        var projectile = new Projectile("bullet");
        projectile.Velocity = new Vector2(0, -250);
        projectile.localPosition = new Vector2(GlobalPosition.X, GlobalPosition.Y + currentSprite.Height / 2);
        ExtendedGame.gameManager.enemyBullets.Add(projectile);
    }

    private void CheckOffScreen()
    {
        if (GlobalPosition.Y > ExtendedGame.worldSize.Y) ExtendedGame.gameManager.enemies.Remove(this);
    }
}

