using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine
{
    public static class Input
    {
        static KeyboardState prevKeyboardState;
        static KeyboardState currentKeyboardState;

        static MouseState prevMouseState;
        static MouseState currentMouseState;

        public static void Initialize()
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
        }

        public static void Update()
        {
            prevKeyboardState = currentKeyboardState;
            prevMouseState = currentMouseState;
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
        }

        public static bool KeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return prevKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyUp(key);
        }

        public static Vector2 GetMousePosition()
        {
            return currentMouseState.Position.ToVector2();
        }

        public static bool WasLMBPressed()
        {
            return currentMouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
