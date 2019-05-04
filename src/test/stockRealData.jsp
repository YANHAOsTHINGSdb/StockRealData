
<script>
<title>StockRealData</title>
<link rel="stylesheet" href="css/tabulator.css">
<link href="css/tabulator_midnight.min.css" rel="stylesheet">
<script type="text/javascript" src="js/jquery-3.3.1.js"></script>
<script type="text/javascript" src="js/jquery.json.js"></script>

<script type="text/javascript" src="js/jquery-ui.js"></script>
<script type="text/javascript" src="js/jquery-ui.min.js"></script>

<script type="text/javascript" src="js/tabulator.min.js"></script>
<script type="text/javascript" src="js/tabulator.js"></script>

<script lang="JavaScript">

		$(function() {
			var $m = $('body');
			var alpha = $m.data('モード');
			if(alpha == '1'){
				$("#search_btn").click();
			}

			$("#response").html("Response Values");
			// Ajax通信テスト ボタンクリック
			$("#ajax_btn").click(function() {
				// outputDataを空に初期化
				//$("#output_data").text("");

				var url = $("url_post").val();
				var JSONdata = {
					name_input : $("#name_input").val(),
					maleFemale_input : $("#maleFemale_input").val(),
					birthDate_input : $("#birthDate_input").val(),
					joinDate_input : $("#joinDate_input").val()
				};

				//alert(JSON.stringify(JSONdata));

				$.ajax({
					type : "GET",
					url : "http://localhost:8080/JavaMiddleClassCompleteSource/案件search",
					dataType : "json", //dataType设置成 json，这个意思是说 ’服务器的数据返回的是json格式数据，需要帮我把数据转换成对象
					data : JSON.stringify(JSONdata),
					scriptCharset : 'utf-8',
					success : function(data) {
						success(data);
					},

					error : function() {

						alert("AJAXの返す処理はERRORがあり by Yan");
					}
				});
			});

			$("#add_btn").click(function() {
				window.status = "処理中です。しばらくお待ちください。";
			});

			$("#search_btn").click(function() {

				$.ajax({
					type : 'POST',
					url : "http://localhost:8080/StockRealData/realData",
					dataType : "json", //dataType设置成 json，这个意思是说 ’服务器的数据返回的是json格式数据，需要帮我把数据转换成对象
					contentType : "application/json",
					data : “”,
					success : function(data) {
						success(data);
					},
					error : function(e) {
						console.log(e);
						alert("AJAXの検索処理はERRORがあり by Yan");
					}
				});
			});

		});


		function end(){
			
			$("automaticUpdateFlg").val('false');
			clearInterval(automaticUpdateTimer);

			executeAction('../PGSBLT14001/PGSBLT14001End.form');
		}

		// Ajax通信成功時処理
		function success(data) {
			/* 			$("#emlist").empty();
			 buildHtmlTable(data,$("#emlist")); */
			$("#example-table").tabulator({
				height : "311px",
				layout : "fitColumns",
				placeholder : "No Data Set",

				columns : [ {
					title : "股票代码",
					field : "股票代码",
					sorter : "string",
					sorter : "boolean",
					cellClick : function(e, cell) {
						oneRowClick(cell.getValue())
					}
				},{
					title : "股票名称",
					field : "股票名称",
					sorter : "string",
					sorter : "boolean",
					cellClick : function(e, cell) {
					    var row = cell.getRow();
					    var data = row.getData();
					    oneRowClick(data.s_ID);
					}
				}, {
					title : "开盘价",
					field : "开盘价",
					sorter : "string",
					width : 200,
					sorter : "boolean"
				}, {
					title : "最高价",
					field : "最高价",
					sorter : "string",
					width : 200,
					sorter : "boolean"
				}, {
					title : "最低价",
					field : "最低价",
					sorter : "string",
					sorter : "boolean"
				}, {
					title : "收盘价",
					field : "收盘价",
					sorter : "string",
					align : "left"
					}
				, {
					title : "成交量",
					field : "成交量",
					sorter : "date",
					align : "left"
					}
				, {
					title : "成交额",
					field : "成交额",
					sorter : "date",
					align : "left"
					}
				, {
					title : "买1价格",
					field : "买1价格",
					sorter : "date",
					align : "left"
					}
				, {
					title : "买1手数",
					field : "买1手数",
					sorter : "string",
					align : "right"
					}
				, {
					title : "买2价格",
					field : "买2价格",
					sorter : "date",
					align : "left"
					}
				, {
					title : "买2手数",
					field : "买2手数",
					sorter : "string",
					align : "right"
					}
				],
				rowClick : function(e, row) {
					/* alert("Row " + row.getIndex() + " Clicked!!!!"); */

				},
			});
			$("#example-table").tabulator("setData", data);
		}
</script>
</head>

<body class='l-np-base'>
<form name="theForm" id="theForm" method="get" action="http://localhost:8080/JavaMiddleClassCompleteSource/add案件">
	<h1>案件情報</h1>
		<br>
		<input type="" style="width:0px;height:0px;display:block;" onfocus="setMainPageFocus(0);" readonly="true">

		<input type="" style="width:0px;height:0px;display:block;" onfocus="setMainPageFocus(1);" readonly="true">

		<input type="hidden" name="automaticUpdateInterval" id="automaticUpdateInterval"/>
		<input type="hidden" name="automaticUpdateFlg" id="automaticUpdateFlg"/>
		<div>
			<input type="button" id="search_btn" value="検索"> <input type="submit" id="add_btn" value="追加">
		</div>
		<br>
		<div>
			<table id="emlist" style="width: 70%">
				<thead>
					<tr>
						<th></th>
					</tr>
				</thead>
				<tbody></tbody>
			</table>
		</div>
		<br>
		<div>
		    <div id="example-table"></div>
		</div>
