// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: operate_channel.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace IM {

  /// <summary>Holder for reflection information generated from operate_channel.proto</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class OperateChannelReflection {

    #region Descriptor
    /// <summary>File descriptor for operate_channel.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static OperateChannelReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVvcGVyYXRlX2NoYW5uZWwucHJvdG8SAklNGhZvcGVyYXRlX3JlcV9iYXNl",
            "LnByb3RvIkYKDk9wZXJhdGVDaGFubmVsEiQKCHJlcV9iYXNlGAEgASgLMhIu",
            "SU0uT3BlcmF0ZVJlcUJhc2USDgoGcmVzdWx0GAIgASgFYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::IM.OperateReqBaseReflection.Descriptor, },
          new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
            new pbr::GeneratedCodeInfo(typeof(global::IM.OperateChannel), global::IM.OperateChannel.Parser, new[]{ "ReqBase", "Result" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class OperateChannel : pb::IMessage<OperateChannel> {
    private static readonly pb::MessageParser<OperateChannel> _parser = new pb::MessageParser<OperateChannel>(() => new OperateChannel());
    public static pb::MessageParser<OperateChannel> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::IM.OperateChannelReflection.Descriptor.MessageTypes[0]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public OperateChannel() {
      OnConstruction();
    }

    partial void OnConstruction();

    public OperateChannel(OperateChannel other) : this() {
      ReqBase = other.reqBase_ != null ? other.ReqBase.Clone() : null;
      result_ = other.result_;
    }

    public OperateChannel Clone() {
      return new OperateChannel(this);
    }

    /// <summary>Field number for the "req_base" field.</summary>
    public const int ReqBaseFieldNumber = 1;
    private global::IM.OperateReqBase reqBase_;
    public global::IM.OperateReqBase ReqBase {
      get { return reqBase_; }
      set {
        reqBase_ = value;
      }
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 2;
    private int result_;
    /// <summary>
    ///  加入/退出结果。 请求时不设置这个域
    /// </summary>
    public int Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    public override bool Equals(object other) {
      return Equals(other as OperateChannel);
    }

    public bool Equals(OperateChannel other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(ReqBase, other.ReqBase)) return false;
      if (Result != other.Result) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (reqBase_ != null) hash ^= ReqBase.GetHashCode();
      if (Result != 0) hash ^= Result.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (reqBase_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(ReqBase);
      }
      if (Result != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Result);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (reqBase_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ReqBase);
      }
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Result);
      }
      return size;
    }

    public void MergeFrom(OperateChannel other) {
      if (other == null) {
        return;
      }
      if (other.reqBase_ != null) {
        if (reqBase_ == null) {
          reqBase_ = new global::IM.OperateReqBase();
        }
        ReqBase.MergeFrom(other.ReqBase);
      }
      if (other.Result != 0) {
        Result = other.Result;
      }
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            if (reqBase_ == null) {
              reqBase_ = new global::IM.OperateReqBase();
            }
            input.ReadMessage(reqBase_);
            break;
          }
          case 16: {
            Result = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
