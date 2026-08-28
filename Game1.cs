using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace _1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player player;
    private Checkpoint checkpoint;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Player.squareSize = 100;
        _graphics.PreferredBackBufferWidth  = 800;
        _graphics.PreferredBackBufferHeight = 500;
        _graphics.ApplyChanges();
        player = new Player();
        checkpoint = new Checkpoint();
        checkpoint.Init();
        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        player.LoadContent(GraphicsDevice);
        checkpoint.LoadContent(GraphicsDevice);
        Console.WriteLine(_graphics.PreferredBackBufferHeight);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        player.Update();
        if(checkpoint.isCollision == false)
        {   
            checkpoint.Update(player.Position);
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.NavajoWhite);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        player.Draw(_spriteBatch);
        if(checkpoint.isCollision == false)
        {
            checkpoint.Draw(_spriteBatch);            
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
