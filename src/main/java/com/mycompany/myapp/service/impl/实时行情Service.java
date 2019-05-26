package com.mycompany.myapp.service.impl;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

public class 实时行情Service {
	// 实时行情Map
	//     |----key=时间 value=所有取到的股票信息Str
	//                           |----市场代码、股票代码、股票名称
	static LinkedHashMap<String, String> 实时行情Map;

	public static void main(String[] args) {
		// TODO 自動生成されたメソッド・スタブ

	}

	/**
	 *
	 * @param 自选股票map
	 * @return
	 */
	public static String 取得最新成交价格_by股票代码(Map 自选股票map) {

		String s股票代码 = (String) 自选股票map.get("股票代码");
		String s市场代码 = (String) 自选股票map.get("市场代码");
		return 取得最新成交价格_by股票代码(s股票代码, s市场代码);
	}

	/**
	 *
	 * @param s最新成交价格
	 * @param 自选股票map
	 * @param s投资组号
	 * @return
	 */
	 public static String 取得折算后的股价(String s最新成交价格, Map 自选股票map, String s投资组号) {

		// 自选股票代码 = 自选股票map.get("股票代码");
		// 取得 固定比率 根据 自选股票代码
		// 取得 折算后的股价 = 最新成交价格 * 固定比率
		 String s折算成本价 = 交易策略Service.取得折算成本价by投资组号(s投资组号);
		 float f最新成交价格 = Float.parseFloat(s最新成交价格);
		 float f固定比例 = f最新成交价格 / Float.parseFloat(s折算成本价);

		return f最新成交价格 / f固定比例 + "";
	}

	/**
	 *
	 * @param 股票代号map
	 * 				|----股票代号
	 * 				|----股票名称
	 * 				|----市场代码
	 * @return
	 */
	public static Map 取得实时行情by股票代号(Map 股票代号map) {

		// 【在已经取得的实时行情中找最新的数据】
		// 准备分个线程专门坐取数据的工作
		// 取得实时行情，其实就是在已经取得的数据中找最新的行情

		// 每次取得的实时行情信息是个大字符串
		// 都有一个key=时间，value=取到的个股汇总的行情信息

		// 取得最后时间对应的信息，
		// 取出其中的个股信息，
		// 转成List<Map>
		//      |   |----个股信息
		//      |--------所有取到的股票信息

		// 从中取得满足条件的个股信息map
		// 返回找到的map


		String s股票代号 = (String) 股票代号map.get("股票代号");
		String s市场代码 = (String) 股票代号map.get("市场代码");
		return 取得实时行情by股票代号(s股票代号, s市场代码);
	}

	/**
	 *
	 * @param key
	 * @param value
	 */
	static void 取得最新成交信息(String key, String value){

		Map map = 实时行情Map;
		Field tail;
		try {
			tail = map.getClass().getDeclaredField("tail");
            tail.setAccessible(true);
            Map.Entry<String, String> entry=(Map.Entry<String, String>) tail.get(map);
             key = entry.getKey();
             value = entry.getValue();

		} catch (NoSuchFieldException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		} catch (SecurityException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		} catch (IllegalArgumentException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		}

	}

	/**
	 *
	 * @param s股票代码
	 * @param s市场代码
	 * @return
	 */
	public static Map 取得实时行情by股票代号(String s股票代码, String s市场代码) {

		// 【在已经取得的实时行情中找最新的数据】
		// 准备分个线程专门坐取数据的工作
		// 取得实时行情，其实就是在已经取得的数据中找最新的行情

		// 每次取得的实时行情信息是个大字符串
		// 都有一个key=时间，value=取到的个股汇总的行情信息

		// 取得最后时间对应的信息，
		// 取出其中的个股信息，
		// 转成List<Map>
		//      |   |----个股信息
		//      |--------所有取到的股票信息

		// 从中取得满足条件的个股信息map
		// 返回找到的map

		String s成交时间 =new String();
		String s成交信息 =new String();
		List<String> 成交信息List =new ArrayList();

		取得最新成交信息(s成交时间, s成交信息);

		String 个股成交信息[] = s成交信息.split("\n");
		for(int o=1; o<个股成交信息.length; o++ ) {
			Map 自选股map = 交易策略Service.自选股List.get(o-1);
			if(s股票代码.equals((String)自选股map.get("股票代码")) && s市场代码.equals((String)自选股map.get("市场代码"))) {
				String 个股信息明细[] =个股成交信息[o].split("\t");
				Map 个股实时行情Map = new HashMap();
				个股实时行情Map.put("", 个股信息明细[0]);
				/**
				 * 此处省略500字
				 */
				return 个股实时行情Map;
			}
		}

		return null;
	}

	/**
	 *
	 * @param s股票代码
	 * @param s市场代码
	 * @return
	 */
	public static String 取得最新成交价格_by股票代码(String s股票代码, String s市场代码) {
		String s成交时间 =new String();
		String s成交信息 =new String();
		List<String> 成交信息List =new ArrayList();

		取得最新成交信息(s成交时间, s成交信息);

		String 个股成交信息[] = s成交信息.split("\n");
		for(int o=1; o<个股成交信息.length; o++ ) {
			Map 自选股map = 交易策略Service.自选股List.get(o-1);
			if(s股票代码.equals((String)自选股map.get("股票代码")) && s市场代码.equals((String)自选股map.get("市场代码"))) {
				String 个股信息明细[] =个股成交信息[o].split("\t");
				return 个股信息明细[o];
			}
		}
		return null;
	}

	/**
	 * 取得开盘价_by股票代码
	 * @param 自选股Map
	 * @return
	 */
	public static String 取得开盘价_by股票代码(Map 自选股Map) {
		return (String) 取得实时行情by股票代号(自选股Map).get("开盘价");
	}

}
