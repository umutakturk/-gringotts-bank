FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS publish

WORKDIR /source
COPY src/. .
WORKDIR /source/GringottsBank.API
RUN dotnet restore --runtime alpine-x64
RUN dotnet publish -c Release -o /app/publish \
    --no-restore \
    --self-contained \
    --runtime alpine-x64 \
    /p:PublishTrimmed=true \
    /p:PublishSingleFile=true

FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-alpine AS final
WORKDIR /app
RUN apk update && \
    apk add --no-cache bash icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib && \
    rm -rf /var/cache/apk/*
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS=http://*:$PORT ./GringottsBank.API
