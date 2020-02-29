using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Dynamic;
using Modobay;
using Bll.Demo;
using Dto.Demo;

namespace Tests
{
    [TestClass]
    public class DemoTest
    {
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var app = AppManager.GetServiceFromRoot<IAppContext>();
            app.User = new UserDto() { UserID = 1, ShopId = 1 };
        }

        [ContractTestCase]
        public void SearchByCondition()
        {
            var manager = AppManager.GetServiceFromRoot<IFollowUpManager>();

            var queryDto = new FollowUpQueryDto()
            {
                PageIndex = 1,
                PageSize = 10
            };

            "跟进查询-无查询条件，有匹配记录".Test(() =>
            {
                var result = manager.Search(queryDto);
                Assert.IsTrue(result != null && result.Count > 0);
            });

            "跟进查询-全部查询条件，无匹配记录".Test(() =>
            {
                queryDto.FollowUpID = int.MaxValue;
                queryDto.StartTime = "2000-01-01";
                queryDto.EndTime = "2000-01-01";
                queryDto.Keyword = "xxxxx";
                queryDto.DictItemCode = "xxxxx";
                var result = manager.Search(queryDto);
                Assert.IsTrue(result == null || result.Count == 0);
            });
        }

        [ContractTestCase]
        public void Get()
        {
            var manager = AppManager.GetServiceFromRoot<IFollowUpManager>();

            "获取跟进详情-有匹配记录".Test(() =>
            {
                var result = manager.Get(1);
                Assert.IsTrue(result != null && result?.FollowUpID > 0);
            });

            "获取跟进详情-无匹配记录".Test(() =>
            {
                var result = manager.Get(-1);
                Assert.IsTrue(result == null);
            });
        }
    }
}
