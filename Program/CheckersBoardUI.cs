using System;
using System.Collections.Generic;
using System.Text;
using Logic;


namespace UI
{
    public class BoardUI
    {
        private readonly string m_SpaceRow;
        private readonly string m_ColumnKeysRow;
        private StringBuilder m_Board;

        public BoardUI(int i_BoardSize, char i_StartColumnKey)
        {
            m_SpaceRow = createSpaceRow(i_BoardSize);
            m_ColumnKeysRow = createColumnKeysRow(i_BoardSize, i_StartColumnKey);
            m_Board = new StringBuilder();
        }

        public StringBuilder Content
        {
            get
            {
                return m_Board;
            }
        }

        private static string createSpaceRow(int i_BoardSize)
        {
            int rowWidth = 4 * i_BoardSize + 1;

            return new string('=', rowWidth).Insert(rowWidth, Environment.NewLine);
        }

        private static string createColumnKeysRow(int i_BoardSize, char i_StartColumnKey)
        {
            char columnKey = i_StartColumnKey;
            StringBuilder columnKeysRow =  new StringBuilder();

            for (int i = 0; i < i_BoardSize; i++)
            {
                columnKeysRow.AppendFormat("{0,4}", columnKey);
                columnKey++;
            }

            columnKeysRow=  columnKeysRow.Insert(columnKeysRow.Length, Environment.NewLine);

            return columnKeysRow.ToString();
        }

        private static void addRow(ref StringBuilder io_Board, CheckersBoard i_DataBoard, char i_RowKey)
        {
            io_Board.AppendFormat("{0}|", i_RowKey);

            char columnKey = i_DataBoard.MinColumnKey;

            for(int j = 0; j < i_DataBoard.Size; j++)
            {
                string key = string.Format("{0}{1}", columnKey, i_RowKey);
                string slotContent;
                if(i_DataBoard[key].Content == null)
                {
                    slotContent = " ";
                }
                else
                {
                    slotContent = i_DataBoard[key].Content.Value.Sign.ToString();

                }

                io_Board.AppendFormat("{0,2} |", slotContent);
                columnKey++;

            }

            io_Board.AppendLine();

        }
        
        public void SetBoard(CheckersBoard i_DataBoard)
        {
            char rowKey = i_DataBoard.MinRowKey;
            char columnKey = i_DataBoard.MinColumnKey;

            m_Board.Clear();
            m_Board.Append(m_ColumnKeysRow);
            m_Board.Append(m_SpaceRow);

            foreach (KeyValuePair<string, BoardSlot> slot in i_DataBoard.Dictionary)
            {

            }
            for (int i = 0; i < i_DataBoard.Size; i++)
            {
                addRow(ref m_Board, i_DataBoard, rowKey);
                m_Board.Append(m_SpaceRow);
                rowKey++;
            }
        }

    }

}
