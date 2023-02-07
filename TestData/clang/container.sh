docker run -itd --privileged --rm --name clangtest -v ~/Grpc/TestData/clang/Data:/opt/Data -v ~/Grpc/GrpcClient/PreparationFiles/clang/bin:/opt/bin/ clang:latest
