// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: join_channel.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace IM {

  /// <summary>Holder for reflection information generated from join_channel.proto</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class JoinChannelReflection {

    #region Descriptor
    /// <summary>File descriptor for join_channel.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static JoinChannelReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChJqb2luX2NoYW5uZWwucHJvdG8SAklNIkIKC0pvaW5DaGFubmVsEg8KB3Vz",
            "ZXJfaWQYASABKAMSEgoKY2hhbm5lbF9pZBgCIAEoBRIOCgZyZXN1bHQYAyAB",
            "KAViBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
            new pbr::GeneratedCodeInfo(typeof(global::IM.JoinChannel), global::IM.JoinChannel.Parser, new[]{ "UserId", "ChannelId", "Result" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class JoinChannel : pb::IMessage<JoinChannel> {
    private static readonly pb::MessageParser<JoinChannel> _parser = new pb::MessageParser<JoinChannel>(() => new JoinChannel());
    public static pb::MessageParser<JoinChannel> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::IM.JoinChannelReflection.Descriptor.MessageTypes[0]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public JoinChannel() {
      OnConstruction();
    }

    partial void OnConstruction();

    public JoinChannel(JoinChannel other) : this() {
      userId_ = other.userId_;
      channelId_ = other.channelId_;
      result_ = other.result_;
    }

    public JoinChannel Clone() {
      return new JoinChannel(this);
    }

    /// <summary>Field number for the "user_id" field.</summary>
    public const int UserIdFieldNumber = 1;
    private long userId_;
    /// <summary>
    ///  请求加入的用户id
    /// </summary>
    public long UserId {
      get { return userId_; }
      set {
        userId_ = value;
      }
    }

    /// <summary>Field number for the "channel_id" field.</summary>
    public const int ChannelIdFieldNumber = 2;
    private int channelId_;
    /// <summary>
    ///  请求要加入的频道id
    /// </summary>
    public int ChannelId {
      get { return channelId_; }
      set {
        channelId_ = value;
      }
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 3;
    private int result_;
    /// <summary>
    ///  加入结果。 请求时不设置这个域
    /// </summary>
    public int Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    public override bool Equals(object other) {
      return Equals(other as JoinChannel);
    }

    public bool Equals(JoinChannel other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (UserId != other.UserId) return false;
      if (ChannelId != other.ChannelId) return false;
      if (Result != other.Result) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (UserId != 0L) hash ^= UserId.GetHashCode();
      if (ChannelId != 0) hash ^= ChannelId.GetHashCode();
      if (Result != 0) hash ^= Result.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (UserId != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(UserId);
      }
      if (ChannelId != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(ChannelId);
      }
      if (Result != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Result);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (UserId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(UserId);
      }
      if (ChannelId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ChannelId);
      }
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Result);
      }
      return size;
    }

    public void MergeFrom(JoinChannel other) {
      if (other == null) {
        return;
      }
      if (other.UserId != 0L) {
        UserId = other.UserId;
      }
      if (other.ChannelId != 0) {
        ChannelId = other.ChannelId;
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
          case 8: {
            UserId = input.ReadInt64();
            break;
          }
          case 16: {
            ChannelId = input.ReadInt32();
            break;
          }
          case 24: {
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
