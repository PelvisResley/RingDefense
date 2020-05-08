using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ring_Defense.Engine;

namespace Ring_Defense
{
    class MenuScene : Scene
    {
        private SpriteBatch sb;

        Camera camera;
        public override void Start(SceneArgs args)
        {
            sb = new SpriteBatch(App.Instance.GraphicsDevice);
        }

        public override void End(){
            sb.Dispose();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw()
        {
            App.Instance.GraphicsDevice.SetRenderTarget(App.Instance.RenderTarget);

            App.Instance.GraphicsDevice.Clear(Color.White);

            sb.Begin(transformMatrix : camera.TransformMatrix);

            sb.End();
        }
    }
}