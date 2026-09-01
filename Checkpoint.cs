using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Checkpoint
{
    //16 steps of 50s to 800
    //10 setps of 50s to 500
    private static readonly Random rand = new Random();
    // okay so yk what private is, but i'll just re-explain for the sake of what I'm already gonna explain
    // private => this just means that the variable is only available to that class!
    // static => if this variable has just been defined but without values it will go to a default value
    // here are all the default values:
    // string = null
    // int, float, double = 0.0 or 0 or 0.0f
    // bool = false
    // char = \0 (null character)
    // struct, every field (basically just a variable w/o get or set) is set to its default values
    // im not gonna explain what a struct is cuz idk yet :D
    // static also means that any instances of this class shares the same static variable
    // so instead of creating a new rng variable, they just use the same rand all around!
    // i don't know how this is useful rn. It might be helpful with speed, but this sure will be helpful with the apples and the
    // score keeping :D
    // readonly => read only, you can't change its value --> prettyyy straightforward lmao (11 lines of comment 😭 )
    public Vector2 Position {get; set;}
    private Texture2D checkpointTexture;
    private Rectangle playerRect;
    private Rectangle checkRect;
    public bool isCollision;
    public void Init()
    {
        isCollision = false;
    }
    public void LoadContent(GraphicsDevice graphics)
    {
        checkpointTexture = new Texture2D(graphics, 1,1);
        checkpointTexture.SetData(new[]{Color.White});
    }
    public void Update(Vector2 playerPosition)
    {
        checkRect = new Rectangle((int)Position.X, (int)Position.Y, Player.squareSize, Player.squareSize);
        playerRect = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, Player.squareSize, Player.squareSize);
        Collisions();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(checkpointTexture, checkRect, Color.RoyalBlue);
    }
    // I think I want to add the collisions for the player and the check point over here cuz, yes ✨
    public void Collisions()
    {
        checkRect.Intersects(playerRect);
        if (checkRect.Intersects(playerRect))
        {
            isCollision = true;
        }
    }
}