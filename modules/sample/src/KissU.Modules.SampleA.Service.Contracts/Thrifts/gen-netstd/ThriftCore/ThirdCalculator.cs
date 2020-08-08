/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thrift;
using Thrift.Processor;
using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;

namespace KissU.Modules.SampleA.Service.Contracts.Thrifts.ThriftCore
{
  public partial class ThirdCalculator
  {
    public interface IAsync
    {
      Task<int> @AddAsync(int num1, int num2, CancellationToken cancellationToken = default(CancellationToken));

      Task<string> SayHelloAsync(CancellationToken cancellationToken = default(CancellationToken));

    }


    public class Client : TBaseClient, IDisposable, IAsync
    {
      public Client(TProtocol protocol) : this(protocol, protocol)
      {
      }

      public Client(TProtocol inputProtocol, TProtocol outputProtocol) : base(inputProtocol, outputProtocol)      {
      }
      public async Task<int> @AddAsync(int num1, int num2, CancellationToken cancellationToken = default(CancellationToken))
      {
        await OutputProtocol.WriteMessageBeginAsync(new TMessage("Add", TMessageType.Call, SeqId), cancellationToken);
        
        var args = new AddArgs();
        args.Num1 = num1;
        args.Num2 = num2;
        
        await args.WriteAsync(OutputProtocol, cancellationToken);
        await OutputProtocol.WriteMessageEndAsync(cancellationToken);
        await OutputProtocol.Transport.FlushAsync(cancellationToken);
        
        var msg = await InputProtocol.ReadMessageBeginAsync(cancellationToken);
        if (msg.Type == TMessageType.Exception)
        {
          var x = await TApplicationException.ReadAsync(InputProtocol, cancellationToken);
          await InputProtocol.ReadMessageEndAsync(cancellationToken);
          throw x;
        }

        var result = new AddResult();
        await result.ReadAsync(InputProtocol, cancellationToken);
        await InputProtocol.ReadMessageEndAsync(cancellationToken);
        if (result.__isset.success)
        {
          return result.Success;
        }
        throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "Add failed: unknown result");
      }

      public async Task<string> SayHelloAsync(CancellationToken cancellationToken = default(CancellationToken))
      {
        await OutputProtocol.WriteMessageBeginAsync(new TMessage("SayHello", TMessageType.Call, SeqId), cancellationToken);
        
        var args = new SayHelloArgs();
        
        await args.WriteAsync(OutputProtocol, cancellationToken);
        await OutputProtocol.WriteMessageEndAsync(cancellationToken);
        await OutputProtocol.Transport.FlushAsync(cancellationToken);
        
        var msg = await InputProtocol.ReadMessageBeginAsync(cancellationToken);
        if (msg.Type == TMessageType.Exception)
        {
          var x = await TApplicationException.ReadAsync(InputProtocol, cancellationToken);
          await InputProtocol.ReadMessageEndAsync(cancellationToken);
          throw x;
        }

        var result = new SayHelloResult();
        await result.ReadAsync(InputProtocol, cancellationToken);
        await InputProtocol.ReadMessageEndAsync(cancellationToken);
        if (result.__isset.success)
        {
          return result.Success;
        }
        throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "SayHello failed: unknown result");
      }

    }

    public class AsyncProcessor : ITAsyncProcessor
    {
      private IAsync _iAsync;

      public AsyncProcessor(IAsync iAsync)
      {
        if (iAsync == null) throw new ArgumentNullException(nameof(iAsync));

        _iAsync = iAsync;
        processMap_["Add"] = Add_ProcessAsync;
        processMap_["SayHello"] = SayHello_ProcessAsync;
      }

      protected delegate Task ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken);
      protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

      public async Task<bool> ProcessAsync(TProtocol iprot, TProtocol oprot)
      {
        return await ProcessAsync(iprot, oprot, CancellationToken.None);
      }

      public async Task<bool> ProcessAsync(TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
      {
        try
        {
          var msg = await iprot.ReadMessageBeginAsync(cancellationToken);

          ProcessFunction fn;
          processMap_.TryGetValue(msg.Name, out fn);

          if (fn == null)
          {
            await TProtocolUtil.SkipAsync(iprot, TType.Struct, cancellationToken);
            await iprot.ReadMessageEndAsync(cancellationToken);
            var x = new TApplicationException (TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
            await oprot.WriteMessageBeginAsync(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID), cancellationToken);
            await x.WriteAsync(oprot, cancellationToken);
            await oprot.WriteMessageEndAsync(cancellationToken);
            await oprot.Transport.FlushAsync(cancellationToken);
            return true;
          }

          await fn(msg.SeqID, iprot, oprot, cancellationToken);

        }
        catch (IOException)
        {
          return false;
        }

        return true;
      }

      public async Task Add_ProcessAsync(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
      {
        var args = new AddArgs();
        await args.ReadAsync(iprot, cancellationToken);
        await iprot.ReadMessageEndAsync(cancellationToken);
        var result = new AddResult();
        try
        {
          result.Success = await _iAsync.@AddAsync(args.Num1, args.Num2, cancellationToken);
          await oprot.WriteMessageBeginAsync(new TMessage("Add", TMessageType.Reply, seqid), cancellationToken); 
          await result.WriteAsync(oprot, cancellationToken);
        }
        catch (TTransportException)
        {
          throw;
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine("Error occurred in processor:");
          Console.Error.WriteLine(ex.ToString());
          var x = new TApplicationException(TApplicationException.ExceptionType.InternalError," Internal error.");
          await oprot.WriteMessageBeginAsync(new TMessage("Add", TMessageType.Exception, seqid), cancellationToken);
          await x.WriteAsync(oprot, cancellationToken);
        }
        await oprot.WriteMessageEndAsync(cancellationToken);
        await oprot.Transport.FlushAsync(cancellationToken);
      }

      public async Task SayHello_ProcessAsync(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
      {
        var args = new SayHelloArgs();
        await args.ReadAsync(iprot, cancellationToken);
        await iprot.ReadMessageEndAsync(cancellationToken);
        var result = new SayHelloResult();
        try
        {
          result.Success = await _iAsync.SayHelloAsync(cancellationToken);
          await oprot.WriteMessageBeginAsync(new TMessage("SayHello", TMessageType.Reply, seqid), cancellationToken); 
          await result.WriteAsync(oprot, cancellationToken);
        }
        catch (TTransportException)
        {
          throw;
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine("Error occurred in processor:");
          Console.Error.WriteLine(ex.ToString());
          var x = new TApplicationException(TApplicationException.ExceptionType.InternalError," Internal error.");
          await oprot.WriteMessageBeginAsync(new TMessage("SayHello", TMessageType.Exception, seqid), cancellationToken);
          await x.WriteAsync(oprot, cancellationToken);
        }
        await oprot.WriteMessageEndAsync(cancellationToken);
        await oprot.Transport.FlushAsync(cancellationToken);
      }

    }


    public partial class AddArgs : TBase
    {
      private int _num1;
      private int _num2;

      public int Num1
      {
        get
        {
          return _num1;
        }
        set
        {
          __isset.num1 = true;
          this._num1 = value;
        }
      }

      public int Num2
      {
        get
        {
          return _num2;
        }
        set
        {
          __isset.num2 = true;
          this._num2 = value;
        }
      }


      public Isset __isset;
      public struct Isset
      {
        public bool num1;
        public bool num2;
      }

      public AddArgs()
      {
      }

      public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          TField field;
          await iprot.ReadStructBeginAsync(cancellationToken);
          while (true)
          {
            field = await iprot.ReadFieldBeginAsync(cancellationToken);
            if (field.Type == TType.Stop)
            {
              break;
            }

            switch (field.ID)
            {
              case 1:
                if (field.Type == TType.I32)
                {
                  Num1 = await iprot.ReadI32Async(cancellationToken);
                }
                else
                {
                  await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                }
                break;
              case 2:
                if (field.Type == TType.I32)
                {
                  Num2 = await iprot.ReadI32Async(cancellationToken);
                }
                else
                {
                  await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                }
                break;
              default: 
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                break;
            }

            await iprot.ReadFieldEndAsync(cancellationToken);
          }

          await iprot.ReadStructEndAsync(cancellationToken);
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          var struc = new TStruct("Add_args");
          await oprot.WriteStructBeginAsync(struc, cancellationToken);
          var field = new TField();
          if (__isset.num1)
          {
            field.Name = "num1";
            field.Type = TType.I32;
            field.ID = 1;
            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            await oprot.WriteI32Async(Num1, cancellationToken);
            await oprot.WriteFieldEndAsync(cancellationToken);
          }
          if (__isset.num2)
          {
            field.Name = "num2";
            field.Type = TType.I32;
            field.ID = 2;
            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            await oprot.WriteI32Async(Num2, cancellationToken);
            await oprot.WriteFieldEndAsync(cancellationToken);
          }
          await oprot.WriteFieldStopAsync(cancellationToken);
          await oprot.WriteStructEndAsync(cancellationToken);
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override bool Equals(object that)
      {
        var other = that as AddArgs;
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;
        return ((__isset.num1 == other.__isset.num1) && ((!__isset.num1) || (System.Object.Equals(Num1, other.Num1))))
          && ((__isset.num2 == other.__isset.num2) && ((!__isset.num2) || (System.Object.Equals(Num2, other.Num2))));
      }

      public override int GetHashCode() {
        int hashcode = 157;
        unchecked {
          if(__isset.num1)
            hashcode = (hashcode * 397) + Num1.GetHashCode();
          if(__isset.num2)
            hashcode = (hashcode * 397) + Num2.GetHashCode();
        }
        return hashcode;
      }

      public override string ToString()
      {
        var sb = new StringBuilder("Add_args(");
        bool __first = true;
        if (__isset.num1)
        {
          if(!__first) { sb.Append(", "); }
          __first = false;
          sb.Append("Num1: ");
          sb.Append(Num1);
        }
        if (__isset.num2)
        {
          if(!__first) { sb.Append(", "); }
          __first = false;
          sb.Append("Num2: ");
          sb.Append(Num2);
        }
        sb.Append(")");
        return sb.ToString();
      }
    }


    public partial class AddResult : TBase
    {
      private int _success;

      public int Success
      {
        get
        {
          return _success;
        }
        set
        {
          __isset.success = true;
          this._success = value;
        }
      }


      public Isset __isset;
      public struct Isset
      {
        public bool success;
      }

      public AddResult()
      {
      }

      public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          TField field;
          await iprot.ReadStructBeginAsync(cancellationToken);
          while (true)
          {
            field = await iprot.ReadFieldBeginAsync(cancellationToken);
            if (field.Type == TType.Stop)
            {
              break;
            }

            switch (field.ID)
            {
              case 0:
                if (field.Type == TType.I32)
                {
                  Success = await iprot.ReadI32Async(cancellationToken);
                }
                else
                {
                  await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                }
                break;
              default: 
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                break;
            }

            await iprot.ReadFieldEndAsync(cancellationToken);
          }

          await iprot.ReadStructEndAsync(cancellationToken);
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          var struc = new TStruct("Add_result");
          await oprot.WriteStructBeginAsync(struc, cancellationToken);
          var field = new TField();

          if(this.__isset.success)
          {
            field.Name = "Success";
            field.Type = TType.I32;
            field.ID = 0;
            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            await oprot.WriteI32Async(Success, cancellationToken);
            await oprot.WriteFieldEndAsync(cancellationToken);
          }
          await oprot.WriteFieldStopAsync(cancellationToken);
          await oprot.WriteStructEndAsync(cancellationToken);
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override bool Equals(object that)
      {
        var other = that as AddResult;
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;
        return ((__isset.success == other.__isset.success) && ((!__isset.success) || (System.Object.Equals(Success, other.Success))));
      }

      public override int GetHashCode() {
        int hashcode = 157;
        unchecked {
          if(__isset.success)
            hashcode = (hashcode * 397) + Success.GetHashCode();
        }
        return hashcode;
      }

      public override string ToString()
      {
        var sb = new StringBuilder("Add_result(");
        bool __first = true;
        if (__isset.success)
        {
          if(!__first) { sb.Append(", "); }
          __first = false;
          sb.Append("Success: ");
          sb.Append(Success);
        }
        sb.Append(")");
        return sb.ToString();
      }
    }


    public partial class SayHelloArgs : TBase
    {

      public SayHelloArgs()
      {
      }

      public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          TField field;
          await iprot.ReadStructBeginAsync(cancellationToken);
          while (true)
          {
            field = await iprot.ReadFieldBeginAsync(cancellationToken);
            if (field.Type == TType.Stop)
            {
              break;
            }

            switch (field.ID)
            {
              default: 
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                break;
            }

            await iprot.ReadFieldEndAsync(cancellationToken);
          }

          await iprot.ReadStructEndAsync(cancellationToken);
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          var struc = new TStruct("SayHello_args");
          await oprot.WriteStructBeginAsync(struc, cancellationToken);
          await oprot.WriteFieldStopAsync(cancellationToken);
          await oprot.WriteStructEndAsync(cancellationToken);
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override bool Equals(object that)
      {
        var other = that as SayHelloArgs;
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;
        return true;
      }

      public override int GetHashCode() {
        int hashcode = 157;
        unchecked {
        }
        return hashcode;
      }

      public override string ToString()
      {
        var sb = new StringBuilder("SayHello_args(");
        sb.Append(")");
        return sb.ToString();
      }
    }


    public partial class SayHelloResult : TBase
    {
      private string _success;

      public string Success
      {
        get
        {
          return _success;
        }
        set
        {
          __isset.success = true;
          this._success = value;
        }
      }


      public Isset __isset;
      public struct Isset
      {
        public bool success;
      }

      public SayHelloResult()
      {
      }

      public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          TField field;
          await iprot.ReadStructBeginAsync(cancellationToken);
          while (true)
          {
            field = await iprot.ReadFieldBeginAsync(cancellationToken);
            if (field.Type == TType.Stop)
            {
              break;
            }

            switch (field.ID)
            {
              case 0:
                if (field.Type == TType.String)
                {
                  Success = await iprot.ReadStringAsync(cancellationToken);
                }
                else
                {
                  await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                }
                break;
              default: 
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                break;
            }

            await iprot.ReadFieldEndAsync(cancellationToken);
          }

          await iprot.ReadStructEndAsync(cancellationToken);
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          var struc = new TStruct("SayHello_result");
          await oprot.WriteStructBeginAsync(struc, cancellationToken);
          var field = new TField();

          if(this.__isset.success)
          {
            if (Success != null)
            {
              field.Name = "Success";
              field.Type = TType.String;
              field.ID = 0;
              await oprot.WriteFieldBeginAsync(field, cancellationToken);
              await oprot.WriteStringAsync(Success, cancellationToken);
              await oprot.WriteFieldEndAsync(cancellationToken);
            }
          }
          await oprot.WriteFieldStopAsync(cancellationToken);
          await oprot.WriteStructEndAsync(cancellationToken);
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override bool Equals(object that)
      {
        var other = that as SayHelloResult;
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;
        return ((__isset.success == other.__isset.success) && ((!__isset.success) || (System.Object.Equals(Success, other.Success))));
      }

      public override int GetHashCode() {
        int hashcode = 157;
        unchecked {
          if(__isset.success)
            hashcode = (hashcode * 397) + Success.GetHashCode();
        }
        return hashcode;
      }

      public override string ToString()
      {
        var sb = new StringBuilder("SayHello_result(");
        bool __first = true;
        if (Success != null && __isset.success)
        {
          if(!__first) { sb.Append(", "); }
          __first = false;
          sb.Append("Success: ");
          sb.Append(Success);
        }
        sb.Append(")");
        return sb.ToString();
      }
    }

  }
}
