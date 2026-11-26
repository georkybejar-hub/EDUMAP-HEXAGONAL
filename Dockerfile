FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["EdumapHexagonal.Api/EdumapHexagonal.Api.csproj", "EdumapHexagonal.Api/"]
COPY ["EdumapHexagonal.Application/EdumapHexagonal.Application.csproj", "EdumapHexagonal.Application/"]
COPY ["EdumapHexagonal.Domain/EdumapHexagonal.Domain.csproj", "EdumapHexagonal.Domain/"]
COPY ["EdumapHexagonal.Infrastructure/EdumapHexagonal.Infrastructure.csproj", "EdumapHexagonal.Infrastructure/"]

RUN dotnet restore "EdumapHexagonal.Api/EdumapHexagonal.Api.csproj"

COPY . .
RUN dotnet publish "EdumapHexagonal.Api/EdumapHexagonal.Api.csproj" \
    -c Release -o /app/publish --no-restore /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=10000
EXPOSE 10000

COPY --from=build /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "EdumapHexagonal.Api.dll"]
