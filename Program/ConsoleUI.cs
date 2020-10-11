using System;
using System.Collections.Generic;
using System.Text;
using Ex02.ConsoleUtils;
using Logic;



namespace UI
{
    public class ConsoleUI
    {
        private BoardUI m_Board;
        private readonly string[] r_ScreenMenuMassages;
        private readonly int r_LegalInputUserMoveLength = 5;

        private enum eMenuScreen
        {
            MainScreen,
            GameModeMenuScreen,
            BoardSizeMenuScreen,
        }

        public ConsoleUI()
        {
            initMenuScreenMassages(out r_ScreenMenuMassages);
            m_Board = null;
        }

        private static void initMenuScreenMassages(out string[] o_SreenMassages)
        {
            string entryMsg = string.Format("Welcome To Draugh World!");
            string gameMoadeMenuMsg = string.Format(
                @"Please Choose Mode To Play
    1. Human Vs Human
    2. Human Vs Computer");


            string boardSizeMenuMsg = string.Format(
                @"Please Choose Board Size
    1. 6 X 6
    2. 8 X 8
    3. 10 X 10");

            o_SreenMassages = new string[3] { entryMsg, gameMoadeMenuMsg, boardSizeMenuMsg };
        }

        private static bool getBoardSizeFromUserInput(int i_UserInput , out int o_BoardSize)
        {
            o_BoardSize = 0;
            bool inputLegal = true;

            // $G$ CSS-999 (-3) You should have used enumerations here.
            switch (i_UserInput)
            {
                case 1:
                    o_BoardSize = 6;
                    break;
                case 2:
                    o_BoardSize = 8;
                    break;
                case 3:
                    o_BoardSize = 10;
                    break;
                default:
                    inputLegal = false;
                    break;
            }

            return inputLegal;
        }
        
        private static bool isQuitCode(string i_Str)
        {
            return i_Str == "Q";

        }
        
        private static bool isInputPatternLegal(string i_InputUserMove)
        {
            bool legalPattern = char.IsUpper(i_InputUserMove[0]) && char.IsUpper(i_InputUserMove[3]);

            legalPattern &= legalPattern & i_InputUserMove[2] == '>';
            legalPattern &= char.IsLower(i_InputUserMove[1]) && char.IsLower(i_InputUserMove[4]);

            return legalPattern;
        }

        private static void displayScores(Player i_PlayerA, Player i_PlayerB)
        {
            StringBuilder displayMassage = new StringBuilder("Game Score:").AppendLine();

            displayMassage.AppendFormat("({0}) {1} : {2} ({3})",i_PlayerA.Name, i_PlayerA.Score, i_PlayerB.Score,  i_PlayerB.Name).AppendLine();
            Console.WriteLine(displayMassage);
        }
        
        private string getErrorMassage(Error i_Error)
        {
            string errorMassage = string.Format("");

            switch(i_Error.Type)
            {
                case Error.eType.FromSlotKeyIsNotInRange:
                    errorMassage = string.Format("Index: {0}, is not in board range", i_Error.FromSlotKey);
                    break;
                case Error.eType.ToSlotKeyIsNotInRange:
                    errorMassage = string.Format("Index: {0}, is not in board range", i_Error.ToSlotKey);
                    break;
                case Error.eType.SlotKeysAreNotInRange:
                    errorMassage = string.Format("Indexes are not in board range");
                    break;
                case Error.eType.NotPossibleMove:
                    errorMassage = string.Format("This move is not possible");
                    break;
                case Error.eType.DidNotMoveToEat:
                    errorMassage = string.Format("You must to pick move that eats enemy Men");
                    break;
                case Error.eType.PlayerMustToEatAgain:
                    errorMassage = string.Format("You must eat again");
                    break;

            }

            errorMassage = string.Format("{0}, please try again", errorMassage);

            return errorMassage;
        }

        private void displayMenuScreen(eMenuScreen i_Screen)
        {
            string massageToDisplay = r_ScreenMenuMassages[(int)i_Screen];

            Console.WriteLine(massageToDisplay);
        }

        private string getUserInput(string i_InputLabel)
        {
            Console.Write("{0} ", i_InputLabel);

            return Console.ReadLine();
        }

        public void DisplayMainScreen()
        {
            Screen.Clear();
            displayMenuScreen(eMenuScreen.MainScreen);
            //setGameSettings();
        }
        
        public GameMode.eGameMode GetGameMode()
        {
            bool parseSucceeded = false;
            GameMode.eGameMode? gameMode = null;

            displayMenuScreen(eMenuScreen.GameModeMenuScreen);
            while(!parseSucceeded)
            {
                string gameModeStr = getUserInput(">>");
                parseSucceeded = GameMode.TryParse(gameModeStr, out gameMode);

                if(!parseSucceeded)
                {
                    Console.WriteLine("Illegal Input, Please Try Again.");
                }
            }

            return (GameMode.eGameMode)gameMode;

        }

        public int GetBoardSize()
        {
            bool parseSucceeded = false;
            int boardSize = 0;

            displayMenuScreen(eMenuScreen.BoardSizeMenuScreen);
            while(!parseSucceeded)
            {

                string boardSizeUserChoose = getUserInput(">>");
                parseSucceeded = int.TryParse(boardSizeUserChoose, out int userInputNum);
                if(parseSucceeded)
                {
                    parseSucceeded = getBoardSizeFromUserInput(userInputNum, out boardSize);
                }

                if(!parseSucceeded)
                {
                    Console.WriteLine("Illegal Input, Please Try Again.");
                }
            }

            return boardSize;
        }

        public void CreateBoardUI(int i_BoardSize)
        {
           m_Board = new BoardUI(i_BoardSize, 'A');
        }

        public void DisplayBoard(CheckersBoard i_DataBoard)
        {
            if(m_Board != null)
            {
                m_Board.SetBoard(i_DataBoard);
                Console.WriteLine(m_Board.Content);
            }
        }

        public void GetUserMove(
            string i_TurnPlayerName,
            CheckersMen.eSign i_TurnPlayerMenSign,
            out string o_FromSlotKey,
            out string o_ToSlotKey,
            out bool o_Quit)
        {
            o_Quit = false;
            o_FromSlotKey = "";
            o_ToSlotKey = "";
            string inputLable = string.Format("{0}'s Turn ({1}):", i_TurnPlayerName, i_TurnPlayerMenSign.ToString());
            bool legalInput = false;

            while(!legalInput)
            {
                string inputUserMove = getUserInput(inputLable);
                legalInput = isInputUserMoveLegal(inputUserMove);
                if(legalInput)
                {
                    if(isQuitCode(inputUserMove))
                    {
                        o_Quit = true;
                    }
                    else
                    {
                        string[] slotKeys = inputUserMove.Split('>');
                        o_FromSlotKey = slotKeys[0];
                        o_ToSlotKey = slotKeys[1];
                    }
                }
                else
                {
                    Console.WriteLine(
                        @"Illegal Input,   
Legal Input Pattern: fromIndex>toIndex Or 'Q' to quit");
                }
            }
        }

        private bool isInputUserMoveLegal(string i_InputUserMove)
        {
            bool legalInput = false;

            if(i_InputUserMove.Length == r_LegalInputUserMoveLength)
            {
                legalInput = isInputPatternLegal(i_InputUserMove);
            }
            else
            {
                legalInput = i_InputUserMove == "Q";
            }

            return legalInput;
        }

        public void DisplayPreviousPlayerMoves(List<PlayerMove> i_PlayerMoves, string i_PlayerName)
        {
            if(i_PlayerMoves.Count > 0)
            {
                StringBuilder playerMovesStr = new StringBuilder();

                playerMovesStr.AppendFormat("{0} Moved: ", i_PlayerName);
                foreach(PlayerMove playerMove in i_PlayerMoves)
                {
                    playerMovesStr.AppendFormat("{0}>{1}", playerMove.FromSlotKey, playerMove.ToSlotKey);
                    if(playerMove.Type == PlayerMove.eMoveType.Eat)
                    {
                        playerMovesStr.AppendFormat(", ate at index {0}", playerMove.SlotKeyToEat);
                    }

                    playerMovesStr.AppendLine();
                }

                Console.WriteLine(playerMovesStr);
            }
        }

        public void DisplayLogicError(Error i_Error)
        {
            string errorMassage = getErrorMassage((i_Error));

            Console.WriteLine(errorMassage);
        }
        
        public string[] GetPlayersNames(GameMode.eGameMode i_GameMode)
        {
            
            string[] playersNames = new string[2];

            playersNames[0] = getUserInput("First Player's Name:");

            if(i_GameMode == GameMode.eGameMode.HumanVsHuman)
            {
                playersNames[1] = getUserInput("Second Player's Name:");
            }

            return playersNames;
        }

        public bool IsPlayersWantsToPlayerAgain(Player i_PlayerA, Player i_PlayerB)
        {
            bool legalInput = false;
            bool playersWantsToPlayerAgain = false;

            Screen.Clear();
            displayScores(i_PlayerA, i_PlayerB);
            while (!legalInput)
            {
                string userInput = getUserInput("Continue Play Y/N: ");

                legalInput = userInput.Equals("Y") || userInput.Equals("N");
                if (legalInput)
                {
                    playersWantsToPlayerAgain = userInput.Equals("Y");
                }
                else
                {
                    Console.WriteLine("Input illegal, please try again");
                }
            }

            return playersWantsToPlayerAgain;
        }

        public void DisplayGameOver(DataGameOver i_DataGameOver)
        {
            StringBuilder displayMassage = new StringBuilder("Game Over!").AppendLine().Append("Result: ");

            if(i_DataGameOver.Draw)
            {
                displayMassage.AppendLine("Draw (Non of the players have possible moves)");
            }
            else
            {
                if(i_DataGameOver.Quit)
                {
                    displayMassage.AppendFormat("{0} quit, ", i_DataGameOver.LoserName);
                }

                displayMassage.AppendFormat(
                    "{0} is the winner with score {1}",
                    i_DataGameOver.WinnerName,
                    i_DataGameOver.WinnerScore);
            }

            Console.WriteLine(displayMassage);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

