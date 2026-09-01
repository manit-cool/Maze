using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace _1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player player;
    private Checkpoint checkpoint;
    List<Tiles> tiles = new List<Tiles>();
    List<Apple> apples = new List<Apple>();
    private int appleCount;

    private int[,] mapGridY =
    //Each value represents a y multiplier! I would preferabbly have a set of {} per each multiplier :D
    // This is really inefficient, but i didn't know how else to do it,
    // so it's stuck that way, for now:) --> might be different for other project:)
    {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},
        {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4},
        {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6},
        {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7},
        {8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8},
        {9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9}
    };

    private int[,] mapGridX =
    // 0-15 are tiles
    // 99 is blank
    // 20 is player
    // 21 is apple
    // 22 checkpoints
    // 67 enemies
    {
        {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15},
        {0,20,2,99,99,99,6,99,99,99,99,99,99,99,99,15},
        {0,99,2,99,4,99,6,99,8,99,99,99,99,99,99,15},
        {0,99,2,99,4,99,99,99,8,99,99,99,99,99,99,15},
        {0,99,2,99,4,99,6,99,8,99,99,99,99,99,99,15},
        {0,99,2,99,4,99,6,99,8,99,99,99,99,99,21,15},
        {0,99,2,99,4,99,6,99,8,99,99,99,99,99,99,15},
        {0,99,2,99,4,99,6,99,8,99,99,99,99,22,99,15},
        {0,99,99,99,4,99,99,8,99,99,99,99,99,99,99,15},
        {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15}
    };
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Player.squareSize = 50;
        appleCount = 0;
        _graphics.PreferredBackBufferWidth  = 800;
        _graphics.PreferredBackBufferHeight = 500;
        _graphics.ApplyChanges();
        player = new Player();
        checkpoint = new Checkpoint();
        checkpoint.Init();
        for(int y = 0; y < mapGridY.GetLength(0); y++)
        {
            for(int x = 0; x < mapGridX.GetLength(1); x++)
            {
                if(mapGridX[y,x] == 21)
                {
                    apples.Add(new Apple());
                    apples[appleCount].Position = new Vector2(x * Player.squareSize, y * Player.squareSize);
                    appleCount++;
                }
            }
        }
        foreach(var apple in apples)
        {
            apple.Init();
        }
        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        player.LoadContent(GraphicsDevice);
        checkpoint.LoadContent(GraphicsDevice);

        for(int y = 0; y < mapGridY.GetLength(0); y++)
        {
            for(int x = 0; x < mapGridX.GetLength(1); x++)
            {
                if(mapGridX[y,x] >= 0 && mapGridX[y,x] <= 15)
                {
                    Tiles tile = new Tiles();
                    tile.Position = new Vector2(mapGridX[y, x]*Player.squareSize, mapGridY[y, x]*Player.squareSize);
                    tiles.Add(tile);
                }
                else if(mapGridX[y,x] == 20)
                {
                    player.Position = new Vector2(x * Player.squareSize, y * Player.squareSize);
                }
                else if(mapGridX[y,x] == 22)
                {
                    checkpoint.Position = new Vector2(x * Player.squareSize, y * Player.squareSize);
                }
            }
        }
        foreach(var tile in tiles)
        {
            tile.LoadContent(GraphicsDevice);
        }
        foreach(var apple in apples)
        {
            apple.LoadContent(GraphicsDevice);
        }
    }

    protected override void Update(GameTime gameTime)
    {  
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        player.Update(tiles);
        if(checkpoint.isCollision == false)
        {   
            checkpoint.Update(player.Position);
        }
        foreach(var apple in apples)
        {
            if (apple.isCollision == false)
            {
                apple.Update(player.Position);
            }
        }
        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.NavajoWhite);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        if(checkpoint.isCollision == false)
        {
            checkpoint.Draw(_spriteBatch);            
        }
        foreach(var tile in tiles)
        {
            tile.Draw(_spriteBatch);
        }
        player.Draw(_spriteBatch);
        foreach(var apple in apples)
        {
            if(apple.isCollision == false)
            {
                apple.Draw(_spriteBatch);
            }
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
