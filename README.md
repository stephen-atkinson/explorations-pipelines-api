dotnet ef migrations add  Initial --project Explorations.Pipelines.Api

dotnet ef migrations bundle -r osx-arm64 --project Explorations.Pipelines.Api --verbose

./efbundle --connection "Server=tcp:explorations-pipelines.database.windows.net,1433;Initial Catalog=Explorations-Pipelines-Development;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;"
