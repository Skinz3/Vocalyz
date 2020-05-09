using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocalyz.RealtimeSpectrum.Graphics
{
    public struct DrawableLine
    {
        public Vector2 Start;

        public Vector2 End;

        public Color Color;

        public string Text;

        public DrawableLine(Vector2 start, Vector2 end, Color color)
        {
            this.Start = start;
            this.End = end;
            this.Color = color;
            this.Text = null;
        }

        public void Draw()
        {
            Debug.DrawLine(Start, End, Color);

            if (Text != null)
            {
                var size = Debug.DefaultFont.MeasureString(Text);
                Debug.DrawText(Text, End - new Vector2(size.X / 2, size.Y), Color.White);
            }
        }
    }
}
