using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Dto;
using Microsoft.Extensions.Configuration;
using Zaabee.FastDfsClient;
using Zaabee.FastDfsClient.Common;
using Dto;

namespace ApiClient
{
    public class FastDFSApiClient
    {
        private static StorageNode storageNode;
        private FastDfsClient client;
        private readonly IConfiguration configuration;

        public FastDFSApiClient(IConfiguration configuration)
        {
            this.configuration = configuration;

            var ip = configuration[$"FastDFS:IP"].Replace("{WebServerIP}", Modobay.AppManager.WebServerIP);
            var port = int.Parse(configuration[$"FastDFS:Port"]);
            var group = configuration[$"FastDFS:Group"];

            client = new FastDfsClient(new List<IPEndPoint>
            {
                new IPEndPoint(IPAddress.Parse(ip), port)
            });

            try
            {
                storageNode = FastDfsClient.GetStorageNode("group1");
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"FastDFSApiClient:Message:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
        }

        public string UploadFile(byte[] data, string fileExt)
        {
            if (client == null) Lib.Log.WriteOperationLog("client null");
            if (storageNode == null) Lib.Log.WriteOperationLog("storageNode null");
            if (data == null) Lib.Log.WriteOperationLog("data null");
            if (fileExt == null) Lib.Log.WriteOperationLog("fileExt null");
            var fileKey = client.UploadFile(storageNode, data, fileExt);
            return fileKey;
        }

        //private AttachmentDto UploadWebFile(Stream data)
        //{
        //    var m = new MultipartParser(data);
        //    var fileKey = client.UploadFile(storageNode, m.FileContents, m.ContentType);
        //    return new AttachmentDto() { FileKey = fileKey, FileSize = m.FileContents.Length, ContentType = m.ContentType };
        //}

        public byte[] DownloadFile(string fileKey)
        {
            return client.DownloadFile(storageNode, fileKey);
        }

        public List<AttachmentDto> DownloadFile(List<AttachmentDto> attachmentDtos)
        {
            foreach (var att in attachmentDtos)
            {
                var data = client.DownloadFile(storageNode, att.FileKey);
                att.Data = new MemoryStream(data);
            }
            return attachmentDtos;
        }

        public void RemoveFile(string fileKey)
        {
            client.RemoveFile(storageNode.GroupName, fileKey);
        }

        public FdfsFileInfo GetFileInfo(string fileKey)
        {
            return client.GetFileInfo(storageNode, fileKey);
        }

        #region 示例代码
        //string fileKey = AttachmentAPI.UploadFile(File.ReadAllBytes(@"c:\1.jpg"), "jpg");
        //string fileKey = AttachmentAPI.UploadWebFile(stream); // web前端提交文件的上传方式，返回AttachmentInfo
        //var data = AttachmentAPI.DownloadFile(fileKey);
        //AttachmentAPI.RemoveFile(fileKey);
        #endregion
    }
}
