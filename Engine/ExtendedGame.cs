using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
enum gameState { StartScreen, playing }
internal class ExtendedGame : Game
{
    gameState currentState;
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    public static Point worldSize;
    private Point windowSize;
    private Matrix scale;
    public static InputHandler inputHandler;
    private Texture2D background;
    public static Random random;
    
    public static AssetManager AssetManager { get; private set; }


    private bool FullScreen
    {
        get => graphics.IsFullScreen;
        set => ApplyResolutionSettings(value);
    }

    public static GameObjectList gameWorld;

    public static CollisionsManager gameManager;
    public static TextManager textManager;
    public static Player player;
    public static int score;
    SpriteGameObject SplashScreen;
    private int delay;

    public ExtendedGame()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        random = new Random();
        AssetManager = new AssetManager(Content);
        inputHandler = new InputHandler();
        gameWorld = new GameObjectList();
        gameManager = new CollisionsManager();
        currentState = new gameState();
        score = 0;
        
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        background = AssetManager.LoadSprite("background2");
        worldSize = new Point(background.Width, background.Height);
        windowSize = new Point(1280, 720);
        FullScreen = false;
        ApplyResolutionSettings(FullScreen);
        IsFixedTimeStep = true;
        graphics.SynchronizeWithVerticalRetrace = true;



        textManager = new TextManager(8, 8, 16, 0.5f);
        player = new Player();
        gameWorld.AddChild(textManager);
        gameWorld.AddChild(player);

        SplashScreen = new SpriteGameObject("Pause");
        currentState = gameState.StartScreen;
        
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (currentState == gameState.playing)
        {
           
            gameWorld.Update(gameTime);
            gameManager.Update(gameTime);
            textManager.DrawTimer(new Vector2(background.Width / 2, 5));
            
            delay++;
        }
        
        else
        {
            SplashScreen.Update(gameTime);
        }

        textManager.TextToScreen($"SCORE:   {score}", new Vector2(10, 5));
        textManager.TextToScreen($"HEALTH: {player.playerHP}", new Vector2(background.Width - 80, 5));
        inputHandler.Update(gameTime);

        if (inputHandler.KeyWasPressed(Keys.Tab))
        {
            FullScreen = !FullScreen;
        }
        if (inputHandler.KeyWasPressed(Keys.Enter))
        {
            if (currentState == gameState.playing)
            {
                currentState = gameState.StartScreen;

            }
            else
            {
                currentState = gameState.playing;
            }
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, scale);
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        gameWorld.Draw(gameTime, spriteBatch);
        gameManager.Draw(gameTime, spriteBatch);

        if(currentState == gameState.StartScreen)
        {
            SplashScreen.Draw(gameTime, spriteBatch);
        }

        spriteBatch.End();
    }

    



    protected void ApplyResolutionSettings(bool FullScreen)
    {
        Point screenSize;
        graphics.IsFullScreen = FullScreen;
        if (FullScreen)
        {
            screenSize = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        }
            
        else
            screenSize = windowSize;
        graphics.PreferredBackBufferWidth = screenSize.X;
        graphics.PreferredBackBufferHeight = screenSize.Y;
        graphics.ApplyChanges();
        GraphicsDevice.Viewport = GetAspectRatio(screenSize);
        scale = Matrix.CreateScale((float) GraphicsDevice.Viewport.Width / worldSize.X,
        (float) GraphicsDevice.Viewport.Height / worldSize.Y, 1);
    }

    protected Viewport GetAspectRatio(Point windowSize)
    {
        var viewPort = new Viewport();
        var gameAspectRatio = (float) worldSize.X / worldSize.Y;
        var windowAspectRatio = (float) windowSize.X / windowSize.Y;

        if (gameAspectRatio > windowAspectRatio)
        {
            viewPort.Width = windowSize.X;
            viewPort.Height = (int) (windowSize.X / gameAspectRatio);
        }
        else
        {
            viewPort.Width = (int) (windowSize.Y * gameAspectRatio);
            viewPort.Height = windowSize.Y;
        }

        viewPort.X = (windowSize.X - viewPort.Width) / 2;
        viewPort.Y = (windowSize.Y - viewPort.Height) / 2;

        return viewPort;
    }
}