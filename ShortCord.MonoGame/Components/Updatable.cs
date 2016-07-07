using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortCord.MonoGame.Components {
    public abstract class Updatable : IUpdatable {
        public bool FixedUpdateEnabled { get; protected set; }

        public bool UpdateEnabled { get; protected set; }

        protected Updatable() {
            FixedUpdateEnabled = true;
            UpdateEnabled = true;
        }

        public virtual void Update(float delta) { }

        public virtual void FixedUpdate(float? delta) {}
    }
}
