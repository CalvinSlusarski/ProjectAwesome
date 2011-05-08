using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectAwesome.InGame
{
    class Timer
    {
        //start time, position in sound clip, length of clip(in ms)
        //and boolean flag to tell if timer is done
        static double start;
        static double position;
        static double length;
        static Boolean finished;
        
        //input a length for the timer and a gametime to init it
        public Timer(double myLength, GameTime theGameTime)
        {
            start = theGameTime.TotalGameTime.Milliseconds;
            position = start;
            length = myLength;
            finished = false;
        }
        //update the position in the timer
        //check to see if timer is finished
        public static void Update(GameTime theGameTime)
        {
            position += theGameTime.ElapsedGameTime.Milliseconds;

            if (position > start + length)
            {
                finished = true;
            }
        }
        public Boolean over()
        {
            return finished;
        }
    }
}
