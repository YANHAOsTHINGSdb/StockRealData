// TradeXDemo.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"

#include <iostream>

using namespace std;

#include "TradeX.h"

int test_trade_funcs();
int test_hq_funcs(const char *pszHqSvrIP, short nPort);
int test_exhq_funcs(const char *pszHqSvrIP, short nPort);
int test_l2hq_funcs();

int _tmain(int argc, _TCHAR* argv[])
{
    //////////////////////////////////////////////////////////////////////////////////

    cout << "\n";
    cout << "\n";
    cout << "\tTradeX四合一完全兼容原有的Trade.dll下单业务，整合了行情API" << endl;
    cout << "\t解决了华泰等券商服务器无法正常连接的问题，在任何时间段都可以正确取数据" << endl;
    cout << "\t支持VC,VB，C#，Python，直连交易服务器和行情服务器" << endl;
    cout << "\n";
    cout << "\tTradeX = trade.dll + tdxhqapi.dll + tdxexhqapi.dll + tdxl2hqapi.dll" << endl;
    cout << "\n";
    cout << "\t如有需要，联系QQ：3048747297； 技术支持QQ群：318139137" << endl;
    cout << "\n";

    cout << "按回车键进行测试..." << endl;
    cin.get();

    cout << endl;
    cout << "\n";

    //
    //
    //
    if (1)
    {
        cout << "测试交易API, 按回车键继续...\n" << std::endl;
        if (!test_trade_funcs())
        {
            cout << "测试结束!!!" << endl;
            cin.get();
            //return 0;
        }
    }

    //
    //
    if (1)
    {
        cout << "测试行情API, 按回车键继续...\n" << std::endl;
        test_hq_funcs("14.17.75.71", 7709);
        getchar();
    }

    //
    //
    if (1)
    {
        cout << "测试扩展行情API, 按回车键继续...\n" << std::endl;
        test_exhq_funcs("59.175.238.38", 7727);
        getchar();
    }

    //
    //
    if (1)
    {
        cout << "测试L2行情API, 按回车键继续...\n" << std::endl;
        test_l2hq_funcs();
    }

    cout << "测试结束!!!" << endl;
    cin.get();

    return 0;
}

