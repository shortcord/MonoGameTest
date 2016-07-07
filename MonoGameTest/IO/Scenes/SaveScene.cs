//
//  SaveScene.cs
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
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace IO.Scenes {

    public static class SaveScene {

        public static void ToBSON(ScenesJson scene, string file) {
            using (var stream = new FileStream(Path.GetFullPath(file), FileMode.OpenOrCreate)) {
                using (var writer = new BsonWriter(stream)) {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(writer, scene);
                }
            }
        }

        public static void ToJSON(ScenesJson scene, string file) {
            using (var stream = new FileStream(Path.GetFullPath(file), FileMode.OpenOrCreate)) {
                using (var textWriter = new StreamWriter(stream)) {
                    using (var writer = new JsonTextWriter(textWriter)) {
                        var serializer = new JsonSerializer();
                        serializer.Serialize(writer, scene);
                    }
                }
            }
        }
    }
}