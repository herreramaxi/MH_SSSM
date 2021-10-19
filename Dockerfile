# Dockerfile

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443
#RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
#RUN apt-get install -y nodejs

#COPY SSSM.Website/ClientApp/package*.json ./

# Copy csproj and restore as distinct layers
COPY SSSM.sln ./
#COPY *.csproj ./
COPY SSSM.Api/*.csproj ./SSSM.Api/
COPY SSSM.Model/*.csproj ./SSSM.Model/
COPY SSSM.Repositories/*.csproj ./SSSM.Repositories/
COPY SSSM.Services/*.csproj ./SSSM.Services/
COPY SSSM.WebAPI/*.csproj ./SSSM.WebAPI/
#COPY SSSM.Website/*.csproj ./SSSM.Website/
RUN dotnet restore -p:RestoreUseSkipNonexistentTargets=false

#RUN npm install -g npm
#RUN npm install

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

# Run the app on container startup
# Use your project name for the second parameter
# e.g. MyProject.dll
#ENTRYPOINT [ "dotnet", "MvcMovie.dll" ]
# Use the following instead for Heroku

CMD ASPNETCORE_URLS=http://*:$PORT dotnet SSSM.WebAPI.dll
