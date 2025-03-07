using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frmSendSky : Form
    {
        public frmSendSky()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        // Dùng GetWindowText để lấy tiêu đề của cửa sổ
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        // Hàm này để lấy độ dài tiêu đề của cửa sổ
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        // Hàm này để đưa cửa sổ lên trước
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        static IntPtr FindInputBox(IntPtr parentWindow)
        {
            // Sử dụng UI Automation để tìm ô nhập tin nhắn trong Skype
            AutomationElement skypeElement = AutomationElement.FromHandle(parentWindow);
            PropertyCondition condition = new PropertyCondition(
                AutomationElement.AutomationIdProperty, "ChatInputBox");

            AutomationElement inputBoxElement = skypeElement.FindFirst(
                TreeScope.Descendants, condition);

            // Kiểm tra xem có tìm thấy ô nhập tin nhắn không
            if (inputBoxElement != null)
            {
                // Lấy thông tin Handle của ô nhập tin nhắn
                int nativeWindowHandle = inputBoxElement.Current.NativeWindowHandle;
                IntPtr handle = new IntPtr(nativeWindowHandle);
                return handle;
            }
            else
            {
                // Nếu không tìm thấy, trả về IntPtr.Zero
                return IntPtr.Zero;
            }
        }

        static void SendMessageToSkype(string message)
        {
            IntPtr skypeWindow = IntPtr.Zero;

            // Liệt kê qua tất cả cửa sổ và kiểm tra nếu có cửa sổ Skype
            EnumWindows((hWnd, lParam) =>
            {
                int length = GetWindowTextLength(hWnd);
                if (length > 0)
                {
                    System.Text.StringBuilder windowText = new System.Text.StringBuilder(length + 1);
                    GetWindowText(hWnd, windowText, windowText.Capacity);

                    if (windowText.ToString().Contains("Skype"))
                    {
                        skypeWindow = hWnd;
                        return false; // Dừng việc liệt kê nếu tìm thấy Skype
                    }
                }
                return true;
            }, IntPtr.Zero);

            // Kiểm tra xem có tìm thấy cửa sổ Skype không
            if (skypeWindow != IntPtr.Zero)
            {
                // Kích hoạt cửa sổ Skype
                SetForegroundWindow(skypeWindow);

                IntPtr inputBox = FindInputBox(skypeWindow); // Hàm tìm kiếm ô nhập tin nhắn
                if (inputBox != IntPtr.Zero)
                {
                    SetForegroundWindow(inputBox);
                    System.Windows.Forms.SendKeys.SendWait("^(a)"); // Chọn tất cả văn bản (Ctrl+A)
                    Thread.Sleep(500);
                    System.Windows.Forms.SendKeys.SendWait("{DEL}"); // Xóa văn bản đã chọn
                }

                // Gửi tin nhắn
                System.Windows.Forms.SendKeys.SendWait(message);
                Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            }
            else
            {
                Console.WriteLine("Không thể tìm thấy cửa sổ Skype.");
            }
        }

        private void frmSendSky_Load(object sender, EventArgs e)
        {

        }

        string Temp = "", Humi = "";

        private void btnStart_Click(object sender, EventArgs e)
        {
            string result = ReadTempHumi();
            if (result != null)
            {
                SendMessageToSkype(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " | " + "Temperature: " + result.Split('|')[0] + " - Humidity: " + result.Split('|')[1]);
            }

            timer1.Interval = Convert.ToInt32(numUDTime.Value) * 60 * 1000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string result = ReadTempHumi();
            if (result != null)
            {
                SendMessageToSkype(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " | " + "Temperature: " + result.Split('|')[0] + " - Humidity: " + result.Split('|')[1]);
            }
        }

        private string ReadTempHumi()
        {
            DateTime dtNow = DateTime.Now;
            string fileName = "Plasma_02_" + dtNow.ToString("yyyy") + dtNow.ToString("MM") + dtNow.ToString("dd") + ".csv";
            string networkFilePath = @"\\172.17.110.250\LEAP Full Traceability\Temp Logfile\MLA\Plasma\" + dtNow.ToString("yyyy") + dtNow.ToString("MM") + "\\" + fileName;
            string localFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"LogTempHumi"; // Replace with the path to your application's Log folder

            string localFilePath = Path.Combine(localFolderPath, fileName);

            try
            {
                // Ensure the local directory exists
                if (!Directory.Exists(localFolderPath))
                {
                    Directory.CreateDirectory(localFolderPath);
                }

                // Check if the file already exists in the local folder
                if (File.Exists(localFilePath))
                {
                    // Delete the existing file
                    File.Delete(localFilePath);
                }

                // Copy the new file from the network location to the local folder
                File.Copy(networkFilePath, localFilePath);

                return ReadFileCSV(localFilePath);

                //Console.WriteLine("File copied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private string ReadFileCSV(string localFilePath)
        {
            using (var fileStream = new FileStream(localFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string lastLine = null;
                string line;

                // Read lines until the end
                while ((line = reader.ReadLine()) != null)
                {
                    lastLine = line;
                }

                string Temp = lastLine.Split(',')[13];
                string Humi = lastLine.Split(',')[14];

                return Temp + "|" + Humi;
            }


            try
            {
                // Check if the file exists before trying to read it
                if (File.Exists(localFilePath))
                {
                    // Open the file for reading
                    using (StreamReader reader = new StreamReader(localFilePath))
                    {
                        string line;

                        // Read the file line by line
                        while ((line = reader.ReadLine()) != null)
                        {
                            // Split the line by commas
                            string[] values = line.Split(',');

                            // Process each value (for demonstration, just print it)
                            foreach (string value in values)
                            {
                                Console.Write(value + " ");
                            }
                            Console.WriteLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The file does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
