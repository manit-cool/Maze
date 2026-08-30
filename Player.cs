using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
public class Player
{
    public Vector2 Position { get; set; } = new Vector2(50, 50); // for this guy, there's no accessing the 
    // x and y values, to my knowledge, as if we try to do Position.X = Position.x += 1;
    // it will error out, since it's really Position.Vector2, and I'm *pretty* sure you can't add .X
    // to the end of that, (it bugged out for me 🤷). Okay so I've been proven wrong, you can access the x
    // and y values, but you can't do Position.X += 1; because it will error out!
    private KeyboardState currentKeyboardState; // Remember all variables have to be defined
    // you can't just say "currentKeyboardState = keyboard.GetState();"
    private KeyboardState prevKeyboardState; // This is for just pressed!
    //Time to draw the square, which is lwk harder than making a sprite. 😭
    private Texture2D squareTexture;
    public Rectangle playerRect;
    public Rectangle futureRect; // this is for collisions
    private bool tileCollision = false;
    public static int squareSize = 50; // this is the size of the square, and it can be changed to any value
    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        prevKeyboardState = Keyboard.GetState();
        squareTexture = new Texture2D(graphicsDevice, 1, 1);
        squareTexture.SetData(new[]{Color.White}); // always give your texture a color! 
        // although, technically it's just a tint-
        // so the colors overlap in the draw over the final color;)
        // try making the "color" blue! you'll get a blackish color lol (i was expecting purple-)
        // btw it's an array of colors because each pixel has it's own color!
        // but since ours is a 1x1 pixel, we only need one color! (the array is just for the sake of the method) --> the method
        // needs it to work :D
    }
    public void Update(List<Tiles> tiles) // remember to call this a method and not a class lmao, cause i did it the first 
    // time
    {
        playerRect = new Rectangle((int)Position.X, (int)Position.Y, squareSize, squareSize);
        //TileCollsion(tiles);
        UpdateInput(tiles);

    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(squareTexture, playerRect, Color.Green);
        //I keep casting the values like int(bleh) when it's meant to be
        // (int)bleh😔
    }
    public void UpdateInput(List<Tiles> tiles)
    {
        Vector2 newPosition = Position;
        currentKeyboardState = Keyboard.GetState();
        if(currentKeyboardState.IsKeyDown(Keys.W))
        {
            if (prevKeyboardState.IsKeyUp(Keys.W) && Position.Y > 0)
            {
                newPosition = new Vector2(Position.X, Position.Y - squareSize); // can't change it's values via +/-/*//   
            }
            // but you can make a new vector2 with the new values and assign it to Position 🥳
        }
        if(currentKeyboardState.IsKeyDown(Keys.S))
        {
            if (prevKeyboardState.IsKeyUp(Keys.S) && Position.Y < 500-squareSize) // for some reason, 480 didn't work, so I just
            // rounded up to 500 :D
            {
                newPosition = new Vector2(Position.X, Position.Y + squareSize);
            }
        }
        if(currentKeyboardState.IsKeyDown(Keys.A))
        {
            if (prevKeyboardState.IsKeyUp(Keys.A) && Position.X > 0)
            {
                newPosition = new Vector2(Position.X - squareSize, Position.Y);
            }
        }
        if(currentKeyboardState.IsKeyDown(Keys.D))
        {
            if (prevKeyboardState.IsKeyUp(Keys.D) && Position.X < 800-squareSize) // 800 is the width of the screen, so we don't want to go past that
            {
                newPosition = new Vector2(Position.X + squareSize, Position.Y);
            }
        }
        tileCollision = false;
        foreach(var tile in tiles)
        {
            if(new Rectangle((int)newPosition.X, (int)newPosition.Y, squareSize, squareSize).Intersects(tile.tileRect))
            {
                tileCollision = true;
                break;
            }
        }
        if (!tileCollision)
        {
            Position = newPosition;
        }
        playerRect = new Rectangle((int)Position.X, (int)Position.Y, squareSize, squareSize);
        prevKeyboardState = currentKeyboardState;
        // now the current state was the previous state
        // so we make the previous state that :D
        
    }
    public void TileCollsion(List<Tiles> tiles)
    {
        foreach(var tile in tiles)
        {
            tileCollision = playerRect.Intersects(tile.tileRect);
            if(tileCollision)
            {
                if(playerRect.Left < tile.tileRect.Right)
                {
                    Position = new Vector2(tile.tileRect.Right, (int)Position.Y);
                }
            }
        }
    }
}