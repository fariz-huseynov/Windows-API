using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using WindowsApi;
using ImageProcessing;

namespace WindowsApiApp
{
    public partial class Form1 : Form
    {

        //https://www.pinvoke.net/

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;


        //bul
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //buyut kucult

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        //focus
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        ///     Changes the size, position, and Z order of a child, pop-up, or top-level window. These windows are ordered
        ///     according to their appearance on the screen. The topmost window receives the highest rank and is the first window
        ///     in the Z order.
        ///     <para>See https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545%28v=vs.85%29.aspx for more information.</para>
        /// </summary>
        /// <param name="hWnd">C++ ( hWnd [in]. Type: HWND )<br />A handle to the window.</param>
        /// <param name="hWndInsertAfter">
        ///     C++ ( hWndInsertAfter [in, optional]. Type: HWND )<br />A handle to the window to precede the positioned window in
        ///     the Z order. This parameter must be a window handle or one of the following values.
        ///     <list type="table">
        ///     <itemheader>
        ///         <term>HWND placement</term><description>Window to precede placement</description>
        ///     </itemheader>
        ///     <item>
        ///         <term>HWND_BOTTOM ((HWND)1)</term>
        ///         <description>
        ///         Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost
        ///         window, the window loses its topmost status and is placed at the bottom of all other windows.
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>HWND_NOTOPMOST ((HWND)-2)</term>
        ///         <description>
        ///         Places the window above all non-topmost windows (that is, behind all topmost windows). This
        ///         flag has no effect if the window is already a non-topmost window.
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>HWND_TOP ((HWND)0)</term><description>Places the window at the top of the Z order.</description>
        ///     </item>
        ///     <item>
        ///         <term>HWND_TOPMOST ((HWND)-1)</term>
        ///         <description>
        ///         Places the window above all non-topmost windows. The window maintains its topmost position
        ///         even when it is deactivated.
        ///         </description>
        ///     </item>
        ///     </list>
        ///     <para>For more information about how this parameter is used, see the following Remarks section.</para>
        /// </param>
        /// <param name="X">C++ ( X [in]. Type: int )<br />The new position of the left side of the window, in client coordinates.</param>
        /// <param name="Y">C++ ( Y [in]. Type: int )<br />The new position of the top of the window, in client coordinates.</param>
        /// <param name="cx">C++ ( cx [in]. Type: int )<br />The new width of the window, in pixels.</param>
        /// <param name="cy">C++ ( cy [in]. Type: int )<br />The new height of the window, in pixels.</param>
        /// <param name="uFlags">
        ///     C++ ( uFlags [in]. Type: UINT )<br />The window sizing and positioning flags. This parameter can be a combination
        ///     of the following values.
        ///     <list type="table">
        ///     <itemheader>
        ///         <term>HWND sizing and positioning flags</term>
        ///         <description>Where to place and size window. Can be a combination of any</description>
        ///     </itemheader>
        ///     <item>
        ///         <term>SWP_ASYNCWINDOWPOS (0x4000)</term>
        ///         <description>
        ///         If the calling thread and the thread that owns the window are attached to different input
        ///         queues, the system posts the request to the thread that owns the window. This prevents the calling
        ///         thread from blocking its execution while other threads process the request.
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_DEFERERASE (0x2000)</term>
        ///         <description>Prevents generation of the WM_SYNCPAINT message. </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_DRAWFRAME (0x0020)</term>
        ///         <description>Draws a frame (defined in the window's class description) around the window.</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_FRAMECHANGED (0x0020)</term>
        ///         <description>
        ///         Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message
        ///         to the window, even if the window's size is not being changed. If this flag is not specified,
        ///         WM_NCCALCSIZE is sent only when the window's size is being changed
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_HIDEWINDOW (0x0080)</term><description>Hides the window.</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOACTIVATE (0x0010)</term>
        ///         <description>
        ///         Does not activate the window. If this flag is not set, the window is activated and moved to
        ///         the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
        ///         parameter).
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOCOPYBITS (0x0100)</term>
        ///         <description>
        ///         Discards the entire contents of the client area. If this flag is not specified, the valid
        ///         contents of the client area are saved and copied back into the client area after the window is sized or
        ///         repositioned.
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOMOVE (0x0002)</term>
        ///         <description>Retains the current position (ignores X and Y parameters).</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOOWNERZORDER (0x0200)</term>
        ///         <description>Does not change the owner window's position in the Z order.</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOREDRAW (0x0008)</term>
        ///         <description>
        ///         Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies
        ///         to the client area, the nonclient area (including the title bar and scroll bars), and any part of the
        ///         parent window uncovered as a result of the window being moved. When this flag is set, the application
        ///         must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOREPOSITION (0x0200)</term><description>Same as the SWP_NOOWNERZORDER flag.</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOSENDCHANGING (0x0400)</term>
        ///         <description>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOSIZE (0x0001)</term>
        ///         <description>Retains the current size (ignores the cx and cy parameters).</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_NOZORDER (0x0004)</term>
        ///         <description>Retains the current Z order (ignores the hWndInsertAfter parameter).</description>
        ///     </item>
        ///     <item>
        ///         <term>SWP_SHOWWINDOW (0x0040)</term><description>Displays the window.</description>
        ///     </item>
        ///     </list>
        /// </param>
        /// <returns><c>true</c> or nonzero if the function succeeds, <c>false</c> or zero otherwise or if function fails.</returns>
        /// <remarks>
        ///     <para>
        ///     As part of the Vista re-architecture, all services were moved off the interactive desktop into Session 0.
        ///     hwnd and window manager operations are only effective inside a session and cross-session attempts to manipulate
        ///     the hwnd will fail. For more information, see The Windows Vista Developer Story: Application Compatibility
        ///     Cookbook.
        ///     </para>
        ///     <para>
        ///     If you have changed certain window data using SetWindowLong, you must call SetWindowPos for the changes to
        ///     take effect. Use the following combination for uFlags: SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER |
        ///     SWP_FRAMECHANGED.
        ///     </para>
        ///     <para>
        ///     A window can be made a topmost window either by setting the hWndInsertAfter parameter to HWND_TOPMOST and
        ///     ensuring that the SWP_NOZORDER flag is not set, or by setting a window's position in the Z order so that it is
        ///     above any existing topmost windows. When a non-topmost window is made topmost, its owned windows are also made
        ///     topmost. Its owners, however, are not changed.
        ///     </para>
        ///     <para>
        ///     If neither the SWP_NOACTIVATE nor SWP_NOZORDER flag is specified (that is, when the application requests that
        ///     a window be simultaneously activated and its position in the Z order changed), the value specified in
        ///     hWndInsertAfter is used only in the following circumstances.
        ///     </para>
        ///     <list type="bullet">
        ///     <item>Neither the HWND_TOPMOST nor HWND_NOTOPMOST flag is specified in hWndInsertAfter. </item>
        ///     <item>The window identified by hWnd is not the active window. </item>
        ///     </list>
        ///     <para>
        ///     An application cannot activate an inactive window without also bringing it to the top of the Z order.
        ///     Applications can change an activated window's position in the Z order without restrictions, or it can activate
        ///     a window and then move it to the top of the topmost or non-topmost windows.
        ///     </para>
        ///     <para>
        ///     If a topmost window is repositioned to the bottom (HWND_BOTTOM) of the Z order or after any non-topmost
        ///     window, it is no longer topmost. When a topmost window is made non-topmost, its owners and its owned windows
        ///     are also made non-topmost windows.
        ///     </para>
        ///     <para>
        ///     A non-topmost window can own a topmost window, but the reverse cannot occur. Any window (for example, a
        ///     dialog box) owned by a topmost window is itself made a topmost window, to ensure that all owned windows stay
        ///     above their owner.
        ///     </para>
        ///     <para>
        ///     If an application is not in the foreground, and should be in the foreground, it must call the
        ///     SetForegroundWindow function.
        ///     </para>
        ///     <para>
        ///     To use SetWindowPos to bring a window to the top, the process that owns the window must have
        ///     SetForegroundWindow permission.
        ///     </para>
        /// </remarks>

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);


        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        /// <summary>
        /// Window handles (HWND) used for hWndInsertAfter
        /// </summary>
        public static class HWND
        {
            public static IntPtr
            NoTopMost = new IntPtr(-2),
            TopMost = new IntPtr(-1),
            Top = new IntPtr(0),
            Bottom = new IntPtr(1);
        }

        /// <summary>
        /// SetWindowPos Flags
        /// </summary>
        public static class SWP
        {
            public static readonly int
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000;
        }

        [DllImport("user32.dll")]

        private static extern int GetWindowRect(IntPtr hwnd, out Rectangle rect);

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();


        /// <summary>
        ///     Special window handles
        /// </summary>
        public enum SpecialWindowHandles
        {
            // ReSharper disable InconsistentNaming
            /// <summary>
            ///     Places the window at the top of the Z order.
            /// </summary>
            HWND_TOP = 0,
            /// <summary>
            ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            /// </summary>
            HWND_BOTTOM = 1,
            /// <summary>
            ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
            /// </summary>
            HWND_TOPMOST = -1,
            /// <summary>
            ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
            /// </summary>
            HWND_NOTOPMOST = -2
            // ReSharper restore InconsistentNaming
        }
        [Flags]
        public enum SetWindowPosFlags : uint
        {
            // ReSharper disable InconsistentNaming

            /// <summary>
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            ///     Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            ///     Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            ///     Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            ///     Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            ///     Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            ///     Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            ///     Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            ///     Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,

            // ReSharper restore InconsistentNaming
        }
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            // retrieve Notepad main window handle
            IntPtr hWnd = FindWindow("Notepad", null);
            if (!hWnd.Equals(IntPtr.Zero)) {
                // SW_SHOWMAXIMIZED to maximize the window
                // SW_SHOWMINIMIZED to minimize the window
                // SW_SHOWNORMAL to make the window be normal size
                ShowWindowAsync(hWnd, SW_SHOWMAXIMIZED);
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            // retrieve Notepad main window handle
            IntPtr hWnd = FindWindow("Notepad", "Untitled - Notepad");
            if (!hWnd.Equals(IntPtr.Zero)) {
                // SW_SHOWMAXIMIZED to maximize the window
                // SW_SHOWMINIMIZED to minimize the window
                // SW_SHOWNORMAL to make the window be normal size
                ShowWindowAsync(hWnd, SW_SHOWMINIMIZED);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            // retrieve Notepad main window handle
            IntPtr hWnd = FindWindow("Notepad", "Untitled - Notepad");
            if (!hWnd.Equals(IntPtr.Zero)) {
                // SW_SHOWMAXIMIZED to maximize the window
                // SW_SHOWMINIMIZED to minimize the window
                // SW_SHOWNORMAL to make the window be normal size
                ShowWindowAsync(hWnd, SW_SHOWNORMAL);
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            // retrieve Notepad main window handle
            IntPtr hWnd = FindWindow("Notepad", "Untitled - Notepad");
            if (!hWnd.Equals(IntPtr.Zero)) {
                SetForegroundWindow(hWnd);
            }
        }

        private void button5_Click(object sender, EventArgs e) {
            IntPtr hWnd = FindWindow("Notepad", "Untitled - Notepad");
            if (!hWnd.Equals(IntPtr.Zero)) {
                MoveWindowToMonitor(hWnd, 1);
            }
        }


        // When I move a window to a different monitor it subtracts 16 from the Width and 38 from the Height, Not sure if this is on my system or others.
        public static void MoveWindowToMonitor(IntPtr windowHandler, int monitor) {
            Rectangle rec = new Rectangle();
            var windowRec = GetWindowRect(windowHandler, out rec);
            SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, Screen.AllScreens[monitor].WorkingArea.Left, Screen.AllScreens[monitor].WorkingArea.Top, rec.Size.Width, rec.Size.Height, SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_SHOWWINDOW);
        }



        [DllImport("kernel32.dll")]
        public static extern SafeWaitHandle CreateWaitableTimer(IntPtr lpTimerAttributes, bool bManualReset, string lpTimerName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWaitableTimer(SafeWaitHandle hTimer, [In] ref long pDueTime, int lPeriod, IntPtr pfnCompletionRoutine, IntPtr lpArgToCompletionRoutine, bool fResume);



        static void SetWaitForWakeUpTime() {
            DateTime utc = DateTime.Now.AddSeconds(10);
            long duetime = utc.ToFileTime();

            using (SafeWaitHandle handle = CreateWaitableTimer(IntPtr.Zero, true, "MyWaitabletimer")) {
                if (SetWaitableTimer(handle, ref duetime, 0, IntPtr.Zero, IntPtr.Zero, true)) {
                    using (EventWaitHandle wh = new EventWaitHandle(false, EventResetMode.AutoReset)) {
                        wh.SafeWaitHandle = handle;
                        wh.WaitOne();
                    }
                } else {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }

            // You could make it a recursive call here, setting it to 1 hours time or similar
            MessageBox.Show("Wake up call");
        }

        private void button6_Click(object sender, EventArgs e) {
            SetWaitForWakeUpTime();
        }

        const double ConcertPitch = 440.0;

        class Note
        {
            [DllImport("Kernel32.dll")]
            public static extern bool Beep(UInt32 frequency, UInt32 duration);

            public const int C = -888;
            public const int CSharp = -798;
            public const int DFlat = CSharp;
            public const int D = -696;
            public const int DSharp = -594;
            public const int EFlat = DSharp;
            public const int E = -498;
            public const int F = -390;
            public const int FSharp = -300;
            public const int GFlat = FSharp;
            public const int G = -192;
            public const int GSharp = -96;
            public const int AFlat = GSharp;
            public const int A = 0;
            public const int ASharp = 108;
            public const int BFlat = ASharp;
            public const int B = 204;

            public int Key { get; set; }
            public int Octave { get; set; }
            public uint Duration { get; set; }

            public Note(int key, int octave, uint duration) {
                this.Key = key;
                this.Octave = octave;
                this.Duration = duration;
            }

            public uint Frequency {
                get {
                    double factor = Math.Pow(2.0, 1.0 / 1200.0);
                    return ((uint)(ConcertPitch * Math.Pow(factor, this.Key + this.Octave * 1200.0)));
                }
            }

            public void Play() {
                Beep(this.Frequency, this.Duration);
            }
        }

        private void button7_Click(object sender, EventArgs e) {
            Note[] melody = new Note[] {
            new Note(Note.A,0, 500),
            new Note(Note.B,0, 500),
            new Note(Note.C,0, 500),
            new Note(Note.D,0, 500),
            new Note(Note.E,0, 500),
            new Note(Note.F,0, 500),
            new Note(Note.G,0, 500),
            new Note(Note.A,0, 500),
            new Note(Note.B,0, 500),
            new Note(Note.C,0, 500)
        };

            foreach (var note in melody) {
                note.Play();
            }
        }

        private void button8_Click(object sender, EventArgs e) {
            //enable USB storage...
            MessageBox.Show(UsbPort(true));
        }

        private void button9_Click(object sender, EventArgs e) {
            //disable USB storage...
            MessageBox.Show(UsbPort(true));
        }


        string UsbPort(bool isEnable) {
            try {
                if (isEnable) {

                    Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", 3, Microsoft.Win32.RegistryValueKind.DWord);
                    return "Usb Port Enabled";
                } else {

                    Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", 4, Microsoft.Win32.RegistryValueKind.DWord);
                    return "Usb Port Disabled";
                }

            } catch (Exception ex) {
                return ex.Message;
            }
        }

        [DllImport("version.dll")]
        static extern int GetFileVersionInfoSize(string fileName, [Out]IntPtr dummy);
        private void button10_Click(object sender, EventArgs e) {
            var dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                var result = GetFileVersionInfoSize(openFileDialog1.FileName, IntPtr.Zero);
                MessageBox.Show(result.ToString());
            }
        }

        [DllImport("winmm.dll", SetLastError = true)]
        static extern bool PlaySound(string pszSound, UIntPtr hmod, uint fdwSound);

        [DllImport("winmm.dll", SetLastError = true)]
        static extern bool PlaySound(byte[] pszSound, IntPtr hmod, SoundFlags fdwSound);

        [Flags]
        public enum SoundFlags
        {
            /// <summary>play synchronously (default)</summary>
            SND_SYNC = 0x0000,
            /// <summary>play asynchronously</summary>
            SND_ASYNC = 0x0001,
            /// <summary>silence (!default) if sound not found</summary>
            SND_NODEFAULT = 0x0002,
            /// <summary>pszSound points to a memory file</summary>
            SND_MEMORY = 0x0004,
            /// <summary>loop the sound until next sndPlaySound</summary>
            SND_LOOP = 0x0008,
            /// <summary>don't stop any currently playing sound</summary>
            SND_NOSTOP = 0x0010,
            /// <summary>Stop Playing Wave</summary>
            SND_PURGE = 0x40,
            /// <summary>The pszSound parameter is an application-specific alias in the registry. You can combine this flag with the SND_ALIAS or SND_ALIAS_ID flag to specify an application-defined sound alias.</summary>
            SND_APPLICATION = 0x80,
            /// <summary>don't wait if the driver is busy</summary>
            SND_NOWAIT = 0x00002000,
            /// <summary>name is a registry alias</summary>
            SND_ALIAS = 0x00010000,
            /// <summary>alias is a predefined id</summary>
            SND_ALIAS_ID = 0x00110000,
            /// <summary>name is file name</summary>
            SND_FILENAME = 0x00020000,
            /// <summary>name is resource name or atom</summary>
            SND_RESOURCE = 0x00040004
        }

        public static void Play(string strFileName) {
            PlaySound(strFileName, UIntPtr.Zero, (uint)(SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC));
        }

        public static void Play(byte[] waveData) //bad idea, see http://blogs.msdn.com/larryosterman/archive/2009/02/19/playsound-xxx-snd-memory-snd-async-is-almost-always-a-bad-idea.aspx
        {
            PlaySound(waveData, IntPtr.Zero, SoundFlags.SND_ASYNC | SoundFlags.SND_MEMORY);
        }
        private void button11_Click(object sender, EventArgs e) {
            var dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                UIntPtr ip = UIntPtr.Zero;
                bool result = PlaySound(openFileDialog1.FileName, ip, 0);

            }
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        private void button12_Click(object sender, EventArgs e) {
            IntPtr hWnd = FindWindow("chrome", null);
            const UInt32 WM_CLOSE = 0x0010;
            if (!hWnd.Equals(IntPtr.Zero)) {
                SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }

        }



        [DllImport("user32.dll", SetLastError = true)]
        static extern int MessageBoxTimeout(IntPtr hwnd, String text, String title, uint type, Int16 wLanguageId, Int32 milliseconds);

        private void button13_Click(object sender, EventArgs e) {
            MessageBoxTimeout(IntPtr.Zero, "text", "title", 1, 1, 0);
        }

        private void Form1_Load(object sender, EventArgs e) {
            IntPtr hWnd = Window.Get("windows api");
            if (!hWnd.Equals(IntPtr.Zero)) {
                MoveWindowToMonitor(hWnd, 1);
            }
        }

        private void button14_Click(object sender, EventArgs e) {
            Process[] chromeInstances = Process.GetProcessesByName("chrome");

            foreach (Process p in chromeInstances)
                p.Kill();
        }

        private void button15_Click(object sender, EventArgs e) {
            WindowsApi.Draw.String("Write to Desktop. Date: " + DateTime.Now.ToShortDateString(), 200, 200, Color.White, 20);
        }

        private void button16_Click(object sender, EventArgs e) {
            RotateDesktop.Run();
        }

        private void button17_Click(object sender, EventArgs e) {
            Desktop.HideTaskBar();
        }

        private void button18_Click(object sender, EventArgs e) {
            Desktop.ShowTaskBar();
        }

        private void button19_Click(object sender, EventArgs e) {
            Bitmap screenshot = Desktop.Screenshot();
            Mask mask = new Mask(screenshot);
            Desktop.HideTaskBar();

            for (int i = 0; i < 80; i++) {
                Mouse.Move(0, 0);
                screenshot = Filter.BlurFast(screenshot);
                mask.Picture.Image = screenshot;
                mask.Picture.Update();

                if (i % 5 == 0) // Perform garbage collection only occassionally as to not reduce performance.
                    GC.Collect();
            }

            for (int i = 0; i < 500; i++) {
                Mouse.Move(0, 0);
                System.Threading.Thread.Sleep(10);
            }
            mask.Close();
            Desktop.ShowTaskBar();

            Mouse.Move(15, Desktop.GetWidth() - 15);
        }

        private void button20_Click(object sender, EventArgs e) {
            OtherFunctions.ShakeMouse();
        }

        private void button21_Click(object sender, EventArgs e) {
            MouseSpam.Run();
        }

        private void button22_Click(object sender, EventArgs e) {
            MouseSpam.RunExtreme();
        }

        private void button23_Click(object sender, EventArgs e) {
            IntPtr hWnd = OtherFunctions.GetFocusedWindow();

            Window.Normalize(hWnd);
            Window.SetFocused(hWnd);
            System.Threading.Thread.Sleep(1000);

            Bitmap screenshot = Desktop.Screenshot();
            screenshot = Tools.Crop(screenshot, new Rectangle(
                Window.GetLocation(hWnd).X,
                Window.GetLocation(hWnd).Y,
                Window.GetSize(hWnd).Width,
                Window.GetSize(hWnd).Height
                ));

            Mask layer = new Mask(hWnd, screenshot);

            Window.Close(hWnd);

            System.Threading.Thread.Sleep(100);

            Bitmap resized = new Bitmap(screenshot);
            for (double i = 1; i > 0.1; i -= 0.02) {
                resized.Dispose();
                resized = Tools.Resize(
                    screenshot,
                    (int)(screenshot.Width * i),
                    (int)(screenshot.Height * i)
                    );
                layer.Picture.Image = resized;
                layer.Size = resized.Size;
                layer.Picture.Update();
                layer.Location = new Point(
                    layer.Location.X + (int)(screenshot.Width * 0.02 / 2),
                    layer.Location.Y + (int)(screenshot.Height * 0.02 / 2)
                    );
                System.Threading.Thread.Sleep(10);
            }

            //    Application.Run();
        }

        private void button24_Click(object sender, EventArgs e) {
            HueShifter.Run();
        }

        private void button25_Click(object sender, EventArgs e) {
            IntPtr hWnd = OtherFunctions.GetFocusedWindow();

            Window.Normalize(hWnd);
            Window.SetFocused(hWnd);
            System.Threading.Thread.Sleep(1000);

            Bitmap screenshot = Desktop.Screenshot();
            screenshot = Tools.Crop(screenshot, new Rectangle(
                Window.GetLocation(hWnd).X,
                Window.GetLocation(hWnd).Y,
                Window.GetSize(hWnd).Width,
                Window.GetSize(hWnd).Height
                ));

            Mask layer = new Mask(hWnd, screenshot);

            // The actual blurring.
            for (int i = 0; i < 10; i++) {
                screenshot = Filter.BlurFast(screenshot);
                System.Threading.Thread.Sleep(100);
                layer.Picture.Image = screenshot;
                layer.Picture.Update();
            }

            Window.Close(hWnd);
            // System.Threading.Thread.Sleep(100);
            //  Application.Run();
        }

        private void button26_Click(object sender, EventArgs e) {
            IntPtr hWnd = OtherFunctions.GetFocusedWindow();

            Window.Normalize(hWnd);
            Window.SetFocused(hWnd);
            System.Threading.Thread.Sleep(1000);

            Bitmap screenshot = Desktop.Screenshot();
            screenshot = Tools.Crop(screenshot, new Rectangle(
                Window.GetLocation(hWnd).X,
                Window.GetLocation(hWnd).Y,
                Window.GetSize(hWnd).Width,
                Window.GetSize(hWnd).Height
                ));

            screenshot = Effect.Invert(screenshot);
            Mask layer = new Mask(hWnd, screenshot);
            Window.Close(hWnd);
            System.Threading.Thread.Sleep(100);
            screenshot.Dispose();
            //  Application.Run();
        }

        private void button27_Click(object sender, EventArgs e) {
            Wavy.Run();
        }

        private void button28_Click(object sender, EventArgs e) {
            Scrambler.Run();
        }

        private void button29_Click(object sender, EventArgs e) {
            int thresholds = 1;
            if (thresholds < 1 || thresholds > 255)
                throw new Exception("Threshold must be 1-255.");

            IntPtr hWnd = OtherFunctions.GetFocusedWindow();

            System.Threading.Thread.Sleep(100);
            Window.SetFocused(hWnd);
            System.Threading.Thread.Sleep(100);
            Bitmap screenshot = Window.Screenshot(hWnd);

            if (thresholds <= 1)
                screenshot = Effect.Threshold(screenshot);
            else {
                int[] array = new int[thresholds];
                int count = 0;
                for (int j = 0; j < array.Length; j++) {
                    count += (255 / thresholds);
                    array[j] = count;
                }
                screenshot = Effect.Threshold(screenshot, array);
            }

            Mask mask = new Mask(hWnd, screenshot);
            mask.TransparencyKey = Color.White;
            System.Threading.Thread.Sleep(100);
            Window.Close(hWnd);
            //   Application.Run();
        }

        private void button30_Click(object sender, EventArgs e) {
            OtherFunctions.WindowShaker();
        }

        private void button31_Click(object sender, EventArgs e) {
            OtherFunctions.WindowShakerExtreme();
        }

        private void button32_Click(object sender, EventArgs e) {
            OtherFunctions.SetTitle();
        }

        private void button33_Click(object sender, EventArgs e) {
            OtherFunctions.ResizeBorders();
        }

        private void button34_Click(object sender, EventArgs e) {
            OtherFunctions.MouseTransparency();

        }

        private void button35_Click(object sender, EventArgs e) {
            OtherFunctions.Hide();
        }

        private void button36_Click(object sender, EventArgs e) {
            OtherFunctions.Show();
        }

        private void button37_Click(object sender, EventArgs e) {
            OtherFunctions.RemoveMenu();
        }

        private void button38_Click(object sender, EventArgs e) {
            OtherFunctions.FlipLeft();
        }

        private void button39_Click(object sender, EventArgs e) {
            OtherFunctions.FlipRight();
        }

        private void button40_Click(object sender, EventArgs e) {
            OtherFunctions.DisableClose();
        }

        private void button41_Click(object sender, EventArgs e) {
            OtherFunctions.DisableMaximize();
        }

        private void button42_Click(object sender, EventArgs e) {
            OtherFunctions.DisableMinimize();
        }

        private void button43_Click(object sender, EventArgs e) {
            Painter.Run();
        }

        private void button44_Click(object sender, EventArgs e) {
            MotionDetector.Run(30, MotionDetector.Quality.High);
        }

        private void button45_Click(object sender, EventArgs e) {
            RippleEffect.Run();
        }

        private void button46_Click(object sender, EventArgs e) {
            DesktopArt.Run();
        }



        private void button47_Click(object sender, EventArgs e) {
            Window.LockWorkStation();
        }

        private void button48_Click(object sender, EventArgs e) {
            Window.Hibernate();
        }

        private void button49_Click(object sender, EventArgs e) {
            Window.Sleep();
        }

        private void button50_Click(object sender, EventArgs e) {
            Process.Start("shutdown", "/s /t 0");
            // starts the shutdown application 
            // the argument /s is to shut down the computer
            // the argument /t 0 is to tell the process that 
            // the specified operation needs to be completed 
            // after 0 seconds
            //https://www.codeproject.com/Tips/480049/Shut-Down-Restart-Log-off-Lock-Hibernate-or-Sleep
        }

        private void button51_Click(object sender, EventArgs e) {
            try {
                var res = Window.Get("Calculator");
                Window.SetFocused(res);
                //  SendKeys.SendWait("^{a}{BACKSPACE}"); //select all and delete
                SendKeys.SendWait("{C}");
                SendKeys.SendWait("5{+}8");
                SendKeys.SendWait("{ENTER}");// or  SendKeys.SendWait("{=}");

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
         
    }
}
