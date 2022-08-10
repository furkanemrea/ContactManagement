#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Contact.API/Contact.API.csproj", "Contact.API/"]
COPY ["Contact.API.Infrastructure/Contact.API.Infrastructure.csproj", "Contact.API.Infrastructure/"]
COPY ["ContactAPI.Core/ContactAPI.Core.csproj", "ContactAPI.Core/"]
COPY ["CommonLibrary/CommonLibrary.csproj", "CommonLibrary/"]
COPY ["ValidationLibrary/ValidationLibrary.csproj", "ValidationLibrary/"]
COPY ["ContactAPI.Application/ContactAPI.Application.csproj", "ContactAPI.Application/"]
COPY ["LoggerLibrary/LoggerLibrary.csproj", "LoggerLibrary/"]
RUN dotnet restore "Contact.API/Contact.API.csproj"
COPY . .
WORKDIR "/src/Contact.API"
RUN dotnet build "Contact.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contact.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contact.API.dll"]