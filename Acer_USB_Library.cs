// Acer_Class.Acer_USB_Library
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;

public class Acer_USB_Library
{
    private enum USB_HUB_NODE
    {
        UsbHub,
        UsbMIParent
    }

    private enum USB_CONNECTION_STATUS
    {
        NoDeviceConnected,
        DeviceConnected,
        DeviceFailedEnumeration,
        DeviceGeneralFailure,
        DeviceCausedOvercurrent,
        DeviceNotEnoughPower,
        DeviceNotEnoughBandwidth,
        DeviceHubNestedTooDeeply,
        DeviceInLegacyHub
    }

    private enum USB_DEVICE_SPEED : byte
    {
        UsbLowSpeed,
        UsbFullSpeed,
        UsbHighSpeed,
        UsbSuperSpeed
    }

    private struct SP_DEVINFO_DATA
    {
        public int cbSize;

        public Guid ClassGuid;

        public IntPtr DevInst;

        public IntPtr Reserved;
    }

    private struct SP_DEVICE_INTERFACE_DATA
    {
        public int cbSize;

        public Guid InterfaceClassGuid;

        public int Flags;

        public IntPtr Reserved;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct SP_DEVICE_INTERFACE_DETAIL_DATA
    {
        public int cbSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
        public string DevicePath;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct USB_HCD_DRIVERKEY_NAME
    {
        public int ActualLength;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
        public string DriverKeyName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct USB_ROOT_HUB_NAME
    {
        public int ActualLength;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
        public string RootHubName;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct USB_HUB_DESCRIPTOR
    {
        public byte bDescriptorLength;

        public byte bDescriptorType;

        public byte bNumberOfPorts;

        public short wHubCharacteristics;

        public byte bPowerOnToPowerGood;

        public byte bHubControlCurrent;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] bRemoveAndPowerMask;
    }

    private struct USB_HUB_INFORMATION
    {
        public USB_HUB_DESCRIPTOR HubDescriptor;

        public byte HubIsBusPowered;
    }

    private struct USB_NODE_INFORMATION
    {
        public int NodeType;

        public USB_HUB_INFORMATION HubInformation;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct USB_NODE_CONNECTION_INFORMATION_EX
    {
        public int ConnectionIndex;

        public USB_DEVICE_DESCRIPTOR DeviceDescriptor;

        public byte CurrentConfigurationValue;

        public byte Speed;

        public byte DeviceIsHub;

        public short DeviceAddress;

        public int NumberOfOpenPipes;

        public int ConnectionStatus;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct USB_DEVICE_DESCRIPTOR
    {
        public byte bLength;

        public byte bDescriptorType;

        public short bcdUSB;

        public byte bDeviceClass;

        public byte bDeviceSubClass;

        public byte bDeviceProtocol;

        public byte bMaxPacketSize0;

        public ushort idVendor;

        public ushort idProduct;

        public short bcdDevice;

        public byte iManufacturer;

        public byte iProduct;

        public byte iSerialNumber;

        public byte bNumConfigurations;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct USB_STRING_DESCRIPTOR
    {
        public byte bLength;

        public byte bDescriptorType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string bString;
    }

    private struct USB_SETUP_PACKET
    {
        public byte bmRequest;

        public byte bRequest;

        public short wValue;

        public short wIndex;

        public short wLength;
    }

    private struct USB_DESCRIPTOR_REQUEST
    {
        public int ConnectionIndex;

        public USB_SETUP_PACKET SetupPacket;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct USB_NODE_CONNECTION_NAME
    {
        public int ConnectionIndex;

        public int ActualLength;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
        public string NodeName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct USB_NODE_CONNECTION_DRIVERKEY_NAME
    {
        public int ConnectionIndex;

        public int ActualLength;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
        public string DriverKeyName;
    }

    public class USBController
    {
        internal int ControllerIndex;

        internal string ControllerDriverKeyName;

        internal string ControllerDevicePath;

        internal string ControllerDeviceDesc;

        public int Index
        {
            get
            {
                return this.ControllerIndex;
            }
        }

        public string DevicePath
        {
            get
            {
                return this.ControllerDevicePath;
            }
        }

        public string DriverKeyName
        {
            get
            {
                return this.ControllerDriverKeyName;
            }
        }

        public string Name
        {
            get
            {
                return this.ControllerDeviceDesc;
            }
        }

        public USBController()
        {
            this.ControllerIndex = 0;
            this.ControllerDevicePath = "";
            this.ControllerDeviceDesc = "";
            this.ControllerDriverKeyName = "";
        }

        public USBHub GetRootHub()
        {
            USBHub uSBHub = new USBHub();
            uSBHub.HubIsRootHub = true;
            uSBHub.HubDeviceDesc = "Root Hub";
            IntPtr intPtr = Acer_USB_Library.CreateFile(this.ControllerDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
            if (intPtr.ToInt32() != -1)
            {
                int num = Marshal.SizeOf(default(USB_ROOT_HUB_NAME));
                IntPtr intPtr2 = Marshal.AllocHGlobal(num);
                int num2 = default(int);
                if (Acer_USB_Library.DeviceIoControl(intPtr, 2229256, intPtr2, num, intPtr2, num, out num2, IntPtr.Zero))
                {
                    USB_ROOT_HUB_NAME uSB_ROOT_HUB_NAME = (USB_ROOT_HUB_NAME)Marshal.PtrToStructure(intPtr2, typeof(USB_ROOT_HUB_NAME));
                    uSBHub.HubDevicePath = "\\\\.\\" + uSB_ROOT_HUB_NAME.RootHubName;
                }
                IntPtr intPtr3 = Acer_USB_Library.CreateFile(uSBHub.HubDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
                if (intPtr3.ToInt32() != -1)
                {
                    USB_NODE_INFORMATION uSB_NODE_INFORMATION = default(USB_NODE_INFORMATION);
                    uSB_NODE_INFORMATION.NodeType = 0;
                    num = Marshal.SizeOf(uSB_NODE_INFORMATION);
                    IntPtr intPtr4 = Marshal.AllocHGlobal(num);
                    Marshal.StructureToPtr(uSB_NODE_INFORMATION, intPtr4, true);
                    if (Acer_USB_Library.DeviceIoControl(intPtr3, 2229256, intPtr4, num, intPtr4, num, out num2, IntPtr.Zero))
                    {
                        uSB_NODE_INFORMATION = (USB_NODE_INFORMATION)Marshal.PtrToStructure(intPtr4, typeof(USB_NODE_INFORMATION));
                        uSBHub.HubIsBusPowered = Convert.ToBoolean(uSB_NODE_INFORMATION.HubInformation.HubIsBusPowered);
                        uSBHub.HubPortCount = uSB_NODE_INFORMATION.HubInformation.HubDescriptor.bNumberOfPorts;
                    }
                    Marshal.FreeHGlobal(intPtr4);
                    Acer_USB_Library.CloseHandle(intPtr3);
                }
                Marshal.FreeHGlobal(intPtr2);
                Acer_USB_Library.CloseHandle(intPtr);
            }
            return uSBHub;
        }
    }

    public class USBHub
    {
        internal int HubPortCount;

        internal string HubDriverKey;

        internal string HubDevicePath;

        internal string HubDeviceDesc;

        internal string HubManufacturer;

        internal string HubProduct;

        internal string HubSerialNumber;

        internal string HubInstanceID;

        internal bool HubIsBusPowered;

        internal bool HubIsRootHub;

        public int PortCount
        {
            get
            {
                return this.HubPortCount;
            }
        }

        public string DevicePath
        {
            get
            {
                return this.HubDevicePath;
            }
        }

        public string DriverKey
        {
            get
            {
                return this.HubDriverKey;
            }
        }

        public string Name
        {
            get
            {
                return this.HubDeviceDesc;
            }
        }

        public string InstanceID
        {
            get
            {
                return this.HubInstanceID;
            }
        }

        public bool IsBusPowered
        {
            get
            {
                return this.HubIsBusPowered;
            }
        }

        public bool IsRootHub
        {
            get
            {
                return this.HubIsRootHub;
            }
        }

        public string Manufacturer
        {
            get
            {
                return this.HubManufacturer;
            }
        }

        public string Product
        {
            get
            {
                return this.HubProduct;
            }
        }

        public string SerialNumber
        {
            get
            {
                return this.HubSerialNumber;
            }
        }

        public USBHub()
        {
            this.HubPortCount = 0;
            this.HubDevicePath = "";
            this.HubDeviceDesc = "";
            this.HubDriverKey = "";
            this.HubIsBusPowered = false;
            this.HubIsRootHub = false;
            this.HubManufacturer = "";
            this.HubProduct = "";
            this.HubSerialNumber = "";
            this.HubInstanceID = "";
        }

        public ReadOnlyCollection<USBPort> GetPorts()
        {
            List<USBPort> list = new List<USBPort>();
            IntPtr intPtr = Acer_USB_Library.CreateFile(this.HubDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
            if (intPtr.ToInt32() != -1)
            {
                int num = Marshal.SizeOf(typeof(USB_NODE_CONNECTION_INFORMATION_EX));
                IntPtr intPtr2 = Marshal.AllocHGlobal(num);
                for (int i = 1; i <= this.HubPortCount; i++)
                {
                    USB_NODE_CONNECTION_INFORMATION_EX uSB_NODE_CONNECTION_INFORMATION_EX = default(USB_NODE_CONNECTION_INFORMATION_EX);
                    uSB_NODE_CONNECTION_INFORMATION_EX.ConnectionIndex = i;
                    Marshal.StructureToPtr(uSB_NODE_CONNECTION_INFORMATION_EX, intPtr2, true);
                    int num2 = default(int);
                    if (Acer_USB_Library.DeviceIoControl(intPtr, 2229320, intPtr2, num, intPtr2, num, out num2, IntPtr.Zero))
                    {
                        uSB_NODE_CONNECTION_INFORMATION_EX = (USB_NODE_CONNECTION_INFORMATION_EX)Marshal.PtrToStructure(intPtr2, typeof(USB_NODE_CONNECTION_INFORMATION_EX));
                        USBPort uSBPort = new USBPort();
                        uSBPort.PortPortNumber = i;
                        uSBPort.PortHubDevicePath = this.HubDevicePath;
                        USB_CONNECTION_STATUS connectionStatus = (USB_CONNECTION_STATUS)uSB_NODE_CONNECTION_INFORMATION_EX.ConnectionStatus;
                        uSBPort.PortStatus = connectionStatus.ToString();
                        USB_DEVICE_SPEED speed = (USB_DEVICE_SPEED)uSB_NODE_CONNECTION_INFORMATION_EX.Speed;
                        uSBPort.PortSpeed = speed.ToString();
                        uSBPort.PortIsDeviceConnected = (uSB_NODE_CONNECTION_INFORMATION_EX.ConnectionStatus == 1);
                        uSBPort.PortIsHub = Convert.ToBoolean(uSB_NODE_CONNECTION_INFORMATION_EX.DeviceIsHub);
                        uSBPort.PortDeviceDescriptor = uSB_NODE_CONNECTION_INFORMATION_EX.DeviceDescriptor;
                        list.Add(uSBPort);
                    }
                }
                Marshal.FreeHGlobal(intPtr2);
                Acer_USB_Library.CloseHandle(intPtr);
            }
            return new ReadOnlyCollection<USBPort>(list);
        }
    }

    public class USBPort
    {
        internal int PortPortNumber;

        internal string PortStatus;

        internal string PortHubDevicePath;

        internal string PortSpeed;

        internal bool PortIsHub;

        internal bool PortIsDeviceConnected;

        internal USB_DEVICE_DESCRIPTOR PortDeviceDescriptor;

        public int PortNumber
        {
            get
            {
                return this.PortPortNumber;
            }
        }

        public string HubDevicePath
        {
            get
            {
                return this.PortHubDevicePath;
            }
        }

        public string Status
        {
            get
            {
                return this.PortStatus;
            }
        }

        public string Speed
        {
            get
            {
                return this.PortSpeed;
            }
        }

        public bool IsHub
        {
            get
            {
                return this.PortIsHub;
            }
        }

        public bool IsDeviceConnected
        {
            get
            {
                return this.PortIsDeviceConnected;
            }
        }

        public USBPort()
        {
            this.PortPortNumber = 0;
            this.PortStatus = "";
            this.PortHubDevicePath = "";
            this.PortSpeed = "";
            this.PortIsHub = false;
            this.PortIsDeviceConnected = false;
        }

        public USBDevice GetDevice()
        {
            if (!this.PortIsDeviceConnected)
            {
                return null;
            }
            USBDevice uSBDevice = new USBDevice();
            uSBDevice.DevicePortNumber = this.PortPortNumber;
            uSBDevice.DeviceHubDevicePath = this.PortHubDevicePath;
            uSBDevice.DeviceDescriptor = this.PortDeviceDescriptor;
            uSBDevice.DeviceVID = this.PortDeviceDescriptor.idVendor;
            uSBDevice.DevicePID = this.PortDeviceDescriptor.idProduct;
            IntPtr intPtr = Acer_USB_Library.CreateFile(this.PortHubDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
            if (intPtr.ToInt32() != -1)
            {
                int num = 2048;
                string s = new string('\0', 2048 / Marshal.SystemDefaultCharSize);
                int num2 = default(int);
                if (this.PortDeviceDescriptor.iManufacturer > 0)
                {
                    USB_DESCRIPTOR_REQUEST structure = default(USB_DESCRIPTOR_REQUEST);
                    structure.ConnectionIndex = this.PortPortNumber;
                    structure.SetupPacket.wValue = (short)(768 + this.PortDeviceDescriptor.iManufacturer);
                    structure.SetupPacket.wLength = (short)(num - Marshal.SizeOf(structure));
                    structure.SetupPacket.wIndex = 1033;
                    IntPtr intPtr2 = Marshal.StringToHGlobalAuto(s);
                    Marshal.StructureToPtr(structure, intPtr2, true);
                    if (Acer_USB_Library.DeviceIoControl(intPtr, 2229264, intPtr2, num, intPtr2, num, out num2, IntPtr.Zero))
                    {
                        USB_STRING_DESCRIPTOR uSB_STRING_DESCRIPTOR = (USB_STRING_DESCRIPTOR)Marshal.PtrToStructure(new IntPtr(intPtr2.ToInt32() + Marshal.SizeOf(structure)), typeof(USB_STRING_DESCRIPTOR));
                        uSBDevice.DeviceManufacturer = uSB_STRING_DESCRIPTOR.bString;
                    }
                    Marshal.FreeHGlobal(intPtr2);
                }
                if (this.PortDeviceDescriptor.iProduct > 0)
                {
                    USB_DESCRIPTOR_REQUEST structure2 = default(USB_DESCRIPTOR_REQUEST);
                    structure2.ConnectionIndex = this.PortPortNumber;
                    structure2.SetupPacket.wValue = (short)(768 + this.PortDeviceDescriptor.iProduct);
                    structure2.SetupPacket.wLength = (short)(num - Marshal.SizeOf(structure2));
                    structure2.SetupPacket.wIndex = 1033;
                    IntPtr intPtr3 = Marshal.StringToHGlobalAuto(s);
                    Marshal.StructureToPtr(structure2, intPtr3, true);
                    if (Acer_USB_Library.DeviceIoControl(intPtr, 2229264, intPtr3, num, intPtr3, num, out num2, IntPtr.Zero))
                    {
                        USB_STRING_DESCRIPTOR uSB_STRING_DESCRIPTOR2 = (USB_STRING_DESCRIPTOR)Marshal.PtrToStructure(new IntPtr(intPtr3.ToInt32() + Marshal.SizeOf(structure2)), typeof(USB_STRING_DESCRIPTOR));
                        uSBDevice.DeviceProduct = uSB_STRING_DESCRIPTOR2.bString;
                    }
                    Marshal.FreeHGlobal(intPtr3);
                }
                if (this.PortDeviceDescriptor.iSerialNumber > 0)
                {
                    USB_DESCRIPTOR_REQUEST structure3 = default(USB_DESCRIPTOR_REQUEST);
                    structure3.ConnectionIndex = this.PortPortNumber;
                    structure3.SetupPacket.wValue = (short)(768 + this.PortDeviceDescriptor.iSerialNumber);
                    structure3.SetupPacket.wLength = (short)(num - Marshal.SizeOf(structure3));
                    structure3.SetupPacket.wIndex = 1033;
                    IntPtr intPtr4 = Marshal.StringToHGlobalAuto(s);
                    Marshal.StructureToPtr(structure3, intPtr4, true);
                    if (Acer_USB_Library.DeviceIoControl(intPtr, 2229264, intPtr4, num, intPtr4, num, out num2, IntPtr.Zero))
                    {
                        USB_STRING_DESCRIPTOR uSB_STRING_DESCRIPTOR3 = (USB_STRING_DESCRIPTOR)Marshal.PtrToStructure(new IntPtr(intPtr4.ToInt32() + Marshal.SizeOf(structure3)), typeof(USB_STRING_DESCRIPTOR));
                        uSBDevice.DeviceSerialNumber = uSB_STRING_DESCRIPTOR3.bString;
                    }
                    Marshal.FreeHGlobal(intPtr4);
                }
                USB_NODE_CONNECTION_DRIVERKEY_NAME uSB_NODE_CONNECTION_DRIVERKEY_NAME = default(USB_NODE_CONNECTION_DRIVERKEY_NAME);
                uSB_NODE_CONNECTION_DRIVERKEY_NAME.ConnectionIndex = this.PortPortNumber;
                num = Marshal.SizeOf(uSB_NODE_CONNECTION_DRIVERKEY_NAME);
                IntPtr intPtr5 = Marshal.AllocHGlobal(num);
                Marshal.StructureToPtr(uSB_NODE_CONNECTION_DRIVERKEY_NAME, intPtr5, true);
                if (Acer_USB_Library.DeviceIoControl(intPtr, 2229280, intPtr5, num, intPtr5, num, out num2, IntPtr.Zero))
                {
                    uSB_NODE_CONNECTION_DRIVERKEY_NAME = (USB_NODE_CONNECTION_DRIVERKEY_NAME)Marshal.PtrToStructure(intPtr5, typeof(USB_NODE_CONNECTION_DRIVERKEY_NAME));
                    uSBDevice.DeviceDriverKey = uSB_NODE_CONNECTION_DRIVERKEY_NAME.DriverKeyName;
                    uSBDevice.DeviceName = Acer_USB_Library.GetDescriptionByKeyName(uSBDevice.DeviceDriverKey);
                    uSBDevice.DeviceInstanceID = Acer_USB_Library.GetInstanceIDByKeyName(uSBDevice.DeviceDriverKey);
                }
                Marshal.FreeHGlobal(intPtr5);
                Acer_USB_Library.CloseHandle(intPtr);
            }
            return uSBDevice;
        }

        public USBHub GetHub()
        {
            if (!this.PortIsHub)
            {
                return null;
            }
            USBHub uSBHub = new USBHub();
            uSBHub.HubIsRootHub = false;
            uSBHub.HubDeviceDesc = "External Hub";
            IntPtr intPtr = Acer_USB_Library.CreateFile(this.PortHubDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
            if (intPtr.ToInt32() != -1)
            {
                USB_NODE_CONNECTION_NAME uSB_NODE_CONNECTION_NAME = default(USB_NODE_CONNECTION_NAME);
                uSB_NODE_CONNECTION_NAME.ConnectionIndex = this.PortPortNumber;
                int num = Marshal.SizeOf(uSB_NODE_CONNECTION_NAME);
                IntPtr intPtr2 = Marshal.AllocHGlobal(num);
                Marshal.StructureToPtr(uSB_NODE_CONNECTION_NAME, intPtr2, true);
                int num2 = default(int);
                if (Acer_USB_Library.DeviceIoControl(intPtr, 2229268, intPtr2, num, intPtr2, num, out num2, IntPtr.Zero))
                {
                    uSB_NODE_CONNECTION_NAME = (USB_NODE_CONNECTION_NAME)Marshal.PtrToStructure(intPtr2, typeof(USB_NODE_CONNECTION_NAME));
                    uSBHub.HubDevicePath = "\\\\.\\" + uSB_NODE_CONNECTION_NAME.NodeName;
                }
                IntPtr intPtr3 = Acer_USB_Library.CreateFile(uSBHub.HubDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
                if (intPtr3.ToInt32() != -1)
                {
                    USB_NODE_INFORMATION uSB_NODE_INFORMATION = default(USB_NODE_INFORMATION);
                    uSB_NODE_INFORMATION.NodeType = 0;
                    num = Marshal.SizeOf(uSB_NODE_INFORMATION);
                    IntPtr intPtr4 = Marshal.AllocHGlobal(num);
                    Marshal.StructureToPtr(uSB_NODE_INFORMATION, intPtr4, true);
                    if (Acer_USB_Library.DeviceIoControl(intPtr3, 2229256, intPtr4, num, intPtr4, num, out num2, IntPtr.Zero))
                    {
                        uSB_NODE_INFORMATION = (USB_NODE_INFORMATION)Marshal.PtrToStructure(intPtr4, typeof(USB_NODE_INFORMATION));
                        uSBHub.HubIsBusPowered = Convert.ToBoolean(uSB_NODE_INFORMATION.HubInformation.HubIsBusPowered);
                        uSBHub.HubPortCount = uSB_NODE_INFORMATION.HubInformation.HubDescriptor.bNumberOfPorts;
                    }
                    Marshal.FreeHGlobal(intPtr4);
                    Acer_USB_Library.CloseHandle(intPtr3);
                }
                USBDevice device = this.GetDevice();
                uSBHub.HubInstanceID = device.DeviceInstanceID;
                uSBHub.HubManufacturer = device.Manufacturer;
                uSBHub.HubProduct = device.Product;
                uSBHub.HubSerialNumber = device.SerialNumber;
                uSBHub.HubDriverKey = device.DriverKey;
                Marshal.FreeHGlobal(intPtr2);
                Acer_USB_Library.CloseHandle(intPtr);
            }
            return uSBHub;
        }
    }

    public class USBDevice
    {
        internal int DevicePortNumber;

        internal ushort DeviceVID;

        internal ushort DevicePID;

        internal string DeviceDriverKey;

        internal string DeviceHubDevicePath;

        internal string DeviceInstanceID;

        internal string DeviceName;

        internal string DeviceManufacturer;

        internal string DeviceProduct;

        internal string DeviceSerialNumber;

        internal USB_DEVICE_DESCRIPTOR DeviceDescriptor;

        public ushort VID
        {
            get
            {
                return this.DeviceVID;
            }
        }

        public ushort PID
        {
            get
            {
                return this.DevicePID;
            }
        }

        public int PortNumber
        {
            get
            {
                return this.DevicePortNumber;
            }
        }

        public string HubDevicePath
        {
            get
            {
                return this.DeviceHubDevicePath;
            }
        }

        public string DriverKey
        {
            get
            {
                return this.DeviceDriverKey;
            }
        }

        public string InstanceID
        {
            get
            {
                return this.DeviceInstanceID;
            }
        }

        public string Name
        {
            get
            {
                return this.DeviceName;
            }
        }

        public string Manufacturer
        {
            get
            {
                return this.DeviceManufacturer;
            }
        }

        public string Product
        {
            get
            {
                return this.DeviceProduct;
            }
        }

        public string SerialNumber
        {
            get
            {
                return this.DeviceSerialNumber;
            }
        }

        public USBDevice()
        {
            this.DevicePortNumber = 0;
            this.DeviceHubDevicePath = "";
            this.DeviceDriverKey = "";
            this.DeviceManufacturer = "";
            this.DeviceProduct = "Unknown USB Device";
            this.DeviceSerialNumber = "";
            this.DeviceName = "";
            this.DeviceInstanceID = "";
            this.DeviceVID = 0;
            this.DevicePID = 0;
        }
    }

    private const int GENERIC_WRITE = 1073741824;

    private const int FILE_SHARE_READ = 1;

    private const int FILE_SHARE_WRITE = 2;

    private const int OPEN_EXISTING = 3;

    private const int INVALID_HANDLE_VALUE = -1;

    private const int IOCTL_GET_HCD_DRIVERKEY_NAME = 2229284;

    private const int IOCTL_USB_GET_ROOT_HUB_NAME = 2229256;

    private const int IOCTL_USB_GET_NODE_INFORMATION = 2229256;

    private const int IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX = 2229320;

    private const int IOCTL_USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION = 2229264;

    private const int IOCTL_USB_GET_NODE_CONNECTION_NAME = 2229268;

    private const int IOCTL_USB_GET_NODE_CONNECTION_DRIVERKEY_NAME = 2229280;

    private const int USB_DEVICE_DESCRIPTOR_TYPE = 1;

    private const int USB_STRING_DESCRIPTOR_TYPE = 3;

    private const int BUFFER_SIZE = 2048;

    private const int MAXIMUM_USB_STRING_LENGTH = 255;

    private const string GUID_DEVINTERFACE_HUBCONTROLLER = "3abf6f2d-71c4-462a-8a92-1e6861e6af27";

    private const string REGSTR_KEY_USB = "USB";

    private const int DIGCF_PRESENT = 2;

    private const int DIGCF_ALLCLASSES = 4;

    private const int DIGCF_DEVICEINTERFACE = 16;

    private const int SPDRP_DRIVER = 9;

    private const int SPDRP_DEVICEDESC = 0;

    private const int REG_SZ = 1;

    [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, int Enumerator, IntPtr hwndParent, int Flags);

    [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SetupDiGetClassDevs(int ClassGuid, string Enumerator, IntPtr hwndParent, int Flags);

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, ref Guid InterfaceClassGuid, int MemberIndex, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, ref SP_DEVICE_INTERFACE_DETAIL_DATA DeviceInterfaceDetailData, int DeviceInterfaceDetailDataSize, ref int RequiredSize, ref SP_DEVINFO_DATA DeviceInfoData);

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool SetupDiGetDeviceRegistryProperty(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, int iProperty, ref int PropertyRegDataType, IntPtr PropertyBuffer, int PropertyBufferSize, ref int RequiredSize);

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, int MemberIndex, ref SP_DEVINFO_DATA DeviceInfoData);

    [DllImport("setupapi.dll", SetLastError = true)]
    private static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, StringBuilder DeviceInstanceId, int DeviceInstanceIdSize, out int RequiredSize);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool DeviceIoControl(IntPtr hDevice, int dwIoControlCode, IntPtr lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, out int lpBytesReturned, IntPtr lpOverlapped);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);

    public static ReadOnlyCollection<USBController> GetHostControllers()
    {
        List<USBController> list = new List<USBController>();
        Guid guid = new Guid("3abf6f2d-71c4-462a-8a92-1e6861e6af27");
        IntPtr deviceInfoSet = Acer_USB_Library.SetupDiGetClassDevs(ref guid, 0, IntPtr.Zero, 18);
        if (deviceInfoSet.ToInt32() != -1)
        {
            IntPtr intPtr = Marshal.AllocHGlobal(2048);
            int num = 0;
            bool flag;
            do
            {
                USBController uSBController = new USBController();
                uSBController.ControllerIndex = num;
                SP_DEVICE_INTERFACE_DATA structure = default(SP_DEVICE_INTERFACE_DATA);
                structure.cbSize = Marshal.SizeOf(structure);
                flag = Acer_USB_Library.SetupDiEnumDeviceInterfaces(deviceInfoSet, IntPtr.Zero, ref guid, num, ref structure);
                if (flag)
                {
                    SP_DEVINFO_DATA structure2 = default(SP_DEVINFO_DATA);
                    structure2.cbSize = Marshal.SizeOf(structure2);
                    SP_DEVICE_INTERFACE_DETAIL_DATA sP_DEVICE_INTERFACE_DETAIL_DATA = default(SP_DEVICE_INTERFACE_DETAIL_DATA);
                    sP_DEVICE_INTERFACE_DETAIL_DATA.cbSize = 4 + Marshal.SystemDefaultCharSize;
                    int num2 = 0;
                    int deviceInterfaceDetailDataSize = 2048;
                    if (Acer_USB_Library.SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref structure, ref sP_DEVICE_INTERFACE_DETAIL_DATA, deviceInterfaceDetailDataSize, ref num2, ref structure2))
                    {
                        uSBController.ControllerDevicePath = sP_DEVICE_INTERFACE_DETAIL_DATA.DevicePath;
                        int num3 = 0;
                        int num4 = 1;
                        if (Acer_USB_Library.SetupDiGetDeviceRegistryProperty(deviceInfoSet, ref structure2, 0, ref num4, intPtr, 2048, ref num3))
                        {
                            uSBController.ControllerDeviceDesc = Marshal.PtrToStringAuto(intPtr);
                        }
                        if (Acer_USB_Library.SetupDiGetDeviceRegistryProperty(deviceInfoSet, ref structure2, 9, ref num4, intPtr, 2048, ref num3))
                        {
                            uSBController.ControllerDriverKeyName = Marshal.PtrToStringAuto(intPtr);
                        }
                    }
                    list.Add(uSBController);
                }
                num++;
            }
            while (flag);
            Marshal.FreeHGlobal(intPtr);
            Acer_USB_Library.SetupDiDestroyDeviceInfoList(deviceInfoSet);
        }
        return new ReadOnlyCollection<USBController>(list);
    }

    private static string GetDescriptionByKeyName(string DriverKeyName)
    {
        string result = "";
        string enumerator = "USB";
        IntPtr deviceInfoSet = Acer_USB_Library.SetupDiGetClassDevs(0, enumerator, IntPtr.Zero, 6);
        if (deviceInfoSet.ToInt32() != -1)
        {
            IntPtr intPtr = Marshal.AllocHGlobal(2048);
            int num = 0;
            bool flag;
            do
            {
                SP_DEVINFO_DATA structure = default(SP_DEVINFO_DATA);
                structure.cbSize = Marshal.SizeOf(structure);
                flag = Acer_USB_Library.SetupDiEnumDeviceInfo(deviceInfoSet, num, ref structure);
                if (flag)
                {
                    int num2 = 0;
                    int num3 = 1;
                    string a = "";
                    if (Acer_USB_Library.SetupDiGetDeviceRegistryProperty(deviceInfoSet, ref structure, 9, ref num3, intPtr, 2048, ref num2))
                    {
                        a = Marshal.PtrToStringAuto(intPtr);
                    }
                    if (a == DriverKeyName)
                    {
                        if (Acer_USB_Library.SetupDiGetDeviceRegistryProperty(deviceInfoSet, ref structure, 0, ref num3, intPtr, 2048, ref num2))
                        {
                            result = Marshal.PtrToStringAuto(intPtr);
                        }
                        break;
                    }
                }
                num++;
            }
            while (flag);
            Marshal.FreeHGlobal(intPtr);
            Acer_USB_Library.SetupDiDestroyDeviceInfoList(deviceInfoSet);
        }
        return result;
    }

    private static string GetInstanceIDByKeyName(string DriverKeyName)
    {
        string result = "";
        string enumerator = "USB";
        IntPtr deviceInfoSet = Acer_USB_Library.SetupDiGetClassDevs(0, enumerator, IntPtr.Zero, 6);
        if (deviceInfoSet.ToInt32() != -1)
        {
            IntPtr intPtr = Marshal.AllocHGlobal(2048);
            int num = 0;
            bool flag;
            do
            {
                SP_DEVINFO_DATA structure = default(SP_DEVINFO_DATA);
                structure.cbSize = Marshal.SizeOf(structure);
                flag = Acer_USB_Library.SetupDiEnumDeviceInfo(deviceInfoSet, num, ref structure);
                if (flag)
                {
                    int num2 = 0;
                    int num3 = 1;
                    string a = "";
                    if (Acer_USB_Library.SetupDiGetDeviceRegistryProperty(deviceInfoSet, ref structure, 9, ref num3, intPtr, 2048, ref num2))
                    {
                        a = Marshal.PtrToStringAuto(intPtr);
                    }
                    if (a == DriverKeyName)
                    {
                        int num4 = 2048;
                        StringBuilder stringBuilder = new StringBuilder(num4);
                        Acer_USB_Library.SetupDiGetDeviceInstanceId(deviceInfoSet, ref structure, stringBuilder, num4, out num2);
                        result = stringBuilder.ToString();
                        break;
                    }
                }
                num++;
            }
            while (flag);
            Marshal.FreeHGlobal(intPtr);
            Acer_USB_Library.SetupDiDestroyDeviceInfoList(deviceInfoSet);
        }
        return result;
    }
}
