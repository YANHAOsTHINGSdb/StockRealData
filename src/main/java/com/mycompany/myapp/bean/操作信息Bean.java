package com.mycompany.myapp.bean;

import java.util.Map;

import lombok.Data;

@Data
public class 操作信息Bean {
	/**
	 操作信息Map
	  |-- 操作       ：买
	  |-- 操作最高价格 ：3.24
	  |-- 操作最低价格 ：3.24
	  |-- 指数代码    ：600734
	  |-- 操作股数    ：500手
	 */
	Map 买操作信息Map;

	Map 卖操作信息Map;
}
