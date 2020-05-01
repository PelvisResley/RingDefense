using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ring_Defense.Engine
{
    // responsible for scaling and pillarboxing, letterboxing, etc
    class ViewBoxer
    {
        protected SpriteBatch spriteBatch;
        public Vector2 TargetRes
        {
            get;
            private set;
        }

        public Vector2 RenderRes
        {
            get;
            private set;
        }

        //the output is outputed to the render target
        //if its null, its to the screen
        public RenderTarget2D renderTarget
        {
            get; private set;
        }

        //what we are rendereing, scaling and boxing
        public RenderTarget2D renderObject
        {
            get; private set;
        }

        protected float targetAspect;

        protected float renderAspect;

        private Rectangle targetRectangle;

        public Color clearColor;

        private Texture2D boxingTexture;

        public Texture2D BoxingTexture
        {
            get { return boxingTexture; }
        }

        public void SetBoxingTexture(Texture2D value)
        {
            if (boxingTexture != null && value.Width == boxingTexture.Width && value.Height == boxingTexture.Height)
            {
                boxingTexture = value;
            }
            else
            {
                boxingTexture = value;
                CalculateCalculations();
            }
        }

        public bool CenterBoxingTexture
        {
            get; private set;
        }

        private int boxingTextureOffsetX;

        private int boxingTextureOffsetY;

        public int additionalBoxingTextureScale
        {
            get; private set;
        }

        public ViewBoxer()
        {
            spriteBatch = new SpriteBatch(App.Instance.GraphicsDevice);
            clearColor = Color.LightPink;
        }

        public void Dispose()
        {
            spriteBatch.Dispose();
            spriteBatch = null;
        }

        public void Draw()
        {
            App.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            App.Instance.GraphicsDevice.Clear(clearColor);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.Deferred);


            if (boxingTexture != null)
            {
                for (int x = -boxingTextureOffsetX; x < (int)RenderRes.X + boxingTextureOffsetX; x += boxingTexture.Width * additionalBoxingTextureScale)
                {
                    for (int y = -boxingTextureOffsetY; y < (int)RenderRes.Y + boxingTextureOffsetY; y += boxingTexture.Height * additionalBoxingTextureScale)
                    {
                        spriteBatch.Draw(boxingTexture, new Vector2(x, y), null, Color.White, 0, Vector2.Zero, additionalBoxingTextureScale, SpriteEffects.None, 1);
                    }
                }
            }

            //AHAHAHAHHaaaaah
            spriteBatch.Draw(renderObject, targetRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);

            spriteBatch.End();
        }
        private void CalculateCalculations()
        {
            //make the boxing?
            targetRectangle.Width = (int)RenderRes.X;
            targetRectangle.Height = (int)(targetRectangle.Width / targetAspect + 0.5f);

            if (targetRectangle.Height > (int)RenderRes.Y)
            {
                targetRectangle.Height = (int)RenderRes.Y;
                targetRectangle.Width = (int)(targetRectangle.Height * targetAspect + 0.5f);
            }

            targetRectangle.X = (int)(RenderRes.X / 2) - (targetRectangle.Width / 2);
            targetRectangle.Y = (int)(RenderRes.Y / 2) - (targetRectangle.Height / 2);

            //do the otehr??
            if (boxingTexture != null)
            {
                boxingTextureOffsetX =
                ((BoxingTexture.Width - ((int)TargetRes.X % BoxingTexture.Width)) / 2) * additionalBoxingTextureScale;

                boxingTextureOffsetY =
                ((BoxingTexture.Height - ((int)TargetRes.Y % BoxingTexture.Height)) / 2) * additionalBoxingTextureScale;
            }
        }

        public void Configure(Vector2 targetRes, Vector2 renderRes, int additionalScale, RenderTarget2D target2D, RenderTarget2D renderObject, bool offsetBoxing)
        {
            this.renderObject = renderObject;
            this.renderTarget = target2D;

            this.TargetRes = targetRes;
            this.RenderRes = renderRes;

            this.renderAspect = renderRes.X / renderRes.Y;
            this.targetAspect = targetRes.X / targetRes.Y;

            this.CenterBoxingTexture = offsetBoxing;

            additionalBoxingTextureScale = additionalScale;

            CalculateCalculations();
        }
    }
}