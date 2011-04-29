using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace ProjectAwesome
{
    public class Camera
    {
        //A simple 2D Camera with Transform Matrix
        //Usage:
        //
        //      spriteBatch.Begin(SpriteSortMode.BackToFront,
        //                    BlendState.AlphaBlend,
        //                    null,
        //                    null,
        //                    null,
        //                    null,
        //                    Camera.Transform(graphicdevice));
        //
        //       Your Draw logic here
        //
        //      spriteBatch.End();
        //Members
        private Vector2 m_CameraPosition;
        private float m_Zoom;
        private float m_Rotation;
        private Matrix m_Transform;

        public Camera()
        {
            m_Zoom = .25f;
            m_Rotation = 0;
            m_CameraPosition = Vector2.Zero;
        }

        #region Set/Get

        /// <summary>
        /// Camera Zoom amount
        /// </summary>
        public float Zoom
        {
            get { return m_Zoom; }
            set { m_Zoom = value; if (m_Zoom < 0.1f) m_Zoom = 0.1f; } // Negative zoom will flip image
        }

        /// <summary>
        /// Camera Rotation amount
        /// </summary>
        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }

        /// <summary>
        /// Moves the camera with the input amount
        /// </summary>
        /// <param name="amount"></param>
        public void Move(Vector2 amount)
        {
            m_CameraPosition += amount;
        }

        /// <summary>
        /// Camera Postion
        /// </summary>
        public Vector2 Position
        {
            get { return m_CameraPosition; }
            set { m_CameraPosition = value; }
        }

        #endregion

        /// <summary>
        /// Updates the cameras transform
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <returns></returns>
        public Matrix Transform(GraphicsDevice graphicsDevice)
        {
            float ViewportWidth = graphicsDevice.Viewport.Width;
            float ViewportHeight = graphicsDevice.Viewport.Height;

            m_Transform =
              Matrix.CreateTranslation(new Vector3(-m_CameraPosition.X, -m_CameraPosition.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
                                         Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));
            return m_Transform;
        }

    }
}

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace ProjectAwesome
//{
//    class Camera
//    {
//        private readonly Viewport _viewport;
//        private Vector2 _position;
//        private Rectangle? _limits;

//        public Camera(Viewport viewport)
//        {
//            _viewport = viewport;
//            Origin = new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
//            Zoom = 1.0f;
//        }

//        public Matrix GetViewMatrix(Vector2 parallax)
//        {
//            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) *
//                   Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
//                   Matrix.CreateRotationZ(Rotation) *
//                   Matrix.CreateScale(Zoom) *
//                   Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
//        }

//        public void LookAt(Vector2 position)
//        {
//            Position = position - new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
//        }

//        public void Move(Vector2 displacement, bool respectRotation = false)
//        {
//            if (respectRotation)
//            {
//                displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(-Rotation));
//            }

//            Position += displacement;
//        }

//        public Rectangle? Limits
//        {
//            get
//            {
//                return _limits;
//            }
//            set
//            {
//                if (value != null)
//                {
//                    // Assign limit but make sure it's always bigger than the viewport
//                    _limits = new Rectangle
//                    {
//                        X = value.Value.X,
//                        Y = value.Value.Y,
//                        Width = System.Math.Max(_viewport.Width, value.Value.Width),
//                        Height = System.Math.Max(_viewport.Height, value.Value.Height)
//                    };

//                    // Validate camera position with new limit
//                    Position = Position;
//                }
//                else
//                {
//                    _limits = null;
//                }
//            }
//        }

//        public Vector2 Origin { get; set; }

//        public float Zoom { get; set; }

//        public float Rotation { get; set; }

//        public Vector2 Position
//        {
//            get
//            {
//                return _position;
//            }
//            set
//            {
//                _position = value;

//                // If there's a limit set and there's no zoom or rotation clamp the position
//                if (Limits != null && Zoom == 1.0f && Rotation == 0.0f)
//                {
//                    _position.X = MathHelper.Clamp(_position.X, Limits.Value.X, Limits.Value.X + Limits.Value.Width - _viewport.Width);
//                    _position.Y = MathHelper.Clamp(_position.Y, Limits.Value.Y, Limits.Value.Y + Limits.Value.Height - _viewport.Height);
//                }
//            }
//        }
//    }
//}
