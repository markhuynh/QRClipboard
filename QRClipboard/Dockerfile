FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
RUN apk add libgdiplus --repository=http://dl-cdn.alpinelinux.org/alpine/edge/testing/
WORKDIR /app
COPY --from=build /src/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "QRClipboard.dll"]
