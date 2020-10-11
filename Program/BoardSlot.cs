using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{

    public class BoardSlot
    {
        private CheckersMen? m_Content;
        private readonly char r_RowKey;
        private readonly char r_ColumnKey;
        private readonly string r_Key;

        public BoardSlot(CheckersMen.eSign? i_Content, char i_ColumnKey, char i_RowKey)
        {
            r_RowKey = i_RowKey;
            r_ColumnKey = i_ColumnKey;
            r_Key = string.Format("{0}{1}", i_ColumnKey, i_RowKey);

            if(i_Content != null)
            {
                m_Content = new CheckersMen(i_Content.Value);
            }
            else
            {
                m_Content = null;
            }
        }

        public CheckersMen? Content
        {
            get
            {
                {
                    return m_Content;
                }
            }
            set
            {
                m_Content = value;
            }
        }

        public char RowKey
        {
            get
            {
                return r_RowKey;
            }
        }


        public char ColumnKey
        {
            get
            {
                return r_ColumnKey;
            }
        }


        public string Key
        {
            get
            {
                return r_Key;
            }
        }


        public bool isEmpty()
        {
            return m_Content == null;
        }

        public void Clear()
        {
            m_Content = null;
        }
    }
}
