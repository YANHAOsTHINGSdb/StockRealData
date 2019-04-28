#ifndef __L2HQ_API_H
#define __L2HQ_API_H

#include <Windows.h>

#ifdef __cplusplus
extern "C" {
#endif

//
// 行情API均是TdxHqApi.dll文件的导出函数，所有行情函数均为客户端主动请求查询，不是服务器推送
//

//
// 连接券商行情服务器
//
bool WINAPI TdxL2Hq_Connect(
    const char *pszIP,
    short nPort,
    const char *pszL2User,
    const char *pszL2Password,
    char *pszResult,
    char *pszErrInfo);

//
// 断开服务器
//
void WINAPI TdxL2Hq_Disconnect();

//
//
//
void WINAPI TdxL2Hq_SetTimeout(
    int nReadTimeout,
    int nWriteTimeout);

//
// 获取指定市场内的证券数目
//
bool WINAPI TdxL2Hq_GetSecurityCount(
    char nMarket,
    short *pnCount,
    char *pszErrInfo);

//
// 获取市场内指定范围内的所有证券代码
//
bool WINAPI TdxL2Hq_GetSecurityList(
    char nMarket,
    short nStart,
    short *pnCount,
    char *pszResult,
    char *pszErrInfo);

//
// 获取盘口五档报价
//
bool WINAPI TdxL2Hq_GetSecurityQuotes(
    const char nMarket[],
    const char *pszZqdm[],
    short *pnCount,
    char *pszResult,
    char *pszErrInfo);

//
// 获取股票K线
//
bool WINAPI TdxL2Hq_GetSecurityBars(
    char nCategory,
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *pnCount,
    char *pszResult,
    char *pszErrInfo);

//
// 获取指数K线
//
bool WINAPI TdxL2Hq_GetIndexBars(
    char nCategory,
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *pnCount,
    char *pszResult,
    char *pszErrInfo);

//
// 获取分时图数据
//
bool WINAPI TdxL2Hq_GetMinuteTimeData(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);

//
//获取历史分时图数据
//
bool WINAPI TdxL2Hq_GetHistoryMinuteTimeData(
    char nMarket,
    const char *pszZqdm,
    int nDate,
    char *pszResult,
    char *pszErrInfo);

//
// 获取分时成交
//
bool WINAPI TdxL2Hq_GetTransactionData(
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *pnCount,
    char *pszResult,
    char *pszErrInfo);

//
// 获取历史分时成交
//
bool WINAPI TdxL2Hq_GetHistoryTransactionData(
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *pnCount,
    int date,
    char *pszResult,
    char *pszErrInfo);

//
// 获取F10信息类别
//
bool WINAPI TdxL2Hq_GetCompanyInfoCategory(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);

//
// 获取F10信息内容
//
bool WINAPI TdxL2Hq_GetCompanyInfoContent(
    char nMarket,
    const char *pszZqdm,
    const char *pszFileName,
    int nStart,
    int nLength,
    char *pszResult,
    char *pszErrInfo);

//
// 获取权息数据
//
bool WINAPI TdxL2Hq_GetXDXRInfo(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);

//
// 获取财务数据
//
bool WINAPI TdxL2Hq_GetFinanceInfo(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);

//
// 获取十挡报价
//
bool WINAPI TdxL2Hq_GetSecurityQuotes10(
    const char nMarket[],
    const char *pszZqdm[],
    short *pnCount,
    char* pszResult,
    char* pszErrInfo);

//
// 获取逐笔成交(从后往前)
//
bool WINAPI TdxL2Hq_GetDetailTransactionData(
    char nMarket,
    const char *pszZqdm,
    int nStart,
    short *pnCount,
    char* pszResult,
    char* pszErrInfo);

//
// 获取逐笔成交(从前往后)
//
bool WINAPI TdxL2Hq_GetDetailTransactionDataEx(
    char nMarket,
    const char *pszZqdm,
    int nStart,
    short *pnCount,
    char* pszResult,
    char* pszErrInfo);

//
// 获取逐笔委托(从后往前)
//
bool WINAPI TdxL2Hq_GetDetailOrderData(
    char nMarket,
    const char *pszZqdm,
    int nStart,
    short *pnCount,
    char* pszResult,
    char* pszErrInfo);

//
// 获取逐笔委托(从前往后)
//
bool WINAPI TdxL2Hq_GetDetailOrderDataEx(
    char nMarket,
    const char *pszZqdm,
    int nStart,
    short *pnCount,
    char* pszResult,
    char* pszErrInfo);

//
// 获取十挡报价
//
bool WINAPI TdxL2Hq_GetBuySellQueue(
    char nMarket,
    const char* pszZqdm,
    char* pszResult,
    char* pszErrInfo);

#ifdef __cplusplus
}
#endif

#endif
