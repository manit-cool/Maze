using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Tiles
{
    public Vector2 Position {get; set;} = new Vector2(0, 0);
    public Rectangle tileRect;
    private Texture2D tileTexture;
    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        tileTexture = new Texture2D(graphicsDevice, 1,1);
        tileTexture.SetData(new[]{Color.White});
        tileRect = new Rectangle((int) Position.X, (int) Position.Y, Player.squareSize, Player.squareSize);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(tileTexture, tileRect, Color.RosyBrown);
    }
    public Vector2 Collisions(Rectangle playerRect, Vector2 playerPosition)
    {
        if(playerRect.Intersects(tileRect))
        {
            return new Vector2(tileRect.Right, (int)playerPosition.Y);
        }
        return new Vector2((int)playerPosition.X, (int)playerPosition.Y);
    }
}