﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CryEngine
{
    class Chunk_Mesh_801
    {
        Chunk header;
        uint size = 260;
        uint reservedSize = 120;

        uint flags1;
        uint flags2;
        uint nVerts;
        uint nIndices;
        uint nSubSets;
        public uint nSubsetsChunkId;
        uint nVertAnimID;
        public uint[] nStreamChunkID = new uint[16];//16x8 | 1 content 7 empty
        uint[] nPhysicsDataChunk = new uint[4];
        float[] bboxMin = new float[3];
        float[] bboxMax = new float[3];
        float texMappingDensity;
        uint geometricMeanFaceArea;
        byte[] reserved;

        public byte[] serialized;

        public Chunk_Mesh_801(byte[] content)
        {
            uint delta = (uint)content.Count()-size;
            size = size + delta;
            reservedSize = reservedSize + delta;
            reserved = new byte[reservedSize];
            //Stream stream = new MemoryStream(content);
            using (MemoryStream stream = new MemoryStream(content))
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    flags1 = br.ReadUInt32();
                    flags2 = br.ReadUInt32();
                    nVerts = br.ReadUInt32();
                    nIndices = br.ReadUInt32();
                    nSubSets = br.ReadUInt32();
                    nSubsetsChunkId = br.ReadUInt32();
                    nVertAnimID = br.ReadUInt32();
                    for (int i = 0; i < 16; i++)
                        nStreamChunkID[i] = br.ReadUInt32();
                    for (int i = 0; i < 4; i++)
                        nPhysicsDataChunk[i] = br.ReadUInt32();
                    for (int i = 0; i < 3; i++)
                        bboxMin[i] = br.ReadSingle();
                    for (int i = 0; i < 3; i++)
                        bboxMax[i] = br.ReadSingle();
                    texMappingDensity = br.ReadSingle();
                    geometricMeanFaceArea = br.ReadUInt32();
                    for (int i = 0; i < reservedSize; i++)
                        reserved[i] = br.ReadByte();
                }
            }

        }
        public void Serialize()
        {
            serialized = new byte[size];
            using (MemoryStream stream = new MemoryStream(serialized))
            {
                using (BinaryWriter bw = new BinaryWriter(stream))
                {
                    bw.Write(flags1);
                    bw.Write(flags2);
                    bw.Write(nVerts);
                    bw.Write(nIndices);
                    bw.Write(nSubSets);
                    bw.Write(nSubsetsChunkId);
                    bw.Write(nVertAnimID);
                    for (int i = 0; i < 16; i++)
                        bw.Write(nStreamChunkID[i]);
                    for (int i = 0; i < 4; i++)
                        bw.Write(nPhysicsDataChunk[i]);
                    for (int i = 0; i < 3; i++)
                        bw.Write(bboxMin[i]);
                    for (int i = 0; i < 3; i++)
                        bw.Write(bboxMax[i]);
                    bw.Write(texMappingDensity);
                    bw.Write(geometricMeanFaceArea);
                    for (int i = 0; i < reservedSize; i++)
                        bw.Write(reserved[i]);
                }
            }
        }
        public int Get_p3s_c4b_t2s_ChunkID()
        {
            int p3s_c4b_t2s_chunkID=0;
            p3s_c4b_t2s_chunkID = (int)nStreamChunkID[15];
            return p3s_c4b_t2s_chunkID;
        }
        public int GetTangentsChunkID()
        {
            int tangetsChunkID = 0;
            tangetsChunkID = (int)nStreamChunkID[6];
            return tangetsChunkID;
        }
        public Vector3 GetBboxMin()
        {
            Vector3 vecBboxMin = new Vector3(bboxMin[0], bboxMin[1], bboxMin[2]);
            return vecBboxMin;
        }
        public Vector3 GetBboxMax()
        {
            Vector3 vecBboxMax = new Vector3(bboxMax[0], bboxMax[1], bboxMax[2]);
            return vecBboxMax;
        }
    }
}
