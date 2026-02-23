###########################################################################################################################################################################
# Use multi-stage builds to optimize the final image size and build process.                                                                                              #
#                                                                                                                                                                         #
# A multi-stage build allows us to separate the build environments and pass forward only the necessary artifacts.                                                         #
# This results in a smaller final image and faster build times, due to removing the need to rebuild the entire image when making changes to the source code.              #
###########################################################################################################################################################################

# Building Stage - Use the heavier SDK image and compiles the application and its dependencies
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy all .csproj files individually
COPY ["src/DeviceGateway.Api/DeviceGateway.Api.csproj", "DeviceGateway.Api/"]
COPY ["src/DeviceGateway.Application/DeviceGateway.Application.csproj", "DeviceGateway.Application/"]
COPY ["src/DeviceGateway.Infrastructure/DeviceGateway.Infrastructure.csproj", "DeviceGateway.Infrastructure/"]
COPY ["src/DeviceGateway.Domain/DeviceGateway.Domain.csproj", "DeviceGateway.Domain/"]

# Restore dependencies
RUN dotnet restore "DeviceGateway.Api/DeviceGateway.Api.csproj"

# Copy the rest of the source code
COPY src/ .
WORKDIR "/src/DeviceGateway.Api"

# Compile and "Publish" the app
RUN dotnet publish "DeviceGateway.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage - Use the lighter runtime image to run the application with only the necessary runtime files
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DeviceGateway.Api.dll"]