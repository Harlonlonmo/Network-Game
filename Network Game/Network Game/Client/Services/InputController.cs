using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Network_Game;
using System;
using Network_Game.Network;

namespace Network_Game.Client.Services
{
    public class InputController : GameComponent
    {
        public enum MouseButton { Left, Midle, Right }

        public InputObject InputObject { get { return inputObject; } }
        private InputObject inputObject;
        public bool UpdateIO { get; set; }

        public MouseState mouseState { get; protected set; }
        public KeyboardState keyboardState { get; protected set; }
        public GamePadState gamePadState { get; protected set; }

        private MouseState oldMs;
        private GamePadState oldGs;
        private KeyboardState oldKs;

        public bool mouseChanged { get; protected set; }
        public bool keyboardChanged { get; protected set; }
        public bool gamePadChanged { get; protected set; }

        public InputController(Game game)
            : base(game)
        {
            inputObject = new InputObject();
        }

        public override void Update(GameTime gameTime)
        {
            oldGs = gamePadState;
            oldKs = keyboardState;
            oldMs = mouseState;

            mouseState = Mouse.GetState();
            mouseChanged = mouseState != oldMs;
            keyboardState = Keyboard.GetState();
            keyboardChanged = keyboardState != oldKs;
            gamePadState = GamePad.GetState(PlayerIndex.One);
            gamePadChanged = gamePadState != oldGs;
            if (UpdateIO)
            {
                inputObject.HorizontalAxis = gamePadState.ThumbSticks.Left.X;
                inputObject.VerticalAxis = -gamePadState.ThumbSticks.Left.Y;
                inputObject.AmingAngle = (float)Math.Atan2(gamePadState.ThumbSticks.Right.Y, gamePadState.ThumbSticks.Right.X);
                inputObject.Buttons = (byte)(
                    (gamePadState.IsButtonDown(Buttons.A) ? 1 : 0) +
                    (gamePadState.IsButtonDown(Buttons.LeftTrigger) ? 2 : 0) +
                    (gamePadState.IsButtonDown(Buttons.RightTrigger) ? 4 : 0) +
                    (gamePadState.IsButtonDown(Buttons.RightShoulder) ? 8 : 0) +
                    (gamePadState.IsButtonDown(Buttons.DPadUp) ? 16 : 0) +
                    (gamePadState.IsButtonDown(Buttons.DPadDown) ? 32 : 0) +
                    (gamePadState.IsButtonDown(Buttons.DPadLeft) ? 64 : 0) +
                    (gamePadState.IsButtonDown(Buttons.DPadRight) ? 128 : 0));
            }
        }

        public bool KeyWasPressed(Keys key)
        {
            if (!keyboardChanged) return false;
            return keyboardState.IsKeyDown(key) && oldKs.IsKeyUp(key);
        }

        public bool ButtonWasPressed(Buttons button)
        {
            if (!gamePadChanged) return false;
            return gamePadState.IsButtonDown(button) && oldGs.IsButtonUp(button);
        }
        public bool MouseButtonWasPressed(MouseButton button)
        {
            if (!mouseChanged) return false;
            if (button == MouseButton.Left) return mouseState.LeftButton == ButtonState.Pressed &&
                oldMs.LeftButton == ButtonState.Released;
            if (button == MouseButton.Midle) return mouseState.MiddleButton == ButtonState.Pressed &&
                oldMs.MiddleButton == ButtonState.Released;
            if (button == MouseButton.Right) return mouseState.RightButton == ButtonState.Pressed &&
                oldMs.RightButton == ButtonState.Released;
            return false;
        }

        public Rectangle getMouseRect()
        {
            return new Rectangle(mouseState.X, mouseState.Y, 1, 1);
        }

        public Point getMouseChange()
        {
            return new Point(oldMs.X - mouseState.X, oldMs.Y - mouseState.Y);
        }
    }
}
