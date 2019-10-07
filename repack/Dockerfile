FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS sdk
COPY . /src
WORKDIR /src
RUN dotnet publish -c release -o publish

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
COPY --from=sdk /src/publish /repack
COPY ./entrypoint.sh /repack/entrypoint.sh
COPY ./db.yml /repack/db.yml
COPY ./ts /repack/ts
COPY ./tsconfig.json /repack/tsconfig.json
COPY ./webpack.config.js /repack/webpack.config.js
COPY ./package.json /repack/package.json
WORKDIR /repack
RUN apt-get update && \
    apt-get install -my wget gnupg && \
    curl -sL https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y nodejs npm && \
    npm install && \
    npm run build && \
    chmod +x ./entrypoint.sh
EXPOSE 80
ENTRYPOINT ["./entrypoint.sh"] 