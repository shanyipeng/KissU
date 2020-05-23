﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using KissU.Core.Dependency;
using KissU.Modules.SampleA.Service.Contracts;
using KissU.Modules.SampleA.Service.Contracts.Dtos;
using KissU.Modules.SampleA.Service.Contracts.Events;
using KissU.Surging.CPlatform.Diagnostics;
using KissU.Surging.CPlatform.Transport.Implementation;
using KissU.Surging.ProxyGenerator;
using Newtonsoft.Json;

namespace KissU.Web.Host
{
    public class TestService : ITransientDependency
    {
        private static int _endedConnenctionCount;
        private static DateTime begintime;

        private void StartRequest(int connectionCount)
        {
            var sw = new Stopwatch();
            sw.Start();
            var userProxy = ServiceLocator.GetService<IServiceProxyFactory>().CreateProxy<IAccountService>("User");
            ServiceResolver.Current.Register("User", userProxy);
            var service = ServiceLocator.GetService<IServiceProxyFactory>();
            userProxy = ServiceResolver.Current.GetService<IAccountService>("User");
            sw.Stop();
            Console.WriteLine($"代理所花{sw.ElapsedMilliseconds}ms");
            ThreadPool.SetMinThreads(100, 100);
            Parallel.For(0, connectionCount / 6000, new ParallelOptions { MaxDegreeOfParallelism = 50 }, async u =>
            {
                for (var i = 0; i < 6000; i++)
                    await Test(userProxy, connectionCount);
            });
        }

        public async Task Test(IAccountService accountProxy, int connectionCount)
        {
            var a = await accountProxy.GetDictionary();
            IncreaseSuccessConnection(connectionCount);
        }

        private void IncreaseSuccessConnection(int connectionCount)
        {
            Interlocked.Increment(ref _endedConnenctionCount);
            if (_endedConnenctionCount == 1)
            {
                begintime = DateTime.Now;
            }

            if (_endedConnenctionCount >= connectionCount)
            {
                Console.WriteLine($"结束时间{(DateTime.Now - begintime).TotalMilliseconds}");
            }
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="serviceProxyFactory"></param>
        public void Test(IServiceProxyFactory serviceProxyFactory)
        {
            var tracingContext = ServiceLocator.GetService<ITracingContext>();
            Task.Run(async () =>
            {
                RpcContext.GetContext().SetAttachment("xid", 124);

                var userProxy = serviceProxyFactory.CreateProxy<IAccountService>("Account");
                var e = userProxy.SetSex(Sex.Woman).GetAwaiter().GetResult();
                var v = userProxy.GetUserId("gongap").GetAwaiter().GetResult();
                var fa = userProxy.GetUserName(1).GetAwaiter().GetResult();
                userProxy.Try().GetAwaiter().GetResult();
                var v1 = userProxy.GetUserLastSignInTime(1).Result;
                var things = userProxy.GetAllThings().Result;
                var apiResult = userProxy.GetApiResult().GetAwaiter().GetResult();
                userProxy.PublishThroughEventBusAsync(new UserEvent
                {
                    UserId = 1,
                    Name = "gongap"
                }).Wait();

                userProxy.PublishThroughEventBusAsync(new UserEvent
                {
                    UserId = 1,
                    Name = "gongap"
                }).Wait();

                var r = await userProxy.GetDictionary();
                var serviceProxyProvider = ServiceLocator.GetService<IServiceProxyProvider>();

                do
                {
                    Console.WriteLine("正在循环 1w次调用 GetUser.....");

                    //1w次调用
                    var watch = Stopwatch.StartNew();
                    for (var i = 0; i < 10000; i++)
                    {
                        //var a = userProxy.GetDictionary().Result;
                        var a = await userProxy.GetDictionary();
                        //var result = serviceProxyProvider.Invoke<object>(new Dictionary<string, object>(), "api/user/GetDictionary", "User").Result;
                    }

                    watch.Stop();
                    Console.WriteLine($"1w次调用结束，执行时间：{watch.ElapsedMilliseconds}ms");
                    Console.WriteLine("Press any key to continue, q to exit the loop...");
                    var key = Console.ReadLine();
                    if (key.ToLower() == "q")
                        break;
                } while (true);
            }).Wait();
        }

        public void TestRabbitMq(IServiceProxyFactory serviceProxyFactory)
        {
            serviceProxyFactory.CreateProxy<IAccountService>("User").PublishThroughEventBusAsync(new UserEvent
            {
                Age = 18,
                Name = "gongap",
                UserId = 1
            });
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        public void TestForRoutePath(IServiceProxyProvider serviceProxyProvider)
        {
            var model = new Dictionary<string, object>();
            model.Add("user", JsonConvert.SerializeObject(new
            {
                Name = "gongap",
                Age = 18,
                UserId = 1,
                Sex = "Man"
            }));
            var path = "api/account/getuser";
            var serviceKey = "User";

            var userProxy = serviceProxyProvider.Invoke<object>(model, path, serviceKey);
            var s = userProxy.Result;
            Console.WriteLine($"{s}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
