using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network_Game.Network
{
    public struct InputObject
    {

        public float HorizontalAxis { get; set; }
        public float VerticalAxis { get; set; }
        public float AmingAngle { get; set; }
        /// <summary>
        /// 1   A      : Jump
        /// 2   LT     : Graplinghook
        /// 4   RT     : Primary fire
        /// 8   RB     : secondery fire
        /// 16  DUp    : primary next
        /// 32  DDown  : primary prev
        /// 64  DLeft  : secondary next
        /// 128 DRight : secondary prev
        /// </summary>
        public byte Buttons { get; set; }

        public bool isButtonDown(Button b)
        {
            return (Buttons & (byte)b) > 0;
        }

        public void setButton(Button b, bool value)
        {
            if (value)
            {
                Buttons |= (byte)b;
            }
            else
            {
                Buttons &= (byte)((byte)b^255);
            }
        }

        public enum Button
        {
            A = 1,
            LT = 2,
            RT = 4,
            RB = 8,
            DUp = 16,
            DDown = 32,
            DLeft = 64,
            DRitht = 128
        }

    }
}
