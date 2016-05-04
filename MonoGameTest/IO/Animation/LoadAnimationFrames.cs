//
//  LoadAnimationFrames.cs
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
using System.IO;
using Animation;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace IO.Animation {

    //http://www.newtonsoft.com/json/help/html/Performance.htm
    public class LoadAnimationFrames {

        /// <summary>
        /// Load Animation Frames and Timings from binary json
        /// </summary>
        /// <returns>FramesJson</returns>
        /// <param name="file">File</param>
        public static FramesJson FromBSON(string file) {
            using (var stream = new FileStream(Path.GetFullPath(file), FileMode.Open)) {
                using (var reader = new BsonReader(stream)) {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<FramesJson>(reader);
                }
            }
        }

        /// <summary>
        /// Load animation frames and timings from json
        /// </summary>
        /// <returns>FramesJson</returns>
        /// <param name="file">File</param>
        public static FramesJson FromJSON(string file) {
            using (var stream = new FileStream(Path.GetFullPath(file), FileMode.Open)) {
                using (var textReader = new StreamReader(stream)) {
                    using (var reader = new JsonTextReader(textReader)) {
                        var serializer = new JsonSerializer();
                        return serializer.Deserialize<FramesJson>(reader);
                    }
                }
            }
        }
    }
}