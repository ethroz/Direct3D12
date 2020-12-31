using SharpDX;
using SharpDX.D3DCompiler;
using D12 = SharpDX.Direct3D12;
using SharpDX.DXGI;
using SharpDX.Mathematics;
using SharpDX.XInput;
using SharpDX.DirectInput;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Direct3D12
{
    sealed class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (Engine engine = new Engine())
            {
                engine.Run();
            }
        }
    }

    public class Engine : IDisposable
    {
        private RenderForm renderForm;
        private D12.Device device;
        private D12.CommandQueue commandQueue;
        private SwapChain swapChain;

        private float RefreshRate = 144; // set to 0 for uncapped
        public float elapsedTime;
        private long time1, time2;
        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        private int Width = 1280, Height = 720; // not reccomended to exceed display size
        public enum WindowState { Normal, Minimized, Maximized, FullScreen };
        private WindowState State = WindowState.Normal;

        public void OnStart()
        {

        }

        public void UserInput()
        {

        }

        public void OnUpdate()
        {

        }

        private void GetTime()
        {
            time2 = sw.ElapsedTicks;
            elapsedTime = (time2 - time1) / 10000000.0f;
            if (RefreshRate != 0)
            {
                while (1.0f / elapsedTime > RefreshRate)
                {
                    time2 = sw.ElapsedTicks;
                    elapsedTime = (time2 - time1) / 10000000.0f;
                }
            }
            time1 = time2;
            renderForm.Text = "Direct3D12   FPS: " + 1.0f / elapsedTime;
        }

        public Engine()
        {
            renderForm = new RenderForm("Direct3D12")
            {
                ClientSize = new Size(Width, Height),
                AllowUserResizing = true
            };
            if (State == WindowState.FullScreen)
            {
                renderForm.TopMost = true;
                renderForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                renderForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
            else if (State == WindowState.Maximized)
            {
                renderForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
            else if (State == WindowState.Minimized)
            {
                renderForm.TopMost = false;
                renderForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                renderForm.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            }

            sw.Start();
            InitializeMouse();
            InitializeKeyboard();
            InitializeDeviceResources();
            InitializeShaders();
            OnStart();
            time1 = sw.ElapsedTicks;
        }

        private void InitializeMouse()
        {

        }

        private void InitializeKeyboard()
        {

        }

        private void InitializeDeviceResources()
        {

        }

        private void InitializeShaders()
        {

        }

        public void Run()
        {
            RenderLoop.Run(renderForm, RenderCallBack);
        }

        private void RenderCallBack()
        {
            UserInput();
            OnUpdate();
            GetTime();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool boolean)
        {
            renderForm.Dispose();
        }
    }
}
