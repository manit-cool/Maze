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
    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        prevKeyboardState = Keyboard.GetState();
        squareTexture = new Texture2D(graphicsDevice, 1, 1);
        squareTexture.SetData(new[]{Color.White}); // always give your texture a color!
        // although, technically it's just a tint-
        // so the colors overlap in the draw over the final color;)
        // try making the "color" blue! you'll get a blackish color lol (i was expecting purple-)
    }
    public void Update() // remember to call this a method and not a class lmao, cause i did it the first 
    // time
    {
        UpdateInput();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(squareTexture, new Rectangle((int)Position.X, (int)Position.Y, 50, 50), Color.Red);
        //I keep casting the values like int(bleh) when it's meant to be
        // (int)bleh😔
    }
    public void UpdateInput()
    {
        currentKeyboardState = Keyboard.GetState();
        if(currentKeyboardState.IsKeyDown(Keys.W))
        {
            if (prevKeyboardState.IsKeyUp(Keys.W))
            {
                Position = new Vector2(Position.X, Position.Y - 50); // can't change it's values via +/-/*//   
            }
            // but you can make a new vector2 with the new values and assign it to Position 🥳
        }
        if(currentKeyboardState.IsKeyDown(Keys.S))
        {
            if (prevKeyboardState.IsKeyUp(Keys.S))
            {
                Position = new Vector2(Position.X, Position.Y + 50);
            }
        }
        if(currentKeyboardState.IsKeyDown(Keys.A))
        {
            if (prevKeyboardState.IsKeyUp(Keys.A))
            {
                Position = new Vector2(Position.X - 50, Position.Y);
            }
        }
        if(currentKeyboardState.IsKeyDown(Keys.D))
        {
            if (prevKeyboardState.IsKeyUp(Keys.D))
            {
                Position = new Vector2(Position.X + 50, Position.Y);
            }
        }

        prevKeyboardState = currentKeyboardState;
        // now the current state was the previous state
        // so we make the previous state that :D
    
    }
}