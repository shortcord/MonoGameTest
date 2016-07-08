using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Components {
    public abstract class GameObject : IDrawable, IUpdatable, IComponent {
        public bool FixedUpdateEnabled { get; protected set; } = false;
        public bool GameDrawEnabled { get; protected set; } = false;
        public bool UiDrawEnabled { get; protected set; } = false;
        public bool UpdateEnabled { get; protected set; } = false;

        public virtual void Start() { }
        public virtual void LoadContent() { }
        public virtual void FixedUpdate(float? delta) { }
        public virtual void Update(float delta) { }
        public virtual void GameDraw(SpriteBatch spriteBatch) { }
        public virtual void UiDraw(UiSpriteBatch spriteBatch) { }
    }
}
