package com.mycompany.myapp.service.impl;

import java.util.HashMap;
import java.util.Map;

public class 成本Service {

	public static void main(String[] args) {
		// TODO 自動生成されたメソッド・スタブ

	}

	public static String 取得成本信息_根据投资组号(String s投资组号) {

		// 目的是为了取得折算后的成本股数
		// 背景：取得成本股数的目的是，
		//      是为了与【可实现股数】做对比
		//      被【收益Service.is有可实现利益】调用
		String 在库折算后筹码数量 = 交易策略Service.取得在库折算后筹码数量(s投资组号);
		return 在库折算后筹码数量;
	}

	public static Map 取得卖操作信息Map(String s投资组号) {
		// 要卖的股票一定是从【成本Service】中取得（股票代码，数量，价格）
		// 这个好说
		// 就是从【在库信息Service】中取得属于该【s投资组号】的所有筹码信息（股票代码，数量）
		// 再配上最新的【成交信息】

		// 【卖操作信息Map】
		//     |-----股票代码 = 在库信息Service.取得在库筹码信息by投资组号(String s投资组)
		//     |-----可卖出数量 = 交易策略Service.取得可购入筹码数量(String s投资组号, String 股票代码);
		//     |-----最低卖出价格 = 实时行情Service.取得最新成交价格_by股票代码(String 股票代码);


		// 卖操作信息Map
		Map<String, String> 卖操作信息Map = new HashMap<String, String>();
		String s在库股票代码 = 在库信息Service.取得在库筹码信息by投资组号(s投资组号).get("股票代码");
		卖操作信息Map.put("股票代码", s在库股票代码);
		卖操作信息Map.put("可卖出数量", 交易策略Service.取得可购入筹码数量( s投资组号,  s在库股票代码));
		卖操作信息Map.put("最低卖出价格", 实时行情Service.取得最新成交价格_by股票代码(s投资组号, s在库股票代码));
		return 卖操作信息Map;
	}

}
