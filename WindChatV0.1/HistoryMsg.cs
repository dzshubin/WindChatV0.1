// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: history_msg.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace IM {

  /// <summary>Holder for reflection information generated from history_msg.proto</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class HistoryMsgReflection {

    #region Descriptor
    /// <summary>File descriptor for history_msg.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static HistoryMsgReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChFoaXN0b3J5X21zZy5wcm90bxICSU0aCmNoYXQucHJvdG8iUgoKSGlzdG9y",
            "eU1zZxIOCgZyZXFfaWQYASABKAMSEQoJdGFyZ2V0X2lkGAIgASgDEiEKDGNo",
            "YXRfbWVzc2FnZRgDIAMoCzILLklNLkNoYXRQa3RiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::IM.ChatReflection.Descriptor, },
          new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
            new pbr::GeneratedCodeInfo(typeof(global::IM.HistoryMsg), global::IM.HistoryMsg.Parser, new[]{ "ReqId", "TargetId", "ChatMessage" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class HistoryMsg : pb::IMessage<HistoryMsg> {
    private static readonly pb::MessageParser<HistoryMsg> _parser = new pb::MessageParser<HistoryMsg>(() => new HistoryMsg());
    public static pb::MessageParser<HistoryMsg> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::IM.HistoryMsgReflection.Descriptor.MessageTypes[0]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public HistoryMsg() {
      OnConstruction();
    }

    partial void OnConstruction();

    public HistoryMsg(HistoryMsg other) : this() {
      reqId_ = other.reqId_;
      targetId_ = other.targetId_;
      chatMessage_ = other.chatMessage_.Clone();
    }

    public HistoryMsg Clone() {
      return new HistoryMsg(this);
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

    /// <summary>Field number for the "chat_message" field.</summary>
    public const int ChatMessageFieldNumber = 3;
    private static readonly pb::FieldCodec<global::IM.ChatPkt> _repeated_chatMessage_codec
        = pb::FieldCodec.ForMessage(26, global::IM.ChatPkt.Parser);
    private readonly pbc::RepeatedField<global::IM.ChatPkt> chatMessage_ = new pbc::RepeatedField<global::IM.ChatPkt>();
    /// <summary>
    ///  消息集合
    /// </summary>
    public pbc::RepeatedField<global::IM.ChatPkt> ChatMessage {
      get { return chatMessage_; }
    }

    public override bool Equals(object other) {
      return Equals(other as HistoryMsg);
    }

    public bool Equals(HistoryMsg other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ReqId != other.ReqId) return false;
      if (TargetId != other.TargetId) return false;
      if(!chatMessage_.Equals(other.chatMessage_)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (ReqId != 0L) hash ^= ReqId.GetHashCode();
      if (TargetId != 0L) hash ^= TargetId.GetHashCode();
      hash ^= chatMessage_.GetHashCode();
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
      chatMessage_.WriteTo(output, _repeated_chatMessage_codec);
    }

    public int CalculateSize() {
      int size = 0;
      if (ReqId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(ReqId);
      }
      if (TargetId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(TargetId);
      }
      size += chatMessage_.CalculateSize(_repeated_chatMessage_codec);
      return size;
    }

    public void MergeFrom(HistoryMsg other) {
      if (other == null) {
        return;
      }
      if (other.ReqId != 0L) {
        ReqId = other.ReqId;
      }
      if (other.TargetId != 0L) {
        TargetId = other.TargetId;
      }
      chatMessage_.Add(other.chatMessage_);
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
          case 26: {
            chatMessage_.AddEntriesFrom(input, _repeated_chatMessage_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code