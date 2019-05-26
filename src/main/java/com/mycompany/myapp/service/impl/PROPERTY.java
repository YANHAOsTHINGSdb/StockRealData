package com.mycompany.myapp.service.impl;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.Properties;

public class PROPERTY {
	static Properties properties = null;

	public static String 取得指定信息(String s指定信息){
		InputStream input = null;
		/**
		 * StockDataCaller
		 * |
		 * |----bin
		 * |	|----recv
		 * |		 |----PROPERTY.class
		 * |----src
		 * |	|----recv
		 * |		 |----PROPERTY.java
		 * |
		 * |----stockInfo.property
		 */
		// 这个是从主函数所在地址参照的吗？？
		String spath="./Property.property" ;
		if(properties != null) {
			return properties.getProperty(s指定信息);
		}else {
			properties = new Properties();
		}

		try {
			// input = new FileInputStream(spath);//加载Java项目根路径下的配置文件
			BufferedReader in = new BufferedReader(
					   new InputStreamReader(
			                      new FileInputStream(spath), "UTF8"));
			properties.load(in);// 加载属性文件
 			return properties.getProperty(s指定信息);
		} catch (IOException io) {
		} finally {
			if (input != null) {
				try {
					input.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
		return null;
	}

	public static String 取得入力文件路径() {
		return 取得指定信息("入力文件路径");
	}

	public static String 取得sh出力目录() {
		return 取得指定信息("sh出力目录");
	}

	public static String 取得sz出力目录() {
		return 取得指定信息("sz出力目录");
	}

	public static String 取得IP() {
		return 取得指定信息("IP");
	}

	public static int 取得Port() {
		return Integer.parseInt(取得指定信息("Port"));
	}


}
