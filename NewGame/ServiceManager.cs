using System;
using NewGame.Collections;
using NewGame.Engine;

namespace NewGame {
    public static class ServiceManager {
        static NewGame.Game gameRef;
        static HashList services;

        public static NewGame.Game Game {
            get { return gameRef; }
            set { if (gameRef != null) { gameRef = value; } }
        }

        public static void AddService<T>(T service) where T : class {
            if (services == null) { services = new HashList(); }
            services.Add(new HashListItem(service));
        }

        public static T GetService<T>() where T : class {
            T toReturn = null;

            for (int i = 0; i < services.Count; i++) {
                var temp = services[i]?.Item as T;
                if (temp != null) {
                    toReturn = temp;
                    break;
                }
            }

            return toReturn;            
        }

        public static void Clear() {
            for (int i = 0; i < services.Count; i++) {
                var service = services[i]?.Item as IDisposable;
                if (service != null) {
                    service.Dispose();
                }
            }
            services.Clear();
        }
    }
}
