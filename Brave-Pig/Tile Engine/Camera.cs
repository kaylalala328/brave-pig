using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tile_Engine
{
    public static class Camera
    {
        #region Declarations
        /// <summary>
        /// 카메라가 나타내는 뷰 영역의 왼쪽 상단 코너
        /// </summary>
        private static Vector2 position = Vector2.Zero;
        /// <summary>
        /// position에서 오른쪽, 아래쪽으로의 픽셀, 즉 뷰 영역에 해당하는 영역을 의미한다.
        /// </summary>
        private static Vector2 viewPortSize = Vector2.Zero;

        private static Rectangle worldRectangle = new Rectangle(0, 0, 0, 0);
        #endregion

        #region Properties
        public static Vector2 Position
        {
            get { return position; }
            set
            {
                ///한계를 지정하고 게임 월드가 항상 화면에 나타나게 지정한다.
                position = new Vector2(MathHelper.Clamp(value.X, worldRectangle.X, worldRectangle.Width - ViewPortWidth),
                                       MathHelper.Clamp(value.Y, worldRectangle.Y, worldRectangle.Height - ViewPortHeight));
            }
        }

        public static Rectangle WorldRectangle
        {
            get { return worldRectangle; }
            set { worldRectangle = value; }
        }

        public static int ViewPortWidth
        {
            get { return (int)viewPortSize.X; }
            set { viewPortSize.X = value; }
        }

        public static int ViewPortHeight
        {
            get { return (int)viewPortSize.Y; }
            set { viewPortSize.Y = value; }
        }

        public static Rectangle ViewPort
        {
            get 
            {
                return new Rectangle((int)Position.X, (int)Position.Y, ViewPortWidth, ViewPortHeight);
            }
        }
        #endregion

        #region Public Methods
        public static void Move(Vector2 offset)
        {
            Position += offset;
        }

        public static bool ObjectIsVisible(Rectangle bounds)
        {
            return (ViewPort.Intersects(bounds));
        }
        
        public static Vector2 WorldToScreen(Vector2 worldLocation)
        {
            return worldLocation - position;
        }
        
        public static Rectangle WorldToScreen(Rectangle worldRectangle)
        {
            return new Rectangle(worldRectangle.Left - (int)position.X, worldRectangle.Top - (int)position.Y, worldRectangle.Width, worldRectangle.Height);
        }

        public static Vector2 ScreenToWorld(Vector2 screenLocation)
        {
            return screenLocation + position;
        }

        public static Rectangle ScreenToWorld(Rectangle screenRectangle)
        {
            return new Rectangle(screenRectangle.Left + (int)position.X, screenRectangle.Top + (int)position.Y, screenRectangle.Width, screenRectangle.Height);
        }
        #endregion
    }
}
