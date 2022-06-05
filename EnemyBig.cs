using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


internal class EnemyBig : Enemy
{
    private int delay;
    private Texture2D debug;

    public EnemyBig(Vector2 startPosition) : base("enemy-big", startPosition)
    {
        debug = ExtendedGame.AssetManager.LoadSprite("debug");
        currentSprite = new Rectangle(3, 2, 26, 30);
        SetOriginToCenter();
        moveBehavior = new MoveDownWavy();
    }

    public Rectangle CollisionBox
    {
        get
        {
            spriteBounds = new Rectangle(0, 11, currentSprite.Width, currentSprite.Height - 11);
            spriteBounds.Offset(GlobalPosition - origin);
            return spriteBounds;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Move();
        CheckOffScreen();

        if (delay > 20)
        {
            Fire();
            delay = 0;
        }

        delay++;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, new Vector2((int) GlobalPosition.X, (int) GlobalPosition.Y), currentSprite,
            Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
    }
    

    private void Fire()
    {
        var projectile = new Projectile("bullet");
        projectile.Velocity = new Vector2(250, -250);
        projectile.localPosition = new Vector2(GlobalPosition.X - 13, GlobalPosition.Y + currentSprite.Height - 18);
        ExtendedGame.gameManager.enemyBullets.Add(projectile);

        var projectile2 = new Projectile("bullet");
        projectile2.Velocity = new Vector2(-250, -250);
        projectile2.localPosition = new Vector2(GlobalPosition.X + 13, GlobalPosition.Y + currentSprite.Height - 18);
        ExtendedGame.gameManager.enemyBullets.Add(projectile2);
    }

    private void CheckOffScreen()
    {
        if (GlobalPosition.Y > ExtendedGame.worldSize.Y) ExtendedGame.gameManager.enemies.Remove(this);
    }
}