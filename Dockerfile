FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

WORKDIR /src
COPY ["Number5Poc/Number5Poc.csproj", "Number5Poc/"]
COPY ["Number5Poc.Data/Number5Poc.Data.csproj", "Number5Poc.Data/"]
COPY ["Number5Poc.Services/Number5Poc.Services.csproj", "Number5Poc.Services/"]
RUN dotnet restore "Number5Poc/Number5Poc.csproj"
COPY . .
WORKDIR "/src/Number5Poc"
RUN dotnet build "Number5Poc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Number5Poc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Number5Poc.dll"]
