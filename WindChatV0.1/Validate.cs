// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: validate.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace IM {

  /// <summary>Holder for reflection information generated from validate.proto</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class ValidateReflection {

    #region Descriptor
    /// <summary>File descriptor for validate.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ValidateReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg52YWxpZGF0ZS5wcm90bxICSU0iOgoOVmFsaWRhdGVSZXN1bHQSDgoGcmVz",
            "dWx0GAEgASgFEgoKAmlwGAIgASgJEgwKBHBvcnQYAyABKAliBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
            new pbr::GeneratedCodeInfo(typeof(global::IM.ValidateResult), global::IM.ValidateResult.Parser, new[]{ "Result", "Ip", "Port" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class ValidateResult : pb::IMessage<ValidateResult> {
    private static readonly pb::MessageParser<ValidateResult> _parser = new pb::MessageParser<ValidateResult>(() => new ValidateResult());
    public static pb::MessageParser<ValidateResult> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::IM.ValidateReflection.Descriptor.MessageTypes[0]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public ValidateResult() {
      OnConstruction();
    }

    partial void OnConstruction();

    public ValidateResult(ValidateResult other) : this() {
      result_ = other.result_;
      ip_ = other.ip_;
      port_ = other.port_;
    }

    public ValidateResult Clone() {
      return new ValidateResult(this);
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 1;
    private int result_;
    /// <summary>
    ///  0-成功 1-失败 2-没有可用的服务器
    /// </summary>
    public int Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "ip" field.</summary>
    public const int IpFieldNumber = 2;
    private string ip_ = "";
    public string Ip {
      get { return ip_; }
      set {
        ip_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "port" field.</summary>
    public const int PortFieldNumber = 3;
    private string port_ = "";
    public string Port {
      get { return port_; }
      set {
        port_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public override bool Equals(object other) {
      return Equals(other as ValidateResult);
    }

    public bool Equals(ValidateResult other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Result != other.Result) return false;
      if (Ip != other.Ip) return false;
      if (Port != other.Port) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Result != 0) hash ^= Result.GetHashCode();
      if (Ip.Length != 0) hash ^= Ip.GetHashCode();
      if (Port.Length != 0) hash ^= Port.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Result != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Result);
      }
      if (Ip.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Ip);
      }
      if (Port.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Port);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Result);
      }
      if (Ip.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Ip);
      }
      if (Port.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Port);
      }
      return size;
    }

    public void MergeFrom(ValidateResult other) {
      if (other == null) {
        return;
      }
      if (other.Result != 0) {
        Result = other.Result;
      }
      if (other.Ip.Length != 0) {
        Ip = other.Ip;
      }
      if (other.Port.Length != 0) {
        Port = other.Port;
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
            Result = input.ReadInt32();
            break;
          }
          case 18: {
            Ip = input.ReadString();
            break;
          }
          case 26: {
            Port = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
