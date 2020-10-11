
namespace Logic

{
    public struct GameMode
    {
        public enum eGameMode
        {
            HumanVsHuman = 1,
            HumanVsComputer

        }

        private static bool isNumberInRange(int i_Number)
        {
            return i_Number > 0 && i_Number < 3;

        }

        public static bool TryParse(string i_StrNum , out eGameMode? o_GameMode)
        {
            o_GameMode = null;
            bool parseSucceeded = int.TryParse(i_StrNum, out int num);

            if(parseSucceeded && isNumberInRange(num))
            {
                o_GameMode = (eGameMode)num;
            }
            else
            {
                parseSucceeded = false;
            }
            
            return parseSucceeded;
        }

    }
}
