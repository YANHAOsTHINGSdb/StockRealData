package com.mycompany.myapp.service.impl;

import java.util.List;

import com.sun.jna.Library;
import com.sun.jna.Native;
import com.sun.jna.ptr.ShortByReference;



public class TdxHqApi209912301
{
	public interface TdxHqApi2099 extends Library
	{
		//连接券商行情服务器
		boolean  TdxHq_Connect(String IP, int Port, byte[] Result, byte[] ErrInfo);
		//获取股票K线
		boolean  TdxHq_GetSecurityBars(byte Category, byte Market, String Zqdm, short Start, ShortByReference Count, byte[] Result, byte[] ErrInfo);
		/// <summary>
		 /// 获取指数的指定范围内K线数据
		/// </summary>
		 /// <param name="Category">K线种类,
		 //		0->5分钟K线
		 //		1->15分钟K线
		 //		2->30分钟K线
		 //		3->1小时K线
		 //		4->日K线
		 //		5->周K线
		 //		6->月K线
		 //		7->1分钟
		 //		8->1分钟K线
		 //		9->日K线
		 //		10->季K线
		 //		11->年K线< / param>
		 /// <param name="Market">市场代码, 0->深圳 1->上海</param>
		 /// <param name="Zqdm">证券代码</param>
		 /// <param name="Start">范围开始位置,最后一条K线位置是0, 前一条是1, 依此类推</param>
		 /// <param name="Count">范围的大小，API执行前,表示用户要请求的K线数目, API执行后,保存了实际返回的K线数目,最大值800</param>
		 /// <param name="Result">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
		 /// <param name="ErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
		 /// <returns>成功返货true, 失败返回false</returns>
		//获取指数K线
		boolean  TdxHq_GetIndexBars(byte Category, byte Market, String Zqdm, short Start, ShortByReference Count, byte[] Result, byte[] ErrInfo);
		//获取分时图数据
		boolean  TdxHq_GetMinuteTimeData(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
		//获取历史分时图数据
		boolean  TdxHq_GetHistoryMinuteTimeData(byte Market, String Zqdm, int date,byte[] Result, byte[] ErrInfo);
		//获取分时成交
		boolean  TdxHq_GetTransactionData(byte Market,String Zqdm, short Start, ShortByReference Count, byte[] Result, byte[] ErrInfo);
		//获取历史分时成交
		boolean  TdxHq_GetHistoryTransactionData(byte Market, String Zqdm, short Start, ShortByReference Count, int date, byte[] Result, byte[] ErrInfo);
		//获取盘口五档报价
		boolean  TdxHq_GetSecurityQuotes(byte[] Market, String[] Zqdm, ShortByReference Count, byte[] Result, byte[] ErrInfo);
		//获取F10信息类别
		boolean  TdxHq_GetCompanyInfoCategory(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
		//获取F10信息内容
		boolean  TdxHq_GetCompanyInfoContent(byte Market, String Zqdm, String FileName, int Start, int Length, byte[] Result, byte[] ErrInfo);
		//获取权息数据
		boolean  TdxHq_GetXDXRInfo(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
		//获取财务数据
		boolean  TdxHq_GetFinanceInfo(byte Market, String Zqdm, byte[] Result, byte[] ErrInfo);
		//获取指定市场内的证券数目
		boolean  TdxHq_GetSecurityCount(byte Market, ShortByReference Result, byte[] ErrInfo);
		//获取市场内指定范围内的所有证券代码
		boolean  TdxHq_GetSecurityList(byte Market, short Start, ShortByReference  Count, byte[] Result, byte[] ErrInfo);
		//断开服务器
		boolean  TdxHq_Disconnect();
    }
	static TdxHqApi2099 TdxHqApi20991230;
	public static void main(String[] args)
	{
		try
		{
			//DLL是32位的,因此必须使用jdk32位开发,才能调用DLL;
			//必须把TdxHqApi.dll复制到java工程目录下;
			//java工程必须添加引用 jna.jar, 在 https://github.com/twall/jna 下载 jna.jar
			//无论用什么语言编程，都必须仔细阅读VC版内的关于DLL导出函数的功能和参数含义说明，不仔细阅读完就提出问题者因时间精力所限，恕不解答。
			//System.setProperty("jna.library.path", "D:\\pleiades\\workspace\\StockDataConvert\\src\\InputData\\");
			System.setProperty("jna.library.path", ".\\src\\InputData\\");
			// NOT this: Native.loadLibrary("dlls/Library.dll", YourInterface.class)
			TdxHqApi2099 TdxHqLibrary1 = (TdxHqApi2099)Native.loadLibrary("TdxHqApi20991230.dll",TdxHqApi2099.class);


			byte[] Result = new byte[65535];
			byte[] ErrInfo = new byte[256];

			boolean boolean1=TdxHqLibrary1.TdxHq_Connect("61.49.50.190", 7709, Result, ErrInfo);
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println(Native.toString(Result, "GBK"));

			// 获取五档报价数据
			byte[] Market={0,0,1};
			String[] Zqdm={"399001", "000523", "000001"};
			ShortByReference Count=new ShortByReference();
			Count.setValue((short)3);
			boolean1 =TdxHqLibrary1.TdxHq_GetSecurityQuotes(Market,  Zqdm,  Count, Result, ErrInfo);
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println(Native.toString(Result, "GBK"));


			// 获取指数K线数据
			ShortByReference Count2=new ShortByReference();
			Count2.setValue((short)1);
			// 数据种类, 0->5分钟K线 1->15分钟K线 2->30分钟K线 3->1小时K线 4->日K线 5->周K线 6->月K线 7->1分钟K线 8->1分钟K线 9->日K线 10->季K线 11->年K线
			// boolean1=TdxHqLibrary1.TdxL2Hq_GetIndexBars((byte)4,(byte)1,"1A0001",(short) 0, Count2, Result, ErrInfo);
			boolean1=TdxHqLibrary1.TdxHq_GetIndexBars((byte)4,(byte)1,"000001",(short) 0, Count2, Result, ErrInfo);
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println(Native.toString(Result, "GBK"));


			// 获取分笔图数据
			ShortByReference Count3=new ShortByReference();
			Count3.setValue((short)80);
			boolean1=  TdxHqLibrary1.TdxHq_GetTransactionData((byte)0, "000001",(short) 0, Count3, Result, ErrInfo);
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println(Native.toString(Result, "GBK"));



			// 获取F10数据的某类别的内容
			boolean1= TdxHqLibrary1.TdxHq_GetCompanyInfoContent((byte)0,"000001", "000001.txt", 0, 10240, Result, ErrInfo);
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println(Native.toString(Result, "GBK"));

			// 获取市场内指定范围内的所有证券代码
			boolean1= TdxHqLibrary1.TdxHq_GetSecurityCount((byte)0, Count, ErrInfo);
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return;
			}
			System.out.println("获取指定市场内的证券数目 =".concat(Count+""));

			// 获取市场内指定范围内的所有证券代码
			// boolean  TdxHq_GetSecurityList(byte Market, short Start, ShortByReference  Count, byte[] Result, byte[] ErrInfo);
			boolean1= TdxHqLibrary1.TdxHq_GetSecurityList((byte)0,(short)0, Count, Result, ErrInfo);
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

	public static byte[] getGetSecurityQuotes(byte[] Market, String[] Zqdm, ShortByReference count, byte[] Result, byte[] ErrInfo) {
		// 这个函数的意义在于通多DLL
		// 取得股票信息
		// 并返回
		// 它不负责出力结果的整理，只负责取得指定的数据，并返回。

//		byte[] Market={0,1};
//		String[] Zqdm={"399001","000001"};
		if(Market.length != Zqdm.length || Market.length != count.getValue()) {
			count.setValue((short)Zqdm.length);
			Market = InputDataUtil.cutByteFromByte(Market,0,Zqdm.length);
		}
		boolean boolean1 =TdxHqApi20991230.TdxHq_GetSecurityQuotes(Market,  Zqdm,  count, Result, ErrInfo);
		if (!boolean1)
		{
			System.out.println(Native.toString(ErrInfo, "GBK"));
			return null;
		}
		System.out.println(Native.toString(Result, "GBK"));
		return Result;

	}

	public static boolean getGetSecurityCount(String Ip, byte[] Market, byte[] Zqdm) {

		byte[] Result = new byte[65535];
		byte[] ErrInfo = new byte[256];
		ShortByReference Count=new ShortByReference();
		boolean boolean1;
		// 获取市场内指定范围内的所有证券代码
		boolean1= TdxHqApi20991230.TdxHq_GetSecurityCount((byte)0, Count, ErrInfo);
		if (!boolean1)
		{
			System.out.println(Native.toString(ErrInfo, "GBK"));
			return false;
		}
		System.out.println("获取指定市场内的证券数目 =".concat(Count+""));
		return true;
	}


	public static boolean getGetSecurityList(byte Market, short start, ShortByReference Count, byte[] Result, byte[] ErrInfo, List<String[]> 股票代码ArrayList) {
		boolean boolean1;
		//List<String> 股票代码List=new ArrayList();

		ShortByReference ResultCount = new ShortByReference();
		boolean1= TdxHqApi20991230.TdxHq_GetSecurityCount(Market, ResultCount, ErrInfo);//获取指定市场内的证券数目
		int iCount = ResultCount.getValue();
		if (!boolean1) {
			System.out.println(Native.toString(ErrInfo, "GBK"));
			return false;
		}

		for(int i=0; i<iCount ; i=i+1000) {

			//byte[] Result = new byte[65535];
			//byte[] ErrInfo = new byte[256];
			//ShortByReference Count=new ShortByReference();

			// 获取市场内指定范围内的所有证券代码
			// boolean  TdxHq_GetSecurityList(byte Market, short Start, ShortByReference  Count, byte[] Result, byte[] ErrInfo);
			boolean1= TdxHqApi20991230.TdxHq_GetSecurityList(Market, (short)i, Count, Result, ErrInfo);
			//iCount = Count.getValue();
			if (!boolean1)
			{
				System.out.println(Native.toString(ErrInfo, "GBK"));
				return false;
			}
			System.out.println(Native.toString(Result, "GBK"));
			List<String[]>股票代码List1=InputDataUtil.getStringListByGBK3(Result);
			股票代码ArrayList.addAll(股票代码List1);
		}
		return true;
	}


	public static boolean getConnect(String IP, int Port, byte[] Result, byte[] ErrInfo) {

		System.setProperty("jna.library.path", ".\\src\\InputData\\");
		TdxHqApi20991230 = (TdxHqApi2099)Native.loadLibrary("TdxHqApi20991230.dll",TdxHqApi2099.class);
		return TdxHqApi20991230.TdxHq_Connect(IP, Port, Result, ErrInfo);
	}
}
