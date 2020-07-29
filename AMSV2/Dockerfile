#添加.nencore3.1基础镜像
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
#容器工作目录
WORKDIR /app
#指定项webapi或mvc站点的内部启动端口，默认是80
ENV ASPNETCORE_URLS http://+:50001
#暴露docker容器端口
EXPOSE 50001
#拷贝发布目录下的所有文件到容器的工作根目录
COPY . .
#容器入口点
ENTRYPOINT ["dotnet", "AMSV2.dll"]