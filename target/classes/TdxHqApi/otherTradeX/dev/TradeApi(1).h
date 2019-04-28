#ifndef __TRADE_API_H
#define __TRADE_API_H

#include <Windows.h>

#define MAX_RESULT_SIZE   0x8010
#define MAX_ERRINFO_SIZE  1024

#ifdef __cplusplus
extern "C" {
#endif

void WINAPI OpenTdx();

void WINAPI CloseTdx();

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

void WINAPI Logoff(int nClientID);

bool WINAPI IsConnectOK(int nClientID);

void WINAPI QueryData(
    int nClientID,
    int nCategory,
    char* pszResult,
    char* pszErrInfo);

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

void WINAPI CancelOrder(
    int nClientID,
    const char* pszExchangeID,
    const char* pszhth,
    char* pszResult,
    char* pszErrInfo);

void WINAPI CancelOrderEx(
    int nClientID,
    const char* pszExchangeID,
    const char* pszhth,
    const char* pszGddm,
    char* pszResult,
    char* pszErrInfo);

void WINAPI GetQuote(
    int nClientID,
    const char* pszZqdm,
    char* pszResult,
    char* pszErrInfo);

void WINAPI GetTradableQuantity(
    int nClientID,
    int nCategory,
    int nPriceType,
    const char *pszGddm,
    const char *pszZqdm,
    float fPrice,
    char *pszResult,
    char *pszErrInfo);

void WINAPI Repay(
    int nClientID,
    const char* pszAmount,
    char* pszResult,
    char* pszErrInfo);

//
//
void WINAPI QueryHistoryData(
    int nClientID,
    int nCategory,
    const char* pszStartDate,
    const char* pszEndDate,
    char* pszResult,
    char* pszErrInfo);

void WINAPI QueryDatas(
    int nClientID,
    int nCategory[],
    int nCount,
    char* pszResult[],
    char* pszErrInfo[]);

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

void WINAPI CancelOrders(
    int nClientID,
    const char* pszExchangeID[],
    const char* pszhth[],
    int nCount,
    char* pszResult[],
    char* pszErrInfo[]);

void WINAPI GetQuotes(
    int nClientID,
    const char* pszZqdm[],
    int nCount,
    char* pszResult[],
    char* pszErrInfo[]);

//
//
//

int WINAPI QuickIPO(int nClientID);

int WINAPI QuickIPODetail(
    int nClientID,
    int nCount,
    char* pszResultOK[],
    char* pszResultFail[],
    char* pszErrInfo);

//
// reverse repos
//
void WINAPI ReverseRepos(
    int nClientID,
    int nCategory,
    int nPriceType,
    const char* pszGddm,
    const char* pszZqdm,
    float fPrice,
    int nQuantity,
    char* pszResult,
    char* pszErrInfo);

#ifdef __cplusplus
}
#endif

#endif
