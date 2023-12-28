namespace MiddlewireRouter.Test
{
    using MiddleWireRouter;
    using System.Diagnostics.Metrics;

    [TestClass]
    public class MiddlewireRouterTest
    {
        [TestMethod]
        public void WithRouteTest()
        {
            string path = "jira.atlassian.com/testRoute/abc";
            string action = "action1";
            var test = new MiddlewireRouter();
            test.withRoute(path, action);
            var result = test.route(path);
            Assert.AreEqual(action, result);
        }
        [TestMethod]
        public void RouteTest()
        {
            var rm = new MiddlewireRouter();
            rm.withRoute("jira.atlassian.com/testRoute/abc", "fooData1");
            rm.withRoute("jira.atlassian.com/testRoute/xyz", "fooData2");
            rm.withRoute("jira.atlassian.com/testRoute", "fooData3");
            rm.withRoute("jira.atlassian.com/testRoute/abc/xyz", "fooData4");
            String v = rm.route("jira.atlassian.com/testRoute/*/xyz");
            String w = rm.route("jira.atlassian.com/testRoute");
            String x = rm.route("jira.atlassian.com/testRoute/abc");
            String y = rm.route("jira.atlassian.com/testRoute/*");
            String z = rm.route("jira.atlassian.com/*/xyz");
        }
    }
}