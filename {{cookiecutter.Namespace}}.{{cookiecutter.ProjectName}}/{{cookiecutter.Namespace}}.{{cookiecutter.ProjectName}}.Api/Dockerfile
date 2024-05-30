FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./ca_cert.pem /usr/local/share/ca-certificates/ca_cert.crt
RUN update-ca-certificates --verbose
COPY app/publish  .
ENV ASPNETCORE_HTTP_PORTS=80
ENTRYPOINT ["dotnet", "{{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.Api.dll"]
