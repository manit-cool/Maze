using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
namespace _1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private Vector2 startingPlayerPosition;
    private SpriteBatch _spriteBatch;
    private Player player;
    private Checkpoint checkpoint;
    List<Tiles> tiles = new List<Tiles>();
    List<Apple> apples = new List<Apple>();
    private int appleCount;
    private int enemyCount;
    List<Enemy> enemies = new List<Enemy>();
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
        {0,99,2,99,4,99,6,67,8,99,99,99,99,99,99,15},
        {0,99,2,99,4,99,99,99,8,99,99,99,99,99,99,15},
        {0,99,2,99,4,99,6,99,8,99,99,21,99,99,99,15},
        {0,99,2,99,4,99,6,99,8,99,99,99,99,99,21,15},
        {0,99,2,99,4,99,6,21,8,99,99,99,99,99,99,15},
        {0,99,2,99,4,99,6,99,8,99,67,99,99,22,99,15},
        {0,99,99,99,4,99,99,8,99,99,67,99,99,99,99,15},
        {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15}
    };

    private SpriteFont font;
    private Vector2 fontPos;

    private Song mainTheme;
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
        enemyCount = 0;
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
                else if(mapGridX[y, x] == 67)
                {
                    enemies.Add(new Enemy());
                    enemies[enemyCount].Position = new Vector2(x * Player.squareSize, y * Player.squareSize);
                    enemyCount++;
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
        mainTheme = Content.Load<Song>("Eternity");
        font = Content.Load<SpriteFont>("MyMenuFont");
        fontPos = new Vector2(75, 25);
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
                    startingPlayerPosition = player.Position;
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
        foreach(var enemy in enemies)
        {
            enemy.LoadContent(GraphicsDevice);
        }
        MediaPlayer.Play(mainTheme);
    }

    protected override void Update(GameTime gameTime)
    {  
        enemies[0].upOrDown = true;
        enemies[1].upOrDown = false;
        enemies[^1].upOrDown = true;
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        player.Update(tiles, startingPlayerPosition, enemies);
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
        foreach(var enemy in enemies)
        {
            enemy.Update(player.playerRect, gameTime, tiles);
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
        foreach(var enemy in enemies)
        {
            enemy.Draw(_spriteBatch);
        }
        string score = $"Score: {Apple.score}";
        _spriteBatch.DrawString(font, score, fontPos, Color.AntiqueWhite, 0, font.MeasureString(score)/2,1.0f,SpriteEffects.None,0.5f);
        
        if(checkpoint.isCollision == true)
        {
            checkpoint.End(_spriteBatch);
            _spriteBatch.DrawString(font,"THE END", new Vector2(400, 250), Color.Aqua, 0,font.MeasureString("THE END")/2, 1.0f, SpriteEffects.None,0.5f);
            _spriteBatch.DrawString(font, score, new Vector2(400, 350), Color.Turquoise, 0, font.MeasureString(score)/2,1.0f,SpriteEffects.None,0.5f);
        }
        checkpoint.Start(_spriteBatch);
        if(checkpoint.started == false)
        {
            _spriteBatch.DrawString(font,"WELCOME!", new Vector2(400, 250), Color.Aqua, 0,font.MeasureString("WELCOME!")/2, 1.0f, SpriteEffects.None,0.5f);
            _spriteBatch.DrawString(font,"(PRESS ENTER TO BEGIN)", new Vector2(400, 350), Color.Aqua, 0,font.MeasureString("(PRESS ENTER TO BEGIN)")/2, 1.0f, SpriteEffects.None,0.5f);

        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}