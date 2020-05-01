using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ring_Defense
{
    class App : Game
    {
        private static App instance;
        public static App Instance{
            get{
                if(instance == null)
                {
                    instance = new App();
                }
                return instance;
            }
        }
        public KeyboardState CurrentKbd
        {
            get; private set;
        }

        public KeyboardState LastKbd
        {
            get; private set;
        }

        public MouseState CurrentMouse
        {
            get; private set;
        }

        public MouseState LastMouse
        {
            get; private set;
        }
        GraphicsDeviceManager m_graphics;

        private Engine.Scene m_currentScene;

        private Rectangle m_windowRect;

        private bool m_fullscreen;

        private Vector2 m_targetRes;

        public RenderTarget2D RenderTarget{get{return m_renderTarget;}}

        Engine.ViewBoxer m_viewBoxer;

        private RenderTarget2D m_renderTarget;

        public Vector2 TargetRes{
            get{return m_targetRes;}
        }
        //Sets the next scene to Run
        public void SetScene<T>(Engine.SceneArgs args) where T : Engine.Scene, new()
        {
            if(m_currentScene != null)
                m_currentScene.End();
            m_currentScene = new T();
            m_currentScene.Start(args);
        }

        public void ToggleFullScreen(bool b)
        {
            Window.IsBorderless = b;
            if(m_fullscreen == b)
                return;
            m_fullscreen = b;
            if(b)
            {
                DisplayMode mode = GraphicsDevice.DisplayMode;
                m_graphics.PreferredBackBufferWidth = (int)mode.Width;
                m_graphics.PreferredBackBufferHeight = (int)mode.Height;
                m_windowRect = Window.ClientBounds;
                Window.Position = new Point(0,0);
            }
            else
            {
                m_graphics.PreferredBackBufferWidth = m_windowRect.Width;;
                m_graphics.PreferredBackBufferHeight = m_windowRect.Height;
                Window.Position = new Point(m_windowRect.X, m_windowRect.Y);
            }

            m_graphics.ApplyChanges();
            m_viewBoxer.Configure(
                TargetRes, Window.ClientBounds.Size.ToVector2(), 1, null, m_renderTarget, false
            );
        }

        private App()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Ring Defense vDev";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            m_windowRect = Window.ClientBounds;
            m_targetRes = new Vector2(800,600);
            m_graphics.PreferredBackBufferWidth = (int)m_targetRes.X;
            m_graphics.PreferredBackBufferHeight = (int)m_targetRes.Y;
            m_graphics.ApplyChanges();
            Window.ClientSizeChanged += (wnd, args) =>
            {
                m_viewBoxer.Configure(
                TargetRes, Window.ClientBounds.Size.ToVector2(), 1, null, m_renderTarget, false
            );
            };
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            m_renderTarget = new RenderTarget2D(GraphicsDevice, (int)TargetRes.X, (int)TargetRes.Y);
            m_viewBoxer = new Engine.ViewBoxer();
            m_viewBoxer.clearColor = Color.Black;
            m_viewBoxer.Configure(
                TargetRes, Window.ClientBounds.Size.ToVector2(), 1, null, m_renderTarget, false
            );
            SetScene<MenuScene>(null);
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            LastKbd = CurrentKbd;
            CurrentKbd = Keyboard.GetState();
            LastMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();

            if(CurrentKbd.IsKeyDown(Keys.F))
                if(!LastKbd.IsKeyDown(Keys.F))
                    ToggleFullScreen(!m_fullscreen);

            // TODO: Add your update logic here
            m_currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            m_currentScene.Draw();

            m_viewBoxer.Draw();

            base.Draw(gameTime);
        }
    }
}
