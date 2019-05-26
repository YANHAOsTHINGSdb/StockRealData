package com.mycompany.myapp.service;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang.StringUtils;

import lombok.Data;

@Data
public class 文件db {

	// String sPath = "C:\\tmp";  // for windows
	String sPath = "/Users/haoyan/Desktop/data";  // for mac
	// String sPath = "";
	String sSubFolder = null;
	Map<String, Map> map_data = new HashMap();

	public void 情報読み込み(String[] fileName) {

		for (int i = 0; i < fileName.length; i++) {
			String sf = fileName[i] + ".txt";
			sf =  this.sPath.concat(sf);
			try {
				//InputStream in = 文件db.class.getResourceAsStream(sf);
				File file = new File(sf);
				//InputStream in = 文件db.class.getClassLoader().getResourceAsStream(sf);
				Map<String, String> map = new HashMap<String, String>();

				if (checkBeforeReadfile(file)) {
				//if (in != null && in.available() >= 0) {
					// if (checkBeforeReadfile(in)) {
					BufferedReader br = new BufferedReader(new FileReader(sf));
					// BufferedReader br = new BufferedReader(new InputStreamReader(in, "UTF-8"));
					String str;
					while ((str = br.readLine()) != null) {
						if (StringUtils.isEmpty(str)) {
							continue;
						}
						String[] reslut = StringUtils.split(str, ",");
						String skey = reslut[0];
						String svalue = reslut[1];

						map.put(skey, svalue);
					}
					System.out.println(map.toString());
					br.close();
					map_data.put(fileName[i], map);
					// in.close();
				} else {
					System.out.println("ファイル読み込めません："+ sf);
				}
			} catch (FileNotFoundException e) {
				System.out.println(e);
			} catch (IOException e) {
				System.out.println(e);
			}
		}
	}

	private static boolean checkBeforeReadfile(File file) {
		if (file.exists()) {
			if (file.isFile() && file.canRead()) {
				return true;
			}
		}

		return false;
	}

	public void 文件書込(String path,String 書込内容 ){
		PrintWriter pw;
		try {
			//pw = new PrintWriter(new BufferedWriter(new FileWriter(path,true)));
			System.out.println(path);
			pw = new PrintWriter(
					new BufferedWriter(
							new FileWriter(
									path, true)));
			pw.println(書込内容);
			pw.close();
		} catch (IOException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		}

	}

	public int 採番(String sPath) throws IOException {
		//这里定义一个字符流的输入流的节点流，用于读取文件（一个字符一个字符的读取）
		System.out.println(sPath);
		FileReader fr = new FileReader(sPath);
		BufferedReader br = new BufferedReader(fr);// 在定义好的流基础上套接一个处理流，用于更加效率的读取文件（一行一行的读取）
		int x = 0; 								// 用于统计行数，从0开始
		while (br.readLine() != null) { 		// readLine()方法是按行读的，返回值是这行的内容
			x++; 								// 每读一行，则变量x累加1
		}
		br.close();
		fr.close();
		return x; 								//返回总的行数
	}

	public List<String> 取得全部ID(String string) {
		List<String> IDList = new ArrayList();
		Map<String, String> 小map = map_data.get(string);
		if(小map == null) return null;
		for (Map.Entry<String, String> entry : 小map.entrySet()) {
			String strKey= entry.getKey().replace("\\", "");
			IDList.add(strKey);
		}
		return IDList;
	}

	public String 取得指定情报_by項目名_ID(String s項目名, String id) {
		String s指定情报 = null;
		Map<String, String> 小map = map_data.get(s項目名);
		if(小map == null) return null;
		for (Map.Entry<String, String> entry : 小map.entrySet()) {
			String strKey= entry.getKey().replace("\\", "");
			if(StringUtils.equals(strKey, id)){
				return entry.getValue().replace("\\", "");
			}
		}
		return null;
	}


	public 文件db(String aSub) {
		sSubFolder = aSub;
		if( sSubFolder != null) {
			this.sPath = this.sPath.concat("/").concat(sSubFolder).concat("/");
		}
	}

/*	public List<String> 取得全員番号ID(){
		List<String> IDList = new ArrayList();
		Map<String, String> 小map = map_data.get("番号");
		for (Map.Entry<String, String> entry : 小map.entrySet()) {
			String strKey= entry.getKey().replace("\\", "");
			IDList.add(strKey);
		}
		return IDList;
	}

	public String 取得社員ID_by社員番号(String s番号){
		List<String> IDList = new ArrayList();
		Map<String, String> 小map = map_data.get("番号");
		if(小map==null) return null;
		for (Map.Entry<String, String> entry : 小map.entrySet()) {
			if(StringUtils.equals(s番号, entry.getValue())) {
				return entry.getKey().replace("\\", "");
			}
		}
		return null;
	}

	public List<String> 取得全ID_by項目名(String s項目名) {
		List<String> IDList = new ArrayList();
		Map<String, String> 小map = map_data.get(s項目名);
		for (Map.Entry<String, String> entry : 小map.entrySet()) {
			String strKey= entry.getKey().replace("\\", "");
			IDList.add(strKey);
		}
		return IDList;
	}*/

}
