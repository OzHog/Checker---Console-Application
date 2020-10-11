using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public readonly struct Direction
    {
        private readonly int r_Forward;
        private readonly int r_Backward;
        private readonly int r_Left;
        private readonly int r_Right;

        public Direction(int i_Forward, int i_Backward, int i_Left, int i_Right)
        {
            r_Forward = i_Forward;
            r_Backward = i_Backward;
            r_Left = i_Left;
            r_Right = i_Right;
        }

        public int Forward
        {
            get
            {
                return r_Forward;
            }
        }
  
        public int Backward {
            get
            {
                return r_Backward;
            }
        }

        public int Left
        {
            get
            {
                return r_Left;
            }
        }

        public int Right
        {
            get
            {
                return r_Right;
            }
        }


    }
}
