// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/docker.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace GrpcServer {

  /// <summary>Holder for reflection information generated from Protos/docker.proto</summary>
  public static partial class DockerReflection {

    #region Descriptor
    /// <summary>File descriptor for Protos/docker.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static DockerReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNQcm90b3MvZG9ja2VyLnByb3RvEgZkb2NrZXIaG2dvb2dsZS9wcm90b2J1",
            "Zi9lbXB0eS5wcm90byKFAQoORmlsZUluZm9tYXRpb24SEAoIZmlsZU5hbWUY",
            "ASABKAkSGAoQc3VibWlzc2lvbkZpbGVJZBgCIAEoCRIUCgxhbnN3ZXJGaWxl",
            "SWQYAyABKAkSDAoEbGFuZxgEIAEoCRIRCgltYXRjaFR5cGUYBSABKAUSEAoI",
            "aW5wdXRTdHIYBiABKAkiUQoPRXhlY3V0aW9uUmVzdWx0EhYKDnN1Ym1pc3Np",
            "b25GaWxlGAEgASgJEhIKCmFuc3dlckZpbGUYAiABKAkSEgoKY29ycmVjdGlv",
            "bhgDIAEoBSIrCg5FeGVjdXRpb25JbnB1dBIZChFleGVjdXRpb25JbnB1dFN0",
            "chgBIAEoCSItCg9FeGVjdXRpb25PdXRwdXQSGgoSZXhlY3V0aW9uT3V0cHV0",
            "U3RyGAEgASgJMuQCCgZEb2NrZXISRQoOQXV0b0V4ZWNTdHJlYW0SFy5kb2Nr",
            "ZXIuRXhlY3V0aW9uUmVzdWx0GhYuZG9ja2VyLkZpbGVJbmZvbWF0aW9uKAEw",
            "ARI7CghBdXRvRXhlYxIWLmRvY2tlci5GaWxlSW5mb21hdGlvbhoXLmRvY2tl",
            "ci5FeGVjdXRpb25SZXN1bHQSQQoNTWFudWFsUmVxdWVzdBIWLmdvb2dsZS5w",
            "cm90b2J1Zi5FbXB0eRoWLmRvY2tlci5GaWxlSW5mb21hdGlvbjABEkcKEE1h",
            "bnVhbEV4ZWNTdHJlYW0SFy5kb2NrZXIuRXhlY3V0aW9uT3V0cHV0GhYuZG9j",
            "a2VyLkV4ZWN1dGlvbklucHV0KAEwARJKChNNYW51YWxFQ2xpZW50U3RyZWFt",
            "EhYuZG9ja2VyLkZpbGVJbmZvbWF0aW9uGhcuZG9ja2VyLkV4ZWN1dGlvbk91",
            "dHB1dCgBMAFCDaoCCkdycGNTZXJ2ZXJiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.EmptyReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::GrpcServer.FileInfomation), global::GrpcServer.FileInfomation.Parser, new[]{ "FileName", "SubmissionFileId", "AnswerFileId", "Lang", "MatchType", "InputStr" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::GrpcServer.ExecutionResult), global::GrpcServer.ExecutionResult.Parser, new[]{ "SubmissionFile", "AnswerFile", "Correction" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::GrpcServer.ExecutionInput), global::GrpcServer.ExecutionInput.Parser, new[]{ "ExecutionInputStr" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::GrpcServer.ExecutionOutput), global::GrpcServer.ExecutionOutput.Parser, new[]{ "ExecutionOutputStr" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  ///ファイル情報　
  ///fileName：ファイル名　submissionFileId：提出ファイルのID　answerFileId：回答ファイルのID　lang：言語　matchType：比較方法　inputStr：入力文字列
  /// </summary>
  public sealed partial class FileInfomation : pb::IMessage<FileInfomation>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<FileInfomation> _parser = new pb::MessageParser<FileInfomation>(() => new FileInfomation());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<FileInfomation> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::GrpcServer.DockerReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FileInfomation() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FileInfomation(FileInfomation other) : this() {
      fileName_ = other.fileName_;
      submissionFileId_ = other.submissionFileId_;
      answerFileId_ = other.answerFileId_;
      lang_ = other.lang_;
      matchType_ = other.matchType_;
      inputStr_ = other.inputStr_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FileInfomation Clone() {
      return new FileInfomation(this);
    }

    /// <summary>Field number for the "fileName" field.</summary>
    public const int FileNameFieldNumber = 1;
    private string fileName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string FileName {
      get { return fileName_; }
      set {
        fileName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "submissionFileId" field.</summary>
    public const int SubmissionFileIdFieldNumber = 2;
    private string submissionFileId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string SubmissionFileId {
      get { return submissionFileId_; }
      set {
        submissionFileId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "answerFileId" field.</summary>
    public const int AnswerFileIdFieldNumber = 3;
    private string answerFileId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string AnswerFileId {
      get { return answerFileId_; }
      set {
        answerFileId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "lang" field.</summary>
    public const int LangFieldNumber = 4;
    private string lang_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Lang {
      get { return lang_; }
      set {
        lang_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "matchType" field.</summary>
    public const int MatchTypeFieldNumber = 5;
    private int matchType_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int MatchType {
      get { return matchType_; }
      set {
        matchType_ = value;
      }
    }

    /// <summary>Field number for the "inputStr" field.</summary>
    public const int InputStrFieldNumber = 6;
    private string inputStr_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string InputStr {
      get { return inputStr_; }
      set {
        inputStr_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as FileInfomation);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(FileInfomation other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (FileName != other.FileName) return false;
      if (SubmissionFileId != other.SubmissionFileId) return false;
      if (AnswerFileId != other.AnswerFileId) return false;
      if (Lang != other.Lang) return false;
      if (MatchType != other.MatchType) return false;
      if (InputStr != other.InputStr) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (FileName.Length != 0) hash ^= FileName.GetHashCode();
      if (SubmissionFileId.Length != 0) hash ^= SubmissionFileId.GetHashCode();
      if (AnswerFileId.Length != 0) hash ^= AnswerFileId.GetHashCode();
      if (Lang.Length != 0) hash ^= Lang.GetHashCode();
      if (MatchType != 0) hash ^= MatchType.GetHashCode();
      if (InputStr.Length != 0) hash ^= InputStr.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (FileName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(FileName);
      }
      if (SubmissionFileId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(SubmissionFileId);
      }
      if (AnswerFileId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(AnswerFileId);
      }
      if (Lang.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Lang);
      }
      if (MatchType != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(MatchType);
      }
      if (InputStr.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(InputStr);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (FileName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(FileName);
      }
      if (SubmissionFileId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(SubmissionFileId);
      }
      if (AnswerFileId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(AnswerFileId);
      }
      if (Lang.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Lang);
      }
      if (MatchType != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(MatchType);
      }
      if (InputStr.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(InputStr);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (FileName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(FileName);
      }
      if (SubmissionFileId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SubmissionFileId);
      }
      if (AnswerFileId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(AnswerFileId);
      }
      if (Lang.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Lang);
      }
      if (MatchType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MatchType);
      }
      if (InputStr.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(InputStr);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(FileInfomation other) {
      if (other == null) {
        return;
      }
      if (other.FileName.Length != 0) {
        FileName = other.FileName;
      }
      if (other.SubmissionFileId.Length != 0) {
        SubmissionFileId = other.SubmissionFileId;
      }
      if (other.AnswerFileId.Length != 0) {
        AnswerFileId = other.AnswerFileId;
      }
      if (other.Lang.Length != 0) {
        Lang = other.Lang;
      }
      if (other.MatchType != 0) {
        MatchType = other.MatchType;
      }
      if (other.InputStr.Length != 0) {
        InputStr = other.InputStr;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            FileName = input.ReadString();
            break;
          }
          case 18: {
            SubmissionFileId = input.ReadString();
            break;
          }
          case 26: {
            AnswerFileId = input.ReadString();
            break;
          }
          case 34: {
            Lang = input.ReadString();
            break;
          }
          case 40: {
            MatchType = input.ReadInt32();
            break;
          }
          case 50: {
            InputStr = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            FileName = input.ReadString();
            break;
          }
          case 18: {
            SubmissionFileId = input.ReadString();
            break;
          }
          case 26: {
            AnswerFileId = input.ReadString();
            break;
          }
          case 34: {
            Lang = input.ReadString();
            break;
          }
          case 40: {
            MatchType = input.ReadInt32();
            break;
          }
          case 50: {
            InputStr = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  /// <summary>
  ///実行結果　submissionFile：提出ファイルの実行結果　answerFile：回答ファイルの実行結果　correction：成否
  /// </summary>
  public sealed partial class ExecutionResult : pb::IMessage<ExecutionResult>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ExecutionResult> _parser = new pb::MessageParser<ExecutionResult>(() => new ExecutionResult());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ExecutionResult> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::GrpcServer.DockerReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExecutionResult() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExecutionResult(ExecutionResult other) : this() {
      submissionFile_ = other.submissionFile_;
      answerFile_ = other.answerFile_;
      correction_ = other.correction_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExecutionResult Clone() {
      return new ExecutionResult(this);
    }

    /// <summary>Field number for the "submissionFile" field.</summary>
    public const int SubmissionFileFieldNumber = 1;
    private string submissionFile_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string SubmissionFile {
      get { return submissionFile_; }
      set {
        submissionFile_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "answerFile" field.</summary>
    public const int AnswerFileFieldNumber = 2;
    private string answerFile_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string AnswerFile {
      get { return answerFile_; }
      set {
        answerFile_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "correction" field.</summary>
    public const int CorrectionFieldNumber = 3;
    private int correction_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Correction {
      get { return correction_; }
      set {
        correction_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ExecutionResult);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ExecutionResult other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (SubmissionFile != other.SubmissionFile) return false;
      if (AnswerFile != other.AnswerFile) return false;
      if (Correction != other.Correction) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (SubmissionFile.Length != 0) hash ^= SubmissionFile.GetHashCode();
      if (AnswerFile.Length != 0) hash ^= AnswerFile.GetHashCode();
      if (Correction != 0) hash ^= Correction.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (SubmissionFile.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(SubmissionFile);
      }
      if (AnswerFile.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(AnswerFile);
      }
      if (Correction != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Correction);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (SubmissionFile.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(SubmissionFile);
      }
      if (AnswerFile.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(AnswerFile);
      }
      if (Correction != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Correction);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (SubmissionFile.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SubmissionFile);
      }
      if (AnswerFile.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(AnswerFile);
      }
      if (Correction != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Correction);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ExecutionResult other) {
      if (other == null) {
        return;
      }
      if (other.SubmissionFile.Length != 0) {
        SubmissionFile = other.SubmissionFile;
      }
      if (other.AnswerFile.Length != 0) {
        AnswerFile = other.AnswerFile;
      }
      if (other.Correction != 0) {
        Correction = other.Correction;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            SubmissionFile = input.ReadString();
            break;
          }
          case 18: {
            AnswerFile = input.ReadString();
            break;
          }
          case 24: {
            Correction = input.ReadInt32();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            SubmissionFile = input.ReadString();
            break;
          }
          case 18: {
            AnswerFile = input.ReadString();
            break;
          }
          case 24: {
            Correction = input.ReadInt32();
            break;
          }
        }
      }
    }
    #endif

  }

  /// <summary>
  ///手動実行入力　executionInputStr：入力文字列
  /// </summary>
  public sealed partial class ExecutionInput : pb::IMessage<ExecutionInput>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ExecutionInput> _parser = new pb::MessageParser<ExecutionInput>(() => new ExecutionInput());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ExecutionInput> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::GrpcServer.DockerReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExecutionInput() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExecutionInput(ExecutionInput other) : this() {
      executionInputStr_ = other.executionInputStr_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExecutionInput Clone() {
      return new ExecutionInput(this);
    }

    /// <summary>Field number for the "executionInputStr" field.</summary>
    public const int ExecutionInputStrFieldNumber = 1;
    private string executionInputStr_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string ExecutionInputStr {
      get { return executionInputStr_; }
      set {
        executionInputStr_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ExecutionInput);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ExecutionInput other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ExecutionInputStr != other.ExecutionInputStr) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (ExecutionInputStr.Length != 0) hash ^= ExecutionInputStr.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (ExecutionInputStr.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ExecutionInputStr);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (ExecutionInputStr.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ExecutionInputStr);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (ExecutionInputStr.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ExecutionInputStr);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ExecutionInput other) {
      if (other == null) {
        return;
      }
      if (other.ExecutionInputStr.Length != 0) {
        ExecutionInputStr = other.ExecutionInputStr;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            ExecutionInputStr = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            ExecutionInputStr = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  /// <summary>
  ///手動実行出力　executionOutputStr：出力文字列
  /// </summary>
  public sealed partial class ExecutionOutput : pb::IMessage<ExecutionOutput>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ExecutionOutput> _parser = new pb::MessageParser<ExecutionOutput>(() => new ExecutionOutput());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ExecutionOutput> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::GrpcServer.DockerReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExecutionOutput() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExecutionOutput(ExecutionOutput other) : this() {
      executionOutputStr_ = other.executionOutputStr_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExecutionOutput Clone() {
      return new ExecutionOutput(this);
    }

    /// <summary>Field number for the "executionOutputStr" field.</summary>
    public const int ExecutionOutputStrFieldNumber = 1;
    private string executionOutputStr_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string ExecutionOutputStr {
      get { return executionOutputStr_; }
      set {
        executionOutputStr_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ExecutionOutput);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ExecutionOutput other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ExecutionOutputStr != other.ExecutionOutputStr) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (ExecutionOutputStr.Length != 0) hash ^= ExecutionOutputStr.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (ExecutionOutputStr.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ExecutionOutputStr);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (ExecutionOutputStr.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ExecutionOutputStr);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (ExecutionOutputStr.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ExecutionOutputStr);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ExecutionOutput other) {
      if (other == null) {
        return;
      }
      if (other.ExecutionOutputStr.Length != 0) {
        ExecutionOutputStr = other.ExecutionOutputStr;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            ExecutionOutputStr = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            ExecutionOutputStr = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
