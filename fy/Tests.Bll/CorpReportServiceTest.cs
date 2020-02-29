using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Dynamic;
using Modobay;
using Bll.Report;
using Dto.Report;

namespace Tests
{
    [TestClass]
   public class CorpReportServiceTest
    {
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var app = AppManager.GetServiceFromRoot<IAppContext>();
            app.User = new UserDto() { UserID = 1, ShopId = 1 };
        }

        [ContractTestCase]
        public void GetWorkReport()
        {
            var manager = AppManager.GetServiceFromRoot<ICorpReportService>();

            "报表-今日".Test(() =>
            {
                var result = manager.GetWorkReport(Dto.DateRangeEnum.Today, null, null);
                Assert.IsTrue(result != null);
            });

            "报表-自定义，未指定日期，无返回".Test(() =>
            {
                var result = manager.GetWorkReport(Dto.DateRangeEnum.Other, null, null);
                Assert.IsTrue(result == null);
            });

            "报表-自定义，指定无效日期，无返回".Test(() =>
            {
                var result = manager.GetWorkReport(Dto.DateRangeEnum.Other, "2334", null);
                Assert.IsTrue(result == null);
            });

            "报表-自定义，有指定日期，有返回".Test(() =>
            {
                var result = manager.GetWorkReport(Dto.DateRangeEnum.Other, "2020-01-01", "2020-01-01");
                Assert.IsTrue(result != null);
            });
        }

        [ContractTestCase]
        public void GetWorkRank()
        {
            var manager = AppManager.GetServiceFromRoot<ICorpReportService>();

            "排行-今日".Test(() =>
            {
                var result = manager.GetWorkRank(StatItemTypeEnum.InputHouse, Dto.DateRangeEnum.Today, null, null);
                Assert.IsTrue(result != null);
            });

            "排行-自定义，未指定日期，无返回".Test(() =>
            {
                var result = manager.GetWorkRank(StatItemTypeEnum.InputHouse, Dto.DateRangeEnum.Other, null, null);
                Assert.IsTrue(result == null);
            });

            "排行-自定义，指定无效日期，无返回".Test(() =>
            {
                var result = manager.GetWorkRank(StatItemTypeEnum.InputHouse, Dto.DateRangeEnum.Other, "2334", null);
                Assert.IsTrue(result == null);
            });

            "排行-自定义，有指定日期，有返回".Test(() =>
            {
                var result = manager.GetWorkRank(StatItemTypeEnum.InputHouse, Dto.DateRangeEnum.Other, "2020-01-01", "2020-01-01");
                Assert.IsTrue(result != null);
            });
        }
    }
}
