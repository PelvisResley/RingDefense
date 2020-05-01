using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Ring_Defense.Engine
{
    abstract class Scene
    {
        public abstract void Start(SceneArgs args);

        public abstract void End();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw();
    }
}