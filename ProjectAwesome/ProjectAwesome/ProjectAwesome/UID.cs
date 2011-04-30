using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <Summary>
/// Creates unique's id's
/// <Summary>
namespace ProjectAwesome
{
    static class UID
    {
        static int currentID = 0;
        public static int getID()
        {
            currentID++;
            return currentID;
        }
    }
}
