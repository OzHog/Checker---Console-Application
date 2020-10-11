using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public readonly struct PlayerMove
    {
        private readonly string r_FromSlotKey;
        private readonly string r_ToSlotKey;
        private readonly string r_SlotKeyToEat;
        private readonly eMoveType r_Type;
        private readonly int r_Value;

        public PlayerMove(string i_FromSlotKey, string i_ToSlotKey, string i_SlotKeyToEat, eMoveType i_Type = eMoveType.NoEat)
        {
            r_FromSlotKey = i_FromSlotKey;
            r_ToSlotKey = i_ToSlotKey;
            r_SlotKeyToEat = i_SlotKeyToEat;
            r_Type = i_Type;
            r_Value = getValueType(i_Type);
        }

        public string FromSlotKey
        {
            get
            {
                return r_FromSlotKey;
            }
        }
        public string ToSlotKey
        {
            get
            {
                return r_ToSlotKey;
            }
        }

        public string SlotKeyToEat
        {
            get
            {
                return r_SlotKeyToEat;
            }
        }
        public enum eMoveType
        {
            NoEat,
            Eat
        }

        public eMoveType Type
        {
            get
            {
                return r_Type;
            }
        }

        private static int getValueType(eMoveType i_Type)
        {
            return (int)i_Type;
        }

    }
}
