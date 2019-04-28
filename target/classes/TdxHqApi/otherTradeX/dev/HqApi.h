#ifndef __HQ_API_H
#define __HQ_API_H

#include <Windows.h>

#ifdef __cplusplus
extern "C" {
#endif

//1.行情API均是TdxHqApi.dll文件的导出函数，包括以下函数：(所有行情函数均为客户端主动请求查询，不是服务器推送)
//bool  TdxHq_Connect(char* IP, int Port, char* Result, char* ErrInfo);//连接券商行情服务器
//void  TdxHq_Disconnect();//断开服务器
//bool  TdxHq_GetSecurityCount(byte Market, short& Result, char* ErrInfo);//获取指定市场内的证券数目
//bool  TdxHq_GetSecurityList(byte Market, short Start, short& Count, char* Result, char* ErrInfo);//获取市场内指定范围内的所有证券代码
//bool  TdxHq_GetSecurityQuotes(byte Market[], char* Zqdm[], short& Count, char* Result, char* ErrInfo);//获取盘口五档报价
//bool  TdxHq_GetSecurityBars(byte Category, byte Market, char* Zqdm, short Start, short& Count, char* Result, char* ErrInfo);//获取股票K线
//bool  TdxHq_GetIndexBars(byte Category, byte Market, char* Zqdm, short Start, short& Count, char* Result, char* ErrInfo);//获取指数K线
//bool  TdxHq_GetMinuteTimeData(byte Market, char* Zqdm, char* Result, char* ErrInfo);//获取分时图数据
//bool  TdxHq_GetHistoryMinuteTimeData(byte Market, char* Zqdm, int date, char* Result, char* ErrInfo);//获取历史分时图数据
//bool  TdxHq_GetTransactionData(byte Market, char* Zqdm, short Start, short& Count, char* Result, char* ErrInfo);//获取分时成交
//bool  TdxHq_GetHistoryTransactionData(byte Market, char* Zqdm, short Start, short& Count, int date, char* Result, char* ErrInfo);//获取历史分时成交
//bool  TdxHq_GetCompanyInfoCategory(byte Market, char* Zqdm, char* Result, char* ErrInfo);//获取F10信息类别
//bool  TdxHq_GetCompanyInfoContent(byte Market, char* Zqdm, char* FileName, int Start, int Length, char* Result, char* ErrInfo);//获取F10信息内容
//bool  TdxHq_GetXDXRInfo(byte Market, char* Zqdm, char* Result, char* ErrInfo);//获取权息数据
//bool  TdxHq_GetFinanceInfo(byte Market, char* Zqdm, char* Result, char* ErrInfo);//获取财务数据

bool WINAPI TdxHq_Connect(
    const char *pszIP,
    short nPort,
    char *pszResult,
    char *pszErrInfo);

/*
bool WINAPI TdxHq_Reconnect(
    char *pszResult,
    char *pszErrInfo);
*/

void WINAPI TdxHq_Disconnect();

void WINAPI TdxHq_SetTimeout(
    int nReadTimeout,
    int nWriteTimeout);

bool WINAPI TdxHq_GetSecurityCount(
    char nMarket,
    short *nCount,
    char *pszErrInfo);

bool WINAPI TdxHq_GetSecurityList(
    char nMarket,
    short nStart,
    short *nCount,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_GetSecurityQuotes(
    const char nMarket[],
    const char *pszZqdm[],
    short *nCount,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_VB_GetSecurityQuotes(
    const char nMarket[],
    VARIANT VBpStrArray,
    short *nCount,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_GetSecurityBars(
    char nCategory,
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *nCount,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_GetIndexBars(
    char nCategory,
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *nCount,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_GetMinuteTimeData(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_GetHistoryMinuteTimeData(
    char nMarket,
    const char *pszZqdm,
    int nDate,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_GetTransactionData(
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *nCount,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_GetHistoryTransactionData(
    char nMarket,
    const char *pszZqdm,
    short nStart,
    short *nCount,
    int date,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_GetCompanyInfoCategory(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_GetCompanyInfoContent(
    char nMarket,
    const char *pszZqdm,
    const char *pszFileName,
    int nStart,
    int nLength,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_GetXDXRInfo(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);

bool WINAPI TdxHq_GetFinanceInfo(
    char nMarket,
    const char *pszZqdm,
    char *pszResult,
    char *pszErrInfo);

#ifdef __cplusplus
}
#endif

#endif
