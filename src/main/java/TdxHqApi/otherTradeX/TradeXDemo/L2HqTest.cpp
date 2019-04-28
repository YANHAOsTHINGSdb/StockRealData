
#include "stdafx.h"

#include <iostream>
#include <string>

using namespace std;

#include "TradeX.h"

#define F1  1 // TdxL2Hq_GetSecurityCount
#define F2  1 // TdxL2Hq_GetSecurityList
#define F3  1 // TdxL2Hq_GetSecurityQuotes
#define F4  1 // TdxL2Hq_GetSecurityBars
#define F5  1 // TdxL2Hq_GetIndexBars
#define F6  1 // TdxL2Hq_GetMinuteTimeData
#define F7  1 // TdxL2Hq_GetHistoryMinuteTimeData
#define F8  1 // TdxL2Hq_GetTransactionData
#define F9  1 // TdxL2Hq_GetHistoryTransactionData
#define F10 1 // TdxL2Hq_GetCompanyInfoCategory
#define F11 1 // TdxL2Hq_GetCompanyInfoContent
#define F12 1 // TdxL2Hq_GetXDXRInfo
#define F13 1 // TdxL2Hq_GetFinanceInfo

#define F14 1 // TdxL2Hq_GetSecurityQuotes10
#define F15 1 // TdxL2Hq_GetDetailTransactionData
#define F16 1 // TdxL2Hq_GetDetailOrderData
#define F17 1 // TdxL2Hq_GetBuySellQueue


int test_l2hq_funcs_internal(const char *pszHqSvrIP, short nPort, const char *pszUser, const char *pszPassword);


/*
HostName01=高级行情_上海双线1
IPAddress01=106.14.76.29
Port01=7709

HostName02=高级行情_上海双线2
IPAddress02=139.196.174.113
Port02=7709

HostName03=高级行情_上海双线3
IPAddress03=139.196.175.118
Port03=7709

HostName04=高级行情_上海电信1
IPAddress04=61.152.107.173
Port04=7707
Areas04=1

HostName05=高级行情_上海电信2
IPAddress05=61.152.168.232
Port05=7715
Areas05=1

HostName06=高级行情_深圳电信1
IPAddress06=113.105.73.81
Port06=7721
Areas06=1

HostName07=高级行情_深圳电信2
IPAddress07=183.3.223.36
Port07=7709
Areas07=1

HostName08=高级行情_深圳电信3
IPAddress08=119.147.86.172
Port08=443
Areas08=1

HostName09=高级行情_深圳双线1
IPAddress09=112.74.31.192
Port09=443

HostName10=高级行情_深圳双线2
IPAddress10=120.77.76.11
Port10=7709

HostName11=高级行情_深圳双线3
IPAddress11=120.77.76.34
Port11=7709

HostName12=高级行情_东莞电信1
IPAddress12=113.105.142.138
Port12=7709
Areas12=1

HostName13=高级行情_东莞电信2
IPAddress13=113.105.142.139
Port13=7709
Areas13=1

HostName14=高级行情_武汉电信1
IPAddress14=119.97.185.4
Port14=7709
Areas14=1

HostName15=高级行情_武汉电信2
IPAddress15=119.97.185.16
Port15=7709
Areas15=1

HostName16=高级行情_北京联通
IPAddress16=61.135.142.90
Port16=443
Areas16=2

HostName17=高级行情_北京双线1
IPAddress17=101.200.127.219
Port17=443

HostName18=高级行情_北京双线2
IPAddress18=59.110.61.176
Port18=7709

HostName19=高级行情_济南联通
IPAddress19=123.129.245.202
Port19=80
Areas19=2

HostName20=高级行情_东莞联通1
IPAddress20=58.253.96.198
Port20=7709
Areas20=2

HostName21=高级行情_东莞联通2
IPAddress21=58.253.96.200
Port21=7709
Areas21=2
*/

#define TEST_CONNECT_L2     0


struct stL2Host
{
    const char *pszName;
    const char *pszHost;
    int nPort;
}
L2HostArray[] =
{
    { "高级行情_上海双线1", "106.14.76.29", 7709 },
    { "高级行情_上海双线2", "139.196.174.113", 7709 },
    { "高级行情_上海双线3", "139.196.175.118", 7709 },
    { "高级行情_上海电信1", "61.152.107.173", 7707 },
    { "高级行情_上海电信2", "61.152.168.232", 7715 },
    { "高级行情_深圳电信1", "113.105.73.81", 7721 },
    { "高级行情_深圳电信2", "183.3.223.36", 7709 },
    { "高级行情_深圳电信3", "119.147.86.172", 443 },
    { "高级行情_深圳双线1", "112.74.31.192", 443 },
    { "高级行情_深圳双线2", "120.77.76.11", 7709 },
    { "高级行情_深圳双线3", "120.77.76.34", 7709 },
    { "高级行情_东莞电信1", "113.105.142.138", 7709 },
    { "高级行情_东莞电信2", "113.105.142.139", 7709 },
    { "高级行情_武汉电信1", "119.97.185.4", 7709 },
    { "高级行情_武汉电信2", "119.97.185.16", 7709 },
    { "高级行情_北京联通", "61.135.142.90", 443 },
    { "高级行情_北京双线1", "101.200.127.219", 443 },
    { "高级行情_北京双线2", "59.110.61.176", 7709 },
    { "高级行情_济南联通", "123.129.245.202", 80 },
    { "高级行情_东莞联通1", "58.253.96.198", 7709 },
    { "高级行情_东莞联通2", "58.253.96.200", 7709 },
};


int test_l2hq_funcs()
{
    //开始获取行情数据

    char *Result = new char[1024 * 1024];
    char *ErrInfo = new char[256];

    short Count = 10;

    //
    // 连接服务器
    //

    char szL2User[32];
    char szL2Password[32];

    cout << "请输出L2账号:";
    cin >> szL2User;

    cout << "请输出L2密码:";
    cin >> szL2Password;

    int nHostNum = -1;

    while (1)
    {
        cout << "服务器列表:" << endl;

        for (int i=0; i<sizeof(L2HostArray)/sizeof(L2HostArray[0]); i++)
        {
            printf("%3d : %+30s\t%s:%d\n", i + 1, L2HostArray[i].pszName, L2HostArray[i].pszHost, L2HostArray[i].nPort);
        }

        cout << endl;

        cout << "请输入服务器序号:";
        cin >> nHostNum;

        if (nHostNum>0 && nHostNum<=sizeof(L2HostArray)/sizeof(L2HostArray[0]))
            break;

        cout << "错误! 请重新选择服务器\n" << endl;
    }

    nHostNum--;

    cout << "\n\tL2User(" << szL2User << "), L2Password(" << szL2Password << ")" << endl;
    cout << "\tL2高级行情服务器: " << L2HostArray[nHostNum].pszName;
    cout << endl;

    cout << "按回车键开始测试..." << std::endl;
    cin.get();

    //const char *pszUser = "togogo";
    //const char *pszPassword = "newxxxxx";

    int nCount = 1;

#ifdef TEST_CONNECT_L2
    nCount = 10;
#endif

    for (int i=0; i<nCount; i++)
    {
        test_l2hq_funcs_internal(L2HostArray[nHostNum].pszHost,
                                 L2HostArray[nHostNum].nPort,
                                 szL2User,
                                 szL2Password);

    }

    cout << "\t按回车键退出......\n";
    cin.get();

    return 1;
}

int test_l2hq_funcs_internal(const char *pszHqSvrIP, short nPort, const char *pszUser, const char *pszPassword)
{
    //开始获取行情数据
    char *Result = new char[1024 * 1024];
    char *ErrInfo= new char[256];

    short Count = 10;

    //连接服务器
    bool bool1 = TdxL2Hq_Connect(pszHqSvrIP, nPort, pszUser, pszPassword, Result, ErrInfo);
    if (!bool1)
    {
        cout << ErrInfo << endl;//连接失败
        getchar();
        return 0;
    }

    std::cout << "\n成功连接L2, 按任意键继续 ..." << std::endl;
	getchar();

#if F14
    {
        cout << "\n*** TdxL2Hq_GetSecurityQuotes10\n\n";

        //获取五档报价数据
        char Market[] = {0,1, 1, 0 };
        const char* Zqdm[] = {"000001","600030", "600269", "000003" };
        short ZqdmCount = 4;

        bool1 = TdxL2Hq_GetSecurityQuotes10(Market, Zqdm, &ZqdmCount, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }
        cout << Result << endl;
        //cout << "Count = " << ZqdmCount << endl;

        getchar();
    }
#endif

#if F15
    {
        cout << "\n*** TdxL2Hq_GetDetailTransactionData\n\n";

        //
        Count = 50;
        bool1 = TdxL2Hq_GetDetailTransactionData(0, "000001", 0, &Count, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }
        cout << Result << endl;
        //cout << "Count = " << Count << endl;

        getchar();
    }
#endif

#if F16
    {
        cout << "\n*** TdxL2Hq_GetDetailOrderData\n\n";

        //
        Count = 100;
        bool1 = TdxL2Hq_GetDetailOrderData(0, "000001", 0, &Count, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }

        cout << Result << endl;
        //cout << "Count = " << Count << endl;

        getchar();
    }
#endif

#if F17
    {
        cout << "\n*** TdxL2Hq_GetBuySellQueue\n\n";

        //
        bool1 = TdxL2Hq_GetBuySellQueue(1, "600300", Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }
        cout << Result << endl;

        //
        bool1 = TdxL2Hq_GetBuySellQueue(0, "000001", Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }

        cout << Result << endl;

        getchar();
    }
#endif

#if F1
    {
        cout << "\n*** TdxL2Hq_GetSecurityCount\n";

        cout << "\t深市=";

        Count = -1;
        bool1 = TdxL2Hq_GetSecurityCount(0, &Count, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;//连接失败
            getchar();
            return 0;
        }

        cout << Count << endl;

        cout << "\t沪市=";

        Count = -1;
        bool1 = TdxL2Hq_GetSecurityCount(1, &Count, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;//连接失败
            getchar();
            return 0;
        }

        cout << Count << endl;
        getchar();
    }
#endif

#ifdef TEST_CONNECT_L2
    TdxL2Hq_Disconnect();

    cout << "已经断开行情服务器" << endl;

    return 1;
#endif

#if F2
    {
        cout << "\n*** TdxL2Hq_GetSecurityList\n";

        Count = -1;
        bool1 = TdxL2Hq_GetSecurityList(0, 0, &Count, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;//连接失败
            getchar();
            return 0;
        }

        cout << "Count = " << Count << endl;
        cout << Result << endl;
        getchar();
    }
#endif

#if F3
    {
        cout << "\n*** TdxL2Hq_GetSecurityQuotes\n";

        //获取五档报价数据
        char xMarket[] = {0,1, 1, 0 };
        const char* Zqdm[] = {"000001","600030", "600269", "000001" };
        short ZqdmCount = 4;
        bool1 = TdxL2Hq_GetSecurityQuotes(xMarket, Zqdm, &ZqdmCount, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }
        cout << Result << endl;
        cout << ZqdmCount << endl;

        getchar();
    }
#endif


#if F4ALL
    {
        cout << "\n*** TdxL2Hq_GetSecurityBars\n";

        //数据种类,
        const char *szCategoryInfo[] =
        {
            "0->5分钟K线",
            "1->15分钟K线",
            "2->30分钟K线",
            "3->1小时K线",
            "4->日K线",
            "5->周K线",
            "6->月K线",
            "7->1分钟K线",
            "8->1分钟K线",
            "9->日K线",
            "10->季K线",
            "11->年K线",
        };

        //
        //

        cout << "\n\t测试沪市 600600" << endl;

        for (int i=0; i<sizeof(szCategoryInfo)/sizeof(szCategoryInfo[0]); i++)
        {
            //数据种类, 0->5分钟K线    1->15分钟K线    2->30分钟K线  3->1小时K线    4->日K线  5->周K线  6->月K线  7->1分钟K线  8->1分钟K线  9->日K线  10->季K线  11->年K线

            cout << "\n\t" << szCategoryInfo[i] << ", 按回车键开始" << endl;
            //getchar();

            Count = 8;

            bool1 = TdxL2Hq_GetSecurityBars(i, 1, "600600", 0, &Count, Result, ErrInfo);
            if (!bool1)
            {
                cout << ErrInfo << endl;
                return 0;
            }

            cout << Result << endl;

            cout << endl;
            cout << "按回车键继续 ...";
            getchar();
        }

        //
        //

        cout << "\n\t测试深市 000001" << endl;

        Count = 8;

        for (int i=0; i<sizeof(szCategoryInfo)/sizeof(szCategoryInfo[0]); i++)
        {
            //数据种类, 0->5分钟K线    1->15分钟K线    2->30分钟K线  3->1小时K线    4->日K线  5->周K线  6->月K线  7->1分钟K线  8->1分钟K线  9->日K线  10->季K线  11->年K线

            cout << "\n\t" << szCategoryInfo[i] << ", 按回车键开始" << endl;
            //getchar();

            bool1 = TdxL2Hq_GetSecurityBars(i, 0, "000001", 0, &Count, Result, ErrInfo);
            if (!bool1)
            {
                cout << ErrInfo << endl;
                return 0;
            }

            cout << Result << endl;

            cout << endl;
            cout << "按回车键继续 ...";
            getchar();
        }
    }
#endif

#if F4
    {
        cout << "\n*** TdxL2Hq_GetSecurityBars\n";

        //数据种类,
        const char *szCategoryInfo[] =
        {
            "0->5分钟K线",
            "1->15分钟K线",
            "2->30分钟K线",
            "3->1小时K线",
            "4->日K线",
            "5->周K线",
            "6->月K线",
            "7->1分钟K线",
            "8->1分钟K线",
            "9->日K线",
            "10->季K线",
            "11->年K线",
        };

        //
        //

        cout << "\n\t测试沪市 600600 - 1分钟 K线" << endl;

        {
            //数据种类, 0->5分钟K线    1->15分钟K线    2->30分钟K线  3->1小时K线    4->日K线  5->周K线  6->月K线  7->1分钟K线  8->1分钟K线  9->日K线  10->季K线  11->年K线
            int i = 8;

            cout << "\n\t" << szCategoryInfo[i] << ", 按回车键开始" << endl;
            //getchar();

            Count = 8;

            bool1 = TdxL2Hq_GetSecurityBars(i, 1, "600600", 0, &Count, Result, ErrInfo);
            if (!bool1)
            {
                cout << ErrInfo << endl;
                return 0;
            }

            cout << Result << endl;

            cout << endl;
            cout << "按回车键继续 ...";
            getchar();
        }

        //
        //

        cout << "\n\t测试深市 000001 - 1分钟 K线" << endl;

        Count = 8;

        {
            //数据种类, 0->5分钟K线    1->15分钟K线    2->30分钟K线  3->1小时K线    4->日K线  5->周K线  6->月K线  7->1分钟K线  8->1分钟K线  9->日K线  10->季K线  11->年K线
            int i= 8;

            cout << "\n\t" << szCategoryInfo[i] << ", 按回车键开始" << endl;
            //getchar();

            bool1 = TdxL2Hq_GetSecurityBars(i, 0, "000001", 0, &Count, Result, ErrInfo);
            if (!bool1)
            {
                cout << ErrInfo << endl;
                return 0;
            }

            cout << Result << endl;

            cout << endl;
            cout << "按回车键继续 ...";
            getchar();
        }
    }
#endif

#if F5ALL
    {
        cout << "\n*** TdxL2Hq_GetIndexBars\n";

        //数据种类,
        const char *szCategoryInfo[] =
        {
            "0->5分钟K线",
            "1->15分钟K线",
            "2->30分钟K线",
            "3->1小时K线",
            "4->日K线",
            "5->周K线",
            "6->月K线",
            "7->1分钟K线",
            "8->1分钟K线",
            "9->日K线",
            "10->季K线",
            "11->年K线",
        };

        cout << "\n\t测试沪市 上证指数000001" << endl;

        Count = 8;

        for (int i=0; i<sizeof(szCategoryInfo)/sizeof(szCategoryInfo[0]); i++)
        {
            //数据种类, 0->5分钟K线    1->15分钟K线    2->30分钟K线  3->1小时K线    4->日K线  5->周K线  6->月K线  7->1分钟K线  8->1分钟K线  9->日K线  10->季K线  11->年K线

            cout << "\n\t" << szCategoryInfo[i] << endl;
            cout << endl;

            bool1 = TdxL2Hq_GetIndexBars(i, 1, "000001", 0, &Count, Result, ErrInfo);
            if (!bool1)
            {
                cout << ErrInfo << endl;
                return 0;
            }

            cout << Result << endl;

            cout << endl;
            cout << "按回车键继续 ...";
            getchar();
        }

        cout << "\n\t测试沪市 深证成指399001" << endl;

        Count = 8;

        for (int i=0; i<sizeof(szCategoryInfo)/sizeof(szCategoryInfo[0]); i++)
        {
            //数据种类, 0->5分钟K线    1->15分钟K线    2->30分钟K线  3->1小时K线    4->日K线  5->周K线  6->月K线  7->1分钟K线  8->1分钟K线  9->日K线  10->季K线  11->年K线

            cout << "\n\t" << szCategoryInfo[i] << endl;
            cout << endl;

            Count = 8;

            bool1 = TdxL2Hq_GetIndexBars(i, 0, "399001", 0, &Count, Result, ErrInfo);
            if (!bool1)
            {
                cout << ErrInfo << endl;
                return 0;
            }

            cout << Result << endl;

            cout << endl;
            cout << "按回车键继续 ...";
            getchar();
        }
    }
#endif

#if F5
    {
        cout << "\n*** TdxL2Hq_GetIndexBars\n";

        //数据种类,
        const char *szCategoryInfo[] =
        {
            "0->5分钟K线",
            "1->15分钟K线",
            "2->30分钟K线",
            "3->1小时K线",
            "4->日K线",
            "5->周K线",
            "6->月K线",
            "7->1分钟K线",
            "8->1分钟K线",
            "9->日K线",
            "10->季K线",
            "11->年K线",
        };

        cout << "\n\t测试沪市 上证指数000001 - 1分钟 K线" << endl;

        Count = 8;

        {
            //数据种类, 0->5分钟K线    1->15分钟K线    2->30分钟K线  3->1小时K线    4->日K线  5->周K线  6->月K线  7->1分钟K线  8->1分钟K线  9->日K线  10->季K线  11->年K线
            int i = 8;

            cout << "\n\t" << szCategoryInfo[i] << endl;
            cout << endl;

            bool1 = TdxL2Hq_GetIndexBars(i, 1, "000001", 0, &Count, Result, ErrInfo);
            if (!bool1)
            {
                cout << ErrInfo << endl;
                return 0;
            }

            cout << Result << endl;

            cout << endl;
            cout << "按回车键继续 ...";
            getchar();
        }

        cout << "\n\t测试沪市 深证成指399001" << endl;

        Count = 8;

        {
            //数据种类, 0->5分钟K线    1->15分钟K线    2->30分钟K线  3->1小时K线    4->日K线  5->周K线  6->月K线  7->1分钟K线  8->1分钟K线  9->日K线  10->季K线  11->年K线
            int i = 8;

            cout << "\n\t" << szCategoryInfo[i] << endl;
            cout << endl;

            Count = 8;

            bool1 = TdxL2Hq_GetIndexBars(i, 0, "399001", 0, &Count, Result, ErrInfo);
            if (!bool1)
            {
                cout << ErrInfo << endl;
                return 0;
            }

            cout << Result << endl;

            cout << endl;
            cout << "按回车键继续 ...";
            getchar();
        }
    }
#endif

#if F6
    {
        cout << "\n*** TdxL2Hq_GetMinuteTimeData\n";

        //获取分时图数据
        bool1 = TdxL2Hq_GetMinuteTimeData(0, "000001",  Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }
        cout << Result << endl;
        getchar();
    }
#endif

#if F7
    {
        cout << "\n*** TdxL2Hq_GetHistoryMinuteTimeData\n";

        //获取历史分时图数据
        bool1 = TdxL2Hq_GetHistoryMinuteTimeData(0, "000001", 20170112, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }
        cout << Result << endl;
        getchar();
    }
#endif


#if F8
    {
        cout << "\n*** TdxL2Hq_GetTransactionData\n";

        //获取分笔图数据
        Count = 20;
        bool1 = TdxL2Hq_GetTransactionData(0, "000001", 0, &Count, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }
        cout << Result << endl;
        getchar();
    }
#endif

#if F9
    {
        cout << "\n*** TdxL2Hq_GetHistoryTransactionData\n";

        //获取历史分笔图数据
        Count = 100;
        bool1 = TdxL2Hq_GetHistoryTransactionData(0, "000001", 0, &Count, 20170112,  Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }

        cout << Result << endl;
        getchar();
    }
#endif

#if F10
    {
        cout << "\n*** TdxL2Hq_GetCompanyInfoCategory\n";

        //获取F10数据的类别
        bool1 = TdxL2Hq_GetCompanyInfoCategory(0, "000001", Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }
        cout << Result << endl;

        getchar();
    }
#endif

#if F11
    {
        cout << "\n*** TdxL2Hq_GetCompanyInfoContent\n";

        //获取F10数据的某类别的内容
        bool1 = TdxL2Hq_GetCompanyInfoContent(1, "600030", "600030.txt", 142577, 5211, Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }
        cout << Result << endl;

        getchar();
    }
#endif

#if F12
    {
        cout << "\n*** TdxL2Hq_GetXDXRInfo\n";

        //获取除权除息信息
        bool1 = TdxL2Hq_GetXDXRInfo(0, "000001", Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }
        cout << Result << endl;

        getchar();
    }
#endif

#if F13
    {
        cout << "\n*** TdxL2Hq_GetFinanceInfo\n";

        //获取财务信息
        bool1 = TdxL2Hq_GetFinanceInfo(0, "000001", Result, ErrInfo);
        if (!bool1)
        {
            cout << ErrInfo << endl;
            return 0;
        }
        cout << Result << endl;

        getchar();
    }
#endif

    TdxL2Hq_Disconnect();

    cout << "已经断开行情服务器" << endl;

    return 1;
}



