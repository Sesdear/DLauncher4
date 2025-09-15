using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace DristLauncher_4
{
    public class MinecraftPing
    {
        public string Motd { get; private set; }
        public int OnlinePlayers { get; private set; }
        public int MaxPlayers { get; private set; }
        public string Version { get; private set; }

        public static MinecraftPing GetStatus(string host, int port = 25565)
        {
            using (var client = new TcpClient())
            {
                client.ReceiveTimeout = 3000;
                client.SendTimeout = 3000;
                client.Connect(host, port);

                using (var stream = client.GetStream())
                using (var writer = new BinaryWriter(stream))
                using (var reader = new BinaryReader(stream))
                {
                    // ===== Handshake =====
                    var hostBytes = Encoding.UTF8.GetBytes(host);
                    using (var ms = new MemoryStream())
                    {
                        WriteVarInt(ms, 0x00); // Handshake packet ID
                        WriteVarInt(ms, 47);   // Protocol version (47 = 1.8, сервер всё равно ответит статусом)
                        WriteVarInt(ms, hostBytes.Length);
                        ms.Write(hostBytes, 0, hostBytes.Length);
                        ms.WriteByte((byte)(port >> 8));
                        ms.WriteByte((byte)(port & 0xFF));
                        WriteVarInt(ms, 1);    // Next state: status

                        WriteVarInt(writer, (int)ms.Length);
                        writer.Write(ms.ToArray());
                    }

                    // ===== Status request =====
                    writer.Write((byte)0x01); // Packet length
                    writer.Write((byte)0x00); // Status request packet ID
                    writer.Flush();

                    // ===== Response =====
                    int packetLength = ReadVarInt(reader);
                    int packetId = ReadVarInt(reader);
                    if (packetId != 0x00) throw new Exception("Invalid packet ID");

                    int jsonLength = ReadVarInt(reader);
                    var jsonBytes = reader.ReadBytes(jsonLength);
                    var json = Encoding.UTF8.GetString(jsonBytes);

                    // ===== Parse JSON =====
                    using (var doc = JsonDocument.Parse(json))
                    {
                        var root = doc.RootElement;
                        var description = root.GetProperty("description");
                        string motd = description.ValueKind == JsonValueKind.Object
                            ? description.GetProperty("text").GetString()
                            : description.GetString();

                        int online = root.GetProperty("players").GetProperty("online").GetInt32();
                        int max = root.GetProperty("players").GetProperty("max").GetInt32();
                        string version = root.GetProperty("version").GetProperty("name").GetString();

                        return new MinecraftPing
                        {
                            Motd = motd,
                            OnlinePlayers = online,
                            MaxPlayers = max,
                            Version = version
                        };
                    }
                }
            }
        }

        // ===== Helpers =====
        private static void WriteVarInt(Stream stream, int value)
        {
            while ((value & -128) != 0)
            {
                stream.WriteByte((byte)(value & 127 | 128));
                value >>= 7;
            }
            stream.WriteByte((byte)value);
        }

        private static void WriteVarInt(BinaryWriter writer, int value)
        {
            while ((value & -128) != 0)
            {
                writer.Write((byte)(value & 127 | 128));
                value >>= 7;
            }
            writer.Write((byte)value);
        }

        private static int ReadVarInt(BinaryReader reader)
        {
            int numRead = 0;
            int result = 0;
            byte read;
            do
            {
                read = reader.ReadByte();
                int value = (read & 0b01111111);
                result |= (value << (7 * numRead));

                numRead++;
                if (numRead > 5) throw new Exception("VarInt too big");
            } while ((read & 0b10000000) != 0);

            return result;
        }
    }
}
