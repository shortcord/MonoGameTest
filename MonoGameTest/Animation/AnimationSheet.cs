//
//  SpriteSheet.cs
//
//  Author:
//       Tristan <tristan@shortcord.com>
//
//  Copyright (c) 2016 Tristan Smith
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Animation {

    public class AnimationSheet {
        private IDictionary<string, Frame[]> jsonFrames;
        private IList<Frame> frames;
        private TimeSpan timeIntoAnimation;
        private bool Stop { get; set; }

        /// <summary>
        /// Total duration of the animation sheet
        /// </summary>
        private TimeSpan Duration {
            get {
                TimeSpan totalSeconds = TimeSpan.Zero;
                foreach (var frame in frames) {
                    totalSeconds += frame.Duration;
                }
                return totalSeconds;
            }
        }

        /// <summary>
        /// Current State
        /// </summary>
        public string CurrentState { get; private set; }

        /// <summary>
        /// Gets the current frame in the animation
        /// </summary>
        /// <value>Rectangle</value>
        public Rectangle CurrentFrameBox {
            get {
                Frame currentFrame = Frame.Zero;
                TimeSpan accumulatedTime = TimeSpan.Zero;

                foreach (var frame in frames) {
                    if (accumulatedTime + frame.Duration >= timeIntoAnimation) {
                        currentFrame = frame;
                        break;
                    } else {
                        accumulatedTime += frame.Duration;
                    }
                }
                if (currentFrame.PlayOnce) {
                    Stop = true;
                    currentFrame.SourceRectangle = frames[frames.Count - 1].SourceRectangle;
                }
                return currentFrame.SourceRectangle;
            }
        }

        /// <summary>
        /// Get all states within this sheet
        /// </summary>
        /// <value>States</value>
        public IReadOnlyList<string> States {
            get {
                var toReturn = new List<string>();
                foreach (KeyValuePair<string, Frame[]> pair in jsonFrames) {
                    toReturn.Add(pair.Key);
                }
                return toReturn.AsReadOnly();
            }
        }

        public AnimationSheet(FramesJson json) {
            frames = new List<Frame>();
            timeIntoAnimation = TimeSpan.Zero;
            jsonFrames = new Dictionary<string, Frame[]>();
            foreach (var item in json.AllStates) {
                jsonFrames.Add(item.State, item.Frames);
            }
            Stop = false;
            SetState("default");
        }

        /// <summary>
        /// Set the current state
        /// </summary>
        /// <returns>If state was found</returns>
        /// <param name="state">State Name</param>
        public bool SetState(string state) {
            if (jsonFrames.ContainsKey(state)) {
                frames.Clear(); //clear list so states dont blend
                foreach (Frame frame in jsonFrames[state]) {
                    frames.Add(frame);
                }
                CurrentState = state; //set current state
                if (state != "death") { //check if we are setting state to death to keep from resetting the stop
                    Stop = false;
                }

                return true; //found state and set current frameset to state
            } else {
                return false; //Failed to find state
            }
        }

        public void Update(GameTime gameTime) {
            //TODO check for a valid death animation
            TimeSpan secondsIntoAnimation = timeIntoAnimation + gameTime.ElapsedGameTime;
            timeIntoAnimation = (Stop ? Duration : TimeSpan.FromSeconds(secondsIntoAnimation.TotalSeconds % Duration.TotalSeconds));
        }
    }
}