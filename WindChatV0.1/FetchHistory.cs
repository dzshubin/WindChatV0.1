// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: fetch_history.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace IM {

  /// <summary>Holder for reflection information generated from fetch_history.proto</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class FetchHistoryReflection {

    #region Descriptor
    /// <summary>File descriptor for fetch_history.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static FetchHistoryReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNmZXRjaF9oaXN0b3J5LnByb3RvEgJJTSI0Cg9GZXRjaEhpc3RvcnlSZXES",
            "DgoGcmVxX2lkGAEgASgDEhEKCXRhcmdldF9pZBgCIAEoA2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
            new pbr::GeneratedCodeInfo(typeof(global::IM.FetchHistoryReq), global::IM.FetchHistoryReq.Parser, new[]{ "ReqId", "TargetId" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class FetchHistoryReq : pb::IMessage<FetchHistoryReq> {
    private static readonly pb::MessageParser<FetchHistoryReq> _parser = new pb::MessageParser<FetchHistoryReq>(() => new FetchHistoryReq());
    public static pb::MessageParser<FetchHistoryReq> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::IM.FetchHistoryReflection.Descriptor.MessageTypes[0]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public FetchHistoryReq() {
      OnConstruction();
    }

    partial void OnConstruction();

    public FetchHistoryReq(FetchHistoryReq other) : this() {
      reqId_ = other.reqId_;
      targetId_ = other.targetId_;
    }

    public FetchHistoryReq Clone() {
      return new FetchHistoryReq(this);
    }

    /// <summary>Field number for the "req_id" field.</summary>
    public const int ReqIdFieldNumber = 1;
    private long reqId_;
    /// <summary>
    ///  请求者id
    /// </summary>
    public long ReqId {
      get { return reqId_; }
      set {
        reqId_ = value;
      }
    }

    /// <summary>Field number for the "target_id" field.</summary>
    public const int TargetIdFieldNumber = 2;
    private long targetId_;
    /// <summary>
    ///  目标用户id
    /// </summary>
    public long TargetId {
      get { return targetId_; }
      set {
        targetId_ = value;
      }
    }

    public override bool Equals(object other) {
      return Equals(other as FetchHistoryReq);
    }

    public bool Equals(FetchHistoryReq other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ReqId != other.ReqId) return false;
      if (TargetId != other.TargetId) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (ReqId != 0L) hash ^= ReqId.GetHashCode();
      if (TargetId != 0L) hash ^= TargetId.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (ReqId != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(ReqId);
      }
      if (TargetId != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(TargetId);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (ReqId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(ReqId);
      }
      if (TargetId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(TargetId);
      }
      return size;
    }

    public void MergeFrom(FetchHistoryReq other) {
      if (other == null) {
        return;
      }
      if (other.ReqId != 0L) {
        ReqId = other.ReqId;
      }
      if (other.TargetId != 0L) {
        TargetId = other.TargetId;
      }
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            ReqId = input.ReadInt64();
            break;
          }
          case 16: {
            TargetId = input.ReadInt64();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
