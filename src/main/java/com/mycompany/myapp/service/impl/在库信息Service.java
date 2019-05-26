package com.mycompany.myapp.service.impl;

import java.util.Map;

public class 在库信息Service {


	/**
	 在库信息Map
		key=	投资组ID
		value=	投资组在库信息Map
			    |-------------在库资金，在库资金Map
										|----现金数量，value
			    |-------------在库筹码，在库筹码Map
										|----股票代码，value
										|----筹码数量，value
	 */
	static Map<String, Map> 在库信息Map;

	/**
	 *
	 *
	 *
	 *
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO 自動生成されたメソッド・スタブ

	}

	public static Map<String, String> 取得在库筹码信息by投资组号(String s投资组号) {
		// 在【在库信息Map】中用key = 投资组号
		// 取得【投资组在库信息Map】
		// 返回 【投资组在库信息Map】中的【在库筹码map】
		Map 投资组在库信息Map = 在库信息Map.get(s投资组号);
		return (Map<String, String>) 投资组在库信息Map.get("在库筹码");
	}

	public static String 取得在库现金信息by投资组号(String s投资组号) {
		// 在【在库信息Map】中用key = 投资组号
		// 取得【投资组在库信息Map】
		// 返回 【投资组在库信息Map】中的【在库筹码map】
		Map 投资组在库信息Map = 在库信息Map.get(s投资组号);
		Map 在库资金Map = (Map) 投资组在库信息Map.get("现金数量");
		return (String) 在库资金Map.get("现金数量");
	}

}
