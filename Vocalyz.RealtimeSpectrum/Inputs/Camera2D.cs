namespace Vocalyz.RealtimeSpectrum.Inputs
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Vocalyz.RealtimeSpectrum.Graphics;

    namespace MonoFramework.Cameras
    {
        public class Camera2D
        {

            float _zoom;
            Matrix _transform;

            public Rectangle View
            {
                get
                {
                    return GetView(Position);
                }
            }
            public Rectangle GetView(Vector2 pos)
            {
                return new Rectangle(pos.ToPoint(), (Debug.ScreenSize / new Vector2(Zoom)).ToPoint());
            }
            public float Zoom
            {
                get
                {
                    return _zoom;
                }
                set
                {
                    _zoom = value;
                    if  (_zoom < 1f) _zoom = 1f;
                } // Negative zoom will flip image at 0.1, < 1 Pixel is not displayed? (Texture Filtering)
            }

            public float Rotation
            {
                get;
                set;
            }

            public Vector2 Position;

            private int OldMouseScrollWhellValue
            {
                get;
                set;
            }

            public Camera2D()
            {
                _zoom = 1.0f;
                Rotation = 0.0f;
                Position = Vector2.Zero;
            }


            public Matrix GetTransformation()
            {
                _transform =
                  Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 1)) *
                                             Matrix.CreateRotationZ(Rotation) *
                                             Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)
                                            );
                return _transform;
            }
        }
    }
}
