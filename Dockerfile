FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["BTI7252-SmartHomeCommander/BTI7252-SmartHomeCommander.csproj", "BTI7252-SmartHomeCommander/"]
RUN dotnet restore "BTI7252-SmartHomeCommander/BTI7252-SmartHomeCommander.csproj"
COPY . .
WORKDIR "/src/BTI7252-SmartHomeCommander"
RUN dotnet build "BTI7252-SmartHomeCommander.csproj" -o /app

FROM build AS publish
RUN dotnet publish "BTI7252-SmartHomeCommander.csproj" -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BTI7252-SmartHomeCommander.dll"]
