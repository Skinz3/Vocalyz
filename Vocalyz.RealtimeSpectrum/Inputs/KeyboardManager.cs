using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocalyz.RealtimeSpectrum.Inputs
{
    public class KeyboardManager
    {
        private static List<Keys> PressedKeys = new List<Keys>();

        public static event Action<Keys> OnKeyPressed;

        public static event Action<Keys> OnKeyDown;

        public static void Update()
        {
            if (OnKeyPressed == null && OnKeyDown == null)
                return;

            KeyboardState state = Keyboard.GetState();

            var keys = state.GetPressedKeys();

            foreach (var pressedKey in PressedKeys.ToArray())
            {
                if (keys.Contains(pressedKey) == false)
                {
                    PressedKeys.Remove(pressedKey);
                    OnKeyPressed?.Invoke(pressedKey);
                }
            }

            foreach (var key in keys)
            {
                if (PressedKeys.Contains(key) == false)
                {
                    OnKeyDown?.Invoke(key);
                    PressedKeys.Add(key);
                }
            }
        }
    }
}
