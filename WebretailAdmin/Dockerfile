FROM microsoft/dotnet:1.1.0-sdk-msbuild
WORKDIR /app

EXPOSE 5000 5001

RUN curl -sL https://deb.nodesource.com/setup_7.x | bash -
RUN apt-get update
RUN apt-get -qq update
RUN apt-get install -y nodejs
RUN update-alternatives --install /usr/bin/node node /usr/bin/nodejs 10

#ADD WebretailAdmin.csproj .
COPY . .

RUN npm install
RUN npm run build-production
RUN rm -rf aot
RUN rm -rf node_modules

RUN dotnet restore
RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet", "out/WebretailService.dll"]
