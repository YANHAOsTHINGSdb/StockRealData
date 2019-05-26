package com.mycompany.myapp.service.impl;

import java.util.List;
import java.util.Map;

public class 交易策略Service {
	public static List<Map> 自选股List;

	public static void main(String[] args) {
		// TODO 自動生成されたメソッド・スタブ

	}
	public static String 取得可实现的资金数(String s投资组号){
		//-------------------------------------------------
		// 1、要找【在库信息Service.取得在库筹码信息by投资组号】
		//    在库筹码信息 = 股票代号+筹码数量
		// 2、利用【实时行情Service.取得实时行情by股票代号】股票代号=1的取得结果
		//    取得买一到买五的 数量与价格
		// 3、用买一到买五的信息计算可实现的资金数


		// 1、要找【在库信息Service.取得在库筹码信息by投资组号】
		Map<String, String> 在库筹码信息 = 在库信息Service.取得在库筹码信息by投资组号(s投资组号);
		// 2、利用【实时行情Service.取得实时行情by股票代号】股票代号=1的取得结果
		String s股票代号 = 在库筹码信息.get("股票代号");
		String s市场代码 = 在库筹码信息.get("市场代码");
		Map 买一到买五的数量与价格 = 实时行情Service.取得实时行情by股票代号(s股票代号, s市场代码);
		// 3、用买一到买五的信息计算可实现的资金数
		String 未实现筹码信息 = new String ();
		String 可实现的资金数 = 可实现价值Service.取得可实现的资金数by买一到买五的信息(买一到买五的数量与价格, 在库筹码信息, 未实现筹码信息);

		return 可实现的资金数;

	}

	public static String 取得当前可利用现金数(String s投资组号){
		//-------------------------------------------------
		// 4、要找【在库信息Service.取得在库现金信息by投资组号】
		// 5、取得【当前可利用现金】 = 3 + 4
		//-------------------------------------------------
		String 可实现的资金数 = 取得可实现的资金数(s投资组号);
		// 4、要找【在库信息Service.取得在库现金信息by投资组号】
		String 在库现金数 = 在库信息Service.取得在库现金信息by投资组号(s投资组号);
		// 5、取得【当前可利用现金】 = 3 + 4
		return Float.parseFloat(可实现的资金数)+ Float.parseFloat(在库现金数) + "";
	}

	public static String 取得可购入筹码数量(String s投资组号, String 要购入的股票代码) {


		// 6、利用【实时行情Service.取得实时行情by股票代号】股票代号=参数：s投资组号
		//    取得卖一到卖五的 数量与价格
		// 7、取得可购入筹码数量by当前可利用现金 【当前可利用现金】
		//-------------------------------------------------

		// 1、要找【在库信息Service.取得在库筹码信息by投资组号】
		Map<String, String> 在库筹码信息 = 在库信息Service.取得在库筹码信息by投资组号(s投资组号);
		// 2、利用【实时行情Service.取得实时行情by股票代号】股票代号=1的取得结果
		String s股票代号 = 在库筹码信息.get("股票代号");
		String s市场代码 = 在库筹码信息.get("市场代码");

		// 5、取得【当前可利用现金】 = 3 + 4
		String 当前可利用现金 = 取得当前可利用现金数(s投资组号);

		// 6、利用【实时行情Service.取得实时行情by股票代号】股票代号=参数：s投资组号
		 Map 卖一到卖五的数量与价格 = 实时行情Service.取得实时行情by股票代号(s股票代号, s市场代码);

		// 7、取得可购入筹码数量by当前可利用现金 【当前可利用现金】
		 String 未利用现金信息 = new String ();
		 String 可购入筹码数量 = 可实现价值Service.取得可购入筹码数量by买一到买五的信息(卖一到卖五的数量与价格, 当前可利用现金, 未利用现金信息);

		return 可购入筹码数量;
	}

	public static String 取得在库折算后筹码数量(String s投资组号) {
		//-------------------------------------------------
		// 1、先取得该组的在库筹码信息
		//    1.1 在库股票代码
		//    1.2 在库股票筹码数
		// 2、取得折算成本价 = 个股中最低开盘价
		// 3、return 在库折算后筹码数量
		//	  3.1 个股实时价=【实时行情Service.取得实时行情by股票代号】股票代号=在库股票代码
		//    3.2 固定比例 = 个股实时价 / 取得折算成本价
		//    3.3 在库折算后筹码数量 = 在库股票筹码数 * 固定比例
		//-------------------------------------------------

		// 1、先取得该组的在库筹码信息
		Map<String, String> 在库筹码信息 = 在库信息Service.取得在库筹码信息by投资组号(s投资组号);
		String s股票代号 = 在库筹码信息.get("股票代号");
		String s市场代码 = 在库筹码信息.get("市场代码");

		String s在库股票筹码数 = 在库筹码信息.get("在库股票筹码数");

		// 2、取得折算成本价 = 个股中最低开盘价
		String s折算成本价 = 交易策略Service.取得折算成本价by投资组号(s投资组号);

		// 3、return 在库折算后筹码数量
		// 		3.1 个股实时价=【实时行情Service.取得实时行情by股票代号】股票代号=在库股票代码
		Map<String, String> 个股实时信息 = 实时行情Service.取得实时行情by股票代号(s股票代号, s市场代码);
		String s个股实时价 = 个股实时信息.get("最新价格");
		float 固定比例 = Float.parseFloat(s个股实时价) / Float.parseFloat(s折算成本价);
		float 在库折算后筹码数量 =Float.parseFloat(s在库股票筹码数) * 固定比例;
		return 在库折算后筹码数量 + "";
	}

	/**
	 *
	 * @param s投资组号
	 * @return
	 */
	public static String 取得折算成本价by投资组号(String s投资组号) {
		// 取得折算成本价 = 个股中最低开盘价
		// 取得实时行情Service.实时行情Map

		// 1、根据 s投资组号 取得 对应的 自选股信息Map
		// 2、在实时行情中，遍历 【自选股】对应的行情信息
		//------------------------------
		//    2.1 在其中找到 最低开盘价
		//------------------------------
		// 3、返回最低开盘价
		String s最低开盘价 = "0";
		for( Map 自选股Map:自选股List) {
			String s开盘价 = 实时行情Service.取得开盘价_by股票代码(自选股Map);
			if (Float.parseFloat(s开盘价) > Float.parseFloat(s最低开盘价)) {
				s最低开盘价 = s开盘价;
			}
		}

		return s最低开盘价;
	}

	/**
	 *
	 * @param 可购入筹码数量
	 * @param 在库折算后筹码数量
	 * @return
	 */
	public static String 取得可实现盈余筹码数量(String 可购入筹码数量, String 在库折算后筹码数量) {
		// 可实现盈余筹码数量 = 可购入筹码数量 - 在库折算后筹码数量;
		Integer 可实现盈余筹码数量 = Integer.parseInt(可购入筹码数量) - Integer.parseInt(在库折算后筹码数量);
		return 可实现盈余筹码数量 + "" ;
	}
}
