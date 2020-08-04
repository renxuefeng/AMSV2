# AMSV2
* 保持AMS项目功能不变
* 基于AMS项目调整项目结构 
* .net core平台升级至3.1
* 项目接口及仓储改为Task异步调用
# 新增
## Quartz调度任务
* 基于Quartz做定时任务处理
## 中间件
* 新增请求中间件记录接口访问记录
## 容器化
* AMSV2项目中新增Dockerfile文件，发布好的项目直接调用生成镜像即可
* 项目已部署在群辉NAS的docker中，地址:http://home.kaixixi.vip:60001/index.html
## UnitOfWork
* 工作单元模式做事务处理
## 缓存
* Redis做数据缓存
