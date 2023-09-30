using ImGuiNET;
using ClickableTransparentOverlay;
using System.Numerics;
using Swed64;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Runtime.InteropServices;
using Vortice.Direct3D11;
using System;
using System.Net;
using ClickableTransparentOverlay.Win32;
using System.Text;
using System.Net.NetworkInformation;
using SixLabors.ImageSharp.Metadata;
using Veldrid.MetalBindings;
using Memory;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using NLua;
using KeraLua;




namespace IMGUITEST
{



    public class Program : Overlay
    {


        Swed Swed;
        IntPtr moduleBase;
        int Gernades = 0;
        int HelathAmmount = 1;
        int PlayerAmount = 0;
        int speedammount = 0;
        string text = "";



        // adress



        bool checkboxVaule1 = false;
        bool checkboxVaule2 = false;
        bool checkboxVaule3 = false;
        bool checkboxVaule4 = false;
        bool checkboxVaule5 = false;
        bool checkboxVaule6 = false;
        bool checkboxVaule8 = false;
        bool checkboxVaule9 = false;
        bool checkboxVaule10 = false;






        // Vector 4 selected colors address


        Vector4 SelcetCollor = new Vector4(1.000f, 0.000f, 0.000f, 2.000f);

        // Data Vaules

        // sets strings to window_name

        string Window_Name = "Ghost - Assualt Cube Trainer";



        String UserName = Environment.UserName;
        string message = "has opened the programe";

        bool showwindow = true;



        // DLLIMPORTS



        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vkey);







        Vector4 circleColor = new Vector4(0f, 1f, 0f, 1f); // Green

        // Define the size and position of the circle
        float circleSize = 100f;
        float circleX = ImGui.GetIO().DisplaySize.X / 2f - circleSize / 2f;
        float circleY = ImGui.GetIO().DisplaySize.Y / 2f - circleSize / 2f;










        protected override void Render()
        {
            

            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 10.0f);

            ImGui.PushStyleColor(ImGuiCol.CheckMark, new Vector4(1.0f, 1.0f, 1.0f, 1.0f)); // Set checkmark color to white
                                                                                           // ... ImGui code here ...
            ImGui.PopStyleColor(); // Reset checkmark color to default value


            // are target process


            // ModueBase Scripts
            // Executors

            
            Swed = new Swed("ac_client");
            moduleBase = Swed.GetModuleBase("ac_client.exe");

            Swed.WriteInt(moduleBase, 0x0003D590 + 0x30, 9);


            // ImGui Rendering box

            // imgui Style Rendering Boxes




            if (GetAsyncKeyState(0x2D) < 0)
            {
                showwindow = !showwindow;

                Thread.Sleep(200);
            }


            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Green, 5);
            int x = this.ClientSize.Width / 2 - 50;
            int y = this.ClientSize.Height / 2 - 50;
            g.DrawEllipse(pen, x, y, 100, 100);

            ImGui.SetNextWindowSize(new Vector2(500, 500));
            ImGui.SetNextWindowPos(new Vector2(0, 0));
            ImGui.Begin("Circle Window", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar);

            // Draw a green circle in the middle of the window
            Vector2 center = ImGui.GetWindowPos() + ImGui.GetWindowSize() / 2;
            float radius = 100;
            uint color = ImGui.ColorConvertFloat4ToU32(new Vector4(0, 1, 0, 1));
            ImDrawListPtr drawList = ImGui.GetWindowDrawList();
            drawList.AddCircle(center, radius, color);

            // End the window
            ImGui.End();



            if (showwindow)
            {
                ImGui.Begin(Window_Name);



                if (ImGui.BeginTabBar("My Tab Bar"))
                {
                    // Create the first tab
                    if (ImGui.BeginTabItem("Cheats"))
                    {



                        ImGui.Checkbox("Inf Amo", ref checkboxVaule1);
                        ImGui.Checkbox("Auto Shoot", ref checkboxVaule3);
                        ImGui.Checkbox("Disable Hold To Shoot", ref checkboxVaule4);
                        ImGui.Checkbox("Air Jump", ref checkboxVaule5);
                        ImGui.Checkbox("RapidFire", ref checkboxVaule6);
                        ImGui.Checkbox("SpingBot", ref checkboxVaule9);

                        ImGui.Text(" ");
                        ImGui.Text(" ");




                            ImGui.Text(" ");
                        ImGui.Text(" ");
                        ImGui.Checkbox("Enable Gernade Slider", ref checkboxVaule2);
                        ImGui.SliderInt("Gernades Ammount", ref Gernades, 0, 9999);


                        ImGui.Text(" ");

                        ImGui.Checkbox("Enable Health Slider", ref checkboxVaule8);
                        ImGui.SliderInt("Health Ammount", ref HelathAmmount, 1, 9999);
                        ImGui.Text(" ");


                        ImGui.Text(" ");

                        ImGui.Checkbox("Enable Speed Slider", ref checkboxVaule10);
                        ImGui.SliderInt("Speed Ammount", ref speedammount, 0, 100);
                        ImGui.Text(" ");

                        ImGui.EndTabItem();
                    }







                    if (ImGui.BeginTabItem("Lua Executor"))
                    {

                        ImGui.InputTextMultiline("##textbox", ref text, 1000, new Vector2(380, 250));


                        if (ImGui.Button("Execute Lua Script"))
                        {
                            string script = text;
                            using (NLua.Lua lua = new NLua.Lua())

                            lua.DoString(script);
                        }

                        ImGui.EndTabItem();

                    }






                    // Create the second tab
                    if (ImGui.BeginTabItem("Color Settings"))
                    {


                        ImGuiStylePtr style = ImGui.GetStyle();

                        style.WindowBorderSize = 2.0f;
                        style.Colors[(int)ImGuiCol.Border] = SelcetCollor;
                        style.Colors[(int)ImGuiCol.TitleBgActive] = SelcetCollor;
                        style.Colors[(int)ImGuiCol.FrameBg] = SelcetCollor;


                        ImGui.ColorPicker4("Color Selector", ref SelcetCollor);

                        ImGui.EndTabItem();
                    }

                    // End the tab bar
                    ImGui.EndTabBar();
                }
            }



            ImGui.NewFrame();

            ImGui.GetWindowDrawList().AddCircleFilled(new Vector2(circleX, circleY), circleSize / 2f, ImGui.GetColorU32(circleColor));







            // ImGui Cheats Bix









            if (checkboxVaule8 == true)
            {
                int HpAmmount = int.Parse(HelathAmmount.ToString());

                var HealthPlayer = Swed.ReadPointer(moduleBase, 0x0017E0A8);



                Swed.WriteBytes(HealthPlayer, 0xEC, BitConverter.GetBytes(HpAmmount));

            }

            if (checkboxVaule2 == true)
            {

                int newammount = int.Parse(Gernades.ToString());

                var localplayer = Swed.ReadPointer(moduleBase, 0x0017E0A8);

                Swed.WriteBytes(localplayer, 0x144, BitConverter.GetBytes(newammount));
            }

            if (checkboxVaule3 == true)
            {

                var shootadr = Swed.ReadPointer(moduleBase, 0x0017E0A8);

                Swed.WriteBytes(shootadr, 0x204, BitConverter.GetBytes(1701860097));
            }

            if (checkboxVaule6 == true)
            {
                Swed.Nop(moduleBase, 0xC73EA, 2); // sets the varible to do nothing

            }
            else
            {
                Swed.WriteBytes(moduleBase, 0xC73EA, "89 08"); // repairs varible
            }

            if (checkboxVaule4 == true)
            {

                var autrapid = Swed.ReadPointer(moduleBase, 0x0017E0A8);

                Swed.WriteBytes(autrapid, 0x204, BitConverter.GetBytes(1701860096));
            }

            if (checkboxVaule5 == true)
            {
                Swed.Nop(moduleBase, 0xC140E, 3);
            }
            else
            {
                Swed.WriteBytes(moduleBase, 0xC140E, "88 45 5D"); // repairs varible
            }

            if (checkboxVaule1 == true)
            {
                Swed.Nop(moduleBase, 0xC73EF, 2); // sets the varible to do nothing

            }
            else
            {
                Swed.WriteBytes(moduleBase, 0xC73EF, "FF 08"); // repairs varible
            }



            if (checkboxVaule9 == true)
            {


                var localcrosair = Swed.ReadPointer(moduleBase, 0x0017E0A8);

                Swed.WriteBytes(localcrosair, 0x38, BitConverter.GetBytes(3266576384));
                Swed.WriteBytes(localcrosair, 0x38, BitConverter.GetBytes(1119092736));
                Swed.WriteBytes(localcrosair, 0x38, BitConverter.GetBytes(3225801175));
            }


            if (checkboxVaule10 == true)
            {

                if (GetAsyncKeyState(0x57) < 0)
                {
                    int speedammounta = int.Parse(speedammount.ToString());

                    var speedplayer = Swed.ReadPointer(moduleBase, 0x0017E0A8);

                    Swed.WriteBytes(speedplayer, 0x74, BitConverter.GetBytes(speedammounta));
                }
                else
                {
                    var speedplayer = Swed.ReadPointer(moduleBase, 0x0017E0A8);

                    Swed.WriteBytes(speedplayer, 0x74, BitConverter.GetBytes(0));
                }

            }





        }


        static void Main(string[] args)
        {










            


            Console.WriteLine("Ghost Client Loading");
            Console.WriteLine("--------------------");
            Console.WriteLine("Injecting Menu");
            Console.WriteLine("Loading Packages");
            Console.WriteLine("Reading Dlls");
            Console.WriteLine("Executing Scripts And Dlls");
            Console.WriteLine("--------------------");




            Program program = new Program();
            program.Start().Wait();

            Console.WriteLine("-------------");
            Console.WriteLine("Menu Loaded");
        }
    }
}
