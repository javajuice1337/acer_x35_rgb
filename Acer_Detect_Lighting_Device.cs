// Acer_Class.Acer_Detect_Lighting_Device
using System;
using System.Threading;

internal class Acer_Detect_Lighting_Device
{
    public const int LATENCY_OF_DETECT_USB_DEVICE = 5;

    public const int DETECT_USB_CONNECTED_LATENCY = 50;

    public static bool IsConnected_GB100;

    public static bool IsConnected_X35;

    public static bool IsConnected_GM310;

    public static bool IsConnected_GK300;

    public static bool IsConnected_GH300;

    public static bool IsConnected_GM300;

    public static bool IsConnected_GB300;

    public static int Connected_Device_Number;

    public static bool Is_USB30_Port(int VID, int PID)
    {
        foreach (Acer_USB_Library.USBController hostController in Acer_USB_Library.GetHostControllers())
        {
            Console.WriteLine("hostCtrl.Index: " + hostController.Index);
            Console.WriteLine("hostCtrl.Name: " + hostController.Name);
            foreach (Acer_USB_Library.USBPort port in hostController.GetRootHub().GetPorts())
            {
                if (port.IsDeviceConnected && !port.IsHub)
                {
                    Acer_USB_Library.USBDevice device = port.GetDevice();
                    if (device.VID == VID && device.PID == PID)
                    {
                        bool result = port.IsHub;
                        Console.WriteLine("IsHub: " + result.ToString());
                        Console.WriteLine("Product: " + device.Product);
                        Console.WriteLine("VID/PID: " + device.VID + " / " + device.PID);
                        Console.WriteLine("Manufacturer: " + device.Manufacturer);
                        Console.WriteLine("HubDevicePath: " + device.HubDevicePath);
                        if (device.HubDevicePath.Contains("HUB30"))
                        {
                            Console.WriteLine("USB 3.0 port");
                            result = true;
                            return result;
                        }
                        Console.WriteLine("Name: " + device.Name);
                        Console.WriteLine("DriverKey: " + device.DriverKey);
                        Console.WriteLine("InstanceID: " + device.InstanceID);
                        Console.WriteLine("Serial: " + device.DeviceSerialNumber);
                        Console.WriteLine("Speed:  " + port.Speed);
                        Console.WriteLine("Port:   " + device.PortNumber + Environment.NewLine);
                    }
                }
            }
            Console.WriteLine("===============\n");
        }
        return false;
    }

    public static bool Detect_Connected_GB100()
    {
        return LED_Control_API.Acer_Detect_Lighting_Device(7649, 44033);
    }

    public static bool Detect_Connected_GM310_Mouse()
    {
        return LED_Control_API.Acer_Detect_Lighting_Device(14648, 4470);
    }

    public static bool Detect_Connected_Archos_K92_KB()
    {
        return LED_Control_API.Acer_Detect_Lighting_Device(14648, 4482);
    }

    public static bool Detect_Connected_Acer_X35()
    {
        return LED_Control_API.Acer_Detect_Lighting_Device(4292, 60048);
    }

    public static bool Detect_Connected_GH300()
    {
        return LED_Control_API.Acer_Detect_Lighting_Device(7649, 44034);
    }

    public static bool Detect_Connected_GM300()
    {
        return LED_Control_API.Acer_Detect_Lighting_Device(14648, 4469);
    }

    public static bool Detect_Connected_GB300()
    {
        return LED_Control_API.Acer_Detect_Lighting_Device(7649, 44035);
    }

    public static void Do_All_Lighting_Device_Detection()
    {
        Acer_Detect_Lighting_Device.Reset_Lighting_Device_Detection_Result();
        //Acer_Detect_Lighting_Device.IsConnected_GB100 = Acer_Detect_Lighting_Device.Detect_Connected_GB100();
        //Thread.Sleep(5);
        //Acer_Detect_Lighting_Device.IsConnected_GM310 = Acer_Detect_Lighting_Device.Detect_Connected_GM310_Mouse();
        //Thread.Sleep(5);
        //Acer_Detect_Lighting_Device.IsConnected_GK300 = Acer_Detect_Lighting_Device.Detect_Connected_Archos_K92_KB();
        //Thread.Sleep(5);
        Acer_Detect_Lighting_Device.IsConnected_X35 = Acer_Detect_Lighting_Device.Detect_Connected_Acer_X35();
        Thread.Sleep(5);
        //Acer_Detect_Lighting_Device.IsConnected_GH300 = Acer_Detect_Lighting_Device.Detect_Connected_GH300();
        //Thread.Sleep(5);
        //Acer_Detect_Lighting_Device.IsConnected_GM300 = Acer_Detect_Lighting_Device.Detect_Connected_GM300();
        //Thread.Sleep(5);
        //Acer_Detect_Lighting_Device.IsConnected_GB300 = Acer_Detect_Lighting_Device.Detect_Connected_GB300();
        //Thread.Sleep(5);
        if (Acer_Detect_Lighting_Device.IsConnected_GB100)
        {
            Acer_Detect_Lighting_Device.Connected_Device_Number++;
        }
        if (Acer_Detect_Lighting_Device.IsConnected_GM310)
        {
            Acer_Detect_Lighting_Device.Connected_Device_Number++;
        }
        if (Acer_Detect_Lighting_Device.IsConnected_GK300)
        {
            Acer_Detect_Lighting_Device.Connected_Device_Number++;
        }
        if (Acer_Detect_Lighting_Device.IsConnected_GH300)
        {
            Acer_Detect_Lighting_Device.Connected_Device_Number++;
        }
        if (Acer_Detect_Lighting_Device.IsConnected_X35)
        {
            Acer_Detect_Lighting_Device.Connected_Device_Number++;
        }
        if (Acer_Detect_Lighting_Device.IsConnected_GM300)
        {
            Acer_Detect_Lighting_Device.Connected_Device_Number++;
        }
        if (Acer_Detect_Lighting_Device.IsConnected_GB300)
        {
            Acer_Detect_Lighting_Device.Connected_Device_Number++;
        }
    }

    public static void Reset_Lighting_Device_Detection_Result()
    {
        Acer_Detect_Lighting_Device.Connected_Device_Number = 0;
        Acer_Detect_Lighting_Device.IsConnected_GB100 = false;
        Acer_Detect_Lighting_Device.IsConnected_GM310 = false;
        Acer_Detect_Lighting_Device.IsConnected_GK300 = false;
        Acer_Detect_Lighting_Device.IsConnected_X35 = false;
        Acer_Detect_Lighting_Device.IsConnected_GH300 = false;
        Acer_Detect_Lighting_Device.IsConnected_GM300 = false;
    }
}
