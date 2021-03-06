// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: channel_members.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace IM {

  /// <summary>Holder for reflection information generated from channel_members.proto</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class ChannelMembersReflection {

    #region Descriptor
    /// <summary>File descriptor for channel_members.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ChannelMembersReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVjaGFubmVsX21lbWJlcnMucHJvdG8SAklNGg5jb250YWN0cy5wcm90byI/",
            "ChJDaGFubmVsTWVtYmVyc0luZm8SKQoOY2hhbm5lbF9tZW1iZXIYASADKAsy",
            "ES5JTS5DaGFubmVsTWVtYmVyIjwKDUNoYW5uZWxNZW1iZXISEgoKY2hhbm5l",
            "bF9pZBgBIAEoBRIXCgV1c2VycxgCIAMoCzIILklNLlVzZXJiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::IM.ContactsReflection.Descriptor, },
          new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
            new pbr::GeneratedCodeInfo(typeof(global::IM.ChannelMembersInfo), global::IM.ChannelMembersInfo.Parser, new[]{ "ChannelMember" }, null, null, null),
            new pbr::GeneratedCodeInfo(typeof(global::IM.ChannelMember), global::IM.ChannelMember.Parser, new[]{ "ChannelId", "Users" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class ChannelMembersInfo : pb::IMessage<ChannelMembersInfo> {
    private static readonly pb::MessageParser<ChannelMembersInfo> _parser = new pb::MessageParser<ChannelMembersInfo>(() => new ChannelMembersInfo());
    public static pb::MessageParser<ChannelMembersInfo> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::IM.ChannelMembersReflection.Descriptor.MessageTypes[0]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public ChannelMembersInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    public ChannelMembersInfo(ChannelMembersInfo other) : this() {
      channelMember_ = other.channelMember_.Clone();
    }

    public ChannelMembersInfo Clone() {
      return new ChannelMembersInfo(this);
    }

    /// <summary>Field number for the "channel_member" field.</summary>
    public const int ChannelMemberFieldNumber = 1;
    private static readonly pb::FieldCodec<global::IM.ChannelMember> _repeated_channelMember_codec
        = pb::FieldCodec.ForMessage(10, global::IM.ChannelMember.Parser);
    private readonly pbc::RepeatedField<global::IM.ChannelMember> channelMember_ = new pbc::RepeatedField<global::IM.ChannelMember>();
    public pbc::RepeatedField<global::IM.ChannelMember> ChannelMember {
      get { return channelMember_; }
    }

    public override bool Equals(object other) {
      return Equals(other as ChannelMembersInfo);
    }

    public bool Equals(ChannelMembersInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!channelMember_.Equals(other.channelMember_)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      hash ^= channelMember_.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      channelMember_.WriteTo(output, _repeated_channelMember_codec);
    }

    public int CalculateSize() {
      int size = 0;
      size += channelMember_.CalculateSize(_repeated_channelMember_codec);
      return size;
    }

    public void MergeFrom(ChannelMembersInfo other) {
      if (other == null) {
        return;
      }
      channelMember_.Add(other.channelMember_);
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            channelMember_.AddEntriesFrom(input, _repeated_channelMember_codec);
            break;
          }
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class ChannelMember : pb::IMessage<ChannelMember> {
    private static readonly pb::MessageParser<ChannelMember> _parser = new pb::MessageParser<ChannelMember>(() => new ChannelMember());
    public static pb::MessageParser<ChannelMember> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::IM.ChannelMembersReflection.Descriptor.MessageTypes[1]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public ChannelMember() {
      OnConstruction();
    }

    partial void OnConstruction();

    public ChannelMember(ChannelMember other) : this() {
      channelId_ = other.channelId_;
      users_ = other.users_.Clone();
    }

    public ChannelMember Clone() {
      return new ChannelMember(this);
    }

    /// <summary>Field number for the "channel_id" field.</summary>
    public const int ChannelIdFieldNumber = 1;
    private int channelId_;
    /// <summary>
    ///  频道id
    /// </summary>
    public int ChannelId {
      get { return channelId_; }
      set {
        channelId_ = value;
      }
    }

    /// <summary>Field number for the "users" field.</summary>
    public const int UsersFieldNumber = 2;
    private static readonly pb::FieldCodec<global::IM.User> _repeated_users_codec
        = pb::FieldCodec.ForMessage(18, global::IM.User.Parser);
    private readonly pbc::RepeatedField<global::IM.User> users_ = new pbc::RepeatedField<global::IM.User>();
    /// <summary>
    ///  所属频道的成员
    /// </summary>
    public pbc::RepeatedField<global::IM.User> Users {
      get { return users_; }
    }

    public override bool Equals(object other) {
      return Equals(other as ChannelMember);
    }

    public bool Equals(ChannelMember other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ChannelId != other.ChannelId) return false;
      if(!users_.Equals(other.users_)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (ChannelId != 0) hash ^= ChannelId.GetHashCode();
      hash ^= users_.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (ChannelId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ChannelId);
      }
      users_.WriteTo(output, _repeated_users_codec);
    }

    public int CalculateSize() {
      int size = 0;
      if (ChannelId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ChannelId);
      }
      size += users_.CalculateSize(_repeated_users_codec);
      return size;
    }

    public void MergeFrom(ChannelMember other) {
      if (other == null) {
        return;
      }
      if (other.ChannelId != 0) {
        ChannelId = other.ChannelId;
      }
      users_.Add(other.users_);
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            ChannelId = input.ReadInt32();
            break;
          }
          case 18: {
            users_.AddEntriesFrom(input, _repeated_users_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
