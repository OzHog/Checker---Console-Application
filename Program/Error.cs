using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public readonly struct Error
    {
        private readonly eType r_Type;
        private readonly string r_FromSlotKey;
        private readonly string r_ToSlotKey;

        public Error(string i_FromSlotKey, string i_ToSlotKey, eType i_Type)
        {
            r_Type = i_Type;
            r_FromSlotKey = i_FromSlotKey;
            r_ToSlotKey = i_ToSlotKey;
        }

        public eType Type
        {
            get
            {
                return r_Type;
            }
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
        public enum eType
        {
            FromSlotKeyIsNotInRange,
            ToSlotKeyIsNotInRange,
            SlotKeysAreNotInRange,
            NotPossibleMove,
            DidNotMoveToEat,
            PlayerMustToEatAgain
        }
    }
}
