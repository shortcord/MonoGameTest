using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using ShortCord.MonoGame.Components;

namespace ShortCord.MonoGame {
    public sealed class Input : Updatable {

        KeyboardState[] keyboardStates;
        MouseState[] mouseStates;
        //GamePadState[] gamePadStates;

        public event EventHandler<Keys[]> KeyPressedEvent;
        public event EventHandler<MouseState> MouseStateChanged;

        #region events
        void OnKeyPressed(Keys[] keys) {
            if (keys.Length > 0) {
                KeyPressedEvent?.Invoke(this, keys);
            }
        }

        void OnMouseStateChanged(MouseState state) {
            if (PreviousMouseState != state) {
                MouseStateChanged?.Invoke(this, state);
            }
        }
        #endregion

        #region keys cache
        List<Keys> keysCache;
        #endregion

        public Input() : base() {
            keysCache = Enum.GetValues(typeof(Keys)).Cast<Keys>().ToList();

            keyboardStates = new KeyboardState[2];
            mouseStates = new MouseState[2];
            FixedUpdateEnabled = false;
            //gamePadStates = new GamePadState[GamePad.MaximumGamePadCount];
        }

        public override void Update(float delta) {
            //Invoke events before we update the states
            OnKeyPressed(CurrentKeyboardState.GetPressedKeys());
            OnMouseStateChanged(CurrentMouseState);

            //Update keyboard
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            //Update Mouse
            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        public KeyboardState CurrentKeyboardState {
            get { return keyboardStates[1]; }
            private set { keyboardStates[1] = value; }
        }

        public KeyboardState PreviousKeyboardState {
            get { return keyboardStates[0]; }
            private set { keyboardStates[0] = value; }
        }
        
        public MouseState CurrentMouseState {
            get { return mouseStates[1]; }
            private set { mouseStates[1] = value; }
        }

        public MouseState PreviousMouseState {
            get { return mouseStates[0]; }
            private set { mouseStates[0] = value; }
        }

        public bool KeyReleased(Keys k) {
            return keyboardStates[0].IsKeyUp(k) && keyboardStates[1].IsKeyDown(k);
        }

        public bool KeyPressed(Keys k) {
            return keyboardStates[0].IsKeyDown(k) && keyboardStates[1].IsKeyUp(k);
        }

        public bool KeyDown(Keys k) {
            return keyboardStates[0].IsKeyDown(k);
        }

        public bool MouseLeftClicked {
            get {
                return mouseStates[0].LeftButton == ButtonState.Pressed && mouseStates[1].LeftButton == ButtonState.Released;
            }
        }

        public bool MouseRightClicked {
            get {
                return mouseStates[0].RightButton == ButtonState.Pressed && mouseStates[1].RightButton == ButtonState.Released;
            }
        }

        public bool MouseWheelUp {
            get {
                if (mouseStates[0].ScrollWheelValue > mouseStates[1].ScrollWheelValue)
                    return true;
                return false;
            }
        }

        public bool MouseWheelDown {
            get {
                if (mouseStates[0].ScrollWheelValue < mouseStates[1].ScrollWheelValue)
                    return true;
                return false;
            }
        }

    }
}
