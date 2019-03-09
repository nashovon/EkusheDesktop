
using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace english
{

    
    public class BNGlobalKeyboardHook
    {
        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr hhk, int code, int wParam, ref keyBoardHookStruct lParam);
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LLKeyboardHook callback, IntPtr hInstance, uint theardID);
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        public delegate int LLKeyboardHook(int Code, int wParam, ref keyBoardHookStruct lParam);

        BNRefine refine = new BNRefine();
        int current=-1, previous=-1;
        bool block = true;

        public struct keyBoardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;
        const int WM_SYSKEYDOWN = 0x0104;
        const int WM_SYSKEYUP = 0x0105;

        LLKeyboardHook llkh;
        public List<Keys> HookedKeys = new List<Keys>();

        IntPtr Hook = IntPtr.Zero;

        // This is the Constructor. This is the code that runs every time you create a new GlobalKeyboardHook object
        public BNGlobalKeyboardHook()
        {
            llkh = new LLKeyboardHook(HookProc);
            // This starts the hook. You can leave this as comment and you have to start it manually (the thing I do in the tutorial, with the button)
            // Or delete the comment mark and your hook will start automatically when your program starts (because a new GlobalKeyboardHook object is created)
            // That's why there are duplicates, because you start it twice! I'm sorry, I haven't noticed this...
            // hook(); <-- Choose!
        }
        ~BNGlobalKeyboardHook()
        { unhook(); }

        public void hook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            Hook = SetWindowsHookEx(WH_KEYBOARD_LL, llkh, hInstance, 0);
        }

        public void unhook()
        {
            UnhookWindowsHookEx(Hook);
            //Debug.WriteLine("unhooked");
        }


        public int HookProc(int Code, int wParam, ref keyBoardHookStruct lParam)
        {
            if (Code >= 0)
            {
                


                // Debug.WriteLine(lParam.vkCode);

                if (block && (lParam.vkCode >= 65 && lParam.vkCode <= 90) || lParam.vkCode == 160 || (lParam.vkCode >= 96 && lParam.vkCode <= 105) )
                {
                    refine.converter(lParam.vkCode);
                    return 1;
                }
                else if(block && lParam.vkCode >= 48 && lParam.vkCode <= 57)
                {
                    block = false;
                    refine.converter(lParam.vkCode);
                    block = true;
                    return 1;
                }
                else if (block && lParam.vkCode == 32)
                {
                    block = false;
                    refine.press_space(BNRefine.output);
                    block = true;

                }

                else if (lParam.vkCode == 8)
                {
                    if (!refine.press_backspace()) return 1;
                }





            }

            return CallNextHookEx(Hook, Code, wParam, ref lParam);
        }
    }
}