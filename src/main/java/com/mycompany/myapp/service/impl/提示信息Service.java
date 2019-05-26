package com.mycompany.myapp.service.impl;

import java.util.List;

import com.mycompany.myapp.bean.操作信息Bean;

public class 提示信息Service {

	/**
	 操作信息Map
	  |-- 操作       ：买
	  |-- 操作最高价格 ：3.24
	  |-- 操作最低价格 ：3.24
	  |-- 指数代码    ：600734
	  |-- 操作股数    ：500手
	  |-- 投资组号    ：1组
	  |-- 操作开始时间 ：20190515 09:01:10

	 */
	static List<操作信息Bean> 操作信息MapList;


	public static void main(String[] args) {
		// TODO 自動生成されたメソッド・スタブ

	}

	/**
	 * 每次取得操作信息。
	 * 如果确实存在可操作的信息，
	 * 就提示给用户
	 */
	public 操作信息Bean 取得操作信息(String s投资组号) {
		操作信息Bean 当前操作信息Bean = new 操作信息Bean();
		// 查询现在时刻是否有可操作的信息
		if(is有可实现利益(s投资组号, 当前操作信息Bean)) {
			// 如果有
			// 就将信息存入【操作信息MapList】
			操作信息MapList.add(当前操作信息Bean);
			// 并返回【当前操作信息MapList】给前台
			return 当前操作信息Bean;
		}
		// 存储【操作信息】
		return null;
	}
	/**
	 * 查询现在时刻是否有可操作的信息
		// 1 先取得最新行情
		// 2 模拟交易
		// 3 看一看是不是盈利
		// 4 最后确定返回值true/false
		// 5 确定当前操作信息Bean的内容
	 * @param 当前操作信息Bean
	 * @return
	 */
	private boolean is有可实现利益(String s投资组号, 操作信息Bean 当前操作信息Bean) {
		// 1 先取得最新行情
		// 2 模拟交易
		// 3 取得收益信息
		// 4 最后确定返回值true/false
		// 5 确定当前操作信息Bean的内容
		if (收益Service.is有可实现利益(s投资组号, 当前操作信息Bean)) {
			// 确定当前操作信息Bean的内容

			return true;
		}

		return false;
	}

	/**
	 *
	 */
	void 取得可实现利益信息(){


	}
}
