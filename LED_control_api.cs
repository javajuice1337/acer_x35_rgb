// Acer_Class.LED_Control_API
using System.Runtime.InteropServices;

internal class LED_Control_API
{
    public const int MAX_LED_NUM = 20;

    public const int HEALTH_ASSISTANT = 0;

    public const int SCREEN_AMBIENT_LIGHT = 1;

    public const int CONTROL_LED_NUM = 20;

    public const int ALL_SYNC = 1;

    public const int NON_ALL_SYNC = 0;

    public const int LIGHTING_STYLE_DISABLE = 0;

    public const int LIGHTING_STYLE_NO_ANIMATION = 1;

    public const int LIGHTING_STYLE_BREATHING = 2;

    public const int LIGHTING_STYLE_FLASHING = 3;

    public const int LIGHTING_STYLE_DOUBLE_FLASHING = 4;

    public const int LIGHTING_STYLE_MARQUEES = 5;

    public const int LIGHTING_STYLE_METEOR = 6;

    public const int LIGHTING_STYLE_RAINBOW = 7;

    public const int LIGHTING_STYLE_POP = 8;

    public const int LIGHTING_STYLE_JAZZ = 9;

    public const int LIGHTING_STYLE_PLAY = 10;

    public const int LIGHTING_STYLE_MOVIE = 11;

    public const int LIGHTING_STYLE_DYNAMIC = 12;

    public const int SPEED_LEVEL_LOW = 0;

    public const int SPEED_LEVEL_MIDDLE = 1;

    public const int SPEED_LEVEL_HIGH = 2;

    public const int BRIGHTNES_LEVEL_20 = 20;

    public const int BRIGHTNES_LEVEL_40 = 40;

    public const int BRIGHTNES_LEVEL_60 = 60;

    public const int BRIGHTNES_LEVEL_80 = 80;

    public const int BRIGHTNES_LEVEL_100 = 100;

    public const int LOL_R = 0;

    public const int LOL_G = 50;

    public const int LOL_B = 50;

    public const int COMFORTABLE_LED_R = 18;

    public const int COMFORTABLE_LED_G = 24;

    public const int COMFORTABLE_LED_B = 8;

    public const int RELAX_LED_R = 9;

    public const int RELAX_LED_G = 18;

    public const int RELAX_LED_B = 21;

    public const int WARM_R = 24;

    public const int WARM_G = 14;

    public const int WARM_B = 2;

    public const int POWERFUL_R = 31;

    public const int POWERFUL_G = 1;

    public const int POWERFUL_B = 1;

    public const int RAINBOW_COLOR_MAX_NUM = 7;

    public const int init_R = 128;

    public const int init_G = 128;

    public const int init_B = 128;

    public const int AYY_MAX_LED_NUM = 18;

    public const int AYY_LightEffect_Static = 3;

    public const int AYY_LightEffect_Breathing = 6;

    public const int AYY_LightEffect_Flash = 7;

    public const int AYY_LightEffect_Wave = 0;

    public const int AYY_LightEffect_Shifting = 4;

    public const int AYY_LightEffect_Off = 8;

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Connect_to_LED_Controller();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Acer_Get_Connect_LED_Count();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_Five_Region_Profile(int IsAll_Sync, int Select_LED_BAR);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Set_Region1_Profile(int Lighting_style, int R, int G, int B, int Speed_Level, int Brightness_Level);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Set_Region2_Profile(int Lighting_style, int R, int G, int B, int Speed_Level, int Brightness_Level);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Set_Region3_Profile(int Lighting_style, int R, int G, int B, int Speed_Level, int Brightness_Level);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Set_Region4_Profile(int Lighting_style, int R, int G, int B, int Speed_Level, int Brightness_Level);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Set_Region5_Profile(int Lighting_style, int R, int G, int B, int Speed_Level, int Brightness_Level);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Set_Target_LED_RGB_Profile(int R, int G, int B, int LED_index);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Get_Target_LED_RGB_Profile(ref int R, ref int G, ref int B, int LED_index);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Init_Individual_Control(int Select_LED_BAR);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_25_LEDs_RGB(int Select_LED_BAR);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_50_LEDs_RGB(int Select_LED_BAR);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Close_LED_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Init_LED_Comport();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Close_LED_Comport();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Control_Region1(int R, int G, int B, int speed, int brightness, int style);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Control_Region2(int R, int G, int B, int speed, int brightness, int style);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Connect_to_Archos_Controller();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_Archos_RGB_Mouse(int lighting_style, int R, int G, int B, int Brightness, int Speed, int Random_Color, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Close_Archos_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Connect_to_Predator_Keyboard_Controller();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_Predator_Keyboard_2(int lighting_style, int R, int G, int B, int Brightness, int Speed, int Rainbow_Color, int duration, int direction, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_Predator_Keyboard_1(int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_Predator_Keyboard_3(int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_Predator_Keyboard_4(int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Close_Predator_Keyboard_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Acer_Get_Predator_LED_Count();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Connect_to_Lumic_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Acer_Get_Connect_Lumic_Count();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_Lumic_Global_Control(int lighting_style, int R, int G, int B, int Brightness, int Speed, int Direction, int Duration, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Close_Lumic_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Set_Lumic_Individual_Control_Profile(int R, int G, int B, int LED_index);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_Lumic_Individual_Control_Profile(int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Set_SubG_Profile(int style, int R, int G, int B, int brightness, int speed, int direction, int zone0, int zone1);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Apply_to_SubG();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Connect_to_K92_Controller();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Close_K92_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_K92_Lighting_Control(int R, int G, int B, int HI_Byte, int Low_Byte, int length, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Acer_Get_K92_Target_HID_Index();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_K92_LED_Brightness_Control(int brightness, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_K92_LED_Speed_Control(int speed_level, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_K92_LED_Light_Effect_Control(int Light_Effect, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_K92_LED_Color_Control(int R, int G, int B, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GK300_LED_Enable_Color_Control(int Enable, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GK300_Light_Bar_Disable(int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GK300_Light_Bar_Color(int R, int G, int B, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Connect_to_GM310_Controller();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Close_GM310_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM310_Lighting_Control(int[] R, int[] G, int[] B, int HI_Byte, int Low_Byte, int length, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Acer_Get_GM310_Target_HID_Index();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM310_Lighting_Global_Control(int R, int G, int B, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM310_LED_Brightness_Control(int brightness, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM310_LED_Light_Effect_Control(int Light_Effect, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM310_LED_Speed_Control(int speed_level, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM310_LED_Enable_Color_Control(int Enable, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM310_LED_Color_Control(int R, int G, int B, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM310_Switch_to_Device_Control(int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Connect_to_GM300_Controller();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Acer_Get_GM300_Target_HID_Index();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Close_GM300_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM300_LED_Light_Effect_Control(int Light_Effect, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM300_LED_Speed_Control(int speed_level, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM300_LED_Enable_Color_Control(int Enable, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM300_LED_Color_Control(int R, int G, int B, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM300_LED_Brightness_Control(int brightness, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM300_Indivisual_Lighting_Control(int[] R, int[] G, int[] B, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GM300_Lighting_Global_Control(int R, int G, int B, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Connect_to_X35_Controller();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Close_X35_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_X35_Enable_Lighting_Control(int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_X35_Region_1(int lighting_style, int R, int G, int B, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_X35_Region_2(int lighting_style, int R, int G, int B, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_X35_Enable_Two_Region_Control(int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_X35_Two_Region_RGB(int R1, int G1, int B1, int R2, int G2, int B2, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Set_X35_SMBus_BitRate(int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Detect_Lighting_Device(ushort VID, ushort PID);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Connect_to_GH300_Controller();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Acer_Close_GH300_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GH300_Globa_Control(int LED_strip_index, int LED_num, int lighting_style, int R, int G, int B, int Brightness, int Speed, int Direction, int Duration, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GH300_Individual_Control(int LED_strip_index, int LED_num, int[] R, int[] G, int[] B, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_TurnOff_GH300_LED(int LED_strip_index, int LED_num, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Connect_to_GB300_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern void Acer_Close_GB300_Control();

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GB300_Global_Control(int lighting_style, int control_zone, int R, int G, int B, int Brightness, int Speed, int Direction, int Duration, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_GB300_Individual_Control_Profile(int control_zone, int[] R, int[] G, int[] B, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Apply_Tx_Control(int style, int R, int G, int B, int brightness, int speed, int direction, int ID_0, int ID_1, int ID_2, int ID_3, int zone0, int zone1, int HID_device_select);

    [DllImport("LED_contor_function_x86", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Acer_Tx_Rx_Switch(int Tx_Rx_Switch, int HID_device_select);
}
