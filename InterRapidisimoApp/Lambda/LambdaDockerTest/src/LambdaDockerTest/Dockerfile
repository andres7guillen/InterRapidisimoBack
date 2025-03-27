FROM public.ecr.aws/lambda/dotnet:8

# Copia el contenido de tu carpeta de publicación al contenedor
COPY "bin/Release/net8.0/linux-x64/publish" /var/task


# Define el punto de entrada para la función Lambda
CMD ["LambdaDockerTest::LambdaDockerTest.Function::FunctionHandler"]