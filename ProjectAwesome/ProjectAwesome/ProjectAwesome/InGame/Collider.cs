using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectAwesome
{
    /// <summary>
    /// I can collide with other objects, required for furture proof lol...
    /// </summary>
    class Collider
    {
        public Rectangle boundingBox;
        public void iHaveCollidedWith()
        {
        }

        #region properties
        Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }
        #endregion
    }
}
