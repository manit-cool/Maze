using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Enemy
{
    public Vector2 Position = new Vector2(500, 400);
    // see i don't need a get or set, because i don't really need to modify those values:D
    // just like in all the other files
    public bool upOrDown; // True = Up, False = Down
    private Texture2D enemyTexture;
    private Rectangle enemyRect;
    private Rectangle futureRect;
    private double timer;
    private int moveDir;
    private Rectangle playerRect;

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        enemyTexture = new Texture2D(graphicsDevice, 1, 1);
        moveDir = 1;
        upOrDown = true;
        timer = 0;
        enemyTexture.SetData(new[]{Color.White});
    }
    public void Update(Rectangle PlayerRect, GameTime gameTime, List<Tiles> tiles)
    {
        enemyRect = new Rectangle((int)Position.X, (int)Position.Y, Player.squareSize, Player.squareSize);
        playerRect = PlayerRect;
        
        timer += gameTime.ElapsedGameTime.TotalSeconds;

        foreach(var tile in tiles)
        {
            UpdateMovement(tile.tileRect);
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(enemyTexture, enemyRect, Color.Black);
    }
    public void UpdateMovement(Rectangle tileRect)
    {

        if(upOrDown == false)
        {

            futureRect = new Rectangle((int)Position.X + Player.squareSize * moveDir,(int) Position.Y, Player.squareSize, Player.squareSize);
            if(futureRect.Intersects(tileRect) && timer >= 1)
            {
                moveDir *= -1;
            }
        }
        if (upOrDown == true)
        {
            futureRect = new Rectangle((int)Position.X,(int) Position.Y + Player.squareSize * moveDir, Player.squareSize, Player.squareSize);
            if(futureRect.Intersects(tileRect))
            {
                moveDir *= -1;
            }
        }
        if (timer>=0.25)
        {
            if(upOrDown == false)
            {
                Position = new Vector2(Position.X + Player.squareSize * moveDir, Position.Y);
                timer = 0;
            }
            if(upOrDown == true)
            {
                Position = new Vector2(Position.X, Position.Y + Player.squareSize * moveDir);
                timer = 0;
            }
        }
    }
}   