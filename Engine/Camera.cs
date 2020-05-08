using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ring_Defense.Engine0
{
    //camera. only responsible for manipulating the view of this, not for scaling to screen or smth
    public class Camera
    {
        public Camera()
        {
            Zoom = 1;
            Rotation = 0;

            Position = new Vector3();
        }
        bool cameraChanged;

        Matrix transform;
        public Matrix Transform { get { Update(); return transform; } }

        public float Zoom { get { return zoom; } set { if (!(zoom == value)) { zoom = value; cameraChanged = true; } } }
        float zoom;
        public Vector3 Position { get { return position; } set { if (!(position == value)) { position = value; cameraChanged = true; } } }
        Vector3 position;
        public float Rotation { get { return rotation; } set { if (!(rotation == value)) { rotation = value; cameraChanged = true; } } }
        float rotation;

        private void Update()
        {
            if(cameraChanged)
            {
                transform = Matrix.CreateTranslation(Position) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateRotationZ(Rotation);

                cameraChanged = false;
            }
        }

        public void Move(Vector3 v)
        {
            Position += v;
        }

        public void Turn(float deg)
        {
            Rotation = Rotation * MathHelper.ToRadians(deg);
        }

        public void SetRotation(float deg)
        {
            Rotation = MathHelper.ToRadians(deg);
        }

        public void AdjustZoom(float z)
        {
            Zoom = Zoom * z;
        }
    }
}