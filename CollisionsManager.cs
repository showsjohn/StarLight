using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


internal class CollisionsManager : GameObject
{
    private int delay, delay1, delay2;
    public List<Projectile> playerBullets;
    public List<Projectile> enemyBullets;
    public List<Enemy> enemies;
    protected IMoveBehavior moveBehavior;

    public CollisionsManager()
    {
        playerBullets = new List<Projectile>();
        enemyBullets = new List<Projectile>();
        enemies = new List<Enemy>();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (delay > 180)
        {
            //Enemy enemy2 = new Enemy("enemy-small", new Vector2(ExtendedGame.random.Next(ExtendedGame.worldSize.X - 50), 0));
            enemies.Add(new EnemyBig(new Vector2(ExtendedGame.random.Next(25, ExtendedGame.worldSize.X - 25), -25)));
            delay = 0;
        }

        if (delay1 > 200)
        {
            Enemy enemy2 = new EnemyMedium(new Vector2(ExtendedGame.random.Next(25, ExtendedGame.worldSize.X - 25), -25));
            moveBehavior = new MoveDownStraight();
            enemy2.SetMoveBehavior(moveBehavior);
            enemies.Add(enemy2);
            delay1 = 0;
        }
        if (delay2 > 150)
        {
            //Enemy enemy2 = new Enemy("enemy-small", new Vector2(ExtendedGame.random.Next(ExtendedGame.worldSize.X - 50), 0));
            enemies.Add(new EnemySmall(new Vector2(ExtendedGame.random.Next(25, ExtendedGame.worldSize.X - 25), -25)));

            delay2 = 0;
        }

            delay++;
            delay1++;
            delay2++;
        //check enemes for bullet collision, remove enemy and player bullet if they collide
        for (var i = 0; i < playerBullets.Count; i++)
        for (var z = 0; z < enemies.Count; z++)
        {
            if (i < playerBullets.Count && playerBullets[i].CollisionBox.Intersects(enemies[z].CollisionBox))
            {
                enemies[z].TakeDamage(10);
                playerBullets.RemoveAt(i);
            }

            //remove player bullets if they go above top of screen
            if (i < playerBullets.Count && playerBullets[i].GlobalPosition.Y+ playerBullets[i].CollisionBox.Height/2 < 1) playerBullets.RemoveAt(i);
        }
         

        //check if enemy bullets collide with the player ship
        for (var i = 0; i < enemyBullets.Count; i++)
        {
            if (ExtendedGame.player.CollisionBox.Intersects(enemyBullets[i].CollisionBox))
            {
                enemyBullets.RemoveAt(i);
                ExtendedGame.player.TakeDamage(10);
            }
            if (i < enemyBullets.Count && enemyBullets[i].localPosition.Y-enemyBullets[i].CollisionBox.Height*3 > ExtendedGame.worldSize.X)
            {
                enemyBullets.RemoveAt(i);
            }
        }
            

        //Update enemies, enemy bullets, and player bullets
        for (var i = 0; i < enemies.Count; i++) enemies[i].Update(gameTime);

        for (var i = 0; i < playerBullets.Count; i++) playerBullets[i].Update(gameTime);

        for (var i = 0; i < enemyBullets.Count; i++) enemyBullets[i].Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        foreach (var enemy in enemies) enemy.Draw(gameTime, spriteBatch);

        foreach (var bullet in playerBullets) bullet.Draw(gameTime, spriteBatch);

        foreach (var bullet in enemyBullets) bullet.Draw(gameTime, spriteBatch);
    }
}