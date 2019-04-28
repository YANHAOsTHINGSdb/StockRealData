

#include "stdafx.h"

#include "TradeX.h"

#include <iostream>

using namespace std;

#define E1  1 // TdxExHq_GetMarkets
#define E2  1 // TdxExHq_GetInstrumentCount
#define E3  1 // TdxExHq_GetInstrumentInfo
#define E4  1 // TdxExHq_GetInstrumentQuote
#define E5  1 // TdxExHq_GetInstrumentBars
#define E6  1 // TdxExHq_GetMinuteTimeData
#define E7  1 // TdxExHq_GetHistoryMinuteTimeData
#define E8  1 // TdxExHq_GetTransactionData
#define E9  1 // TdxExHq_GetHistoryTransactionData

int test_exhq_funcs(const char *pszHqSvrIP, short nPort)
{
    //开始获取行情数据
    char* Result = new char[1024 * 1024];
    char* ErrInfo = new char[256];
    short Count = 10;

    //连接服务器
    bool bool1 = TdxExHq_Connect(pszHqSvrIP, nPort, Result, ErrInfo);
    if (!bool1)
    {
        cout << ErrInfo << endl;
        return 0;
    }

    std::cout << Result << std::endl;

#if E1
    {
        cout << "\n*** TdxExHq_GetMarkets\n";

        bool1 = TdxExHq_GetMarkets(Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;//连接失败
            getchar();
            return 0;
        }

        getchar();
    }
#endif

#if E2
    {
        cout << "\n*** TdxExHq_GetInstrumentCount\n";

        int nCount = -1;
        bool1 = TdxExHq_GetInstrumentCount(&nCount, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;//连接失败
            getchar();
            return 0;
        }

        cout << "Count = " << nCount << endl;
        cout << Result << endl;
        getchar();
    }
#endif

#if E3
    {
        cout << "\n*** TdxExHq_GetInstrumentInfo\n";

        Count = 0;
        bool1 = TdxExHq_GetInstrumentInfo(0, &Count,  Result, ErrInfo); //Count最大511
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }

        cout << "Count = " << Count << endl;
        getchar();
        cout << Result << endl;
        getchar();
    }

    if (0)
    {
        cout << "\n*** TdxExHq_GetInstrumentInfo\n";

        int nStart = 0;

        FILE *fp;
        fp = fopen("z:\\xxx.log", "w+");

        while (1)
        {


            Count = 511;
            bool1 = TdxExHq_GetInstrumentInfo(nStart, &Count,  Result, ErrInfo); //Count最大511
            if (!bool1)
            {
                cout << ErrInfo << endl;
                return 0;
            }

            fprintf(fp, "%s", Result);

            nStart += Count;

            if (Count < 511)
                break;

        }
        fclose(fp);

        cout << "Count = " << Count << endl;
        getchar();
        cout << Result << endl;
        getchar();
    }

#endif

#if E4
    {
        cout << "\n*** TdxExHq_GetInstrumentQuote\n";

        bool1 = TdxExHq_GetInstrumentQuote(47, "IF1701",  Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }

        cout << Result << endl;
        getchar();
    }
#endif

#if E5
    {
        cout << "\n*** TdxExHq_GetInstrumentBars\n";

        // 0->5分钟K线    1->15分钟K线    2->30分钟K线  3->1小时K线    4->日K线  5->周K线  6->月K线  7->1分钟  8->1分钟K线  9->日K线  10->季K线  11->年K线
        Count = 50;
        bool1 = TdxExHq_GetInstrumentBars(7, 539, "HSI", 0, &Count, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }

        cout << Result << endl;
        getchar();
    }
#endif

#if E6
    {
        cout << "\n*** TdxExHq_GetMinuteTimeData\n";

        bool1 = TdxExHq_GetMinuteTimeData(47, "IF1701", Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }

        cout << Result << endl;
        getchar();
    }
#endif

#if E7
    {
        cout << "\n*** TdxExHq_GetHistoryMinuteTimeData\n";

        bool1 = TdxExHq_GetHistoryMinuteTimeData(47, "IF1701", 20170113, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }

        cout << Result << endl;
        getchar();
    }
#endif

#if E8
    {
        cout << "\n*** TdxExHq_GetTransactionData\n";

        Count = 200;
        bool1 = TdxExHq_GetTransactionData(47, "IF1701", 0, &Count, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }

        cout << Result << endl;
        getchar();
    }
#endif

#if E9
    {
        cout << "\n*** TdxExHq_GetHistoryTransactionData\n";

        bool1 = TdxExHq_GetHistoryTransactionData(47, "IF1701", 20170113, 0, &Count, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }

        cout << Result << endl;
        getchar();
    }
#endif

    TdxExHq_Disconnect();

    cout << "已经断开行情服务器" << endl;

    getchar();
    return 0;
}


