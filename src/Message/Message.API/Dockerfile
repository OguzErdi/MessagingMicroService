#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Message/Message.API/Message.API.csproj", "src/Message/Message.API/"]
COPY ["src/Message/Message.Infrastructure/Message.Infrastructure.csproj", "src/Message/Message.Infrastructure/"]
COPY ["src/Message/Message.Core/Message.Core.csproj", "src/Message/Message.Core/"]
COPY ["src/Message/Message.Application/Message.Application.csproj", "src/Message/Message.Application/"]
RUN dotnet restore "src/Message/Message.API/Message.API.csproj"
COPY . .
WORKDIR "/src/src/Message/Message.API"
RUN dotnet build "Message.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Message.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Message.API.dll"]
