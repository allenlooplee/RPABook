# RPA开发与应用

在过去一年里，RPA 在企业的数字化转型中扮演着越来越重要的角色，大量会议、网站和公众号都在给我们展示了 RPA 将会带来什么变化，然而，所有这些最终都会回归到一个问题——我们需要 RPA 开发者来实现。《RPA 开发与应用》的任务就是助你快速了解在企业内部启动 RPA 需要了解和考虑哪些方面，实现 RPA 需要掌握哪些知识和技术，以及如何在 RPA 中使用 OCR、NLP 和 ML 等技术，此外，我们还将深入到 UiPath 的底层工作流引擎，探讨如何通过构建自定义活动来重用代码和提高开发效率。

## 阅读指南

《RPA 开发与应用》的写作从2018年11月开始，到2019年9月结束，前后耗时10个月，它系统地记录了我在这段时间里的所学、所用、所思、所想。从刚接触RPA到现在，我从社区学到了很多，现在是时候回馈社区了，我希望这本书能够助我踏出第一步，帮助现在的新手快速成长，就像社区帮助当初的我一样。

全书分为四个部分，第一部分介绍了 RPA 和 UiPath 的基本概念。如果你是一个技术新手，你想快点做出东西，你可以先读第2章，然后根据情况选读后面的章节，最后阅读第1章。如果你是一个管理者，你想了解为企业实施 RPA 需要考虑哪些东西，你可以花点时间精读第1章，然后根据情况浏览后面的章节。

第二部分系统地讲解开发的基础知识和技术，其中，第3章讲解的内容每个 RPA 项目都会用到，包括创建和调试项目、录制和播放流程、版本控制和发布部署等等，建议技术人员精读并掌握，第4章讲解的内容可以根据情况选读，比如说，你的 RPA 项目需要定期处理文件，你可以选读第1节和第6节。如果时间允许，我仍然建议你从头到尾阅读，因为部分示例涉及多个章节，单独阅读这些章节可能造成上下文缺失。

第三部分个人觉得是全书最有意思的部分，它探索 RPA 如何与百度 OCR、NLP 等服务和微软 ML.NET 框架集成，也探讨如何利用 WF 的知识为 UiPath 创建自定义活动包。随着你的不断深入，你会接触到更多更复杂的流程，你终将无法满足于官方提供和自带的构件，这个时候，集成第三方服务以及创建自定义构件就会变的尤为重要，我希望这个部分能够抛砖引玉，在这个方面对你有所启发。

如果你看了很多资料，写了很多示例，也做了很多交流，依然觉得在解决实际问题时有所欠缺，那么你离出师可能还差一个真实案例。你需要一个机会把你学过的东西串起来，从头到尾经历一个完整的项目，并解决在这个过程中遇到的实际问题，而这正是最后一章的目的。当然，真实项目有的是你未曾想过的奇葩问题，因此，请把握机会，参与项目，解决问题，积累经验。噢，对了，别忘了在真实项目中遇到有（qi）趣（pa）的问题拿出来跟大家一起分享哦！

以上这些内容其实是我在构思这本书大纲时的思考，把这些内容写下来一方面希望帮你找到合适的阅读方式，另一方面也想让你了解本书为何写成这样。

## 代码清单

项目名称|项目描述|对应章节
---|---|---
[HelloWorld](https://github.com/allenlooplee/RPABook/tree/master/src/HelloWorld)|通过 Outlook 发送 Hello World 邮件|第2章 UiPath 概览
[Demo1](https://github.com/allenlooplee/RPABook/tree/master/src/Demo1)|操作计算器、操作浏览器|第3章 开发基础
[Demo2](https://github.com/allenlooplee/RPABook/tree/master/src/Demo2)|操作文件和文件夹、抓取网页信息、操作 Office 软件、响应用户事件、读写配置文件、编写单元测试|第4章 常用技能和示例
[Demo3](https://github.com/allenlooplee/RPABook/tree/master/src/Demo3)|操作 SQLite 数据库、读写配置文件|第4章 常用技能和示例
[Demo4](https://github.com/allenlooplee/RPABook/tree/master/src/Demo4)|使用百度 OCR 识别增值税发票、使用百度 NLP 提取新闻标签并使用 Python 生成词云图|第5章 RPA x OCR、第6章 RPA x NLP
[ConsoleApp1](https://github.com/allenlooplee/RPABook/tree/master/src/ConsoleApp1)|使用 ML.NET Model Builder 自动训练模型|第7章 RPA x AutoML
[Demo5](https://github.com/allenlooplee/RPABook/tree/master/src/Demo5)|通过 HeartDiseaseDetection 自定义活动进行预测|第7章 RPA x AutoML
[Demo6](https://github.com/allenlooplee/RPABook/tree/master/src/Demo6)|通过 ML 自定义活动进行数据处理|第7章 RPA x AutoML
[HeartDiseaseDetection](https://github.com/allenlooplee/RPABook/tree/master/src/HeartDiseaseDetection)|创建自定义预测活动|第8章 RPA x WF x WPF
[ML](https://github.com/allenlooplee/RPABook/tree/master/src/ML)|创建自定义数据处理活动|第8章 RPA x WF x WPF
[Demo7](https://github.com/allenlooplee/RPABook/tree/master/src/Demo7)|从京东金融中获取小金库收益信息，并在网易有钱中对账和记账|第9章 案例实践：货基收益自动对账

*注意：*
* *在运行 Demo4 之前，请在相应的地方填入你的百度 AI 开放平台的 API Key 和 Secret Key。*
* *RPA x OCR 已从调用百度 OCR 的 .NET SDK 演变成 [Cloud OCR Activities Pack](https://github.com/allenlooplee/CloudOcrActivitiesPack)，并支持百度、合合、腾讯和阿里等多个云 OCR 服务。*
