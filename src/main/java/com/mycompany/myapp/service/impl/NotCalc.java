package com.mycompany.myapp.service.impl;

import java.util.ArrayList;
import java.util.List;

public class NotCalc implements Calc {

	/**
	 * 返回两个List的Not集
	 */
	public List 論理計算(List list1, List list2) {

		List local_List1 = new ArrayList(list1);
		List local_List2 = new ArrayList(list2);

		// 取两个结果集的共同部分
		// 例, A ={1,2,3} B={2,3,4,5}
        //     計算結果 = {1}
		local_List1.removeAll(local_List2);

		return local_List1;
	}

}
