using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ring_Defense.Engine;

namespace Ring_Defense
{
    class MenuScene : Scene
    {
        private SpriteBatch m_spriteBatch;
        public override void Start(SceneArgs args)
        {
            m_spriteBatch = new SpriteBatch(App.Instance.GraphicsDevice);
        }

        public override void End(){
            m_spriteBatch.Dispose();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw()
        {
            App.Instance.GraphicsDevice.SetRenderTarget(App.Instance.RenderTarget);

            App.Instance.GraphicsDevice.Clear(Color.White);
        }
    }
}