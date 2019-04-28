package TdxHqApi;

import com.sun.jna.Library;
import com.sun.jna.Native;
import com.sun.jna.ptr.ShortByReference;



public class hqApiJava
{
	public interface TdxHqLibrary extends Library 
	{
		void TdxHq_Disconnect();
		boolean  TdxHq_Connect(String IP, int Port, byte[] Result, byte[] ErrInfo);
		boolean  TdxHq_GetSecurityBars(byte Category, byte Market, String Zqdm, short Start, ShortByReference Count, byte[] Result, byte[] ErrInfo);
		boolean  TdxHq_GetIndexBars(byte Category, byte Market, String Zqdm, short Start, ShortByReference Count, byte[] Result, byte[] ErrInfo);
		boolean  TdxHq_GetMinuteTimeData(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
		boolean  TdxHq_GetHistoryMinuteTimeData(byte Market, String Zqdm, int date,byte[] Result, byte[] ErrInfo);
		boolean  TdxHq_GetTransactionData(byte Market,String Zqdm, short Start, ShortByReference Count, byte[] Result, byte[] ErrInfo);
		boolean  TdxHq_GetHistoryTransactionData(byte Market, String Zqdm, short Start, ShortByReference Count, int date, byte[] Result, byte[] ErrInfo);
		boolean  TdxHq_GetSecurityQuotes(byte[] Market, String[] Zqdm, ShortByReference Count, byte[] Result, byte[] ErrInfo);
		boolean  TdxHq_GetCompanyInfoCategory(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
		boolean  TdxHq_GetCompanyInfoContent(byte Market, String Zqdm, String FileName, int Start, int Length, byte[] Result, byte[] ErrInfo);
		boolean  TdxHq_GetXDXRInfo(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
		boolean  TdxHq_GetFinanceInfo(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
                boolean  TdxHq_GetSecurityCount(byte Market, ShortByReference Result, byte[] ErrInfo);
                boolean  TdxHq_GetSecurityList(byte Market, short Start, ShortByReference  Count, byte[] Result, byte[] ErrInfo);
	}
	
	public static void main(String[] args) 
	{
		try
		{
			//DLL是32位的,因此必须使用jdk32位开发,才能调用DLL; 
			//必须把TdxHqApi.dll复制到java工程目录下; 
			//java工程必须添加引用 jna.jar, 在 https://github.com/twall/jna 下载 jna.jar 
			//无论用什么语言编程，都必须仔细阅读VC版内的关于DLL导出函数的功能和参数含义说明，不仔细阅读完就提出问题者因时间精力所限，恕不解答。
			System.setProperty("jna.library.path", "D:\\pleiades\\workspace\\StockData\\src\\main\\java\\TdxHqApi\\");
			TdxHqLibrary TdxHqLibrary1 = (TdxHqLibrary)Native.loadLibrary("TdxHqApi",TdxHqLibrary.class);
			
			
			byte[] Result = new byte[65535];
			byte[] ErrInfo = new byte[256];

			

			boolean boolean1=TdxHqLibrary1.TdxHq_Connect("61.49.50.190", 7709, Result, ErrInfo);
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println(Native.toString(Result, "GBK"));
			
		
			byte[] Market={0,1};
			String[] Zqdm={"000750","600030"};
			ShortByReference Count=new ShortByReference();
			Count.setValue((short)2);
			boolean1 =TdxHqLibrary1.TdxHq_GetSecurityQuotes(Market,  Zqdm,  Count, Result, ErrInfo);
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println(Native.toString(Result, "GBK"));
			
			
			
			ShortByReference Count2=new ShortByReference();
			Count2.setValue((short)20);
			boolean1=TdxHqLibrary1.TdxHq_GetIndexBars((byte)0,(byte)1,"000001",(short) 0, Count2, Result, ErrInfo);
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println(Native.toString(Result, "GBK"));
			
			
			
			ShortByReference Count3=new ShortByReference();
			Count3.setValue((short)80);
			boolean1=  TdxHqLibrary1.TdxHq_GetTransactionData((byte)0, "000001",(short) 0, Count3, Result, ErrInfo);
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println(Native.toString(Result, "GBK"));
			
			
			
			
			boolean1= TdxHqLibrary1.TdxHq_GetCompanyInfoContent((byte)0,"000001", "000001.txt", 0, 10240, Result, ErrInfo);
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println(Native.toString(Result, "GBK"));
			
			
			
			TdxHqLibrary1.TdxHq_Disconnect();
			
			System.out.println("�ѶϿ�����");
		}
		catch(Exception e)
		{
			
		}
	}
}
