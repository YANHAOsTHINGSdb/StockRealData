
#include "stdafx.h"

#include <iostream>
#include <string>

using namespace std;

#include "TradeX.h"

int test_trade_funcs()
{
    char *Result = new char[1024 *1024];
    char *ErrInfo = new char[256];

    //
    //

    cout << "1 - OpenTdx() ... ";
    OpenTdx();

    cout << "ok\n" << endl;
    cout << "\t按回车键继续......\n";

    //
    //
    cout << "2 - Logon(\"mock.tdx.com.cn\", 7708, \"6.40\", 9000, \"net828@163.com\", \"001001001005792\", \"123123\", \"\", ErrInfo) ... ";
    int nClientID = Logon("mock.tdx.com.cn", 7708, "6.40", 9000, "net828@163.com","001001001005792", "123123", "", ErrInfo);
    if (nClientID < 0)
    {
        cout << "fail" << endl;
        cout << "\t" << ErrInfo << endl;

        cin.get();

        return 0;
    }

    cout << "ok\n" << endl;

    cout << "\t按回车键继续......\n";
    cin.get();

    //
    //

    if (1)
    {

        cout << "3 - QueryData\n" << endl;

        cout << "\t 0 - 查询资金 QueryData(nClientID, 0, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 0, Result, ErrInfo);
        cout << "查询资金结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;

        cout << "\t 1 - 查询股份     QueryData(nClientID, 1, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 1, Result, ErrInfo);
        cout << "查询股份结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;

        cout << "\t 2 - 查询当日委托 QueryData(nClientID, 2, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 2, Result, ErrInfo);
        cout << "查询当日委托结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;

        cout << "\t 3 - 查询当日成交 QueryData(nClientID, 3, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 3, Result, ErrInfo);
        cout << "查询当日成交结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;

        cout << "\t 4 - 查询可撤单   QueryData(nClientID, 4, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 4, Result, ErrInfo);
        cout << "查询可撤单结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;

        cout << "\t 5 - 查询股东代码 QueryData(nClientID, 5, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 5, Result, ErrInfo);
        cout << "查询股东代码结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;
        cout << "\t按回车键继续......\n";
        cin.get();

        /*
        cout << "\t 6 - 查询融资余额 QueryData(nClientID, 6, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 6, Result, ErrInfo);
        cout << "查询股东代码结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;

        cout << "\t 7 - 查询融券余额 QueryData(nClientID, 7, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 7, Result, ErrInfo);
        cout << "查询融券余额结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;

        cout << "\t按回车键继续......\n";
        cin.get();

        cout << "\t 8 - 查询可融证券 QueryData(nClientID, 8, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 8, Result, ErrInfo);
        cout << "查询可融证券结果:\n"<< Result << " " << ErrInfo << endl;

        cout << "\t按回车键继续......\n";
        cin.get();
        */

        cout << "\t 12 - 可申购新股查询 QueryData(nClientID, 12, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 12, Result, ErrInfo);
        cout << "可申购新股查询结果:\n"<< Result << " " << ErrInfo << endl;
        cout << endl;

        cout << "\t 13 - 新股申购额度查询 QueryData(nClientID, 13, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 13, Result, ErrInfo);
        cout << "新股申购额度查询结果:\n"<< Result << " " << ErrInfo << endl;
        cout << endl;

        cout << "\t 14 - 配号查询 QueryData(nClientID, 14, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 14, Result, ErrInfo);
        cout << "配号查询结果:\n"<< Result << " " << ErrInfo << endl;
        cout << endl;

        cout << "\t 15 - 中签查询 QueryData(nClientID, 15, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 15, Result, ErrInfo);
        cout << "中签查询结果:\n"<< Result << " " << ErrInfo << endl;
        cout << endl;

        cout << endl;

        cout << "\t按回车键继续......\n";
        cin.get();
    }

    if (1)
    {
        cout << "4 - QuickIPO(nClientID) ... " << endl;

        QuickIPO(nClientID);

        cout << "\t按回车键继续......\n";
        cin.get();

        cout << "\t 3 - 查询当日成交 QueryData(nClientID, 3, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 3, Result, ErrInfo);
        cout << "查询当日成交结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;

        cout << "\t 4 - 查询可撤单   QueryData(nClientID, 4, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 4, Result, ErrInfo);
        cout << "查询可撤单结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;

        cout << "\t按回车键继续......\n";
        cin.get();
    }

    if (1)
    {
        cout << "4 - QuickIPODetail(nClientID, 4, Result, ErrInfo) ... " << endl;

        int nCount = 4;

        char *ResultArray[4];
        char *ErrInfoArray[4];

        for (int i= 0; i<4; i++)
        {
            ResultArray[i] = new char[0x8010];
            ErrInfoArray[i] = new char[256];
        }

        int nRet = QuickIPODetail(nClientID, nCount, ResultArray, ErrInfoArray, ErrInfo);
        if (nRet <= 0) // error
        {
            cout << "fail\n" << endl;
            cout << "\t" << ErrInfo << endl;
        }
        else
        {
            for (int i=0; i<nCount; i++)
            {
                cout << ResultArray[i] << ", " << ErrInfoArray[i] << endl;
            }
        }

        for (int i=0; i<4; i++)
        {
            delete []ResultArray[i];
            delete []ErrInfoArray[i];
        }

        cout << "\t按回车键继续......\n";
        cin.get();

        cout << "\t 3 - 查询当日成交 QueryData(nClientID, 3, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 3, Result, ErrInfo);
        cout << "查询当日成交结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;

        cout << "\t 4 - 查询可撤单   QueryData(nClientID, 4, Result, ErrInfo)\n" << endl;
        QueryData(nClientID, 4, Result, ErrInfo);
        cout << "查询可撤单结果:\n"<< Result << " " << ErrInfo << endl;

        cout << endl;

        cout << "\t按回车键继续......\n";
        cin.get();
    }

    //
    //

    cout << "4 - SendOrder(nClientID, 0, 4, \"p001001001005793\", \"601988\", 0, 100, Result, ErrInfo) ... ";

    std::string sErrInfo;

    SendOrder(nClientID, 0, 4, "p001001001005793", "601988", 0, 100, Result, ErrInfo);
    sErrInfo = ErrInfo;
    if (sErrInfo.empty())
    {
        cout << "ok\n" << endl;
    }
    else
    {
        cout << "fail\n" << endl;
        cout << "\t" << ErrInfo << endl;
    }

    SendOrder(nClientID, 0, 4, "p001001002005793", "000001", 0, 100, Result, ErrInfo);
    sErrInfo = ErrInfo;
    if (sErrInfo.empty())
    {
        cout << "ok\n" << endl;
    }
    else
    {
        cout << "fail\n" << endl;
        cout << "\t" << ErrInfo << endl;
    }

    cout << "\t按回车键继续......\n";
    cin.get();

    cout << "\t 3 - 查询当日成交 QueryData(nClientID, 3, Result, ErrInfo)\n" << endl;
    QueryData(nClientID, 3, Result, ErrInfo);
    cout << "查询当日成交结果:\n"<< Result << " " << ErrInfo << endl;

    cout << endl;

    cout << "\t 4 - 查询可撤单   QueryData(nClientID, 4, Result, ErrInfo)\n" << endl;
    QueryData(nClientID, 4, Result, ErrInfo);
    cout << "查询可撤单结果:\n"<< Result << " " << ErrInfo << endl;

    cout << endl;

    cout << "\t按回车键继续......\n";
    cin.get();

    //
    //

    Logoff(nClientID);
    CloseTdx();

    return 1;
}

