package com.mycompany.myapp.service.impl;

import com.mycompany.myapp.bean.操作信息Bean;

public class 收益Service {

	String 增加股数;
	String 增加现金数;

	public static void main(String[] args) {
		// TODO 自動生成されたメソッド・スタブ

	}

	public static boolean is有可实现利益(String s投资组号, 操作信息Bean 当前操作信息Bean) {
		String s成本股数 = 成本Service.取得成本信息_根据投资组号(s投资组号);
		String s可实现股数 = 可实现价值Service.取得可实现价值信息_根据投资组号(s投资组号);

		// 当前操作信息操作信息怎么取得。
		// 就是在什么股票，在什么地方买，要买多少
		//      什么股票，在什么地方卖，要卖多少
		// 要卖的股票一定是从【成本Service】中取得（股票代码，数量，价格）
		// 要买的股票一定是从【可实现价值Service】中取得（股票代码，数量，价格）
		当前操作信息Bean.set卖操作信息Map(成本Service.取得卖操作信息Map(s投资组号));
		当前操作信息Bean.set买操作信息Map(可实现价值Service.取得实操作信息Map(s投资组号));

		if(Float.parseFloat(s可实现股数) > Float.parseFloat(s成本股数)){
			return true;
		}
		return false;
	}

}
