using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AmazingCars
{
    class Car
    {
        public Vector2 Position;
        public float Heading;

        public float Speed;

        public void Update(GameTime gameTime)
        {
            var elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var direction = new Vector2((float)Math.Sin(Heading), -(float)Math.Cos(Heading));
            Position += direction * Speed * elapsedSeconds;
        }
    }
}
