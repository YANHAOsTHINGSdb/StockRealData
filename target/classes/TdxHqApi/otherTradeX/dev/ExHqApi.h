#ifndef __EXHQ_API_H
#define __EXHQ_API_H

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
bool WINAPI TdxExHq_Connect(
    const char *pszIP,
    short nPort,
    char *pszResult,
    char *pszErrInfo);

//
// 断开服务器
//
void WINAPI TdxExHq_Disconnect();

//
//
//
void WINAPI TdxExHq_SetTimeout(
    int nReadTimeout,
    int nWriteTimeout);

//
//
// 获取扩展行情中支持的各个市场的市场代码
//
// <param name="Result">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
// <param name="ErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
//
bool WINAPI TdxExHq_GetMarkets(
    char *pszResult,
    char *pszErrInfo);

//
// 获取所有品种的数目
//
bool WINAPI TdxExHq_GetInstrumentCount(
    int *nCount,
    char *pszErrInfo);

//
// 获取所有品种代码
//
bool WINAPI TdxExHq_GetInstrumentInfo(
    int nStart,
    short* pnCount,
    char* pszResult,
    char* pszErrInfo);

//
// 获取指定品种的盘口报价
//
bool WINAPI TdxExHq_GetInstrumentQuote(
    char nMarket,
    const char* pszZqdm,
    char* pszResult,
    char* pszErrInfo);

//
// 获取指定品种的K线数据
//
bool WINAPI TdxExHq_GetInstrumentBars(
    char nCategory,
    char nMarket,
    const char* pszZqdm,
    int nStart,
    short* pnCount,
    char* pszResult,
    char* pszErrInfo);

//
// 获取指定品种的分时图数据
//
bool WINAPI TdxExHq_GetMinuteTimeData(
    char nMarket,
    const char* pszZqdm,
    char* pszResult,
    char* pszErrInfo);

//
// 获取指定品种的分时图数据
//
bool WINAPI TdxExHq_GetHistoryMinuteTimeData(
    char nMarket,
    const char* pszZqdm,
    int nDate,
    char* pszResult,
    char* pszErrInfo);

//
// 获取指定品种的分时成交数据
//
bool WINAPI TdxExHq_GetTransactionData(
    char nMarket,
    const char* pszZqdm,
    int nStart,
    short* pnCount,
    char* pszResult,
    char* pszErrInfo);

//
// 获取指定品种的历史分时成交数据
//
bool WINAPI TdxExHq_GetHistoryTransactionData(
    char nMarket,
    const char* pszZqdm,
    int nDate,
    int nStart,
    short* pnCount,
    char* pszResult,
    char* pszErrInfo);

#ifdef __cplusplus
}
#endif

#endif
