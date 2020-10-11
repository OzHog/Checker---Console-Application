using System;
using Ex02.ConsoleUtils;
using System.Collections.Generic;
using System.Text;
using UI;
using Logic;

// $G$ SFN-999 (-7) the program should not accept player names with spaces

namespace Program
{
    // $G$ CSS-999 (-3) The class must have an access modifier.
    class Program
    {


        // $G$ CSS-999 (-3) The method must have an access modifier.

        static void Main()
        {
            // Console.WriteLine(string.Format("{0,3}", 5));
            // DraughtBoard draughtBoard = new DraughtBoard(6);
            // DraughtsUI.PrintBoard(draughtBoard);

            PlayCheckers();
        }

        public static void PlayCheckers()
        {
            CheckersGame checkersGame = new CheckersGame();
            checkersGame.LaunchGame();

        }
    }


    // $G$ CSS-999 (-5) Every Class/Enum which is not nested should be in a separate file.

    public class CheckersGame
    {
        private bool m_PlayerQuit = false;
        private readonly ConsoleUI r_UIEngine;
        private CheckersLogic m_GameLogic;

        public CheckersGame()
        {
            r_UIEngine = new ConsoleUI();
            m_GameLogic = null;
        }

        public void LaunchGame()
        {
            r_UIEngine.DisplayMainScreen();
            getGameInitializeParameters(out GameMode.eGameMode gameMode, out int boardSize, out string[] playersNames);
            r_UIEngine.CreateBoardUI(boardSize);
            m_GameLogic = new CheckersLogic(gameMode);
            startNewGame(gameMode, boardSize, playersNames);
        }

        private void getGameInitializeParameters(out GameMode.eGameMode o_GameMode, out int o_BoardSize, out string[] o_PlayersNames)
        {
            o_GameMode = r_UIEngine.GetGameMode();
            o_PlayersNames = r_UIEngine.GetPlayersNames(o_GameMode);
            Screen.Clear();
            o_BoardSize = r_UIEngine.GetBoardSize();
        }


        private void displayBoard(bool i_DisplayPreviousPlayerMoves)
        {
            Screen.Clear();
            r_UIEngine.DisplayBoard(m_GameLogic.Board);
            if (i_DisplayPreviousPlayerMoves)
            {
                r_UIEngine.DisplayPreviousPlayerMoves(m_GameLogic.PlayerEnemy.LastMovesPlayed, m_GameLogic.PlayerEnemy.Name);
            }
        }

        //  CSS-013 (-3) Input parameters names should start with i_PascaleCase.
        private void startNewGame(GameMode.eGameMode? o_GameMode = null, int i_BoardSize = 0, string[] i_PlayersNames = null)
        {
            m_PlayerQuit = false;
            m_GameLogic.InitNewGame(o_GameMode, i_BoardSize, i_PlayersNames);
            playCheckers();
        }

        private void playCheckers()
        {
            while(!m_GameLogic.GameOver && !m_PlayerQuit)
            {
                displayBoard(true);
                playTurn();
            }

            displayBoard(true);
            gameOver();
        }

        private void gameOver()
        {
            if(m_GameLogic.GameOver)
            {
                r_UIEngine.DisplayGameOver(m_GameLogic.DataGameOver.Value);
            }
            else
            {
               
                int score = m_GameLogic.PlayerEnemy.Score - m_GameLogic.PlayerTurn.Score;
                m_GameLogic.PlayerEnemy.Score += score;
                DataGameOver dataGameOver = new DataGameOver(m_GameLogic.PlayerEnemy.Name, m_GameLogic.PlayerTurn.Name,score,false, true);
                r_UIEngine.DisplayGameOver(dataGameOver);
            }

            bool playersWantsToPlayAgain = r_UIEngine.IsPlayersWantsToPlayerAgain(m_GameLogic.PlayerTurn, m_GameLogic.PlayerEnemy);
            if(playersWantsToPlayAgain)
            {
                if(m_PlayerQuit)
                {
                    m_GameLogic.ChangeTurn();
                }
                startNewGame();
            }
        }

        private void playTurn()
        {
            Player.eType playerTurnType = m_GameLogic.PlayerTurn.Type;

            do
            {
                if(playerTurnType == Player.eType.Human)
                {
                    humanPlayerPlay();
                    if(m_GameLogic.PlayerTurn.CanEatAgain)
                    {
                       displayBoard(false);
                    }
                }
                else
                {
                    computerPlayerPlay();
                }
            }
            while(m_GameLogic.PlayerTurn.CanEatAgain);
        }

        private void humanPlayerPlay()
        {
            bool processSucceeded = false;
            while (!processSucceeded && !m_PlayerQuit)
            {
                r_UIEngine.GetUserMove(m_GameLogic.PlayerTurn.Name, m_GameLogic.PlayerTurn.RegularCheckersMen.Sign, out string fromSlotKey, out string toSlotKey, out m_PlayerQuit);
                if (!m_PlayerQuit)
                {
                    processSucceeded = m_GameLogic.ProcessUserMove(fromSlotKey, toSlotKey, out Error? error);
                    if (!processSucceeded)
                    {
                        r_UIEngine.DisplayLogicError(error.Value);
                    }
                }
            }
        }

        private void computerPlayerPlay()
        {
            m_GameLogic.ComputerPlay();
        }
    }
}


