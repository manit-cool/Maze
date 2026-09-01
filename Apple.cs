using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Apple
{
    // I literally just have to paste in the checkpoint code and add 1 more line! And of course change
    public Vector2 Position {get; set;}
    public static int score;
    private Texture2D appleTexture;
    private Rectangle playerRect;
    private Rectangle appleRect;
    public bool isCollision;
    public void Init()
    {
        isCollision = false;
        score = 0;
    }
    public void LoadContent(GraphicsDevice graphics)
    {
        appleTexture = new Texture2D(graphics, 1,1);
        appleTexture.SetData(new[]{Color.White});
    }
    public void Update(Vector2 playerPosition)
    {
        appleRect = new Rectangle((int)Position.X, (int)Position.Y, Player.squareSize, Player.squareSize);
        playerRect = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, Player.squareSize, Player.squareSize);
        Collisions();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(appleTexture, appleRect, Color.Red);
    }
    // I think I want to add the collisions for the player and the check point over here cuz, yes ✨
    public void Collisions()
    {
        appleRect.Intersects(playerRect);
        if (appleRect.Intersects(playerRect))
        {
            isCollision = true;
            score += 1;
        }
    }
}