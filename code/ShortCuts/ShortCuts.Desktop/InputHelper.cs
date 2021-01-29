using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ShortCuts.Desktop
{
    /// <summary>
    /// original source from https://www.codeproject.com/Articles/5264831/How-to-Send-Inputs-using-Csharp
    /// </summary>
    public class InputHelper
    {
        #region keyboard structs
        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)] public MouseInput mi;
            [FieldOffset(0)] public KeyboardInput ki;
            [FieldOffset(0)] public HardwareInput hi;
        }

        public struct Input
        {
            public int type;
            public InputUnion u;
        }

        [Flags]
        public enum InputType
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        [Flags]
        public enum KeyEventF
        {
            KeyDown = 0x0000,
            ExtendedKey = 0x0001,
            KeyUp = 0x0002,
            Unicode = 0x0004,
            Scancode = 0x0008
        }

        [Flags]
        public enum MouseEventF
        {
            Absolute = 0x8000,
            HWheel = 0x01000,
            Move = 0x0001,
            MoveNoCoalesce = 0x2000,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            VirtualDesk = 0x4000,
            Wheel = 0x0800,
            XDown = 0x0080,
            XUp = 0x0100
        }
        #endregion

        #region keyboard constants
        public class Keys
        {
            public const ushort KEY_ESC = 0X01; //(Esc)
            public const ushort KEY_1 = 02; //(1!)
            public const ushort KEY_2 = 03; //(2@)
            public const ushort KEY_3 = 0X04; //(3#)
            public const ushort KEY_4 = 0X05; //(4$)
            public const ushort KEY_5 = 0X06; //(5%E)
            public const ushort KEY_6 = 0X07; //(6^)
            public const ushort KEY_7 = 0X08; //(7&)
            public const ushort KEY_8 = 0X09; //(8*)
            public const ushort KEY_9 = 0X0a; //(9()
            public const ushort KEY_0 = 0X0b; //(0))
            public const ushort KEY_MINUS = 0X0c; //(-_)
            public const ushort KEY_EQUAL = 0X0d; //(=+)
            public const ushort KEY_BACKSPACE = 0X0e; //(Backspace)
            public const ushort KEY_TAB = 0X0f; //(Tab)
            public const ushort KEY_Q = 0X10; //(Q)
            public const ushort KEY_W = 0X11; //(W)
            public const ushort KEY_E = 0X12; //(E)
            public const ushort KEY_R = 0X13; //(R)
            public const ushort KEY_T = 0X14; //(T)
            public const ushort KEY_Y = 0X15; //(Y)
            public const ushort KEY_U = 0X16; //(U)
            public const ushort KEY_I = 0X17; //(I)
            public const ushort KEY_O = 0X18; //(O)
            public const ushort KEY_P = 0X19; //(P)
            public const ushort KEY_L_SQUARE_BRACKET = 0X1a; //([{)
            public const ushort KEY_R_SQUARE_BRACKET = 0X1b; //(]})
            public const ushort KEY_ENTER = 0X1c; //(Enter)
            public const ushort KEY_L_CTRL = 0X1d; //(LCtrl)
            public const ushort KEY_A = 0X1e; //(A)
            public const ushort KEY_S = 0X1f; //(S)
            public const ushort KEY_D = 0X20; //(D)
            public const ushort KEY_F = 0X21; //(F)
            public const ushort KEY_G = 0X22; //(G)
            public const ushort KEY_H = 0X23; //(H)
            public const ushort KEY_J = 0X24; //(J)
            public const ushort KEY_K = 0X25; //(K)
            public const ushort KEY_L = 0X26; //(L)
            public const ushort KEY_SEMICOLON = 0X27; //(;:)
            public const ushort KEY_QUOTE = 0X28; //('")
            public const ushort KEY_APOSTROPH = 0X29; //(`~)
            public const ushort KEY_L_SHIFT = 0X2a; //(LShift)
            public const ushort KEY_BACK_SLASH = 0X2b; //(\|) on a 102-key keyboard
            public const ushort KEY_Z = 0X2c; //(Z)
            public const ushort KEY_X = 0X2d; //(X)
            public const ushort KEY_C = 0X2e; //(C)
            public const ushort KEY_V = 0X2f; //(V)
            public const ushort KEY_B = 0X30; //(B)
            public const ushort KEY_N = 0X31; //(N)
            public const ushort KEY_M = 0X32; //(M)
            public const ushort KEY_COMMA = 0X33; //(,<)
            public const ushort KEY_DOT = 0X34; //(.>)
            public const ushort KEY_SLASH = 0X35; //(/?)
            public const ushort KEY_R_SHIFT = 0X36; //(RShift)
            public const ushort KEY_PAD_ASTRISK = 0X37; //(Keypad-*) or(*/PrtScn) on a 83/84-key keyboard
            public const ushort KEY_L_ALT = 0X38; //(LAlt)
            public const ushort KEY_SPACE = 0X39; //(Space bar)
            public const ushort KEY_CAPS = 0X3a; //(CapsLock)
            public const ushort KEY_F1 = 0X3b; //(F1)
            public const ushort KEY_F2 = 0X3c; //(F2)
            public const ushort KEY_F3 = 0X3d; //(F3)
            public const ushort KEY_F4 = 0X3e; //(F4)
            public const ushort KEY_F5 = 0X3f; //(F5)
            public const ushort KEY_F6 = 0X40; //(F6)
            public const ushort KEY_F7 = 0X41; //(F7)
            public const ushort KEY_F8 = 0X42; //(F8)
            public const ushort KEY_F9 = 0X43; //(F9)
            public const ushort KEY_F10 = 0X44; //(F10)
            public const ushort KEY_NUMLOCK = 0X45; //(NumLock)
            public const ushort KEY_SCROLLLOCK = 0X46; //(ScrollLock)
            public const ushort KEY_PAD_7 = 0X47; //(Keypad-7/Home)
            public const ushort KEY_PAD_8 = 0X48; //(Keypad-8/Up)
            public const ushort KEY_PAD_9 = 0X49; //(Keypad-9/PgUp)
            public const ushort KEY_PAD_MINUS = 0X4a; //(Keypad--)
            public const ushort KEY_PAD_4 = 0X4b; //(Keypad-4/Left)
            public const ushort KEY_PAD_5 = 0X4c; //(Keypad-5)
            public const ushort KEY_PAD_6 = 0X4d; //(Keypad-6/Right)
            public const ushort KEY_PAD_PLUS = 0X4e; //(Keypad-+)
            public const ushort KEY_PAD_1 = 0X4f; //(Keypad-1/End)
            public const ushort KEY_PAD_2 = 0X50; //(Keypad-2/Down)
            public const ushort KEY_PAD_3 = 0X51; //(Keypad-3/PgDn)
            public const ushort KEY_PAD_0 = 0X52; //(Keypad-0/Ins)
            public const ushort KEY_PAD_DOT = 0X53; //(Keypad-./Del)
        }
        #endregion

        #region external methods
        [DllImport("user32.dll")]
        private static extern UInt32 SendInput(UInt32 nInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] Input[] pInputs, Int32 cbSize);

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();
        #endregion

        public static void SendKeyboardInput(KeyboardInput[] kbInputs)
        {
            Input[] inputs = new Input[kbInputs.Length];

            for (int i = 0; i < kbInputs.Length; i++)
            {
                inputs[i] = new Input
                {
                    type = (int)InputType.Keyboard,
                    u = new InputUnion
                    {
                        ki = kbInputs[i]
                    }
                };
            }

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        public static void ClickKey(ushort scanCode)
        {
            var inputs = new KeyboardInput[]
            {
                new KeyboardInput
                {
                    wScan = scanCode,
                    dwFlags = (uint)(KeyEventF.KeyDown | KeyEventF.Scancode),
                    dwExtraInfo = GetMessageExtraInfo()
                },
                new KeyboardInput
                {
                    wScan = scanCode,
                    dwFlags = (uint)(KeyEventF.KeyUp | KeyEventF.Scancode),
                    dwExtraInfo = GetMessageExtraInfo()
                }
            };
            SendKeyboardInput(inputs);
        }

        public static void SendMouseInput(MouseInput[] mInputs)
        {
            Input[] inputs = new Input[mInputs.Length];

            for (int i = 0; i < mInputs.Length; i++)
            {
                inputs[i] = new Input
                {
                    type = (int)InputType.Mouse,
                    u = new InputUnion
                    {
                        mi = mInputs[i]
                    }
                };
            }

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }
    }
}
