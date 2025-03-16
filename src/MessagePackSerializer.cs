﻿using System;
using System.IO;
using System.Threading.Tasks;
using Whitestone.Cambion.Interfaces;
using Whitestone.Cambion.Types;

using MessagePack_Serializer = MessagePack.MessagePackSerializer;

namespace Whitestone.Cambion.Serializer.MessagePack
{
    public class MessagePackSerializer : ISerializer
    {
        public async Task<byte[]> SerializeAsync(MessageWrapper message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            byte[] messageBytes;
            using (var ms = new MemoryStream())
            {
                await MessagePack_Serializer.Typeless.SerializeAsync(ms, message).ConfigureAwait(false);
                messageBytes = ms.ToArray();
            }

            return messageBytes;
        }

        public async Task<MessageWrapper> DeserializeAsync(byte[] serialized)
        {
            MessageWrapper message;
            using (var ms = new MemoryStream(serialized))
            {
                message = await MessagePack_Serializer.Typeless.DeserializeAsync(ms).ConfigureAwait(false) as MessageWrapper;
            }

            return message;
        }
    }
}
