#ifndef __TRADEX_H
#define __TRADEX_H

#include <Windows.h>

#ifdef __cplusplus
extern "C" {
#endif

//
// 交易API
//

//1.交易API均是Trade.dll文件的导出函数，包括以下函数：
//基本版的9个函数：
// void  OpenTdx();//打开通达信
// void  CloseTdx();//关闭通达信
//  int  Logon(char* IP, short Port, char* Version, short YybID, char* AccountNo,char* TradeAccount, char* JyPassword,   char* TxPassword, char* ErrInfo);//登录帐号
// void  Logoff(int ClientID);//注销
// void  QueryData(int ClientID, int Category, char* Result, char* ErrInfo);//查询各类交易数据
// void  SendOrder(int ClientID, int Category ,int PriceType,  char* Gddm,  char* Zqdm , float Price, int Quantity,  char* Result, char* ErrInfo);//下单
// void  CancelOrder(int ClientID, char* ExchangeID, char* hth, char* Result, char* ErrInfo);//撤单
// void  GetQuote(int ClientID, char* Zqdm, char* Result, char* ErrInfo);//获取五档报价
// void  Repay(int ClientID, char* Amount, char* Result, char* ErrInfo);//融资融券账户直接还款


//普通批量版新增的5个函数：(有些券商对批量操作进行了限速，最大批量操作数目请咨询券商)
// void  QueryHistoryData(int ClientID, int Category, char* StartDate, char* EndDate, char* Result, char* ErrInfo);//查询各类历史数据
// void  QueryDatas(int ClientID, int Category[], int Count, char* Result[], char* ErrInfo[]);//单账户批量查询各类交易数据
// void  SendOrders(int ClientID, int Category[] , int PriceType[], char* Gddm[],  char* Zqdm[] , float Price[], int Quantity[],  int Count, char* Result[], char* ErrInfo[]);//单账户批量下单
// void  CancelOrders(int ClientID, char* ExchangeID[], char* hth[], int Count, char* Result[], char* ErrInfo[]);//单账户批量撤单
// void  GetQuotes(int ClientID, char* Zqdm[], int Count, char* Result[], char* ErrInfo[]);//单账户批量获取五档报价


///交易接口执行后，如果失败，则字符串ErrInfo保存了出错信息中文说明；
///如果成功，则字符串Result保存了结果数据,形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。
///Result是\n，\t分隔的中文字符串，比如查询股东代码时返回的结果字符串就是 

///"股东代码\t股东名称\t帐号类别\t保留信息\n
///0000064567\t\t0\t\nA000064567\t\t1\t\n
///2000064567\t\t2\t\nB000064567\t\t3\t"

///查得此数据之后，通过分割字符串， 可以恢复为几行几列的表格形式的数据



//2.API使用流程为: 应用程序先调用OpenTdx打开通达信实例，一个实例下可以同时登录多个交易账户，每个交易账户称之为ClientID.
//通过调用Logon获得ClientID，然后可以调用其他API函数向各个ClientID进行查询或下单; 应用程序退出时应调用Logoff注销ClientID, 最后调用CloseTdx关闭通达信实例. 
//OpenTdx和CloseTdx在整个应用程序中只能被调用一次.API带有断线自动重连功能，应用程序只需根据API函数返回的出错信息进行适当错误处理即可。


//3. 各个函数功能说明

/// <summary>
/// 打开通达信实例
/// </summary>
void WINAPI OpenTdx();


/// <summary>
/// 关闭通达信实例
/// </summary>
void WINAPI CloseTdx();


/// <summary>
/// 交易账户登录
/// </summary>
/// <param name="pszIP">券商交易服务器IP</param>
/// <param name="nPort">券商交易服务器端口</param>
/// <param name="pszVersion">设置通达信客户端的版本号</param>
/// <param name="nYybID">营业部代码，请到网址 http://www.chaoguwaigua.com/downloads/qszl.htm 查询</param>
/// <param name="pszAccountNo">完整的登录账号，券商一般使用资金帐户或客户号</param>
/// <param name="pszTradeAccount">交易账号，一般与登录帐号相同. 请登录券商通达信软件，查询股东列表，股东列表内的资金帐号就是交易帐号, 具体查询方法请见网站“热点问答”栏目</param>
/// <param name="pszJyPassword">交易密码</param>
/// <param name="pszTxPassword">通讯密码</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>客户端ID，失败时返回-1</returns>
int WINAPI Logon(
    const char* pszIP,
    short nPort,
    const char* pszVersion,
    short nYybID,
    const char* pszAccountNo,
    const char* pszTradeAccount,
    const char* pszJyPassword,
    const char* pszTxPassword,
    char* pszErrInfo);


/// <summary>
/// 交易账户注销
/// </summary>
/// <param name="ClientID">客户端ID</param>
void WINAPI Logoff(int nClientID);


/// <summary>
/// 查询各种交易数据
/// </summary>
/// <param name="nClientID">客户端ID</param>
/// <param name="nCategory">表示查询信息的种类，0资金  1股份   2当日委托  3当日成交     4可撤单   5股东代码  6融资余额   7融券余额  8可融证券</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
/// <param name="pszErrInfo">同Logon函数的ErrInfo说明</param>
void WINAPI QueryData(
    int nClientID,
    int nCategory,
    char* pszResult,
    char* pszErrInfo);


/// <summary>
/// 查询各种历史数据
/// </summary>
/// <param name="nClientID">客户端ID</param>
/// <param name="nCategory">表示查询信息的种类，0历史委托  1历史成交   2交割单</param>
/// <param name="pszStartDate">表示开始日期，格式为yyyyMMdd,比如2014年3月1日为  20140301
/// <param name="pszEndDate">表示结束日期，格式为yyyyMMdd,比如2014年3月1日为  20140301
/// <param name="pszResult">同上</param>
/// <param name="pszErrInfo">同上</param>
void WINAPI QueryHistoryData(
    int nClientID,
    int nCategory,
    const char* pszStartDate,
    const char* pszEndDate,
    char* pszResult,
    char* pszErrInfo);


/// <summary>
/// 批量查询各种交易数据,用数组传入每个委托的参数，数组第i个元素表示第i个查询的相应参数
/// </summary>
/// <param name="nClientID">客户端ID</param>
/// <param name="nCategory">信息的种类的数组, 第i个元素表示第i个查询的信息种类，0资金  1股份   2当日委托  3当日成交     4可撤单   5股东代码  6融资余额   7融券余额  8可融证券</param>
/// <param name="nCount">查询的个数，即数组的长度</param>
/// <param name="pszResult">返回数据的数组, 第i个元素表示第i个委托的返回信息. 此API执行返回后，Result[i]含义同上。</param>
/// <param name="pszErrInfo">错误信息的数组，第i个元素表示第i个委托的错误信息. 此API执行返回后，ErrInfo[i]含义同上。</param>
void WINAPI QueryDatas(
    int nClientID,
    int nCategory[],
    int nCount,
    char* pszResult[],
    char* pszErrInfo[]);


/// <summary>
/// 下委托交易证券
/// </summary>
/// <param name="nClientID">客户端ID</param>
/// <param name="nCategory">表示委托的种类，0买入 1卖出  2融资买入  3融券卖出   4买券还券   5卖券还款  6现券还券</param>
/// <param name="nPriceType">表示报价方式 0上海限价委托 深圳限价委托 1(市价委托)深圳对方最优价格  2(市价委托)深圳本方最优价格  3(市价委托)深圳即时成交剩余撤销  4(市价委托)上海五档即成剩撤 深圳五档即成剩撤 5(市价委托)深圳全额成交或撤销 6(市价委托)上海五档即成转限价
/// <param name="pszGddm">股东代码, 交易上海股票填上海的股东代码；交易深圳的股票填入深圳的股东代码</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="fPrice">委托价格</param>
/// <param name="nQuantity">委托数量</param>
/// <param name="pszResult">同上,其中含有委托编号数据</param>
/// <param name="pszErrInfo">同上</param>
void WINAPI SendOrder(
    int nClientID,
    int nCategory,
    int nPriceType,
    const char* pszGddm,
    const char* pszZqdm,
    float fPrice,
    int nQuantity,
    char* pszResult,
    char* pszErrInfo);


/// <summary>
/// 批量下委托交易证券，用数组传入每个委托的参数，数组第i个元素表示第i个委托的相应参数
/// </summary>
/// <param name="nClientID">客户端ID</param>
/// <param name="nCategory">委托种类的数组，第i个元素表示第i个委托的种类，0买入 1卖出  2融资买入  3融券卖出   4买券还券   5卖券还款  6现券还券</param>
/// <param name="nPriceType">表示报价方式的数组,  第i个元素表示第i个委托的报价方式, 0上海限价委托 深圳限价委托 1(市价委托)深圳对方最优价格  2(市价委托)深圳本方最优价格  3(市价委托)深圳即时成交剩余撤销  4(市价委托)上海五档即成剩撤 深圳五档即成剩撤 5(市价委托)深圳全额成交或撤销 6(市价委托)上海五档即成转限价
/// <param name="pszGddm">股东代码数组，第i个元素表示第i个委托的股东代码，交易上海股票填上海的股东代码；交易深圳的股票填入深圳的股东代码</param>
/// <param name="pszZqdm">证券代码数组，第i个元素表示第i个委托的证券代码</param>
/// <param name="fPrice">委托价格数组，第i个元素表示第i个委托的委托价格</param>
/// <param name="nQuantity">委托数量数组，第i个元素表示第i个委托的委托数量</param>
/// <param name="nCount">委托的个数，即数组的长度</param>
/// <param name="pszResult">同上</param>
/// <param name="pszErrInfo">同上</param>
void WINAPI SendOrders(
    int nClientID,
    int nCategory[],
    int nPriceType[],
    const char* pszGddm[],
    const char* pszZqdm[],
    float fPrice[],
    int nQuantity[],
    int nCount,
    char* pszResult[],
    char* pszErrInfo[]);


/// <summary>
/// 撤委托
/// </summary>
/// <param name="nClientID">客户端ID</param>
/// <param name="pszExchangeID">交易所ID， 上海1，深圳0(招商证券普通账户深圳是2)</param>
/// <param name="pszhth">表示要撤的目标委托的编号</param>
/// <param name="pszResult">同上</param>
/// <param name="pszErrInfo">同上</param>
void WINAPI CancelOrder(
    int nClientID,
    const char* pszExchangeID,
    const char* pszhth,
    char* pszResult,
    char* pszErrInfo);


/// <summary>
/// 批量撤委托, 用数组传入每个委托的参数，数组第i个元素表示第i个撤委托的相应参数
/// </summary>
/// <param name="nClientID">客户端ID</param>
// <param name="pszExchangeID">交易所ID， 上海1，深圳0(招商证券普通账户深圳是2)</param>
/// <param name="pszhth">表示要撤的目标委托的编号</param>
/// <param name="nCount">撤委托的个数，即数组的长度</param>
/// <param name="pszResult">同上</param>
/// <param name="pszErrInfo">同上</param>
void WINAPI CancelOrders(
    int nClientID,
    const char* pszExchangeID[],
    const char* pszhth[],
    int nCount,
    char* pszResult[],
    char* pszErrInfo[]);


/// <summary>
/// 获取证券的实时五档行情
/// </summary>
/// <param name="nClientID">客户端ID</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="pszResult">同上</param>
/// <param name="pszErrInfo">同上</param>
void WINAPI GetQuote(
    int nClientID,
    const char* pszZqdm,
    char* pszResult,
    char* pszErrInfo);


/// <summary>
/// 批量获取证券的实时五档行情
/// </summary>
/// <param name="nClientID">客户端ID</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="nCount">证券的个数，即数组的长度</param>
/// <param name="pszResult">同上</param>
/// <param name="pszErrInfo">同上</param>
void WINAPI GetQuotes(
    int nClientID,
    const char* pszZqdm[],
    int nCount,
    char* pszResult[],
    char* pszErrInfo[]);


/// <summary>
/// 融资融券直接还款
/// </summary>
/// <param name="nClientID">客户端ID</param>
/// <param name="pszAmount">还款金额</param>
/// <param name="pszResult">同上</param>
/// <param name="pszErrInfo">同上</param>
void WINAPI Repay(
    int nClientID,
    const char* pszAmount,
    char* pszResult,
    char* pszErrInfo);



//
// 行情API
//

//1.行情API均是TradeX.dll文件的导出函数，包括以下函数：(所有行情函数均为客户端主动请求查询，不是服务器推送)

//2.API使用流程为: 应用程序先调用TdxHq_Connect连接通达信行情服务器,然后才可以调用其他接口获取行情数据,应用程序应自行处理网络断线问题, 接口是线程安全的

//3.各个函数功能说明

/// <summary>
///  连接通达信行情服务器,服务器地址可在券商软件登录界面中的通讯设置中查得
/// </summary>
/// <param name="pszIP">服务器IP</param>
/// <param name="nPort">服务器端口</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_Connect(
    const char *pszIP,
    short nPort,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 断开同服务器的连接
/// </summary>
void WINAPI TdxHq_Disconnect();


/// <summary>
/// 获取指定市场内的证券数目
/// </summary>
bool WINAPI TdxHq_GetSecurityCount(
    char nMarket,
    short *nCount,
    char *pszErrInfo);


/// <summary>
/// 获取指定市场内的证券列表
/// </summary>
bool WINAPI TdxHq_GetSecurityList(
    char nMarket,
    short nStart,
    short *nCount,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 获取证券的K线数据
/// </summary>
/// <param name="nCategory">K线种类, 0->5分钟K线    1->15分钟K线    2->30分钟K线  3->1小时K线    4->日K线  5->周K线  6->月K线  7->1分钟  8->1分钟K线  9->日K线  10->季K线  11->年K线< / param>
/// <param name="nMarket">市场代码,   0->深圳     1->上海</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="nStart">K线开始位置,最后一条K线位置是0, 前一条是1, 依此类推</param>
/// <param name="nCount">API执行前,表示用户要请求的K线数目, API执行后,保存了实际返回的K线数目, 最大值800</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_GetSecurityBars(
    char nCategory,
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *nCount,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 获取指数的K线数据
/// </summary>
/// <param name="nCategory">K线种类, 0->5分钟K线    1->15分钟K线    2->30分钟K线  3->1小时K线    4->日K线  5->周K线  6->月K线  7->1分钟  8->1分钟K线  9->日K线  10->季K线  11->年K线< / param>
/// <param name="nMarket">市场代码,   0->深圳     1->上海</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="nStart">K线开始位置,最后一条K线位置是0, 前一条是1, 依此类推</param>
/// <param name="nCount">API执行前,表示用户要请求的K线数目, API执行后,保存了实际返回的K线数目,最大值800</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_GetIndexBars(
    char nCategory,
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *nCount,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 获取分时数据
/// </summary>
/// <param name="nMarket">市场代码,   0->深圳     1->上海</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_GetMinuteTimeData(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 获取历史分时数据
/// </summary>
/// <param name="nMarket">市场代码,   0->深圳     1->上海</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="nDate">日期, 比如2014年1月1日为整数20140101</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_GetHistoryMinuteTimeData(
    char nMarket,
    const char *pszZqdm,
    int nDate,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 获取分时成交数据
/// </summary>
/// <param name="nMarket">市场代码,   0->深圳     1->上海</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="nStart">K线开始位置,最后一条K线位置是0, 前一条是1, 依此类推</param>
/// <param name="nCount">API执行前,表示用户要请求的K线数目, API执行后,保存了实际返回的K线数目</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_GetTransactionData(
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *nCount,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 获取历史分时成交数据
/// </summary>
/// <param name="nMarket">市场代码,   0->深圳     1->上海</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="nStart">K线开始位置,最后一条K线位置是0, 前一条是1, 依此类推</param>
/// <param name="nCount">API执行前,表示用户要请求的K线数目, API执行后,保存了实际返回的K线数目</param>
/// <param name="nDate">日期, 比如2014年1月1日为整数20140101</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_GetHistoryTransactionData(
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *nCount,
    int nDate,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 批量获取多个证券的五档报价数据
/// </summary>
/// <param name="nMarket">市场代码,   0->深圳     1->上海, 第i个元素表示第i个证券的市场代码</param>
/// <param name="pszZqdm">证券代码, Count个证券代码组成的数组</param>
/// <param name="nCount">API执行前,表示用户要请求的证券数目,最大290, API执行后,保存了实际返回的数目</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_GetSecurityQuotes(
    char nMarket[],
    const char *pszZqdm[],
    short *nCount,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 获取F10资料的分类
/// </summary>
/// <param name="nMarket">市场代码,   0->深圳     1->上海</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_GetCompanyInfoCategory(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 获取F10资料的某一分类的内容
/// </summary>
/// <param name="nMarket">市场代码,   0->深圳     1->上海</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="pszFileName">类目的文件名, 由TdxHq_GetCompanyInfoCategory返回信息中获取</param>
/// <param name="nStart">类目的开始位置, 由TdxHq_GetCompanyInfoCategory返回信息中获取</param>
/// <param name="nLength">类目的长度, 由TdxHq_GetCompanyInfoCategory返回信息中获取</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据,出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_GetCompanyInfoContent(
    char nMarket,
    const char *pszZqdm,
    const char *pszFileName,
    int nStart,
    int nLength,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 获取除权除息信息
/// </summary>
/// <param name="nMarket">市场代码,   0->深圳     1->上海</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据,出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_GetXDXRInfo(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);


/// <summary>
/// 获取财务信息
/// </summary>
/// <param name="nMarket">市场代码,   0->深圳     1->上海</param>
/// <param name="pszZqdm">证券代码</param>
/// <param name="pszResult">此API执行返回后，Result内保存了返回的查询数据,出错时为空字符串。</param>
/// <param name="pszErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
/// <returns>成功返货true, 失败返回false</returns>
bool WINAPI TdxHq_GetFinanceInfo(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);

#ifdef __cplusplus
}
#endif

#endif
