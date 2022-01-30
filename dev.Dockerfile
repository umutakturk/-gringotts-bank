FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/GringottsBank.API/GringottsBank.API.csproj", "src/GringottsBank.API/"]
COPY ["src/GringottsBank.Application/GringottsBank.Application.csproj", "src/GringottsBank.Application/"]
COPY ["src/GringottsBank.Infrastructure/GringottsBank.Infrastructure.csproj", "src/GringottsBank.Infrastructure/"]
COPY ["src/GringottsBank.Domain/GringottsBank.Domain.csproj", "src/GringottsBank.Domain/"]
COPY ["src/GringottsBank.Common/GringottsBank.Common.csproj", "src/GringottsBank.Common/"]
RUN dotnet restore "src/GringottsBank.API/GringottsBank.API.csproj"
COPY . .
WORKDIR "/src/src/GringottsBank.API"
RUN dotnet build "GringottsBank.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GringottsBank.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GringottsBank.API.dll"]
