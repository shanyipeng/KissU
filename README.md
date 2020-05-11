# KissU框架介绍 
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://mit-license.org/)

<blockquote>
KissU 是一个分布式微服务应用框架,提供高性能RPC远程服务调用，采用Zookeeper、Consul作为服务的注册中心，集成了哈希，随机，轮询，压力最小优先作为负载均衡的算法，RPC集成采用的是netty框架，采用异步传输。
</blockquote>

> 源码路径：[源代码 ***C#***](https://github.com/gongap/KissU/)

## 文档目录

> 本文档会持续更新

- 概述
  - 简介
  - 版本更新
- 教程
  - 服务主机如何构建
    - 依赖注入服务
    - 配置日志组件
    - 如何设置成服务提供者
    - 启动配置
    - 配置文件构建
  - 协议主机构建
    - 构建TCP协议服务主机
    - 构建MQTT协议服务主机
    - 构建WS协议服务主机
    - 构建UDP协议服务主机
    - 构建Http协议服务主机
    - 构建Dns协议服务主机
  - 如何构建微服务
    - 微服务规则
    - 配置routepath
    - 配置ServiceMetadata
    - 服务拦截器配置
    - 配置依赖注入
    - 如何配置和读取配置项
  - 服务注册发现和服务治理
    - consul的注册与发现
    - zookeeper的注册与发现
    - 服务路由的负载均衡
    - 容错策略
    - 服务熔断
  - 引擎核心组件
    - 配置使用Swagger组件
    - 配置ProtoBuffer、MessagePack或Json.net组件编解码消息
    - 配置Log4net日志组件
    - 配置NLog日志组件
    - 配置Stage关卡组件
    - 配置扩展SystemModule组件
    - 配置扩展BusinessModule组件
    - 配置扩展EnginePartModule组件
    - 配置扩展KestrelHttpModule组件
  - 中间件
    - 缓存中间件
    - 消息中间件
  - 网关
    - 配置gatewaySettings.json文件
    - jwt鉴权
    - 基于Stage生成的第三方接口网关
  - 示例
    - 缓存中间件使用
    - 消息中间件使用
    - 服务之间本地和远程调用
  - 部署
    - 引擎部署到window环境中
    - 引擎部署到linux环境容器中
    - 引擎构建镜像push到镜像库
    - 扫描业务模块组件和中间件装载到引擎中
    - 基于rancher 服务编排

## 贡献与反馈

> 如果你在阅读或使用发现Bug，或有更佳实现方式，请通知我们。

> 为了保持代码简单，目前很多功能只建立了基本结构，细节特性未进行迁移，在后续需要时进行添加，如果你发现某个类无法满足你的需求，请通知我们。

> 你可以通过github的Issue或Pull Request向我们提交问题和代码，如果你更喜欢使用QQ进行交流，请加入我们的交流QQ群。

> 对于你提交的代码，如果我们决定采纳，可能会进行相应重构，以统一代码风格。

> 对于热心的同学，将会把你的名字放到**贡献者**名单中。
    
## 免责申明

- 虽然我们对代码已经进行高度审查，并用于自己的项目中，但依然可能存在某些未知的BUG，如果你的生产系统蒙受损失，KissU团队不会对此负责。

- 出于成本的考虑，我们不会对已发布的API保持兼容，每当更新代码时，请注意该问题。
