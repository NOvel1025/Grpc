// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/docker.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GrpcClient {
  public static partial class Docker
  {
    static readonly string __ServiceName = "docker.Docker";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcClient.ExecutionResult> __Marshaller_docker_ExecutionResult = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcClient.ExecutionResult.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcClient.SubmissionInformation> __Marshaller_docker_SubmissionInformation = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcClient.SubmissionInformation.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcClient.ExecutionOutput> __Marshaller_docker_ExecutionOutput = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcClient.ExecutionOutput.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcClient.ExecutionInput> __Marshaller_docker_ExecutionInput = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcClient.ExecutionInput.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcClient.ExecutionResult, global::GrpcClient.SubmissionInformation> __Method_AutoExecStream = new grpc::Method<global::GrpcClient.ExecutionResult, global::GrpcClient.SubmissionInformation>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "AutoExecStream",
        __Marshaller_docker_ExecutionResult,
        __Marshaller_docker_SubmissionInformation);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcClient.SubmissionInformation, global::GrpcClient.ExecutionResult> __Method_AutoExec = new grpc::Method<global::GrpcClient.SubmissionInformation, global::GrpcClient.ExecutionResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AutoExec",
        __Marshaller_docker_SubmissionInformation,
        __Marshaller_docker_ExecutionResult);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::GrpcClient.SubmissionInformation> __Method_ManualRequest = new grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::GrpcClient.SubmissionInformation>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "ManualRequest",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_docker_SubmissionInformation);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcClient.ExecutionOutput, global::GrpcClient.ExecutionInput> __Method_ManualExecStream = new grpc::Method<global::GrpcClient.ExecutionOutput, global::GrpcClient.ExecutionInput>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "ManualExecStream",
        __Marshaller_docker_ExecutionOutput,
        __Marshaller_docker_ExecutionInput);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcClient.SubmissionInformation, global::GrpcClient.ExecutionOutput> __Method_ManualEClientStream = new grpc::Method<global::GrpcClient.SubmissionInformation, global::GrpcClient.ExecutionOutput>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "ManualEClientStream",
        __Marshaller_docker_SubmissionInformation,
        __Marshaller_docker_ExecutionOutput);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GrpcClient.DockerReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for Docker</summary>
    public partial class DockerClient : grpc::ClientBase<DockerClient>
    {
      /// <summary>Creates a new client for Docker</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public DockerClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Docker that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public DockerClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected DockerClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected DockerClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      ///自動実行用のファイル情報送信　引数：実行結果　戻り値：ファイル情報
      /// </summary>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::GrpcClient.ExecutionResult, global::GrpcClient.SubmissionInformation> AutoExecStream(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AutoExecStream(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///自動実行用のファイル情報送信　引数：実行結果　戻り値：ファイル情報
      /// </summary>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::GrpcClient.ExecutionResult, global::GrpcClient.SubmissionInformation> AutoExecStream(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_AutoExecStream, null, options);
      }
      /// <summary>
      ///自動実行依頼送信　引数：ファイル情報　戻り値：実行結果
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GrpcClient.ExecutionResult AutoExec(global::GrpcClient.SubmissionInformation request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AutoExec(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///自動実行依頼送信　引数：ファイル情報　戻り値：実行結果
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GrpcClient.ExecutionResult AutoExec(global::GrpcClient.SubmissionInformation request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AutoExec, null, options, request);
      }
      /// <summary>
      ///自動実行依頼送信　引数：ファイル情報　戻り値：実行結果
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GrpcClient.ExecutionResult> AutoExecAsync(global::GrpcClient.SubmissionInformation request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AutoExecAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///自動実行依頼送信　引数：ファイル情報　戻り値：実行結果
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GrpcClient.ExecutionResult> AutoExecAsync(global::GrpcClient.SubmissionInformation request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AutoExec, null, options, request);
      }
      /// <summary>
      ///手動実行リクエスト用の待機サーバーストリーム　引数：なし　戻り値：ファイル情報
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncServerStreamingCall<global::GrpcClient.SubmissionInformation> ManualRequest(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ManualRequest(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///手動実行リクエスト用の待機サーバーストリーム　引数：なし　戻り値：ファイル情報
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncServerStreamingCall<global::GrpcClient.SubmissionInformation> ManualRequest(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_ManualRequest, null, options, request);
      }
      /// <summary>
      ///手動実行ストリーム　引数：手動実行出力　戻り値：手動実行入力
      /// </summary>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::GrpcClient.ExecutionOutput, global::GrpcClient.ExecutionInput> ManualExecStream(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ManualExecStream(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///手動実行ストリーム　引数：手動実行出力　戻り値：手動実行入力
      /// </summary>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::GrpcClient.ExecutionOutput, global::GrpcClient.ExecutionInput> ManualExecStream(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_ManualExecStream, null, options);
      }
      /// <summary>
      ///手動実行ストリームEX2のクライアント側との通信　引数：ファイル情報　戻り値：手動実行出力
      /// </summary>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::GrpcClient.SubmissionInformation, global::GrpcClient.ExecutionOutput> ManualEClientStream(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ManualEClientStream(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///手動実行ストリームEX2のクライアント側との通信　引数：ファイル情報　戻り値：手動実行出力
      /// </summary>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::GrpcClient.SubmissionInformation, global::GrpcClient.ExecutionOutput> ManualEClientStream(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_ManualEClientStream, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override DockerClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new DockerClient(configuration);
      }
    }

  }
}
#endregion
