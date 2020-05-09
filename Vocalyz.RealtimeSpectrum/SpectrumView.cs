using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Vocalyz.Graphics;
using Vocalyz.Music;
using Vocalyz.RealtimeSpectrum.Graphics;
using Vocalyz.RealtimeSpectrum.Inputs;
using Vocalyz.RealtimeSpectrum.Inputs.MonoFramework.Cameras;

namespace Vocalyz.RealtimeSpectrum
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SpectrumView : Game
    {
        public const int WIDOW_WIDTH = 1300;
        public const int WINDOW_HEIGHT = 800;
        public const int CAMERA_SPEED = 6;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        DrawableSpectrum spectrum;
        bool m_paused;
        Camera2D camera;
        string currentFile;


        public SpectrumView()
        {

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WIDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            Content.RootDirectory = "Content";
            camera = new Camera2D();
            m_paused = false;

            NotesManager.Initialize();

        }

        protected override void Initialize()
        {
            base.Initialize();

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Debug.Initialize(spriteBatch, Content);

            KeyboardManager.OnKeyPressed += OnKeyPressed;

            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "WAV Files|*.wav|MP3 Files|*.mp3";
            var r = openFileDialog.ShowDialog();

            if (r == System.Windows.Forms.DialogResult.OK)
            {
                currentFile = openFileDialog.FileName;
                spectrum = new DrawableSpectrum(openFileDialog.FileName);
                spectrum.Start();
            }
            else
                Environment.Exit(0);


        }

        private void OnKeyPressed(Keys obj)
        {
            if (obj == Keys.Space)
            {
                if (!m_paused)
                {

                    spectrum.Pause();
                    m_paused = true;

                }
                else
                {
                    spectrum.Start();
                    m_paused = false;
                }
            }
            if (obj == Keys.Back)
            {
                spectrum.Dispose();

                spectrum = new DrawableSpectrum(currentFile);
                spectrum.Start();
            }
        }

        protected override void UnloadContent()
        {
            spectrum.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Right))
            {
                camera.Position.X += CAMERA_SPEED;
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                camera.Position.X -= CAMERA_SPEED;
            }
            else if (state.IsKeyDown(Keys.Up))
            {
                camera.Position.Y -= CAMERA_SPEED;
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                camera.Position.Y += CAMERA_SPEED;
            }
            KeyboardManager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Debug.SpriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend, SamplerState.PointClamp,
                        null,
                        null,
                        null,
            camera.GetTransformation());

            spectrum?.Draw();
            Debug.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
