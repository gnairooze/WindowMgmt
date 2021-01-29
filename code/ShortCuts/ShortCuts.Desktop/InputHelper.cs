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
            public const ushort KEY_ESC = 0x01; //(Esc)
            public const ushort KEY_1 = 02; //(1!)
            public const ushort KEY_2 = 03; //(2@)
            public const ushort KEY_3 = 0x04; //(3#)
            public const ushort KEY_4 = 0x05; //(4$)
            public const ushort KEY_5 = 0x06; //(5%E)
            public const ushort KEY_6 = 0x07; //(6^)
            public const ushort KEY_7 = 0x08; //(7&)
            public const ushort KEY_8 = 0x09; //(8*)
            public const ushort KEY_9 = 0x0a; //(9()
            public const ushort KEY_0 = 0x0b; //(0))
            public const ushort KEY_MINUS = 0x0c; //(-_)
            public const ushort KEY_EQUAL = 0x0d; //(=+)
            public const ushort KEY_BACKSPACE = 0x0e; //(Backspace)
            public const ushort KEY_TAB = 0x0f; //(Tab)
            public const ushort KEY_Q = 0x10; //(Q)
            public const ushort KEY_W = 0x11; //(W)
            public const ushort KEY_E = 0x12; //(E)
            public const ushort KEY_R = 0x13; //(R)
            public const ushort KEY_T = 0x14; //(T)
            public const ushort KEY_Y = 0x15; //(Y)
            public const ushort KEY_U = 0x16; //(U)
            public const ushort KEY_I = 0x17; //(I)
            public const ushort KEY_O = 0x18; //(O)
            public const ushort KEY_P = 0x19; //(P)
            public const ushort KEY_L_SQUARE_BRACKET = 0x1a; //([{)
            public const ushort KEY_R_SQUARE_BRACKET = 0x1b; //(]})
            public const ushort KEY_ENTER = 0x1c; //(Enter)
            public const ushort KEY_L_CTRL = 0x1d; //(LCtrl)
            public const ushort KEY_A = 0x1e; //(A)
            public const ushort KEY_S = 0x1f; //(S)
            public const ushort KEY_D = 0x20; //(D)
            public const ushort KEY_F = 0x21; //(F)
            public const ushort KEY_G = 0x22; //(G)
            public const ushort KEY_H = 0x23; //(H)
            public const ushort KEY_J = 0x24; //(J)
            public const ushort KEY_K = 0x25; //(K)
            public const ushort KEY_L = 0x26; //(L)
            public const ushort KEY_SEMICOLON = 0x27; //(;:)
            public const ushort KEY_QUOTE = 0x28; //('")
            public const ushort KEY_APOSTROPH = 0x29; //(`~)
            public const ushort KEY_L_SHIFT = 0x2a; //(LShift)
            public const ushort KEY_BACK_SLASH = 0x2b; //(\|) on a 102-key keyboard
            public const ushort KEY_Z = 0x2c; //(Z)
            public const ushort KEY_X = 0x2d; //(X)
            public const ushort KEY_C = 0x2e; //(C)
            public const ushort KEY_V = 0x2f; //(V)
            public const ushort KEY_B = 0x30; //(B)
            public const ushort KEY_N = 0x31; //(N)
            public const ushort KEY_M = 0x32; //(M)
            public const ushort KEY_COMMA = 0x33; //(,<)
            public const ushort KEY_DOT = 0x34; //(.>)
            public const ushort KEY_SLASH = 0x35; //(/?)
            public const ushort KEY_R_SHIFT = 0x36; //(RShift)
            public const ushort KEY_PAD_ASTRISK = 0x37; //(Keypad-*) or(*/PrtScn) on a 83/84-key keyboard
            public const ushort KEY_L_ALT = 0x38; //(LAlt)
            public const ushort KEY_SPACE = 0x39; //(Space bar)
            public const ushort KEY_CAPS = 0x3a; //(CapsLock)
            public const ushort KEY_F1 = 0x3b; //(F1)
            public const ushort KEY_F2 = 0x3c; //(F2)
            public const ushort KEY_F3 = 0x3d; //(F3)
            public const ushort KEY_F4 = 0x3e; //(F4)
            public const ushort KEY_F5 = 0x3f; //(F5)
            public const ushort KEY_F6 = 0x40; //(F6)
            public const ushort KEY_F7 = 0x41; //(F7)
            public const ushort KEY_F8 = 0x42; //(F8)
            public const ushort KEY_F9 = 0x43; //(F9)
            public const ushort KEY_F10 = 0x44; //(F10)
            public const ushort KEY_NUMLOCK = 0x45; //(NumLock)
            public const ushort KEY_SCROLLLOCK = 0x46; //(ScrollLock)
            public const ushort KEY_PAD_7 = 0x47; //(Keypad-7/Home)
            public const ushort KEY_PAD_8 = 0x48; //(Keypad-8/Up)
            public const ushort KEY_PAD_9 = 0x49; //(Keypad-9/PgUp)
            public const ushort KEY_PAD_MINUS = 0x4a; //(Keypad--)
            public const ushort KEY_PAD_4 = 0x4b; //(Keypad-4/Left)
            public const ushort KEY_PAD_5 = 0x4c; //(Keypad-5)
            public const ushort KEY_PAD_6 = 0x4d; //(Keypad-6/Right)
            public const ushort KEY_PAD_PLUS = 0x4e; //(Keypad-+)
            public const ushort KEY_PAD_1 = 0x4f; //(Keypad-1/End)
            public const ushort KEY_PAD_2 = 0x50; //(Keypad-2/Down)
            public const ushort KEY_PAD_3 = 0x51; //(Keypad-3/PgDn)
            public const ushort KEY_PAD_0 = 0x52; //(Keypad-0/Ins)
            public const ushort KEY_PAD_DOT = 0x53; //(Keypad-./Del)
            public const ushort KEY_F11 = 0x57; //(F11)
            public const ushort KEY_F12 = 0x58; //(F12) both on a 101+ key keyboard
            public const ushort KEY_COMP1 = 0xe0; //escape code to be combined with scancode
            public const ushort KEY_COMP2 = 0xe1; //escape code to be combined with scancode used in pause break
            /*
            escape scancodes
                e0 1c (Keypad Enter)
                e0 1d (RCtrl)
                e0 2a (fake LShift)
                e0 35 (Keypad-/)
                e0 36 (fake RShift)
                e0 37 (Ctrl-PrtScn)
                e0 38 (RAlt)
                e0 46 (Ctrl-Break)
                e0 47 (Grey Home)
                e0 48 (Grey Up)
                e0 49 (Grey PgUp)
                e0 4b (Grey Left)
                e0 4d (Grey Right)
                e0 4f (Grey End)
                e0 50 (Grey Down)
                e0 51 (Grey PgDn)
                e0 52 (Grey Insert)
                e0 53 (Grey Delete)
                e0 5b (LeftWindow)
                e0 5c (RightWindow)
                e0 5d (Menu)
            */
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
