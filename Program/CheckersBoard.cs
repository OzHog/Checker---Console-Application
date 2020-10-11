using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Logic
{
    public  class CheckersBoard
    {
        private readonly Dictionary<string, BoardSlot> r_Board;
        private readonly int r_Size;
        private readonly char r_MinRowKey;
        private readonly char r_MinColumnKey;
        private readonly char r_MaxRowKey;
        private readonly char r_MaxColumnKey;

        public int Size
        {
            get
            {
                return r_Size;
            }
        }

        public char MinRowKey
        {
            get
            {
                return r_MinRowKey;
            }
        }

        public char MaxRowKey
        {
            get
            {
                return r_MaxRowKey;
            }
        }

        public char MinColumnKey
        {
            get
            {
                return r_MinColumnKey;
            }
        }

        public char MaxColumnKey
        {
            get
            {
                return r_MaxColumnKey;
            }
        }

        public Dictionary<string, BoardSlot> Dictionary
        {
            get
            {
                return r_Board;
            }
        }

        public static void GetRowAndColumnFromKey(string i_Key, out char o_Row, out char o_Column)
        {
            o_Column = i_Key[0];
            o_Row = i_Key[1];
        }

        public static string CreateKey(char i_ColumnKey, char i_RowKey)
        {
            return string.Format("{0}{1}", i_ColumnKey, i_RowKey);
        }


        public BoardSlot this[string key]
        {
            get
            {
                return r_Board[key];
            }
            set
            {
                r_Board.Add(key, value);
            }
        }

        public CheckersBoard(int i_Size)
        {
            r_MinRowKey = 'a';
            r_MinColumnKey = 'A';
            r_Size = i_Size;
            r_MaxRowKey = GetCharByDistance(r_MinRowKey, i_Size - 1);
            r_MaxColumnKey = GetCharByDistance(r_MinColumnKey, i_Size - 1);
            r_Board = new Dictionary<string, BoardSlot>();

            initBoard(i_Size);
        }

        public int GetAmountOfCheckersMen(CheckersMen.eSign i_CheckerMenSign)
        {
            int amountOfCheckersMen = 0;

            foreach(KeyValuePair<string, BoardSlot> slot in r_Board)
            {
                if(!slot.Value.isEmpty())
                {
                    CheckersMen.eSign slotContent = slot.Value.Content.Value.Sign;
                    if(slotContent == i_CheckerMenSign)
                    {
                        amountOfCheckersMen++;
                    }
                }
            }

            return amountOfCheckersMen;
        }

        public void SetPlayerMove(PlayerMove i_PlayerMove)
        {
            CheckersMen FromSlotContent = r_Board[i_PlayerMove.FromSlotKey].Content.Value;

            r_Board[i_PlayerMove.FromSlotKey].Clear();
            r_Board[i_PlayerMove.ToSlotKey].Content = FromSlotContent;
            if(i_PlayerMove.Type == PlayerMove.eMoveType.Eat)
            {
                r_Board[i_PlayerMove.SlotKeyToEat].Clear();
            }
        }

        public bool IsKeyInRange(string i_Key)
        {
            char rowKey;
            char columnKey;


            GetRowAndColumnFromKey(i_Key, out rowKey, out columnKey);

            return isRowKeyInRange(rowKey) && isColumnKeyInRange(columnKey);
        }
        
        public string GetSlotKeyByDirection(string i_FromSlotKey, int i_RowDirection = 0, int i_ColumnDirection = 0)
        {
            GetRowAndColumnFromKey(i_FromSlotKey, out char fromRowKey, out char fromColumnKey);
            char requireSlotRowKey = GetCharByDistance(fromRowKey, i_RowDirection);
            char requireSlotColumnKey = GetCharByDistance(fromColumnKey, i_ColumnDirection);
            string requireSlotKey = CheckersBoard.CreateKey(requireSlotColumnKey, requireSlotRowKey);

            if (!IsKeyInRange(requireSlotKey))
            {
                requireSlotKey = null;
            }

            return requireSlotKey;
        }

        public static char GetCharByDistance(char i_StartChar, int i_Distance)
        {
            int intChar = i_StartChar;
            int requiredASCIINum = intChar + i_Distance;

            return (char)requiredASCIINum;
        }

        private bool isRowKeyInRange(char i_RowKey)
        {
            return (i_RowKey.CompareTo(r_MinRowKey) >= 0) && (i_RowKey.CompareTo(r_MaxRowKey) <= 0);
        }

        private bool isColumnKeyInRange(char i_ColumnKey)
        {
            return (i_ColumnKey.CompareTo(r_MinColumnKey) >= 0) && (i_ColumnKey.CompareTo(r_MaxColumnKey) <= 0);
        }

        private void addBoardSlot(CheckersMen.eSign? i_Content, string i_Key)
        {
            BoardSlot boardSlot;

            if(i_Content == null)
            {
                boardSlot = new BoardSlot(null, i_Key[0], i_Key[1]);

            }
            else
            {
                boardSlot = new BoardSlot(i_Content, i_Key[0], i_Key[1]);
            }

            r_Board.Add(i_Key, boardSlot);
        }

        private void initBoard(int i_BoardSize)
        {
            bool setMen = false;
            CheckersMen.eSign menSign = CheckersMen.eSign.O;
            int numOfRowsForEachPlayer = (i_BoardSize / 2);

            for(int i = 0; i < i_BoardSize; i++)
            {
                char rowKey = GetCharByDistance(r_MinRowKey, i);

                if(i == numOfRowsForEachPlayer + 1)
                {
                    menSign = CheckersMen.eSign.X;
                }

                for(int j = 0; j < i_BoardSize; j++)
                {
                    char columnKey = GetCharByDistance(r_MinColumnKey, j);
                    string key = CreateKey(columnKey, rowKey);
                    CheckersMen.eSign? slotContent = null;

                    if(setMen && (i > (numOfRowsForEachPlayer) || i < (numOfRowsForEachPlayer - 1)))
                    {
                        slotContent = menSign;
                    }

                    addBoardSlot(slotContent, key);
                    setMen = !setMen;
                }

                setMen = !setMen;
            }
        }
    
    }
}
