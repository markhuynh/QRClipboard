FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
COPY --from=build /src/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "QRClipboard.dll"]
