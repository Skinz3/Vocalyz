using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocalyz.RealtimeSpectrum.Graphics
{
    public class Debug
    {
        public static Point CURSOR_SIZE = new Point(1, 1);

        /// <summary>
        /// The texture used when drawing rectangles, lines and other 
        /// primitives. This is a 1x1 white texture created at runtime.
        /// </summary>
        public static Texture2D DummyTexture
        {
            get;
            private set;
        }
        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return SpriteBatch.GraphicsDevice;
            }
        }
        public static SpriteBatch SpriteBatch
        {
            get;
            private set;
        }
        public static Viewport Viewport
        {
            get
            {
                return GraphicsDevice.Viewport;
            }
        }

        public static Vector2 ScreenSize
        {
            get
            {
                return new Vector2(Viewport.Width, Viewport.Height);
            }
        }
        public static SpriteFont DefaultFont
        {
            get;
            private set;
        }
        public static Rectangle ScreenBounds
        {
            get
            {
                return Viewport.Bounds;
            }
        }
        public static void Initialize(SpriteBatch batch, ContentManager content)
        {
            SpriteBatch = batch;
            DefaultFont = content.Load<SpriteFont>("font");
            DummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            DummyTexture.SetData(new Color[] { Color.White });
        }

        public static void DrawText(string text, Vector2 position, Color color)
        {
            SpriteBatch.DrawString(DefaultFont, text, position, color);
        }
        public static void DrawLine(Vector2 start, Vector2 end, Color color, float thickness = 1f)
        {
            float length = (end - start).Length();
            float rotation = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            SpriteBatch.Draw(DummyTexture, start, null, color, rotation, Vector2.Zero, new Vector2(length, thickness), SpriteEffects.None, 0);
        }
        public static void DrawRectangle(Rectangle rectangle, Color color, int thickness = 1)
        {
            SpriteBatch.Draw(DummyTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, thickness), color);
            SpriteBatch.Draw(DummyTexture, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, thickness), color);
            SpriteBatch.Draw(DummyTexture, new Rectangle(rectangle.Left, rectangle.Top, thickness, rectangle.Height), color);
            SpriteBatch.Draw(DummyTexture, new Rectangle(rectangle.Right, rectangle.Top, thickness, rectangle.Height + thickness), color);
        }
        public static void FillRectangle(Rectangle rectangle, Color color)
        {
            SpriteBatch.Draw(DummyTexture, rectangle, color);
        }
    }
}
