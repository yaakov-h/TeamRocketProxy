// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: POGOProtos/Networking/Responses/LevelUpRewardsResponse.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace POGOProtos.Networking.Responses {

  /// <summary>Holder for reflection information generated from POGOProtos/Networking/Responses/LevelUpRewardsResponse.proto</summary>
  public static partial class LevelUpRewardsResponseReflection {

    #region Descriptor
    /// <summary>File descriptor for POGOProtos/Networking/Responses/LevelUpRewardsResponse.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static LevelUpRewardsResponseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjxQT0dPUHJvdG9zL05ldHdvcmtpbmcvUmVzcG9uc2VzL0xldmVsVXBSZXdh",
            "cmRzUmVzcG9uc2UucHJvdG8SH1BPR09Qcm90b3MuTmV0d29ya2luZy5SZXNw",
            "b25zZXMaJlBPR09Qcm90b3MvSW52ZW50b3J5L0l0ZW0vSXRlbUlkLnByb3Rv",
            "GilQT0dPUHJvdG9zL0ludmVudG9yeS9JdGVtL0l0ZW1Bd2FyZC5wcm90byKX",
            "AgoWTGV2ZWxVcFJld2FyZHNSZXNwb25zZRJOCgZyZXN1bHQYASABKA4yPi5Q",
            "T0dPUHJvdG9zLk5ldHdvcmtpbmcuUmVzcG9uc2VzLkxldmVsVXBSZXdhcmRz",
            "UmVzcG9uc2UuUmVzdWx0EjsKDWl0ZW1zX2F3YXJkZWQYAiADKAsyJC5QT0dP",
            "UHJvdG9zLkludmVudG9yeS5JdGVtLkl0ZW1Bd2FyZBI5Cg5pdGVtc191bmxv",
            "Y2tlZBgEIAMoDjIhLlBPR09Qcm90b3MuSW52ZW50b3J5Lkl0ZW0uSXRlbUlk",
            "IjUKBlJlc3VsdBIJCgVVTlNFVBAAEgsKB1NVQ0NFU1MQARITCg9BV0FSREVE",
            "X0FMUkVBRFkQAmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::POGOProtos.Inventory.Item.ItemIdReflection.Descriptor, global::POGOProtos.Inventory.Item.ItemAwardReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Networking.Responses.LevelUpRewardsResponse), global::POGOProtos.Networking.Responses.LevelUpRewardsResponse.Parser, new[]{ "Result", "ItemsAwarded", "ItemsUnlocked" }, null, new[]{ typeof(global::POGOProtos.Networking.Responses.LevelUpRewardsResponse.Types.Result) }, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class LevelUpRewardsResponse : pb::IMessage<LevelUpRewardsResponse> {
    private static readonly pb::MessageParser<LevelUpRewardsResponse> _parser = new pb::MessageParser<LevelUpRewardsResponse>(() => new LevelUpRewardsResponse());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<LevelUpRewardsResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::POGOProtos.Networking.Responses.LevelUpRewardsResponseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LevelUpRewardsResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LevelUpRewardsResponse(LevelUpRewardsResponse other) : this() {
      result_ = other.result_;
      itemsAwarded_ = other.itemsAwarded_.Clone();
      itemsUnlocked_ = other.itemsUnlocked_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LevelUpRewardsResponse Clone() {
      return new LevelUpRewardsResponse(this);
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 1;
    private global::POGOProtos.Networking.Responses.LevelUpRewardsResponse.Types.Result result_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::POGOProtos.Networking.Responses.LevelUpRewardsResponse.Types.Result Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "items_awarded" field.</summary>
    public const int ItemsAwardedFieldNumber = 2;
    private static readonly pb::FieldCodec<global::POGOProtos.Inventory.Item.ItemAward> _repeated_itemsAwarded_codec
        = pb::FieldCodec.ForMessage(18, global::POGOProtos.Inventory.Item.ItemAward.Parser);
    private readonly pbc::RepeatedField<global::POGOProtos.Inventory.Item.ItemAward> itemsAwarded_ = new pbc::RepeatedField<global::POGOProtos.Inventory.Item.ItemAward>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::POGOProtos.Inventory.Item.ItemAward> ItemsAwarded {
      get { return itemsAwarded_; }
    }

    /// <summary>Field number for the "items_unlocked" field.</summary>
    public const int ItemsUnlockedFieldNumber = 4;
    private static readonly pb::FieldCodec<global::POGOProtos.Inventory.Item.ItemId> _repeated_itemsUnlocked_codec
        = pb::FieldCodec.ForEnum(34, x => (int) x, x => (global::POGOProtos.Inventory.Item.ItemId) x);
    private readonly pbc::RepeatedField<global::POGOProtos.Inventory.Item.ItemId> itemsUnlocked_ = new pbc::RepeatedField<global::POGOProtos.Inventory.Item.ItemId>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::POGOProtos.Inventory.Item.ItemId> ItemsUnlocked {
      get { return itemsUnlocked_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as LevelUpRewardsResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(LevelUpRewardsResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Result != other.Result) return false;
      if(!itemsAwarded_.Equals(other.itemsAwarded_)) return false;
      if(!itemsUnlocked_.Equals(other.itemsUnlocked_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Result != 0) hash ^= Result.GetHashCode();
      hash ^= itemsAwarded_.GetHashCode();
      hash ^= itemsUnlocked_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Result != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Result);
      }
      itemsAwarded_.WriteTo(output, _repeated_itemsAwarded_codec);
      itemsUnlocked_.WriteTo(output, _repeated_itemsUnlocked_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Result);
      }
      size += itemsAwarded_.CalculateSize(_repeated_itemsAwarded_codec);
      size += itemsUnlocked_.CalculateSize(_repeated_itemsUnlocked_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(LevelUpRewardsResponse other) {
      if (other == null) {
        return;
      }
      if (other.Result != 0) {
        Result = other.Result;
      }
      itemsAwarded_.Add(other.itemsAwarded_);
      itemsUnlocked_.Add(other.itemsUnlocked_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            result_ = (global::POGOProtos.Networking.Responses.LevelUpRewardsResponse.Types.Result) input.ReadEnum();
            break;
          }
          case 18: {
            itemsAwarded_.AddEntriesFrom(input, _repeated_itemsAwarded_codec);
            break;
          }
          case 34:
          case 32: {
            itemsUnlocked_.AddEntriesFrom(input, _repeated_itemsUnlocked_codec);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the LevelUpRewardsResponse message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum Result {
        [pbr::OriginalName("UNSET")] Unset = 0,
        [pbr::OriginalName("SUCCESS")] Success = 1,
        [pbr::OriginalName("AWARDED_ALREADY")] AwardedAlready = 2,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
