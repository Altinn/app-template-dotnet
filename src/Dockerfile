FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine@sha256:abee55b11f42473da7a512149a4c1e48f5a1248533614f7e88f29de7ebd3411d AS build
WORKDIR /App

COPY /App/App.csproj .
RUN dotnet restore App.csproj

COPY /App .

RUN dotnet publish App.csproj --configuration Release --output /app_output

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine@sha256:503fd45ad9765c13bdc09bd715cf78b2c0a9bea397fde120a4c5ec496dc6b1db AS final
EXPOSE 5005
WORKDIR /App
COPY --from=build /app_output .
ENV ASPNETCORE_URLS=

# setup the user and group
# busybox doesn't include longopts, so the options are roughly
# -g --gid
# -u --uid
# -G --group
# -D --disable-password
# -s --shell
RUN addgroup -g 3000 dotnet && adduser -u 1000 -G dotnet -D -s /bin/false dotnet

# Add globalization timezone support
RUN apk add --no-cache icu-libs tzdata
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

USER dotnet
RUN mkdir /tmp/logtelemetry

ENTRYPOINT ["dotnet", "Altinn.Application.dll"]
