#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/User/User.API/User.API.csproj", "src/User/User.API/"]
COPY ["src/User/User.Infrastructer/User.Infrastructer.csproj", "src/User/User.Infrastructer/"]
COPY ["src/User/User.Core/User.Core.csproj", "src/User/User.Core/"]
COPY ["src/User/User.Application/User.Application.csproj", "src/User/User.Application/"]
RUN dotnet restore "src/User/User.API/User.API.csproj"
COPY . .
WORKDIR "/src/src/User/User.API"
RUN dotnet build "User.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.API.dll"]
