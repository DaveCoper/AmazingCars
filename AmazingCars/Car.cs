using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AmazingCars
{
    internal class Car : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D carTexture, tireTexture;

        private Rectangle carMask;
        private Rectangle interiorMask;

        private Vector2 maskOrigin;
        private Vector2 interiorOrigin;

        public Vector2 Position;

        public float TireAngle;
        public float Heading;
        public float Speed;

        public Vector2 FrontTireOffset;
        public Vector2 RearTireOffset;

        public Car(Game game) : base(game)
        {
            FrontTireOffset = new Vector2(260, 170);
            RearTireOffset = new Vector2(-270, 170);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            carTexture = Game.Content.Load<Texture2D>("Rx-7");
            tireTexture = Game.Content.Load<Texture2D>("white");

            carMask = new Rectangle(12, 23, 940, 420);
            interiorMask = new Rectangle(12, 23 + 512, 940, 420);

            maskOrigin = new Vector2(carMask.Width, carMask.Height) / 2.0f;
            interiorOrigin = new Vector2(interiorMask.Width, interiorMask.Height) / 2.0f;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float carSpeed = elapsedSeconds * Speed;
            float heading = this.Heading;
            float frontTireAngle = heading + this.TireAngle;

            var wheelBase = new Vector2(0, FrontTireOffset.Y - RearTireOffset.Y);

            var frontWheel = Position + wheelBase * 0.5f * new Vector2(
                (float)Math.Cos(heading),
                (float)Math.Sin(heading));
            var backWheel = Position - wheelBase * 0.5f * new Vector2(
                (float)Math.Cos(heading),
                (float)Math.Sin(heading));

            backWheel += carSpeed * new Vector2(
                (float)Math.Cos(heading),
                (float)Math.Sin(heading));

            frontWheel += carSpeed * new Vector2(
                (float)Math.Cos(frontTireAngle),
                (float)Math.Sin(frontTireAngle));

            Position = (frontWheel + backWheel) * 0.5f;
            Heading = (float)Math.Atan2(frontWheel.Y - backWheel.Y, frontWheel.X - backWheel.X);
        }

        public override void Draw(GameTime gameTime)
        {
            const float carScale = 0.2f;
            var transform =
                    Matrix.CreateScale(carScale) *
                    Matrix.CreateRotationZ(this.Heading) *
                    Matrix.CreateTranslation(new Vector3(this.Position, 0.0f));

            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                transformMatrix: transform);

            var tireOffset = this.FrontTireOffset;

            DrawTire(spriteBatch, tireOffset, this.TireAngle);
            DrawTire(spriteBatch, new Vector2(tireOffset.X, -tireOffset.Y), this.TireAngle);

            tireOffset = this.RearTireOffset;
            DrawTire(spriteBatch, tireOffset, 0);
            DrawTire(spriteBatch, new Vector2(tireOffset.X, -tireOffset.Y), 0);

            spriteBatch.Draw(
                carTexture,
                Vector2.Zero,
                carMask,
                Color.Blue,
                0,
                maskOrigin,
                Vector2.One,
                SpriteEffects.None,
                0.0f);

            spriteBatch.Draw(
                carTexture,
                Vector2.Zero,
                interiorMask,
                Color.White,
                0,
                interiorOrigin,
                Vector2.One,
                SpriteEffects.None,
                0.0f);

            spriteBatch.End();
        }

        private void DrawTire(SpriteBatch spriteBatch, Vector2 offset, float tireAngle)
        {
            spriteBatch.Draw(
                   tireTexture,
                   offset,
                   new Rectangle(0, 0, 32, 32),
                   Color.DarkSlateGray,
                   tireAngle,
                   new Vector2(16, 16),
                   new Vector2(4.6f, 1.2f),
                   SpriteEffects.None,
                   0.0f);
        }
    }
}