package TdxHqApi;

import com.sun.jna.Callback;
import com.sun.jna.Library;
import com.sun.jna.Native;
import com.sun.jna.ptr.ShortByReference;
import com.sun.jna.win32.StdCallLibrary;



public class TradeXApiJava
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
        
/**---------------------TdxHqLibrary.dll
TdxHq_Connect
TdxHq_Disconnect
TdxHq_GetCompanyInfoCategory
TdxHq_GetCompanyInfoContent
TdxHq_GetFinanceInfo
TdxHq_GetHistoryMinuteTimeData
TdxHq_GetHistoryTransactionData
TdxHq_GetIndexBars
TdxHq_GetMinuteTimeData
TdxHq_GetSecurityBars
TdxHq_GetSecurityCount
TdxHq_GetSecurityList
TdxHq_GetSecurityQuotes
TdxHq_GetTransactionData
TdxHq_GetXDXRInfo
 */
	}
	
	/**---------------------TradeX.dll
	CancelOrder
	CancelOrders
	CloseTdx
	GetQuote
	GetQuotes
	Logoff
	Logon
	OpenTdx
	QueryData
	QueryDatas
	QueryHistoryData
	Repay
	SendOrder
	SendOrders
	TdxHq_Connect
	TdxHq_Disconnect
	TdxHq_GetCompanyInfoCategory
	TdxHq_GetCompanyInfoContent
	TdxHq_GetFinanceInfo
	TdxHq_GetHistoryMinuteTimeData
	TdxHq_GetHistoryTransactionData
	TdxHq_GetIndexBars
	TdxHq_GetMinuteTimeData
	TdxHq_GetSecurityBars
	TdxHq_GetSecurityCount
	TdxHq_GetSecurityList
	TdxHq_GetSecurityQuotes
	TdxHq_GetTransactionData
	TdxHq_GetXDXRInfo
	 */
	public interface TradeX extends StdCallLibrary 
	{
//		void TdxExHq_Disconnect();
//  bool WINAPI   TdxHq_Connect(const char *pszIP,short nPort,char *pszResult,char *pszErrInfo);
		boolean  TdxHq_Connect(String IP, int Port, byte[] Result, byte[] ErrInfo);
//		boolean  TdxExHq_Connect(String IP, int Port, byte[] Result, byte[] ErrInfo);
//  void WINAPI   TdxHq_Disconnect();

//		boolean  TdxExHq_GetSecurityBars(byte Category, byte Market, String Zqdm, short Start, ShortByReference Count, byte[] Result, byte[] ErrInfo);
//		boolean  TdxExHq_GetIndexBars(byte Category, byte Market, String Zqdm, short Start, ShortByReference Count, byte[] Result, byte[] ErrInfo);
//		boolean  TdxExHq_GetMinuteTimeData(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
//		boolean  TdxExHq_GetHistoryMinuteTimeData(byte Market, String Zqdm, int date,byte[] Result, byte[] ErrInfo);
//		boolean  TdxExHq_GetTransactionData(byte Market,String Zqdm, short Start, ShortByReference Count, byte[] Result, byte[] ErrInfo);
//		boolean  TdxExHq_GetHistoryTransactionData(byte Market, String Zqdm, short Start, ShortByReference Count, int date, byte[] Result, byte[] ErrInfo);
//		boolean  TdxExHq_GetSecurityQuotes(byte[] Market, String[] Zqdm, ShortByReference Count, byte[] Result, byte[] ErrInfo);
//		boolean  TdxExHq_GetCompanyInfoCategory(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
//		boolean  TdxExHq_GetCompanyInfoContent(byte Market, String Zqdm, String FileName, int Start, int Length, byte[] Result, byte[] ErrInfo);
//		boolean  TdxExHq_GetXDXRInfo(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
//		boolean  TdxExHq_GetFinanceInfo(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
////  bool WINAPI   TdxExHq_GetSecurityCount(char nMarket, short *nCount, char *pszErrInfo);
//        boolean  TdxExHq_GetSecurityCount(byte Market, ShortByReference Result, byte[] ErrInfo);
////  bool WINAPI   TdxExHq_GetSecurityList(char nMarket, short nStart, short *nCount, char *pszResult, char *pszErrInfo);        
//        boolean  TdxExHq_GetSecurityList(byte Market, short Start, ShortByReference  Count, byte[] Result, byte[] ErrInfo);
		
/**
CancelOrder
CancelOrderEx
CancelOrders
CloseTdx
GetQuote
GetQuotes
GetTradableQuantity
IsConnectOK
Logoff
OpenTdx
QueryData
QueryDatas
QueryHistoryData
QuickIPO
QuickIPODetail
Repay
ReverseRepos
SendOrder
SendOrders
TdxExHq_Connect
TdxExHq_Disconnect
TdxExHq_GetHistoryMinuteTimeData
TdxExHq_GetHistoryTransactionData
TdxExHq_GetInstrumentBars
TdxExHq_GetInstrumentCount
TdxExHq_GetInstrumentInfo
TdxExHq_GetInstrumentQuote
TdxExHq_GetMarkets
TdxExHq_GetMinuteTimeData
TdxExHq_GetTransactionData
TdxExHq_SetTimeout
TdxHq_Connect
TdxHq_Disconnect
TdxHq_GetCompanyInfoCategory
TdxHq_GetCompanyInfoContent
TdxHq_GetFinanceInfo
TdxHq_GetHistoryMinuteTimeData
TdxHq_GetHistoryTransactionData
TdxHq_GetIndexBars
TdxHq_GetMinuteTimeData
TdxHq_GetSecurityBars
TdxHq_GetSecurityCount
TdxHq_GetSecurityList
TdxHq_GetSecurityQuotes
TdxHq_GetTransactionData
TdxHq_GetXDXRInfo
TdxHq_SetTimeout
TdxHq_VB_GetSecurityQuotes
TdxL2Hq_Disconnect
TdxL2Hq_GetBuySellQueue
TdxL2Hq_GetCompanyInfoCategory
TdxL2Hq_GetCompanyInfoContent
TdxL2Hq_GetDetailOrderData
TdxL2Hq_GetDetailOrderDataEx
TdxL2Hq_GetDetailTransactionData
TdxL2Hq_GetDetailTransactionDataEx
TdxL2Hq_GetFinanceInfo
TdxL2Hq_GetHistoryMinuteTimeData
TdxL2Hq_GetHistoryTransactionData
TdxL2Hq_GetIndexBars
TdxL2Hq_GetMinuteTimeData
TdxL2Hq_GetSecurityBars
TdxL2Hq_GetSecurityCount
TdxL2Hq_GetSecurityList
TdxL2Hq_GetSecurityQuotes
TdxL2Hq_GetSecurityQuotes10
TdxL2Hq_GetTransactionData
TdxL2Hq_GetXDXRInfo
TdxL2Hq_SetTimeout
Logon
LogonEx
TdxL2Hq_Connect

 */
	}
	
	public interface ServerErrorEvent extends Callback
	{
	    void invoke(long serverConnectionHandlerID, String errorMessage, int error, String returnCode, String extraMessage);
	}
	
	public static void main(String[] args) 
	{
		try
		{
			//DLL是32位的,因此必须使用jdk32位开发,才能调用DLL; 
			//必须把TdxHqApi.dll复制到java工程目录下; 
			//java工程必须添加引用 jna.jar, 在 https://github.com/twall/jna 下载 jna.jar 
			//无论用什么语言编程，都必须仔细阅读VC版内的关于DLL导出函数的功能和参数含义说明，不仔细阅读完就提出问题者因时间精力所限，恕不解答。
		System.setProperty("jna.library.path", "D:\\pleiades\\workspace\\StockData\\src\\main\\java\\TdxHqApi\\otherTradeX\\");
	//		System.setProperty("jna.library.path", "D:\\pleiades\\workspace\\StockData\\src\\main\\java\\TdxHqApi\\");
			TradeX TdxHqLibrary1 = (TradeX)Native.loadLibrary("TradeX",TradeX.class);
			
			
			byte[] Result = new byte[65535];
			byte[] ErrInfo = new byte[256];

			

//			boolean boolean1=TdxHqLibrary1.TdxExHq_Connect("61.49.50.190", 7709, Result, ErrInfo);
			boolean boolean1=TdxHqLibrary1.TdxHq_Connect("61.49.50.190", 7709, Result, ErrInfo);;
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println(Native.toString(Result, "GBK"));
			
		
//			byte[] Market={0,1};
//			String[] Zqdm={"000750","600030"};
//			ShortByReference Count=new ShortByReference();
//			Count.setValue((short)2);
//			boolean1 =TdxHqLibrary1.TdxHq_GetSecurityQuotes(Market,  Zqdm,  Count, Result, ErrInfo);
//			if (!boolean1)
//			{
//				System.out.println(Native.toString(ErrInfo, "GBK"));
//				return;
//			}
//			System.out.println(Native.toString(Result, "GBK"));
//			
//			
//			
//			ShortByReference Count2=new ShortByReference();
//			Count2.setValue((short)20);
//			boolean1=TdxHqLibrary1.TdxHq_GetIndexBars((byte)0,(byte)1,"000001",(short) 0, Count2, Result, ErrInfo);
//			if (!boolean1)
//			{
//				System.out.println(Native.toString(ErrInfo, "GBK"));
//				return;
//			}
//			System.out.println(Native.toString(Result, "GBK"));
//			
//			
//			
//			ShortByReference Count3=new ShortByReference();
//			Count3.setValue((short)80);
//			boolean1=  TdxHqLibrary1.TdxHq_GetTransactionData((byte)0, "000001",(short) 0, Count3, Result, ErrInfo);
//			if (!boolean1)
//			{
//				System.out.println(Native.toString(ErrInfo, "GBK"));
//				return;
//			}
//			System.out.println(Native.toString(Result, "GBK"));
//			
//			
//			
//			
//			boolean1= TdxHqLibrary1.TdxHq_GetCompanyInfoContent((byte)0,"000001", "000001.txt", 0, 10240, Result, ErrInfo);
//			if (!boolean1)
//			{
//				System.out.println(Native.toString(ErrInfo, "GBK"));
//				return;
//			}
//			System.out.println(Native.toString(Result, "GBK"));
//			
//			
//			
//			TdxHqLibrary1.TdxHq_Disconnect();
			
			System.out.println("�ѶϿ�����");
		}
		catch(Exception e)
		{
			
		}
	}
}
