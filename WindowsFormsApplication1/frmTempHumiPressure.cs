using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyModbus;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class frmTempHumiPressure : Form
    {
        public frmTempHumiPressure()
        {
            InitializeComponent();

            GetSettingPath();
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

        private void frmTempHumiPressure_Load(object sender, EventArgs e)
        {
            //ReadSensorAsyn(); // Gọi hàm bắt đầu cập nhật

            //StartThreads();

            //RunThread();

            //StartModbusTaskTimeOut();

            // StartModbusTask();

            // Khởi tạo ModbusClient và kết nối
            ModClient = new ModbusClient(ComPort);
            ModClient.Baudrate = 9600;
            ModClient.Parity = System.IO.Ports.Parity.None;
            ModClient.Connect();  // Kết nối với thiết bị Modbus

            StartUpdatingLabel();
        }

        private int counter = 0; // Đếm số giây đã trôi qua
        // Hàm bắt đầu cập nhật giá trị label
        private async void StartUpdatingLabel()
        {
            while (true)  // Lặp vô hạn
            {
                // Cập nhật giá trị label sau mỗi giây
                await Task.Delay(1000);  // Đợi 1 giây mà không làm treo UI thread
                ModClient.UnitIdentifier = 1;
                int[] temp1 = ModClient.ReadInputRegisters(0, 1);
                int[] humi1 = ModClient.ReadInputRegisters(1, 1);
                lbTemp1.Text = temp1[0].ToString().Substring(0, 2) + "." + temp1[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi1.Text = humi1[0].ToString().Substring(0, 2) + "." + humi1[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 2;
                int[] temp2 = ModClient.ReadInputRegisters(0, 1);
                int[] humi2 = ModClient.ReadInputRegisters(1, 1);
                lbTemp2.Text = temp2[0].ToString().Substring(0, 2) + "." + temp2[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi2.Text = humi2[0].ToString().Substring(0, 2) + "." + humi2[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 3;
                int[] temp3 = ModClient.ReadInputRegisters(0, 1);
                int[] humi3 = ModClient.ReadInputRegisters(1, 1);
                lbTemp3.Text = temp3[0].ToString().Substring(0, 2) + "." + temp3[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi3.Text = humi3[0].ToString().Substring(0, 2) + "." + humi3[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 4;
                int[] temp4 = ModClient.ReadInputRegisters(0, 1);
                int[] humi4 = ModClient.ReadInputRegisters(1, 1);
                lbTemp4.Text = temp4[0].ToString().Substring(0, 2) + "." + temp4[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi4.Text = humi4[0].ToString().Substring(0, 2) + "." + humi4[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 5;
                int[] temp5 = ModClient.ReadInputRegisters(0, 1);
                int[] humi5 = ModClient.ReadInputRegisters(1, 1);
                lbTemp5.Text = temp5[0].ToString().Substring(0, 2) + "." + temp5[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi5.Text = humi5[0].ToString().Substring(0, 2) + "." + humi5[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 6;
                int[] temp6 = ModClient.ReadInputRegisters(0, 1);
                int[] humi6 = ModClient.ReadInputRegisters(1, 1);
                lbTemp6.Text = temp6[0].ToString().Substring(0, 2) + "." + temp6[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi6.Text = humi6[0].ToString().Substring(0, 2) + "." + humi6[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 7;
                int[] temp7 = ModClient.ReadInputRegisters(0, 1);
                int[] humi7 = ModClient.ReadInputRegisters(1, 1);
                lbTemp7.Text = temp7[0].ToString().Substring(0, 2) + "." + temp7[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi7.Text = humi7[0].ToString().Substring(0, 2) + "." + humi7[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 8;
                int[] temp8 = ModClient.ReadInputRegisters(0, 1);
                int[] humi8 = ModClient.ReadInputRegisters(1, 1);
                lbTemp8.Text = temp8[0].ToString().Substring(0, 2) + "." + temp8[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi8.Text = humi8[0].ToString().Substring(0, 2) + "." + humi8[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 9;
                int[] temp9 = ModClient.ReadInputRegisters(0, 1);
                int[] humi9 = ModClient.ReadInputRegisters(1, 1);
                lbTemp9.Text = temp9[0].ToString().Substring(0, 2) + "." + temp9[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi9.Text = humi9[0].ToString().Substring(0, 2) + "." + humi9[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 10;
                int[] temp10 = ModClient.ReadInputRegisters(0, 1);
                int[] humi10 = ModClient.ReadInputRegisters(1, 1);
                lbTemp10.Text = temp10[0].ToString().Substring(0, 2) + "." + temp10[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi10.Text = humi10[0].ToString().Substring(0, 2) + "." + humi10[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 11;
                int[] temp11 = ModClient.ReadInputRegisters(0, 1);
                int[] humi11 = ModClient.ReadInputRegisters(1, 1);
                lbTemp11.Text = temp11[0].ToString().Substring(0, 2) + "." + temp11[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi11.Text = humi11[0].ToString().Substring(0, 2) + "." + humi11[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 12;
                int[] temp12 = ModClient.ReadInputRegisters(0, 1);
                int[] humi12 = ModClient.ReadInputRegisters(1, 1);
                lbTemp12.Text = temp12[0].ToString().Substring(0, 2) + "." + temp12[0].ToString().Substring(2, 2); //Đọc nhiệt độ
                lbHumi12.Text = humi12[0].ToString().Substring(0, 2) + "." + humi12[0].ToString().Substring(2, 2); //Đọc độ ẩm

                ModClient.UnitIdentifier = 13;
                int[] temp13 = ModClient.ReadHoldingRegisters(0, 1);
                int[] humi13 = ModClient.ReadHoldingRegisters(1, 1);
                int[] press13 = ModClient.ReadHoldingRegisters(2, 1);
                int[] press1311 = ModClient.ReadHoldingRegisters(4, 1);

                lbTemp13.Text = temp13[0].ToString().Substring(0, 2) + "." + temp13[0].ToString().Substring(2); //Đọc nhiệt độ
                lbHumi13.Text = humi13[0].ToString().Substring(0, 2) + "." + humi13[0].ToString().Substring(2); //Đọc độ ẩm
                lbPreasure13.Text = press13[0].ToString(); //Đọc áp suất

                ModClient.UnitIdentifier = 14;
                int[] temp14 = ModClient.ReadHoldingRegisters(0, 1);
                int[] humi14 = ModClient.ReadHoldingRegisters(1, 1);
                int[] press14 = ModClient.ReadHoldingRegisters(2, 1);
                int[] press1411 = ModClient.ReadHoldingRegisters(4, 1);

                lbTemp14.Text = temp14[0].ToString().Substring(0, 2) + "." + temp14[0].ToString().Substring(2); //Đọc nhiệt độ
                lbHumi14.Text = humi14[0].ToString().Substring(0, 2) + "." + humi14[0].ToString().Substring(2); //Đọc độ ẩm
                lbPreasure14.Text = press14[0].ToString(); //Đọc áp suất
            }
        }

        ModbusClient ModClient;
        private async void ReadSensorAsyn()
        {
            while (true)
            {
                await Task.Delay(1000);

                RunThread();
            }
        }

        // Hàm bắt đầu 10 Thread, mỗi thread sẽ cập nhật 1 Label
        private void StartThreads()
        {
            // Giả sử bạn đã tạo sẵn 10 label như myLabel1, myLabel2, ..., myLabel10
            Label[] labelsArr = new Label[] { lbTemp1, lbTemp2, lbTemp3, lbTemp4, lbTemp5, lbTemp6, lbTemp7, lbTemp8, lbTemp9, lbTemp10, lbTemp11, lbTemp12, lbTemp13, lbTemp14, lbTemp15 };

            for (int i = 0; i < labelsArr.Count(); i++)
            {
                int taskId = i; // Lưu taskId để tránh vấn đề đóng gói (capturing)
                //Thread thread = new Thread(() => RunThread(taskId, labelsArr[taskId]));
                //thread.Start();
            }
        }

        // Mỗi thread sẽ chạy và cập nhật label tương ứng
        // Mỗi thread sẽ chạy và cập nhật label tương ứng
        private void RunThread()
        {
            try
            {
                // Khởi tạo ModbusClient và kết nối
                ModClient = new ModbusClient("COM2");
                ModClient.Baudrate = 9600;
                ModClient.Parity = System.IO.Ports.Parity.None;
                ModClient.Connect();  // Kết nối với thiết bị Modbus


                while (true)
                {
                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 1;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp1.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp1.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 2;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp2.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp2.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 3;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp3.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp3.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 4;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp4.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp4.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 5;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp5.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp5.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 6;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp6.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp6.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 7;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp7.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp7.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 8;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp8.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp8.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 9;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp9.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp9.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 10;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp10.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp10.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 11;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp11.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp11.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 12;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp12.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp12.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 13;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);

                        lbTemp13.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp13.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 14;
                        int[] vals = ModClient.ReadHoldingRegisters(1, 1);

                        lbTemp14.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp14.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 15;
                        int[] vals = ModClient.ReadHoldingRegisters(1, 1);

                        lbTemp15.Text = vals[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp15.Text = "Er";
                        })); ;
                    }

                    //lbSensor1
                    try
                    {
                        ModClient.UnitIdentifier = 16;
                        int[] vals = ModClient.ReadInputRegisters(1, 1);
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbTemp1.Text = "Er";
                        })); ;
                    }
                    Thread.Sleep(1000);  // Đợi 1 giây
                }
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                // Đảm bảo rằng ModClient sẽ ngắt kết nối khi hoàn thành
                ModClient.Disconnect();
            }
        }

        private async void StartModbusTask()
        {
            try
            {
                // Khởi tạo ModbusClient và kết nối
                ModClient = new ModbusClient("COM2");
                ModClient.Baudrate = 9600;
                ModClient.Parity = System.IO.Ports.Parity.None;
                ModClient.Connect();  // Kết nối với thiết bị Modbus

                await Task.Run(() =>
                {
                    while (true)
                    {
                        // Các thiết bị Modbus từ UnitIdentifier 1 đến 16
                        for (int unitId = 1; unitId < 16; unitId++)
                        {
                            try
                            {
                                int[] vals;
                                // Cập nhật việc đọc dữ liệu cho từng thiết bị

                                ModClient.UnitIdentifier = Convert.ToByte(unitId);

                                if (unitId == 14 || unitId == 15)
                                {
                                    // Đọc Holding Registers cho UnitIdentifier 14 và 15
                                    vals = ModClient.ReadHoldingRegisters(1, 1);
                                }
                                else
                                {
                                    // Đọc Input Registers cho các UnitIdentifier khác
                                    vals = ModClient.ReadInputRegisters(1, 1);
                                }

                                // Cập nhật giá trị trên Label tương ứng
                                UpdateLabel(unitId, vals[0].ToString());
                            }
                            catch (Exception ex)
                            {
                                // Nếu có lỗi, hiển thị "Er"
                                UpdateLabel(unitId, "Er");
                            }
                        }
                        // Đợi 1 giây trước khi tiếp tục đọc
                        Thread.Sleep(100);
                    }
                });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi không thể kết nối với ModbusClient
                MessageBox.Show($"Lỗi kết nối: {ex.Message}");
            }
            finally
            {
                // Đảm bảo ngắt kết nối khi hoàn thành
                ModClient.Disconnect();
            }
        }

        private async void StartModbusTaskTimeOut()
        {
            try
            {
                // Khởi tạo ModbusClient và kết nối
                ModClient = new ModbusClient("COM2");
                ModClient.Baudrate = 9600;
                ModClient.Parity = System.IO.Ports.Parity.None;
                ModClient.Connect();  // Kết nối với thiết bị Modbus

                while (true)
                {
                    try
                    {
                        // Đọc tất cả các cảm biến trong một task duy nhất
                        await Task.Run(() => ReadAllSensors());
                    }
                    catch (Exception ex)
                    {
                        // Xử lý các lỗi ngoài Task chính
                        MessageBox.Show($"Lỗi khi đọc dữ liệu: {ex.Message}");
                    }

                    // Đợi 10 giây trước khi tiếp tục đọc lần tiếp theo
                    await Task.Delay(10000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}");
            }
            finally
            {
                // Ngắt kết nối sau khi hoàn thành
                ModClient.Disconnect();
            }
        }

        private void ReadAllSensors()
        {
            for (int unitId = 1; unitId <= 15; unitId++)
            {
                // Đọc dữ liệu từ cảm biến
                ReadSensor(unitId);
            }
        }

        private async void ReadSensor(int unitId)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            // Thiết lập timeout là 1 giây
            var timeoutTask = Task.Delay(2000, token); // 1000ms = 1s

            try
            {
                // Cập nhật UnitIdentifier cho cảm biến
                ModClient.UnitIdentifier = Convert.ToByte(unitId);

                // Tạo task đọc dữ liệu cho cảm biến
                Task<int[]> readTask = null;

                // Đọc Holding Registers cho UnitIdentifier 14 và 15, Input Registers cho các cảm biến còn lại
                if (unitId == 14 || unitId == 15)
                {
                    // Đọc dữ liệu từ Holding Registers
                    readTask = Task.Run(() =>
                    {
                        try
                        {
                            return ModClient.ReadHoldingRegisters(1, 1);
                        }
                        catch (Exception ex)
                        {
                            // Nếu có lỗi, ghi log hoặc xử lý lỗi tại đây
                            return null;
                        }
                    }, token);
                }
                else
                {
                    // Đọc dữ liệu từ Input Registers
                    readTask = Task.Run(() =>
                    {
                        try
                        {
                            return ModClient.ReadInputRegisters(1, 1);
                        }
                        catch (Exception ex)
                        {
                            // Nếu có lỗi, ghi log hoặc xử lý lỗi tại đây
                            return null;
                        }
                    }, token);
                }

                // Chờ cho đến khi task đọc xong hoặc timeout xảy ra
                var completedTask = await Task.WhenAny(readTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    // Nếu đã hết thời gian mà chưa có kết quả thì throw TimeoutException
                    throw new TimeoutException($"Timeout khi đọc cảm biến {unitId}");
                }

                // Đọc dữ liệu thành công
                int[] vals = await readTask;

                if (vals == null || vals.Length == 0)
                {
                    // Nếu đọc dữ liệu thất bại (ví dụ null hoặc mảng rỗng), báo lỗi và bỏ qua
                    throw new Exception($"Lỗi khi đọc cảm biến {unitId}");
                }

                // Cập nhật dữ liệu lên UI (vì đang ở thread khác nên cần gọi Invoke)
                UpdateLabel(unitId, vals[0].ToString());
            }
            catch (TimeoutException ex)
            {
                // Nếu hết thời gian, hiển thị lỗi "Timeout" và chuyển sang cảm biến tiếp theo
                UpdateLabel(unitId, "Timeout");
            }
            catch (Exception ex)
            {
                // Các lỗi khác (ví dụ kết nối bị gián đoạn, lỗi đọc dữ liệu) và chuyển sang cảm biến tiếp theo
                UpdateLabel(unitId, "Er");
            }
        }

        private async Task ReadSensorAsync(int unitId)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            // Thiết lập timeout là 1 giây
            var timeoutTask = Task.Delay(1000, token); // 1000ms = 1s

            try
            {
                // Tạo task đọc dữ liệu cho mỗi cảm biến
                ModClient.UnitIdentifier = Convert.ToByte(unitId);
                Task<int[]> readTask;

                if (unitId == 14 || unitId == 15)
                {
                    // Đọc Holding Registers cho UnitIdentifier 14 và 15
                    readTask = Task.Run(() => ModClient.ReadHoldingRegisters(1, 1), token);
                }
                else
                {
                    // Đọc Input Registers cho các UnitIdentifier còn lại
                    readTask = Task.Run(() => ModClient.ReadInputRegisters(1, 1), token);
                }

                // Chờ cho đến khi task đọc xong hoặc timeout xảy ra
                var completedTask = await Task.WhenAny(readTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    // Nếu đã hết thời gian mà chưa có kết quả thì throw TimeoutException
                    throw new TimeoutException($"Timeout khi đọc cảm biến {unitId}");
                }

                // Đọc dữ liệu thành công
                int[] vals = await readTask;

                // Cập nhật dữ liệu lên UI (vì đang ở thread khác nên cần gọi Invoke)
                UpdateLabel(unitId, vals[0].ToString());
            }
            catch (TimeoutException ex)
            {
                // Xử lý khi timeout, hiển thị lỗi lên label
                UpdateLabel(unitId, "Timeout");
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác (ví dụ lỗi kết nối, lỗi đọc dữ liệu)
                UpdateLabel(unitId, "Er");
            }
        }

        private async Task<int[]> ReadModbusWithTimeout(int unitId, int startAddress, int numRegisters, int timeoutMilliseconds)
        {
            // Tạo CancellationTokenSource để kiểm soát việc hủy tác vụ nếu hết thời gian
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            var task = Task.Run(async () =>
            {
                try
                {
                    ModClient.UnitIdentifier = Convert.ToByte(unitId);
                    await Task.Delay(100);
                    ModClient.Connect();
                    await Task.Delay(100);

                    // Đọc Input Registers hoặc Holding Registers tùy thuộc vào UnitIdentifier
                    if (unitId == 14 || unitId == 15)
                    {
                        return ModClient.ReadHoldingRegisters(startAddress, numRegisters);
                    }
                    else
                    {
                        return ModClient.ReadInputRegisters(startAddress, numRegisters);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi đọc dữ liệu từ Unit {unitId}: {ex.Message}");
                    throw new TimeoutException($"Timeout khi đọc dữ liệu từ thiết bị {unitId}"); // Ném TimeoutException nếu có lỗi
                }
            }, token);

            // Kiểm tra nếu task hoàn thành trong thời gian timeout
            if (await Task.WhenAny(task, Task.Delay(timeoutMilliseconds)) == task)
            {
                return task.Result; // Trả về kết quả nếu task hoàn thành trước timeout
            }
            else
            {
                cts.Cancel(); // Hủy task nếu quá thời gian
                throw new TimeoutException($"Timeout khi đọc dữ liệu từ thiết bị {unitId}"); // Ném TimeoutException
            }
        }

        private void UpdateLabel(int unitId, string value)
        {
            // Cập nhật dữ liệu lên các label theo UnitIdentifier
            // Gọi Invoke để đảm bảo cập nhật trên UI thread
            this.Invoke(new Action(() =>
            {
                switch (unitId)
                {
                    case 1: lbTemp1.Text = value; break;
                    case 2: lbTemp2.Text = value; break;
                    case 3: lbTemp3.Text = value; break;
                    case 4: lbTemp4.Text = value; break;
                    case 5: lbTemp5.Text = value; break;
                    case 6: lbTemp6.Text = value; break;
                    case 7: lbTemp7.Text = value; break;
                    case 8: lbTemp8.Text = value; break;
                    case 9: lbTemp9.Text = value; break;
                    case 10: lbTemp10.Text = value; break;
                    case 11: lbTemp11.Text = value; break;
                    case 12: lbTemp12.Text = value; break;
                    case 13: lbTemp13.Text = value; break;
                    case 14: lbTemp14.Text = value; break;
                    case 15: lbTemp15.Text = value; break;
                    default: break;
                }
            }));
        }

        private void btnSetupUnit_Click(object sender, EventArgs e)
        {

        }
    }
}
