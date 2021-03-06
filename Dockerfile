FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

ENV ASPNETCORE_URLS "https://+443;http://+80"

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY . .
RUN dotnet restore "AnyTimerApi/AnyTimerApi.csproj"
WORKDIR "/src/AnyTimerApi"
RUN dotnet build "AnyTimerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnyTimerApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AnyTimerApi.dll"]