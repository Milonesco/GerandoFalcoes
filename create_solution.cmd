@echo off
dotnet new sln -n TransformeseSolution
dotnet sln TransformeseSolution.sln add Transformese.Domain\Transformese.Domain.csproj
dotnet sln TransformeseSolution.sln add Transformese.Data\Transformese.Data.csproj
dotnet sln TransformeseSolution.sln add Transformese.Api\Transformese.Api.csproj
dotnet sln TransformeseSolution.sln add Transformese.Web\Transformese.Web.csproj
dotnet sln TransformeseSolution.sln add Transformese.Desktop\Transformese.Desktop.csproj
echo Solution created. Open TransformeseSolution.sln in Visual Studio.
pause
