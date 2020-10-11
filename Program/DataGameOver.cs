using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public readonly struct DataGameOver
    {
        private readonly string r_WinnerName;
        private readonly string r_LoserName;
        private readonly int r_WinnerScore;
        private readonly bool r_Draw;
        private readonly bool r_Quit;


        internal DataGameOver(string i_WinnerName, string i_LoserName, int i_WinnerScore, bool i_Draw, bool i_Quit = false)
        {

            r_WinnerName = i_WinnerName;
            r_LoserName = i_LoserName;
            r_WinnerScore = i_WinnerScore;
            r_Draw = i_Draw;
            r_Quit = i_Quit;
        }

        public string WinnerName
        {
            get
            {
                return r_WinnerName;
            }
        }

        public string LoserName
        {
            get
            {
                return r_LoserName;
            }
        }

        public int WinnerScore
        {
            get
            {
                return r_WinnerScore;
            }
        }

        public bool Draw
        {
            get
            {
                return r_Draw;
            }
        }

        public bool Quit
        {
            get
            {
                return r_Quit;
            }
        }

    }
}
