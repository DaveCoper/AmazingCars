using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AmazingCars
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D _carTexture;

        Rectangle carMask;
        Rectangle interiorMask;

        Vector2 maskOrigin;
        Vector2 interiorOrigin;

        Car car;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var viewportBounds = GraphicsDevice.Viewport.Bounds;
            car = new Car { Position = new Vector2(viewportBounds.Width * 0.5f, viewportBounds.Height * 0.5f) };
            _carTexture = Content.Load<Texture2D>("Rx-7");
            carMask = new Rectangle(22, 12, 418, 940);
            interiorMask = new Rectangle(512 + 22, 12, 418, 940);

            maskOrigin = new Vector2(carMask.Width, carMask.Height) / 2.0f;
            interiorOrigin = new Vector2(interiorMask.Width, interiorMask.Height) / 2.0f;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            car.Speed = 0;
            if (keyboardState.IsKeyDown(Keys.W))
                car.Speed += 120.0f;
            if (keyboardState.IsKeyDown(Keys.S))
                car.Speed -= 120.0f;


            var elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.A))
                car.Heading -= elapsedSeconds;
            if (keyboardState.IsKeyDown(Keys.D))
                car.Heading += elapsedSeconds;

            car.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            spriteBatch.Draw(
                _carTexture,
                car.Position,
                carMask,
                Color.Red,
                car.Heading,
                maskOrigin,
                Vector2.One * 0.2f,
                SpriteEffects.None, 0.0f);

            spriteBatch.Draw(
                _carTexture,
                car.Position,
                interiorMask,
                Color.White,
                car.Heading,
                interiorOrigin,
                Vector2.One * 0.2f,
                SpriteEffects.None, 0.0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
