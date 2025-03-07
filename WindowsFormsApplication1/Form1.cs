using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using AutoIt;
using EasyModbus;
using Skype4Sharp;
using Skype4Sharp.Auth;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        ModbusClient ModClient;
        Skype4Sharp.Skype4Sharp mainSkype;
        SkypeCredentials authCreds;
        static string triggerString = "!";


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
                    System.Windows.Forms.SendKeys.SendWait("{DEL}"); // Xóa văn bản đã chọn
                }

                // Gửi tin nhắn
                System.Windows.Forms.SendKeys.SendWait(message);
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            }
            else
            {
                Console.WriteLine("Không thể tìm thấy cửa sổ Skype.");
            }
        }


        public Form1()
        {
            InitializeComponent();

            GetSettingPath();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnConnect_Click(sender, e);

            //timer1.Interval = 216000;
            //timer1.Start();

            //timerMail.Interval = 864000;
            //timerMail.Start();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ModClient = new ModbusClient(ComPort);
            ModClient.Baudrate = int.Parse(cboBaudrate.Text);
            ModClient.UnitIdentifier = 14;

            if (cboParity.Text == "None")
            {
                ModClient.Parity = System.IO.Ports.Parity.None;
            }
            else if (cboParity.Text == "Even")
            {
                ModClient.Parity = System.IO.Ports.Parity.Even;
            }
            else if (cboParity.Text == "Odd")
            {
                ModClient.Parity = System.IO.Ports.Parity.Odd;
            }

            try
            {
                ModClient.Connect();
                lblStatus.Text = "Connected";
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                grpRW.Enabled = true;

                btnReadHolding_Click(sender,e);
                btnReadAnalog_Click(sender, e);

                timer1.Interval = 3600000;
                timer1.Start();

                timerMail.Interval = 14400000;
                timerMail.Start();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error! " + ex.ToString();

                timer1.Interval = 3600000;
                timer1.Start();

                timerMail.Interval = 14400000;
                timerMail.Start();
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            ModClient.Disconnect();
            lblStatus.Text = "Disconnected";
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            grpRW.Enabled = false;

            timer1.Stop();
        }

        string Temp = "";
        string Humi = "";

        private void btnReadHolding_Click(object sender, EventArgs e)
        {
            try
            {
                int[] vals;

                vals = ModClient.ReadHoldingRegisters(int.Parse(txtReg.Text), 1);

                vals = ModClient.ReadInputRegisters(int.Parse(txtReg.Text), 1);
                txtValue.Text = vals[0].ToString().Substring(0, 2) + "." + vals[0].ToString().Substring(2, 2);
                Temp = vals[0].ToString().Substring(0, 2) + "." + vals[0].ToString().Substring(2, 2);
            }
            catch (Exception)
            {
                Temp = "Read Error!";
            }
        }
        private void btnReadAnalog_Click(object sender, EventArgs e)
        {
            try
            {
                int[] vals;
                vals = ModClient.ReadInputRegisters(int.Parse(txtReg1.Text), 1);
                txtValue2.Text = vals[0].ToString().Substring(0, 2) + "." + vals[0].ToString().Substring(2, 2);
                Humi = vals[0].ToString().Substring(0, 2) + "." + vals[0].ToString().Substring(2, 2);
            }
            catch(Exception)
            {
                Humi = "Read Error!";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ModClient.Connect();

                btnReadHolding_Click(sender, e);
                btnReadAnalog_Click(sender, e);

                SendMessageToSkype(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " | " + "Temperature: " + Temp +" - Humidity: " + Humi);
            }
            catch 
            {
                Temp = "Read Error!";
                Humi = "Read Error!";
                SendMessageToSkype(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " | " + "Temperature: " + Temp + " - Humidity: " + Humi);
                return; 
            }
        }

        private void timerMail_Tick(object sender, EventArgs e)
        {
            try
            {
                ModClient.Connect();

                btnReadHolding_Click(sender, e);
                btnReadAnalog_Click(sender, e);
                SendMail(Temp, Humi);
            }
            catch (Exception)
            {
                Temp = "Read Error!";
                Humi = "Read Error!";
                SendMail(Temp, Humi);
                return;
            }
        }

        private string ComPort = "";
        string intervalSend = "";


        public void GetSettingPath()
        {
            try
            {
                var data = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Setting\\Setting.txt");
                if (data.Count() != 0)
                {
                    ComPort = data[1];
                    intervalSend = data[2];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check Setting file!" + ex);
                return;
            }
        }

        public void SendMail(string temp, string humi)
        {
            try
            {
                // Tạo đối tượng Outlook Application
                Outlook.Application outlookApp = new Outlook.Application();

                // Tạo đối tượng MailItem (đối tượng thư mới)
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                // Thiết lập thông tin thư
                mailItem.Subject = "THÔNG BÁO: KIỂM SOÁT NHIỆT ĐỘ - ĐỘ ẨM";
                mailItem.Body = "Thời gian kiểm tra: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


                // Tạo bảng HTML trong nội dung email
                string tableHtml = "<table border='1'>" +
                    "<tr>" +
                    "<th style=\"padding: 10px; text-align: center; vertical-align: middle; border: 1px solid #ddd;\"> Nhiệt độ </th>" +
                    "<th style=\"padding: 10px; text-align: center; vertical-align: middle; border: 1px solid #ddd;\"> Độ ẩm </th>" +
                    "</tr>";

                tableHtml += $"<tr>" +
                    $"<td style=\"padding: 10px; text-align: center; vertical-align: middle; border: 1px solid #ddd;\">{temp}</td>" +
                    $"<td style=\"padding: 10px; text-align: center; vertical-align: middle; border: 1px solid #ddd;\">{humi}</td>" +
                    $"</tr>";
                tableHtml += "</table>";

                // Thêm bảng vào nội dung email
                mailItem.HTMLBody = $"{mailItem.HTMLBody} {tableHtml}";

                // Lấy tên email đang sử dụng
                Outlook.NameSpace outlookNamespace = outlookApp.GetNamespace("MAPI");
                string currentUserEmail = outlookNamespace.CurrentUser.Address;

                // Thêm người nhận
                //mailItem.To = "thang.nguyen@nti-nanofilm.com";
                try
                {
                    var data = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Setting\\Mail.txt");

                    for (int i = 0; i < data.Count(); i++)
                    {
                        //mailItem.To += data[i];

                        mailItem.Recipients.Add(data[i]);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Check Setting file!" + ex);
                    return;
                }

                // Gửi thư
                mailItem.Send();

                // Đóng ứng dụng Outlook
                System.Runtime.InteropServices.Marshal.ReleaseComObject(outlookApp);

                Console.WriteLine("Email sent successfully!");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
